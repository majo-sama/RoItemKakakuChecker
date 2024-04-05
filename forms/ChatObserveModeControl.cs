using System;
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

namespace RoItemKakakuChecker.forms
{
    public partial class ChatObserveModeControl : UserControl
    {
        private MainForm mainForm;
        private const string RO_CHAT_SERVER_IP = "18.182.57.";
        private bool isObserving = false;

        public void SetMainForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
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
            dataGridView.RowsAdded += DataGridView_RowsAdded;
        }


        private void DataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                var entity = dgv.Rows[e.RowIndex].DataBoundItem as ChatLogEntity;
                if (entity.MessageType == "Party")
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.IndianRed;
                }
                else if (entity.MessageType == "Guild")
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Green;
                }
                else if (entity.MessageType == "Whisper")
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Goldenrod;
                }
                else
                {
                    dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
            }
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
            byte[] buf = new byte[28682];
            int i = 0;

            mainForm.UpdateToolStripLabel("チャットメッセージの監視を開始します。");

            await Task.Run(() =>
            {
                byte[] joinedBody = new byte[28682];
                int nextIndex = 0;
                bool appendMode = false;
                while (true)
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
                            joinedBody = new byte[28682];
                            nextIndex = 0;
                        }

                        var bodySize = len - 40; // IPヘッダ 20bytes, TCPヘッダ 20bytes



                        byte[] body = new byte[bodySize];
                        Array.Copy(buf, 40, body, 0, bodySize); // bodyにヘッダを除いた本体をコピー
                        var str = BitConverter.ToString(body);

                        // bodyに今まで貯めた分も含めてコピー
                        // 終端パケットかどうかはPSHを見て判断する
                        Array.Copy(body, 0, joinedBody, nextIndex, body.Length);
                        appendMode = !hasPshFlag;
                        if (appendMode)
                        {
                            nextIndex += body.Length;
                        }

                        // 終端パケットまで読んだら解析
                        if (hasPshFlag)
                        {
                            var chatLine = Analyze(joinedBody);
                            if (chatLine != null)
                            {
                                AppendToGridView(chatLine);
                                AppendToLogFile(chatLine);
                            }
                        }
                    }

                    if (!isObserving)
                    {
                        mainForm.UpdateToolStripLabel("チャットメッセージの監視を停止しました。");
                        break;
                    }
                }
            });
        }

        private string nextPacketChatType = null;
        private ChatLogEntity Analyze(byte[] data)
        {
            // ギルド・PTチャットは2パケットに分かれる
            if (data[0] == 0x09 && data[1] == 0x01)
            {
                nextPacketChatType = "party";
                return null;
            }
            else if (data[0] == 0x7f && data[1] == 0x01)
            {
                nextPacketChatType = "guild";
                return null;
            }

            string sjisStr = "";
            var chatLine = new ChatLogEntity();
            if (nextPacketChatType == "party")
            {
                // PTチャット2パケット目はヘッダ無し
                sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(data);
                chatLine.MessageType = "Party";
            }
            else if (nextPacketChatType == "guild")
            {
                // ギルドチャット2パケット目はヘッダ無し
                sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(data);
                chatLine.MessageType = "Guild";
            }
            else if (data[0] == 0x8e && data[1] == 0x00)
            {
                // 全体チャットは4バイトのヘッダ有り
                var textdata = new byte[data.Length - 4];
                Array.Copy(data, 4, textdata, 0, data.Length - 4);
                sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(textdata);
                chatLine.MessageType = "Public";
            }
            else if (data[0] == 0xde && data[1] == 0x09)
            {
                // WISは6バイトのヘッダ有り
                var textdata = new byte[data.Length - 6];
                Array.Copy(data, 6, textdata, 0, data.Length - 6);
                sjisStr = Encoding.GetEncoding("Shift-JIS").GetString(textdata);
                chatLine.MessageType = "Whisper";
            }
            else
            {
                // チャット以外のパケットは無視
                nextPacketChatType = null;
                return null;
            }
            nextPacketChatType = null;

            chatLine.Message = sjisStr.TrimEnd('\0');
            chatLine.DateTimeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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
                list.Add(chatLine);
                chatLogEntityBindingSource.DataSource = list;
                chatLogEntityBindingSource.ResetBindings(true);
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
            text += @"チャットメッセージ取得モードは、メッセージの監視中に受信したメッセージ（通常・パーティ・ギルド・Wis）を表示し、同時に外部ファイルに保存するものです。
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
                chatLogEntityBindingSource.ResetBindings(true);
            });
        }
    }
}
