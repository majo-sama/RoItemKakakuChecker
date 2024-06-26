﻿using RoItemKakakuChecker.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoItemKakakuChecker
{
    public partial class ChatLogModeControl : UserControl
    {
        private MainForm mainForm;


        public ChatLogModeControl(Form parent)
        {
            InitializeComponent();
            this.mainForm = parent as MainForm;
            this.Load += ChatLogModeControl_Load;

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

        private void ChatLogModeControl_Load(object sender, EventArgs e)
        {
            mainForm.UpdateToolStripLabel("");

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
                txtChatDir.Text = mainForm.settings.ChatLogDir;
            }
            else
            {
                comboApiLimit.SelectedIndex = 5;
            }

            comboApiLimit.SelectedValueChanged += ComboApiLimit_SelectedValueChanged;
            btnFetchKakaku.Enabled = false;
            UpdateApiLimitMessageStatusLabel();
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //dataGridView.ClearSelection();
        }


        private void ComboApiLimit_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateApiLimitMessageStatusLabel();

            mainForm.settings.SaveSettings(Convert.ToInt32(comboApiLimit.SelectedItem.ToString()), txtChatDir.Text);
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


        /// <summary>
        /// チャットログディレクトリ指定ボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnChatDir_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog()
            {
                FileName = "ChatLog",
                Filter = "Folder|.",
                ValidateNames = false,
                CheckFileExists = false,
                CheckPathExists = true
            })
            {
                var initialDir = @"C:\Gravity\Ragnarok\Chat";
                if (Directory.Exists(initialDir))
                {
                    openFileDialog.InitialDirectory = initialDir;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtChatDir.Text = Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
        }



        /// <summary>
        /// チャットログ読み込みボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadChatLog_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists(txtChatDir.Text))
            {
                MessageBox.Show("ディレクトリが存在しません。");
                return;
            }
            var directoryInfo = new DirectoryInfo(txtChatDir.Text);
            IEnumerable<FileInfo> files = directoryInfo
                .EnumerateFiles("*.txt", SearchOption.AllDirectories)
            .OrderByDescending(f => f.CreationTime);


            mainForm.settings.SaveSettings(Convert.ToInt32(comboApiLimit.SelectedItem.ToString()), txtChatDir.Text);



            if (files == null || files.Count() == 0)
            {
                MessageBox.Show("チャットログが存在しません。");
                return;
            }

            IEnumerable<Item> items = ChatLogReader.SearchItemLogs(files);
            if (!items.Any())
            {
                MessageBox.Show("アイテムの獲得ログが存在しません。");
                return;
            }

            foreach (var item in items)
            {
                mainForm.itemIdNameMap.ValueKeyMap.TryGetValue(item.Name, out int id);
                if (id != 0)
                {
                    item.ItemId = id;
                }
            }


            ApplyDataGridView(items);

            btnFetchKakaku.Enabled = true;
        }


        private void ApplyDataGridView(IEnumerable<Item> items)
        {
            //BindingList<Item> list = new BindingList<Item>();
            //foreach (var item in items)
            //{
            //    list.Add(item);
            //}


            SortableBindingList<Item> list = new SortableBindingList<Item>();
            foreach (var item in items)
            {
                list.Add(item);
            }


            dataGridView.DataSource = list;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                Item rowItem = row.DataBoundItem as Item;
                var linkCell = row.Cells[0] as DataGridViewLinkCell;
                if (linkCell != null)
                {
                    if (rowItem.ItemId == 0)
                    {
                        linkCell.LinkBehavior = LinkBehavior.NeverUnderline;
                        linkCell.LinkColor = Color.Black;
                    }
                    else
                    {
                        linkCell.LinkBehavior = LinkBehavior.HoverUnderline;
                        linkCell.LinkColor = Color.Blue;
                    }
                }

            }
        }

        /// <summary>
        /// 価格取得ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnFetchKakaku_Click(object sender, EventArgs e)
        {
            var caller = new ApiCaller(dataGridView, mainForm, comboApiLimit);
            await caller.OnClickedFetchKakakuButton();
            //isFetching = true;
            //ApiCaller caller = new ApiCaller();
            //var items = (IEnumerable<Item>)dataGridView.DataSource;

            //LoadCache(items);

            //mainForm.UpdateToolStripProgressBarSetting(0, items.Count());
            //mainForm.UpdateToolStripProgressBarValue(0);

            //SortableBindingList<Item> newList = new SortableBindingList<Item>();
            //List<Item> forUpdate = new List<Item>();

            //int count = 1;
            //foreach (var item in items)
            //{
            //    if (stopFlag)
            //    {
            //        mainForm.UpdateToolStripLabel("取得を中断しました。");
            //        mainForm.UpdateToolStripProgressBarValue(0);
            //        isFetching = false;
            //        stopFlag = false;
            //        return;
            //    }


            //    mainForm.UpdateToolStripLabel($"価格情報取得中 ({count++}/{items.Count()})");

            //    Item dataFetchedItem = null;

            //    int limit = Convert.ToInt32(comboApiLimit.SelectedItem);
            //    if (item.LastFetchedAt >= DateTime.Now.AddDays(limit * -1))
            //    {
            //        dataFetchedItem = item;
            //    }
            //    else
            //    {
            //        dataFetchedItem = await caller.GetItemAsync(item.Name);
            //        if (dataFetchedItem == null)
            //        {
            //            mainForm.IncrementToolStripProgressBarValue();
            //            continue;
            //        }
            //        forUpdate.Add(dataFetchedItem);
            //    }



            //    mainForm.IncrementToolStripProgressBarValue();
            //    dataFetchedItem.Count = item.Count;
            //    dataFetchedItem.LastFetchedAt = DateTime.Now;
            //    dataFetchedItem.TotalPrice = dataFetchedItem.EachPrice * dataFetchedItem.Count;
            //    newList.Add(dataFetchedItem);


            //}

            //mainForm.UpdateToolStripLabel($"価格情報取得完了");
            //dataGridView.DataSource = newList;

            //SaveItemsCache(forUpdate);

            //isFetching = false;
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

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string text = "";
            text += "チャットログから確認モードは、チャットログに含まれるアイテム獲得メッセージを抽出して これを素にRO公式ツールが使用しているAPIから価格情報を取得するものです。\n\n";
            text += "チャットログからアイテム名を判別できないもの（名称が長すぎて改行される場合など）やアイテム名が公式ツールに登録されていないもの（カード挿し・エンチャ・強化済み装備など）はデータを取得することができません。\n";
            text += "アイテム情報の取得に失敗した場合、情報が表示されない または近い名称の別のアイテムの情報が表示されることがあります。\n";
            text += "倉庫に溜まったカードのおおよその値段を調べる程度の使い方を推奨します。\n\n";
            text += "使い方：\n";
            text += "1. ゲーム内でアイテムを獲得します（倉庫から取り出すなど）。\n";
            text += "2. ゲーム内で /savechat を実行しチャットログを保存します。\n";
            text += "3. RO価格確認機を起動し、チャットログの保存フォルダを指定します。\n";
            text += "4. ログ読込ボタンを押します。\n";
            text += "5. 価格取得ボタンを押します。\n\n";
            text += "注意：このツールはRO公式ツールが使用しているAPIを利用するため、大量のデータを頻繁に再取得することは避けてください。\n";
            text += "（『「X」日以内にサーバーから取得したデータは再取得しない』で大きめの数字を指定することを推奨します。）\n";

            MessageBox.Show(text, "ヘルプ");
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
