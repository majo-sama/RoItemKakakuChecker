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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Xml.Linq;
using System.Reflection;
using System.Threading;

namespace RoItemKakakuChecker
{
    public partial class StorageObserveModeControl : UserControl
    {
        private MainForm mainForm;
        private static bool stopFlag = false;
        public static bool observing = false;
        private const string RO_STORAGE_SERVER_IP = "18.182.57.";
        public MyNetworkInterface selectedNetworkInterface;
        public ComboBox ComboBoxNetworkInterfaces { get => comboBoxNetworkInterfaces; }
        public BindingSource MyNetworkInterfaceBindingSource { get => myNetworkInterfaceBindingSource; }



        public void SetMainForm(Form parent)
        {
            this.mainForm = parent as MainForm;

            bool isSucceeded = mainForm.settings.ReadSettings();

            if (isSucceeded)
            {
                switch (mainForm.settings.ApiLimit)
                {
                    case 0: comboApiLimit.SelectedIndex = 0; break;
                    case 1: comboApiLimit.SelectedIndex = 1; break;
                    case 2: comboApiLimit.SelectedIndex = 2; break;
                    case 3: comboApiLimit.SelectedIndex = 3; break;
                    case 5: comboApiLimit.SelectedIndex = 4; break;
                    case 10: comboApiLimit.SelectedIndex = 5; break;
                    case 20: comboApiLimit.SelectedIndex = 6; break;
                    case 30: comboApiLimit.SelectedIndex = 7; break;
                    default: comboApiLimit.SelectedIndex = 0; break;
                }
            }
            else
            {
                comboApiLimit.SelectedIndex = 5;
            }

            comboApiLimit.SelectedValueChanged += ComboApiLimit_SelectedValueChanged; ;
            btnFetchKakaku.Enabled = false;
            UpdateApiLimitMessageStatusLabel();




            comboBoxNetworkInterfaces.SelectedIndexChanged += ComboBoxNetworkInterfaces_SelectedIndexChanged;

            Size maxSize = new Size(0, 0);
            foreach (var nif in mainForm.networkInterfaces)
            {
                Size size = TextRenderer.MeasureText(nif.Name, comboBoxNetworkInterfaces.Font);
                if (size.Width > maxSize.Width)
                {
                    maxSize = size;
                }
            }
            comboBoxNetworkInterfaces.DropDownWidth = maxSize.Width + 20;
        }

        private void ComboBoxNetworkInterfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            StopButtonClicked();

            selectedNetworkInterface = comboBoxNetworkInterfaces.SelectedValue as MyNetworkInterface;

            mainForm.ChatObserveModeControl.ComboBoxNetworkInterfaces.SelectedIndex = comboBoxNetworkInterfaces.SelectedIndex;
        }




        public StorageObserveModeControl()
        {
            InitializeComponent();

            this.btnObserve.Text = "倉庫監視 開始";

            dataGridView.CellContentClick += DataGridView_CellContentClick;
            dataGridView.SelectionChanged += DataGridView_SelectionChanged;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.ReadOnly = true;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView.Columns[2].HeaderText = "単体価格\n(中央値)";
            dataGridView.Columns[2].HeaderCell.Style.Padding = new Padding(0, 0, 0, 0);

            this.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            dataGridView.CellMouseMove += DataGridView_CellMouseMove;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0xdd, 0xf1, 0xf4);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 230);
            dataGridView.EnableHeadersVisualStyles = false;

        }


        private void ComboApiLimit_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateApiLimitMessageStatusLabel();
            mainForm.settings.SaveSettings(Convert.ToInt32(comboApiLimit.SelectedItem.ToString()), mainForm.chatLogModeControl.txtChatDir.Text);
        }


        private void UpdateApiLimitMessageStatusLabel()
        {
            if (Convert.ToInt32(comboApiLimit.SelectedItem) < 3)
            {
                mainForm.UpdateToolStripLabel("注意: 大量のデータを頻繁に再取得することは避けてください。ガンホーに怒られますよ！");

            }
            else
            {
                mainForm.UpdateToolStripLabel("");
            }
        }




        private void DataGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewCellStyle tcs = new DataGridViewCellStyle();
                tcs.SelectionBackColor = Color.FromArgb(0xdd, 0xf1, 0xf4);
                dataGridView.AlternatingRowsDefaultCellStyle = tcs;

                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                foreach (DataGridViewRow r in dataGridView.Rows)
                {
                    r.Selected = false;
                }

                dataGridView.Rows[e.RowIndex].Selected = true;
            }
        }


        private void StopButtonClicked()
        {
            stopFlag = true;
            observing = false;
            this.btnObserve.Text = "倉庫監視 開始";
            btnFetchKakaku.Enabled = true;
        }

        public Socket socket;
        private async void btnObserve_Click(object sender, EventArgs e)
        {
            // 停止ボタン押下時
            if (observing)
            {
                StopButtonClicked();
                return;
            }



            stopFlag = false;
            observing = true;
            this.btnObserve.Text = "倉庫監視 停止";

            if (selectedNetworkInterface == null)
            {
                selectedNetworkInterface = mainForm.networkInterfaces[0];
            }

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            socket.Bind(new IPEndPoint(selectedNetworkInterface.Address, 0));
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AcceptConnection, 1);
            byte[] ib = new byte[] { 1, 0, 0, 0 };
            byte[] ob = new byte[] { 0, 0, 0, 0 };
            socket.IOControl(IOControlCode.ReceiveAll, ib, ob); //SIO_RCVALL
            byte[] buf = new byte[28682];
            int i = 0;

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


                    if (protocol == "TCP" && srcIp.StartsWith(RO_STORAGE_SERVER_IP))
                    {
                        if (!appendMode)
                        {
                            joinedBody = new byte[28682];
                            nextIndex = 0;
                        }

                        var bodySize = len - 40; // IPヘッダ 20bytes, TCPヘッダ 20bytes



                        byte[] body = new byte[bodySize];
                        Array.Copy(buf, 40, body, 0, bodySize); // bodyにヘッダを除いた本体をコピー

                        // bodyに今まで貯めた分も含めてコピー
                        // 終端パケットかどうかはPSHを見て判断する
                        Array.Copy(body, 0, joinedBody, nextIndex, body.Length);
                        appendMode = !hasPshFlag;
                        //if (appendMode)
                        //{
                            nextIndex = nextIndex + body.Length;
                        //}

                        // 終端パケットまで読んだら解析
                        if (hasPshFlag)
                        {



                            // 末尾の予備用の領域を削除
                            var data = joinedBody.Take(nextIndex + 1).ToArray();

                            if (data.Length >= 39)
                            {
                                Analyze(data);
                            }
                        }



                    }



                    if (stopFlag)
                    {
                        break;
                    }
                }
            });


        }

        private void Analyze(byte[] data)
        {
            // 謎ヘッダ除去
            if (data[0] == 0x46 && data[1] == 0x04)
            {
                Analyze(data.Skip(14).ToArray());
            }
            // アイテム倉庫 ヘッダがある場合の除去
            else if (data[0] == 0x08 && data[1] == 0x0b)
            {

                for (int i = 2; i < data.Length - 2; i++)
                {
                    // 09 0b がくるまでskipする必要がある
                    // ほとんどの場合14バイトskipすればいいが、15バイトが必要な場合もある
                    if (data[i] == 0x09 && data[i + 1] == 0x0b)
                    {
                        Analyze(data.Skip(i).ToArray());
                    }
                }
            }
            // アイテム倉庫
            else if (data[0] == 0x09 && data[1] == 0x0b)
            {
                var items = AnalyzeItemsOrEquipsDataArray(data, 5, 34);
                if (items.Count > 0)
                {
                    AnalyzeItems(items);
                }
            }
            // 装備品倉庫
            else if (data[0] == 0x39 && data[1] == 0x0b)
            {
                var items = AnalyzeItemsOrEquipsDataArray(data, 5, 68);
                if (items.Count > 0)
                {
                    AnalyzeEquips(items);
                }
            }
            else
            {
                return;
            }
        }

        // アイテム・装備品データのみのバイト配列を取得する
        private List<byte[]> AnalyzeItemsOrEquipsDataArray(byte[] data, int headerLength, int itemOrEquipDataLength)
        {

            var body = data.Skip(headerLength).ToArray();

            // 各アイテムのバイト配列の一覧
            var items = new List<byte[]>();

            while (body.Length >= itemOrEquipDataLength)
            {
                // 謎データが来たら終わり
                if (body.Length >= 2 && body[0] == 0x46 && body[1] == 0x04)
                {
                    break;
                }

                var itemData = body.Take(itemOrEquipDataLength);

                // dataは必要以上に大きく確保してあるため、すべて0のとき、無駄なものとして無視する
                if (itemData.All(b => b == 0))
                {
                    break;
                }
                items.Add(itemData.ToArray());
                body = body.Skip(itemOrEquipDataLength).ToArray(); ;
            }
            return items;
        }


        private void AnalyzeEquips(List<byte[]> equips)
        {
            var storageItems = new List<Item>();
            foreach (var item in equips)
            {

                try
                {
                    var storageItem = new Item();

                    byte[] itemId = new byte[4];
                    Array.Copy(item, 2, itemId, 0, 3); // 恐らくItemIDは3バイト
                    int intItemId = BitConverter.ToInt32(itemId, 0);
                    storageItem.ItemId = intItemId;
                    storageItem.Count = 1;
                    mainForm.itemIdNameMap.Map.TryGetValue(intItemId, out string name);
                    if (name == null)
                    {
                        continue;
                    }

                    int i_slot1 = 0, i_slot2 = 0, i_slot3 = 0, i_slot4 = 0;
                    int i_op1Key = 0, i_op2Key = 0, i_op3Key = 0, i_op4Key = 0, i_op5Key = 0;
                    int i_op1Value = 0, i_op2Value = 0, i_op3Value = 0, i_op4Value = 0, i_op5Value = 0;

                    int i_createGrade = 0;
                    // ユーザー製の武器
                    if (item[15] == 0xff || item[15] == 0xfe)
                    {
                        switch (item[20])
                        {
                            case 0x05: i_createGrade = 1; break;
                            case 0x0a: i_createGrade = 2; break;
                            case 0x0f: i_createGrade = 3; break;
                            default: i_createGrade = 0; break;
                        }
                        storageItem.CreateGrade = i_createGrade;
                    }
                    // 通常の装備
                    else
                    {

                        byte[] b_slot1 = new byte[4];
                        Array.Copy(item, 15, b_slot1, 0, 3);
                        i_slot1 = BitConverter.ToInt32(b_slot1, 0);
                        storageItem.Slot1ItemId = i_slot1;

                        byte[] b_slot2 = new byte[4];
                        Array.Copy(item, 19, b_slot2, 0, 3);
                        i_slot2 = BitConverter.ToInt32(b_slot2, 0);
                        storageItem.Slot2ItemId = i_slot2;

                        byte[] b_slot3 = new byte[4];
                        Array.Copy(item, 23, b_slot3, 0, 3);
                        i_slot3 = BitConverter.ToInt32(b_slot3, 0);
                        storageItem.Slot3ItemId = i_slot3;

                        byte[] b_slot4 = new byte[4];
                        Array.Copy(item, 27, b_slot4, 0, 3);
                        i_slot4 = BitConverter.ToInt32(b_slot4, 0);
                        storageItem.Slot4ItemId = i_slot4;

                        byte[] b_op1Key = new byte[2];
                        Array.Copy(item, 40, b_op1Key, 0, 2);
                        i_op1Key = BitConverter.ToInt16(b_op1Key, 0);
                        storageItem.Option1Key = i_op1Key;

                        byte[] b_op1Value = new byte[2];
                        Array.Copy(item, 42, b_op1Value, 0, 2);
                        i_op1Value = BitConverter.ToInt16(b_op1Value, 0);
                        storageItem.Option1Value = i_op1Value;

                        byte[] b_op2Key = new byte[2];
                        Array.Copy(item, 45, b_op2Key, 0, 2);
                        i_op2Key = BitConverter.ToInt16(b_op2Key, 0);
                        storageItem.Option2Key = i_op2Key;

                        byte[] b_op2Value = new byte[2];
                        Array.Copy(item, 47, b_op2Value, 0, 2);
                        i_op2Value = BitConverter.ToInt16(b_op2Value, 0);
                        storageItem.Option2Value = i_op2Value;

                        byte[] b_op3Key = new byte[2];
                        Array.Copy(item, 50, b_op3Key, 0, 2);
                        i_op3Key = BitConverter.ToInt16(b_op3Key, 0);
                        storageItem.Option3Key = i_op3Key;

                        byte[] b_op3Value = new byte[2];
                        Array.Copy(item, 52, b_op3Value, 0, 2);
                        i_op3Value = BitConverter.ToInt16(b_op3Value, 0);
                        storageItem.Option3Value = i_op3Value;

                        byte[] b_op4Key = new byte[2];
                        Array.Copy(item, 55, b_op4Key, 0, 2);
                        i_op4Key = BitConverter.ToInt16(b_op4Key, 0);
                        storageItem.Option4Key = i_op4Key;

                        byte[] b_op4Value = new byte[2];
                        Array.Copy(item, 57, b_op4Value, 0, 2);
                        i_op4Value = BitConverter.ToInt16(b_op4Value, 0);
                        storageItem.Option4Value = i_op4Value;

                        byte[] b_op5Key = new byte[2];
                        Array.Copy(item, 60, b_op5Key, 0, 2);
                        i_op5Key = BitConverter.ToInt16(b_op5Key, 0);
                        storageItem.Option5Key = i_op5Key;

                        byte[] b_op5Value = new byte[2];
                        Array.Copy(item, 62, b_op5Value, 0, 2);
                        i_op5Value = BitConverter.ToInt16(b_op5Value, 0);
                        storageItem.Option5Value = i_op5Value;
                    }


                    byte[] b_enhanceLevel = new byte[2];
                    Array.Copy(item, 65, b_enhanceLevel, 0, 2);
                    int i_enhanceLevel = BitConverter.ToInt16(b_enhanceLevel, 0);
                    storageItem.EnhanceLevel = i_enhanceLevel;


                    var builder = new StringBuilder();
                    if (i_enhanceLevel > 0)
                    {
                        builder.Append($"+{i_enhanceLevel} ");
                    }
                    if (i_createGrade > 0)
                    {
                        switch (i_createGrade)
                        {
                            case 1: builder.Append("ぷち強い "); break;
                            case 2: builder.Append("強い "); break;
                            case 3: builder.Append("超強い "); break;
                            default: break;
                        }
                        builder.Append($"{name} ");
                    }
                    // 自作武器ではないとき
                    else
                    {
                        builder.Append($"{name} ");

                        var itemNameMap = mainForm.itemIdNameMap.Map;
                        if (i_slot1 > 0)
                        {
                            itemNameMap.TryGetValue(i_slot1, out string mName);
                            if (name != null) builder.Append($"{mName} ");
                        }
                        if (i_slot2 > 0)
                        {
                            itemNameMap.TryGetValue(i_slot2, out string mName);
                            if (name != null) builder.Append($"{mName} ");
                        }
                        if (i_slot3 > 0)
                        {
                            itemNameMap.TryGetValue(i_slot3, out string mName);
                            if (name != null) builder.Append($"{mName} ");
                        }
                        if (i_slot4 > 0)
                        {
                            itemNameMap.TryGetValue(i_slot4, out string mName);
                            if (name != null) builder.Append($"{mName} ");
                        }
                        var optionNameMap = mainForm.optionIdNameMap.Map;
                        if (i_op1Key > 0 && i_op1Value > 0)
                        {
                            optionNameMap.TryGetValue(i_op1Key, out string opDescription);
                            if (opDescription != null)
                            {
                                var description = string.Format(opDescription, i_op1Value);
                                builder.Append($"{description} ");
                            }
                        }
                        if (i_op2Key > 0 && i_op2Value > 0)
                        {
                            optionNameMap.TryGetValue(i_op2Key, out string opDescription);
                            if (opDescription != null)
                            {
                                var description = string.Format(opDescription, i_op2Value);
                                builder.Append($"{description} ");
                            }
                        }
                        if (i_op3Key > 0 && i_op3Value > 0)
                        {
                            optionNameMap.TryGetValue(i_op3Key, out string opDescription);
                            if (opDescription != null)
                            {
                                var description = string.Format(opDescription, i_op3Value);
                                builder.Append($"{description} ");
                            }
                        }
                        if (i_op4Key > 0 && i_op4Value > 0)
                        {
                            optionNameMap.TryGetValue(i_op4Key, out string opDescription);
                            if (opDescription != null)
                            {
                                var description = string.Format(opDescription, i_op4Value);
                                builder.Append($"{description} ");
                            }
                        }
                        if (i_op5Key > 0 && i_op5Value > 0)
                        {
                            optionNameMap.TryGetValue(i_op5Key, out string opDescription);
                            if (opDescription != null)
                            {
                                var description = string.Format(opDescription, i_op5Value);
                                builder.Append($"{description} ");
                            }
                        }
                    }
                    storageItem.Name = builder.ToString();

                    storageItems.Add(storageItem);
                }
                catch (Exception e)
                {
                    mainForm.LogError($"AnalyzeEquips: Failed to parse a packet: {BitConverter.ToString(item)}, Exception: {e.Message}");
                    continue;
                }
            }


            dataGridView.Invoke((MethodInvoker)delegate
            {
                var list = dataGridView.DataSource as SortableBindingList<Item>;
                if (list == null)
                {
                    list = new SortableBindingList<Item>();
                    dataGridView.DataSource = list;
                }

                foreach (var item in storageItems)
                {
                    list.Add(item);
                }
            });

            return;
        }


        private void AnalyzeItems(List<byte[]> items)
        {
            // アイテム情報は100個単位で送信されるため、最大6回（600個分）これが呼ばれる可能性がある
            
            var storageItems = new List<Item>();
            foreach (var item in items)
            {
                try
                {
                    byte[] itemId = new byte[4];
                    Array.Copy(item, 2, itemId, 0, 3); // 恐らくItemIDは3バイト
                    int intItemId = BitConverter.ToInt32(itemId, 0);

                    byte[] itemCount = new byte[2]; // ROのアイテム所持上限が30000なので、おそらに2byteだろう
                    Array.Copy(item, 7, itemCount, 0, 2);
                    int intItemCount = BitConverter.ToInt16(itemCount, 0);

                    byte[] itemLimitDateTime = new byte[4];
                    Array.Copy(item, 29, itemLimitDateTime, 0, 4);
                    int unixtime = BitConverter.ToInt32(itemLimitDateTime, 0);

                    var storageItem = new Item();
                    storageItem.ItemId = intItemId;
                    mainForm.itemIdNameMap.Map.TryGetValue(intItemId, out string name);
                    if (name == null)
                    {
                        continue;
                    }
                    storageItem.Name = name;


                    storageItem.Count = intItemCount;
                    if (unixtime > 0)
                    {
                        storageItem.LimitDateTime = DateTimeOffset.FromUnixTimeSeconds(unixtime).ToLocalTime();
                    }
                    storageItems.Add(storageItem);
                }
                catch (Exception ex)
                {
                    mainForm.LogError($"AnalyzeItems: Failed to parse a packet: {BitConverter.ToString(item)}, Exception: {ex.Message}");
                    continue;
                }

            }


            dataGridView.Invoke((MethodInvoker)delegate
            {
                var list = dataGridView.DataSource as SortableBindingList<Item>;
                if (list == null)
                {
                    list = new SortableBindingList<Item>();
                    dataGridView.DataSource = list;
                }

                foreach (var item in storageItems)
                {
                    list.Add(item);
                }
            });


            return;
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



        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var gridView = (DataGridView)sender;
            if (gridView.Columns[e.ColumnIndex].Name == "nameDataGridViewTextBoxColumn")
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                var items = ((IEnumerable<Item>)dataGridView.DataSource).ToList();
                var selectedItem = items[e.RowIndex];
                if (selectedItem.ItemId != 0)
                {
                    var url = $"https://rotool.gungho.jp/item/{selectedItem.ItemId}/0/";
                    System.Diagnostics.Process.Start(url);
                }
            }
            else if (gridView.Columns[e.ColumnIndex].Name == "linkDataGridViewTextBoxColumn")
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                var items = ((IEnumerable<Item>)dataGridView.DataSource).ToList();
                var selectedItem = items[e.RowIndex];
                if (selectedItem.ItemId != 0)
                {
                    mainForm.itemIdNameMap.Map.TryGetValue(selectedItem.ItemId, out var originalName);
                    if (originalName != null)
                    {
                        var url = $"http://unitrix.net/?w=BreNoa&i={originalName}";
                        System.Diagnostics.Process.Start(url);
                    }

                }
            }
        }


        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //dataGridView.ClearSelection();
        }

        private async void btnFetchKakaku_Click(object sender, EventArgs e)
        {
            var caller = new ApiCaller(dataGridView, mainForm, comboApiLimit);
            await caller.OnClickedFetchKakakuButton();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string text = "";
            text += "倉庫監視モードは、倉庫を開いた際のパケットを監視して これを素にRO公式ツールが使用しているAPIから価格情報を取得するものです。\n\n";
            text += "ROのサーバー/クライアントプログラムの変更やサーバーのIPアドレスの変更などにより、この機能は利用できなくなる場合があります。\n";
            text += "また解析が不十分なため、意図しない結果が表示されたり、本ツールがクラッシュするなどの可能性があります。\n";
            text += "倉庫に溜まったカードのおおよその値段を調べる程度の使い方を推奨します。\n";
            text += "尚、本機能は受信パケットの監視を行いますが、送信パケットに一切の手を加えるものではありません。\n\n";
            text += "使い方：\n";
            text += "1. 「倉庫監視 開始」ボタンを押下します。\n";
            text += "2. ゲーム内で倉庫を開きます。\n";
            text += "3. 「倉庫監視 停止」ボタンを押下します。\n";
            text += "注意：このツールはRO公式ツールが使用しているAPIを利用するため、大量のデータを頻繁に再取得することは避けてください。\n";
            text += "（『「X」日以内にサーバーから取得したデータは再取得しない』で大きめの数字を指定することを推奨します。）\n";
            text += "監視が有効な状態のときに倉庫を複数回開くと 開いた回数だけデータが追加されます。必要な時のみ監視を有効にしてください。\n";

            MessageBox.Show(text, "ヘルプ");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridView.Invoke((MethodInvoker)delegate { dataGridView.DataSource = new SortableBindingList<Item>(); });
        }

    }
}
