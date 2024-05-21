﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using RoItemKakakuChecker.Properties;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Speech.Synthesis;


namespace RoItemKakakuChecker.forms
{
    public partial class ChatObserveModeControl : UserControl
    {
        private MainForm mainForm;
        private const string RO_CHAT_SERVER_IP = "18.182.57.";
        private bool isObserving = false;
        private DateTime lastYomiageTime = DateTime.MinValue;

        public void SetMainForm(MainForm mainForm)
        {
            this.mainForm = mainForm;


            bool isSucceeded = mainForm.settings.ReadSettings();
            if (isSucceeded)
            {
                checkBoxPublic.Checked = mainForm.settings.SpeechPublic;
                checkBoxParty.Checked = mainForm.settings.SpeechParty;
                checkBoxGuild.Checked = mainForm.settings.SpeechGuild;
                checkBoxWhisper.Checked = mainForm.settings.SpeechWhisper;
                checkBoxWord.Checked = mainForm.settings.SpeechWord;
                textBoxWord.Text = mainForm.settings.SpeechKeyWord;
                checkBoxMdYomiage.Checked = mainForm.settings.EnableMdYomiage;
                numericUpDownMdYomiage.Value = mainForm.settings.MdYomiageMax;
            }
        }

        public ChatObserveModeControl()
        {
            InitializeComponent();



            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ReadOnly = true;

            this.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0xdd, 0xf1, 0xf4);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 230);
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;


            var toolTipWord = new ToolTip();
            toolTipWord.AutoPopDelay = 5000;
            toolTipWord.InitialDelay = 100;
            toolTipWord.ReshowDelay = 100;
            var text = "指定したワードに応じて読み上げを行います。\n通常・PT・ギルド・WISの全てに対して反応します。\n複数のワードを指定する場合はセミコロンで区切ってください。\n（例: おはよう;こんにちは;こんばんは）";
            toolTipWord.SetToolTip(this.checkBoxWord, text);
            toolTipWord.SetToolTip(this.textBoxWord, text);

            checkBoxPublic.CheckedChanged += (sender, e) => mainForm.settings.SpeechPublic = checkBoxPublic.Checked;
            checkBoxParty.CheckedChanged += (sender, e) => mainForm.settings.SpeechParty = checkBoxParty.Checked;
            checkBoxGuild.CheckedChanged += (sender, e) => mainForm.settings.SpeechGuild = checkBoxGuild.Checked;
            checkBoxWhisper.CheckedChanged += (sender, e) => mainForm.settings.SpeechWhisper = checkBoxWhisper.Checked;
            checkBoxWord.CheckedChanged += (sender, e) => mainForm.settings.SpeechWord = checkBoxWord.Checked;
            textBoxWord.LostFocus += (sender, e) => mainForm.settings.SpeechKeyWord = textBoxWord.Text;

            checkBoxMdYomiage.CheckedChanged += (sender, e) => mainForm.settings.EnableMdYomiage = checkBoxMdYomiage.Checked;
            numericUpDownMdYomiage.ValueChanged += (sender, e) => mainForm.settings.MdYomiageMax = (int)numericUpDownMdYomiage.Value;
        }



        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView.ClearSelection();
        }

        private async void btnObserveChat_Click(object sender, EventArgs e)
        {
            // 停止ボタン
            if (isObserving)
            {
                isObserving = false;
                btnObserveChat.Text = "チャット監視 開始";
            }
            // 開始ボタン
            else
            {
                isObserving = true;
                btnObserveChat.Text = "チャット監視 停止";
                await ObserveChatMessage();
            }
        }

        private async Task ObserveChatMessage()
        {

            var he = Dns.GetHostEntry(Dns.GetHostName());
            var addr = he.AddressList.Where((h) => h.AddressFamily == AddressFamily.InterNetwork).ToList();
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            socket.Bind(new IPEndPoint(addr[0], 0));
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AcceptConnection, 1);
            byte[] ib = new byte[] { 1, 0, 0, 0 };
            byte[] ob = new byte[] { 0, 0, 0, 0 };
            socket.IOControl(IOControlCode.ReceiveAll, ib, ob); //SIO_RCVALL
            byte[] buf = new byte[1024 * 64];

            mainForm.UpdateToolStripLabel("会話メッセージの監視を開始します。");

            await Task.Run(() =>
            {
                byte[] joinedBody = new byte[0];// = new byte[28682*2];
                //int nextIndex = 0;
                bool appendMode = false;
                while (true)
                {

                    try
                    {


                        IAsyncResult iares = socket.BeginReceive(buf, 0, buf.Length, SocketFlags.None, null, null);
                        var len = socket.EndReceive(iares);

                        var srcIp = Ip(buf, 12);
                        var protocol = Proto(buf[9]);
                        var dstIp = Ip(buf, 16);
                        var ttl = buf[8];
                        var flags = buf[33];
                        var flagPsh = 8;
                        var hasPshFlag = (flags & flagPsh) != 0;

                        if (protocol == "TCP" && srcIp.StartsWith(RO_CHAT_SERVER_IP))
                        {

                            if (!appendMode)
                            {
                                //joinedBody = new byte[28682];
                                //nextIndex = 0;
                            }

                            var bodySize = len - 40; // IPヘッダ 20bytes, TCPヘッダ 20bytes



                            byte[] body = new byte[bodySize];
                            Array.Copy(buf, 40, body, 0, bodySize); // bodyにヘッダを除いた本体をコピー

                            // bodyに今まで貯めた分も含めてコピー
                            // 終端パケットかどうかはPSHを見て判断する
                            joinedBody = joinedBody.Concat(body).ToArray();
                            //Array.Copy(body, 0, joinedBody, nextIndex, body.Length);

                            appendMode = !hasPshFlag;
                            //if (appendMode)
                            //{
                            //    nextIndex += body.Length;
                            //}

                            // 終端パケットまで読んだら解析
                            if (hasPshFlag)
                            {
                                var chatLine = Analyze(joinedBody);
                                joinedBody = new byte[0];
                                if (chatLine != null && chatLine.Message.Contains(" : "))
                                {
                                    AppendToGridView(chatLine);
                                    AppendToLogFile(chatLine);

                                    if (mainForm.speaker.Enabled)
                                    {
                                        Speak(chatLine);
                                    }
                                }
                            }
                        }

                        if (!isObserving)
                        {
                            mainForm.UpdateToolStripLabel("会話メッセージの監視を停止しました。");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        mainForm.LogError(ex.Message + "\n" + ex.StackTrace + "\n" + BitConverter.ToString(joinedBody));
                    }
                }
            });
        }

        private string nextPacketChatType = null;
        private int nextPacketTextLength = -1;
        private ChatLogEntity Analyze(byte[] data)
        {
            bool hasExtraHeader = false;

            // ギルド・PTチャットは2パケットに分かれる
            if (data[0] == 0x09 && data[1] == 0x01)
            {
                nextPacketChatType = "party";
                nextPacketTextLength = (int)data[2] - 8;

                // 1パケットに詰め込まれている場合
                // 滅多にないが…
                if (data.Length > 8)
                {
                    if (mainForm.settings.DebugMode == 1)
                    {
                        mainForm.LogError("Party(Header 1/1 packet): " + BitConverter.ToString(data));
                    }
                    hasExtraHeader = true;
                }
                // 2パケットに分かれている場合
                else
                {
                    if (mainForm.settings.DebugMode == 1)
                    {
                        mainForm.LogError("Party(Header): " + BitConverter.ToString(data));
                    }
                    return null;
                }


            }
            else if (data[0] == 0x7f && data[1] == 0x01)
            {
                nextPacketChatType = "guild";
                nextPacketTextLength = (int)data[2] - 4;
                // 1パケットに詰め込まれている場合
                // 滅多にないが…
                if (data.Length > 8)
                {
                    if (mainForm.settings.DebugMode == 1)
                    {
                        mainForm.LogError("Guild(Header 1/1 packet): " + BitConverter.ToString(data));
                    }
                    hasExtraHeader = true;
                }
                // 2パケットに分かれている場合
                else
                {
                    if (mainForm.settings.DebugMode == 1)
                    {
                        mainForm.LogError("Guild(Header 1/2 packet): " + BitConverter.ToString(data));
                    }
                    return null;
                }

            }

            string sjisStr = "";
            var chatLine = new ChatLogEntity();
            if (nextPacketChatType == "party")
            {
                if (hasExtraHeader)
                {
                    // なぜか1パケットに詰め込まれている場合
                    byte[] textdata = data.Skip(8).Take(nextPacketTextLength).ToArray();
                    sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(textdata);
                }
                else
                {
                    byte[] textdata = data.Take(nextPacketTextLength).ToArray();
                    // PTチャット2パケット目はヘッダ無し
                    sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(textdata);
                }


                chatLine.MessageType = "Party";
            }
            else if (nextPacketChatType == "guild")
            {
                if (hasExtraHeader)
                {
                    // なぜか1パケットに詰め込まれている場合
                    byte[] textdata = data.Skip(4).Take(nextPacketTextLength).ToArray();
                    sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(textdata);

                    //sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(data, 4, data.Length - 4);
                }
                else
                {
                    // ギルドチャット2パケット目は通常はヘッダ無し
                    byte[] textdata = data.Take(nextPacketTextLength).ToArray();
                    sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(textdata);
                }
                chatLine.MessageType = "Guild";
            }
            else if (data[0] == 0x8e && data[1] == 0x00)
            {
                // 全体チャットは4バイトのヘッダ有り
                //var textdata = new byte[data.Length - 4];
                //Array.Copy(data, 4, textdata, 0, data.Length - 4);
                //sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(textdata);

                int textLength = (int)data[2] - 4;
                var textdata = data.Skip(4).Take(textLength).ToArray();
                sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(textdata);
                chatLine.MessageType = "Public";
            }
            else if (data[0] == 0x8d && data[1] == 0x00)
            {
                // 全体チャット別パターン
                //var textdata = new byte[data.Length - 8];
                //Array.Copy(data, 8, textdata, 0, data.Length - 8);
                int textLength = (int)data[2] - 8;
                var textdata = data.Skip(8).Take(textLength).ToArray();
                sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(textdata);
                chatLine.MessageType = "Public";
            }
            else if (data[0] == 0xde && data[1] == 0x09)
            {
                // WISは8バイトのヘッダ有り

                int lengthWithHeader = (int)data[2];

                // キャラクター名が25バイト固定
                byte[] charName = data.Skip(8).Take(25).ToArray();
                string sjisCharName = Encoding.GetEncoding("Shift-JIS").GetString(charName).TrimEnd('\0');

                byte[] message = data.Skip(33).Take(lengthWithHeader - 33).ToArray();
                string sjisMessage = Encoding.GetEncoding("Shift-JIS").GetString(message);

                sjisStr = sjisCharName + " : " + sjisMessage;
                chatLine.MessageType = "Whisper";
            }
            else if (data[0] == 0xcc && data[1] == 0x02)
            {
                // MD待機人数が減った時
                byte[] taikiArr = { 0, 0, 0, 0 };
                taikiArr[0] = data[2];
                taikiArr[1] = data[3];
                int taiki = BitConverter.ToInt32(taikiArr, 0);
 

                var now = DateTime.Now;
                if (now > lastYomiageTime.AddSeconds(30))
                {
                    lastYomiageTime = now;

                    if (checkBoxMdYomiage.Checked && taiki <= numericUpDownMdYomiage.Value)
                    {
                        mainForm.speaker2.Message = $"MD待機、あと {taiki} です!";
                        mainForm.speaker2.Speak();
                    }
                }

            }
            else if (data[0] == 0xcd && data[1] == 0x02)
            {
                // MDが開いたとき
                if (checkBoxMdYomiage.Checked)
                {
                    mainForm.speaker2.Message = $"MDがあきました!";
                    mainForm.speaker2.Speak();
                }

            }
            else
            {
                // チャット以外のパケットは無視
                nextPacketChatType = null;
                nextPacketTextLength = -1;
                return null;
            }
            nextPacketChatType = null;
            nextPacketTextLength = -1;

            //chatLine.Message = sjisStr.TrimEnd('\0');
            sjisStr = sjisStr.TrimEnd('\0');
            chatLine.Message = Regex.Replace(sjisStr, @"<ITEML>.*</ITEML>", "＜ITEM＞");

            chatLine.DateTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


            if (mainForm.settings.DebugMode == 1)
            {
                mainForm.LogError($"Message {chatLine.DateTimeStr} {chatLine.MessageType} {chatLine.Message}\n" + BitConverter.ToString(data));
            }


            return chatLine;
        }



        static string Ip(byte[] buf, int i)
        {
            return string.Format("{0}.{1}.{2}.{3}", buf[i], buf[i + 1], buf[i + 2], buf[i + 3]);
        }

        static string Proto(byte b)
        {
            if (b == 6)
                return "TCP";
            else if (b == 17)
                return "UDP";
            return "Other";
        }

        private void AppendToGridView(ChatLogEntity chatLine)
        {
            if (chatLogEntityBindingSource.DataSource.GetType() != typeof(List<ChatLogEntity>))
            {
                dataGridView.Invoke((MethodInvoker)delegate { chatLogEntityBindingSource.DataSource = new List<ChatLogEntity>(); });

            }
            var list = chatLogEntityBindingSource.DataSource as List<ChatLogEntity>;
            dataGridView.Invoke((MethodInvoker)delegate
            {
                
                var rowIdx = dataGridView.FirstDisplayedScrollingRowIndex;
                
                list.Add(chatLine);
                //chatLogEntityBindingSource.DataSource = list;
                chatLogEntityBindingSource.ResetBindings(false);

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    var entity = row.DataBoundItem as ChatLogEntity;

                    if (entity.MessageType == "Party")
                    {
                        row.DefaultCellStyle.ForeColor = Color.IndianRed;
                    }
                    else if (entity.MessageType == "Guild")
                    {
                        row.DefaultCellStyle.ForeColor = Color.Green;
                    }
                    else if (entity.MessageType == "Whisper")
                    {
                        row.DefaultCellStyle.ForeColor = Color.Goldenrod;
                    }
                    else
                    {
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }

                //dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows.Count - 1;
                dataGridView.FirstDisplayedScrollingRowIndex = rowIdx + 1;
            });

        }

        private void AppendToLogFile(ChatLogEntity chatLine)
        {
            var dir = "chatlog";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var fileName = $"chatlog_{date}.txt";
            Directory.CreateDirectory(dir);
            using (var writer = new StreamWriter($@"{dir}\{fileName}", true, Encoding.UTF8))
            {
                string text = $"{chatLine.DateTimeStr}\t{chatLine.MessageType}\t{chatLine.Message}";
                writer.WriteLine(text);
                mainForm.UpdateToolStripLabel($@"受信したメッセージは {dir}\{fileName} に自動保存されます。");
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string text = "";
            text += @"会話メッセージ取得モードは、メッセージの監視中に受信したメッセージ（通常・パーティ・ギルド・Wis）を表示し、同時に外部ファイルに保存するものです。
チャットルームや天の声などには対応していません。またWisは受信メッセージのみ対応しています。（自身が発言したメッセージは記録されません）
おまけ機能のため、継続して利用する場合は類似の別のツールを使うことを推奨します。
この機能は、作者が通常/PT/ギルド チャットの色の見分けが付かず困った時のために作りました。
必要なときのみ監視状態を有効にしてください。";


            MessageBox.Show(text, "ヘルプ");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridView.Invoke((MethodInvoker)delegate
            {
                chatLogEntityBindingSource.DataSource = new List<ChatLogEntity>();
                chatLogEntityBindingSource.ResetBindings(false);
            });
        }

        private void Speak(ChatLogEntity chatLine)
        {
            var separator = new char[] { ';', '；' };
            var words = textBoxWord.Text.Split(separator).ToList();
            var chatBody = chatLine.Message.Split(new string[] { " : " }, StringSplitOptions.None);

            if ((chatLine.MessageType == "Public" && checkBoxPublic.Checked) ||
                (chatLine.MessageType == "Party" && checkBoxParty.Checked) ||
                (chatLine.MessageType == "Guild" && checkBoxGuild.Checked) ||
                (chatLine.MessageType == "Whisper" && checkBoxWhisper.Checked) ||
                (!string.IsNullOrWhiteSpace(textBoxWord.Text) && words.Any(w => chatBody[1].Contains(w) && checkBoxWord.Checked)))
            {

                mainForm.speaker.Message = chatBody[0] + "、" + chatBody[1];
                mainForm.speaker.Speak();
            }
        }

    }
}
