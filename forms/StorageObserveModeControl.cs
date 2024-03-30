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

namespace RoItemKakakuChecker
{
    public partial class StorageObserveModeControl : UserControl
    {
        private static bool stopFlag = false;
        private static bool observing = false;
        private const string RO_STORAGE_SERVER_IP = "18.182.57.203";
        private ItemIdNameMap itemIdNameMap = new ItemIdNameMap();


        public StorageObserveModeControl()
        {
            InitializeComponent();
            this.btnObserve.Text = "倉庫監視 開始";
        }

        private async void btnObserve_Click(object sender, EventArgs e)
        {
            if (observing)
            {
                stopFlag = true;
                observing = false;
                this.btnObserve.Text = "倉庫監視 開始";
                return;
            }



            stopFlag = false;
            observing = true;
            this.btnObserve.Text = "倉庫監視 停止";

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

            // ファイアウォール設定解除しないと受信パケットを確認できない
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

                    if (protocol == "TCP" && srcIp == RO_STORAGE_SERVER_IP)
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
                            nextIndex = nextIndex + body.Length;
                        }

                        // 終端パケットまで読んだら解析
                        if (hasPshFlag)
                        {
                            Analyze(joinedBody);
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
            // アイテム倉庫通常パターン
            if (data[0] == 0x09 && data[1] == 0x0b)
            {
                // 各アイテムのバイト配列の一覧
                var items = new List<byte[]>();
                
                int index = 5; // 5バイトのヘッダ
                while (data.Length - index >= 34)
                {
                    byte[] item = new byte[34];
                    Array.Copy(data, index, item, 0, 34); // アイテム1つ34バイト

                    // dataは必要以上に大きく確保してあるため、
                    // すべて0のとき、無駄なものとして無視する
                    if (item.All(b => b == 0))
                    {
                        break;
                    }

                    items.Add(item);
                    index += 34;
                }
                AnalyzeItems(items);
            }
            // アイテム倉庫 先頭にヘッダがあるパターン
            else if (data[0] == 0x08 && data[1] == 0x0b && data[14] == 0x09 && data[15] == 0x0b)
            {
                // 各アイテムのバイト配列の一覧
                var items = new List<byte[]>();
                int index = 19; // 19バイトのヘッダ
                while (data.Length - index >= 34)
                {
                    byte[] item = new byte[34];
                    Array.Copy(data, index, item, 0, 34); // アイテム1つ34バイト
                    // dataは必要以上に大きく確保してあるため、
                    // すべて0のとき、無駄なものとして無視する
                    if (item.All(b => b == 0))
                    {
                        break;
                    }

                    items.Add(item);
                    index += 34;
                }
                AnalyzeItems(items);
            }
            // 装備品
            else if (data[0] == 0x39 && data[1] == 0x0b)
            {
                // 各アイテムのバイト配列の一覧
                var items = new List<byte[]>();
                int index = 5; // 5バイトのヘッダ
                while (data.Length - index >= 68)
                {
                    byte[] item = new byte[68];
                    Array.Copy(data, index, item, 0, 68); // アイテム1つ68バイト
                    // dataは必要以上に大きく確保してあるため、
                    // すべて0のとき、無駄なものとして無視する
                    if (item.All(b => b == 0))
                    {
                        break;
                    }

                    items.Add(item);
                    index += 68;
                }
                AnalyzeEquips(items);
            }

        }

        private void AnalyzeEquips(List<byte[]> equips)
        {
            var storageItems = new List<Item>();
            foreach (var item in equips)
            {
                var storageItem = new Item();

                byte[] itemId = new byte[4];
                Array.Copy(item, 2, itemId, 0, 3); // 恐らくItemIDは3バイト
                int intItemId = BitConverter.ToInt32(itemId, 0);
                storageItem.ItemId = intItemId;
                storageItem.Name = itemIdNameMap.Map[intItemId];



                int i_createGrade = 0;
                // ユーザー製の武器
                if (item[15] == 0xff || item[15] == 0xfe)
                {
                    switch (item[20])
                    {
                        case 0x05: i_createGrade = 1; break;
                        case 0x0a: i_createGrade = 2; break;
                        case 0x0f: i_createGrade = 3; break;
                        default:   i_createGrade = 0; break;
                    }
                    storageItem.CreateGrade = i_createGrade;
                }
                // 通常の装備
                else
                {

                    byte[] b_slot1 = new byte[2];
                    Array.Copy(item, 15, b_slot1, 0, 2);
                    int i_slot1 = BitConverter.ToInt16(b_slot1, 0);
                    storageItem.Slot1ItemId = i_slot1;

                    byte[] b_slot2 = new byte[2];
                    Array.Copy(item, 19, b_slot2, 0, 2);
                    int i_slot2 = BitConverter.ToInt16(b_slot2, 0);
                    storageItem.Slot2ItemId = i_slot2;

                    byte[] b_slot3 = new byte[2];
                    Array.Copy(item, 23, b_slot3, 0, 2);
                    int i_slot3 = BitConverter.ToInt16(b_slot3, 0);
                    storageItem.Slot3ItemId = i_slot3;

                    byte[] b_slot4 = new byte[2];
                    Array.Copy(item, 27, b_slot4, 0, 2);
                    int i_slot4 = BitConverter.ToInt16(b_slot4, 0);
                    storageItem.Slot4ItemId = i_slot4;

                    byte[] b_op1Key = new byte[2];
                    Array.Copy(item, 40, b_op1Key, 0, 2);
                    int i_op1Key = BitConverter.ToInt16(b_op1Key, 0);
                    storageItem.Option1Key = i_op1Key;

                    byte[] b_op1Value = new byte[2];
                    Array.Copy(item, 42, b_op1Value, 0, 2);
                    int i_op1Value = BitConverter.ToInt16(b_op1Value, 0);
                    storageItem.Option1Value = i_op1Value;

                    byte[] b_op2Key = new byte[2];
                    Array.Copy(item, 45, b_op2Key, 0, 2);
                    int i_op2Key = BitConverter.ToInt16(b_op2Key, 0);
                    storageItem.Option2Key = i_op2Key;

                    byte[] b_op2Value = new byte[2];
                    Array.Copy(item, 47, b_op2Value, 0, 2);
                    int i_op2Value = BitConverter.ToInt16(b_op2Value, 0);
                    storageItem.Option2Value = i_op2Value;

                    byte[] b_op3Key = new byte[2];
                    Array.Copy(item, 50, b_op3Key, 0, 2);
                    int i_op3Key = BitConverter.ToInt16(b_op3Key, 0);
                    storageItem.Option3Key = i_op3Key;

                    byte[] b_op3Value = new byte[2];
                    Array.Copy(item, 52, b_op3Value, 0, 2);
                    int i_op3Value = BitConverter.ToInt16(b_op3Value, 0);
                    storageItem.Option3Value = i_op3Value;

                    byte[] b_op4Key = new byte[2];
                    Array.Copy(item, 55, b_op4Key, 0, 2);
                    int i_op4Key = BitConverter.ToInt16(b_op4Key, 0);
                    storageItem.Option4Key = i_op4Key;

                    byte[] b_op4Value = new byte[2];
                    Array.Copy(item, 57, b_op4Value, 0, 2);
                    int i_op4Value = BitConverter.ToInt16(b_op4Value, 0);
                    storageItem.Option4Value = i_op4Value;

                    byte[] b_op5Key = new byte[2];
                    Array.Copy(item, 60, b_op5Key, 0, 2);
                    int i_op5Key = BitConverter.ToInt16(b_op5Key, 0);
                    storageItem.Option5Key = i_op5Key;

                    byte[] b_op5Value = new byte[2];
                    Array.Copy(item, 62, b_op5Value, 0, 2);
                    int i_op5Value = BitConverter.ToInt16(b_op5Value, 0);
                    storageItem.Option5Value = i_op5Value;
                }


                byte[] b_enhanceLevel = new byte[2];
                Array.Copy(item, 65, b_enhanceLevel, 0, 2);
                int i_enhanceLevel = BitConverter.ToInt16(b_enhanceLevel, 0);
                storageItem.EnhanceLevel = i_enhanceLevel;


                storageItems.Add(storageItem);
            }

            var list = dataGridView.DataSource as SortableBindingList<Item>;
            if (list == null)
            {
                list = new SortableBindingList<Item>();
            }

            foreach (var item in storageItems)
            {
                list.Add(item);
            }

            dataGridView.Invoke((MethodInvoker)delegate { dataGridView.DataSource = list; });
            //dataGridView.DataSource = list;
            return;
        }


        private void AnalyzeItems(List<byte[]> items)
        {
            // アイテム情報は100個単位で送信されるため、最大6回（600個分）これが呼ばれる可能性がある
            
            var storageItems = new List<Item>();
            foreach (var item in items)
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
                storageItem.Name = itemIdNameMap.Map[intItemId];
                storageItem.Count = intItemCount;
                if (unixtime > 0)
                {
                   storageItem.LimitDateTime = DateTimeOffset.FromUnixTimeSeconds(unixtime).ToLocalTime();
                }
                storageItems.Add(storageItem);
            }
            var list = dataGridView.DataSource as SortableBindingList<Item>;
            if (list == null)
            {
                list = new SortableBindingList<Item>();
            }
            
            foreach (var item in storageItems)
            {
                list.Add(item);
            }

            dataGridView.Invoke((MethodInvoker)delegate { dataGridView.DataSource = list; });
            

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


    }
}
