using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

// TODO: ログ読込時はキャッシュを読まない。APIアクセス時のみキャッシュを読むようにする！！！！

namespace RoItemKakakuChecker
{
    public partial class MainForm : Form
    {
        private AppSettings settings;
        private bool stopFlag = false;
        private bool isFetching = false;

        public MainForm()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
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
        }

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView.ClearSelection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";

            settings = new AppSettings();
            bool isSucceeded = settings.ReadSettings();

            if (isSucceeded)
            {
                switch (settings.ApiLimit)
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
                txtChatDir.Text = settings.ChatLogDir;
            }
            else
            {
                comboApiLimit.SelectedIndex = 3;
            }

            comboApiLimit.SelectedValueChanged += ComboApiLimit_SelectedValueChanged;
            btnFetchKakaku.Enabled = false;
            UpdateApiLimitMessageStatusLabel();
        }

        private void ComboApiLimit_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateApiLimitMessageStatusLabel();

            settings.SaveSettings(Convert.ToInt32(comboApiLimit.SelectedItem.ToString()), txtChatDir.Text);
        }

        private void UpdateApiLimitMessageStatusLabel()
        {
            if (Convert.ToInt32(comboApiLimit.SelectedItem) < 3)
            {
                toolStripStatusLabel.Text = "注意: 大量のデータを頻繁に再取得することは避けてください。ガンホーに怒られますよ！";
            }
            else
            {
                toolStripStatusLabel.Text = "";
            }
        }


        /// <summary>
        /// チャットログディレクトリ指定ボタン押下時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChatDir_Click(object sender, EventArgs e)
        {
            using(var openFileDialog = new OpenFileDialog()
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


            settings.SaveSettings(Convert.ToInt32(comboApiLimit.SelectedItem.ToString()), txtChatDir.Text);



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

            // LoadCache(items);


            ApplyDataGridView(items);

            btnFetchKakaku.Enabled = true;
        }

        private void LoadCache(IEnumerable<Item> items)
        {
            string cacheFilePath = Application.StartupPath + @"\cache";

            if (!File.Exists(cacheFilePath))
            {
                return;
            }

            try
            {
                using (var stream = new FileStream(cacheFilePath, FileMode.Open))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        var cachedItems = JsonSerializer.Deserialize<IEnumerable<Item>>(sr.ReadToEnd());

                        foreach (var item in items) {
                            var cachedItem = cachedItems.FirstOrDefault(ci => item.Name == ci.Name);
                            if (cachedItem != null)
                            {
                                item.LastFetchedAt = cachedItem.LastFetchedAt;
                                item.ItemId = cachedItem.ItemId;
                                item.EachPrice = cachedItem.EachPrice;
                            }
                        }

                        return;
                    }
                }
            }
            catch (Exception)
            {
                File.Delete(cacheFilePath);
            }

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
            isFetching = true;
            ApiCaller caller = new ApiCaller();
            var items = (IEnumerable<Item>)dataGridView.DataSource;

            LoadCache(items);

            toolStripProgressBar.Minimum = 0;
            toolStripProgressBar.Maximum = items.Count();
            toolStripProgressBar.Value = 0;

            SortableBindingList<Item> newList = new SortableBindingList<Item>();
            List<Item> forUpdate = new List<Item>();

            int count = 1;
            foreach (var item in items)
            {
                if (stopFlag)
                {
                    toolStripStatusLabel.Text = $"取得を中断しました。";
                    toolStripProgressBar.Value = 0;
                    isFetching = false;
                    stopFlag = false;
                    return;
                }


                toolStripStatusLabel.Text = $"価格情報取得中 ({count++}/{items.Count()})";

                Item dataFetchedItem = null;

                int limit = Convert.ToInt32(comboApiLimit.SelectedItem);
                if (item.LastFetchedAt >= DateTime.Now.AddDays(limit * -1)) {
                    dataFetchedItem = item;
                }
                else
                {
                    dataFetchedItem = await caller.GetItemAsync(item.Name);
                    if (dataFetchedItem == null)
                    {
                        toolStripProgressBar.Value++;
                        continue;
                    }
                    forUpdate.Add(dataFetchedItem);
                }



                toolStripProgressBar.Value++;
                dataFetchedItem.Count = item.Count;
                dataFetchedItem.LastFetchedAt = DateTime.Now;
                dataFetchedItem.TotalPrice = dataFetchedItem.EachPrice * dataFetchedItem.Count;
                newList.Add(dataFetchedItem);


            }

            toolStripStatusLabel.Text = $"価格情報取得完了";
            dataGridView.DataSource = newList;

            SaveItemsCache(forUpdate);

            isFetching = false;
        }

        private void SaveItemsCache(IEnumerable<Item> updatedItems)
        {

            string cacheFilePath = Application.StartupPath + @"\cache";

            List<Item> cachedItems = new List<Item>();

            if (File.Exists(cacheFilePath))
            {
                try
                {
                    using (var stream = new FileStream(cacheFilePath, FileMode.Open))
                    {
                        using (var sr = new StreamReader(stream))
                        {

                            cachedItems = JsonSerializer.Deserialize<List<Item>>(sr.ReadToEnd());
                        }
                    }
                }
                catch (Exception)
                {
                    File.Delete(cacheFilePath);
                }
            }


            foreach (Item updatedItem in updatedItems) {
                var cachedItem = cachedItems.FirstOrDefault(ci => updatedItem.ItemId == ci.ItemId);
                if (cachedItem != null)
                {
                    cachedItem.EachPrice = updatedItem.EachPrice;
                    cachedItem.LastFetchedAt = updatedItem.LastFetchedAt;
                }
                else
                {
                    cachedItems.Add(updatedItem);
                }
            }


            string jsonStr = JsonSerializer.Serialize(cachedItems);

            using (var writer = new StreamWriter(cacheFilePath, false, Encoding.UTF8))
            {
                writer.Write(jsonStr);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (isFetching)
            {
                stopFlag = true;
                isFetching = false;
            }
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
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string text = "RO価格確認機 v1.1.1\n\n";
            text += "このツールは、チャットログに含まれるアイテム獲得メッセージを抽出して これを素にRO公式ツールが使用しているAPIから価格情報を取得するものです。\n\n";
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
            text += "（『「X」日以内にサーバーから取得したデータは再取得しない』で大きめの数字を指定することを推奨します。無理な使い方をしてガンホーに怒られても知りませんよ！）\n";
            text += "また何らかの不利益が発生したとしても作者は責任を負いません。\n";
            text += "ソースコードが欲しい方は @majo_sama まで。";


            MessageBox.Show(text, "ヘルプ");
        }

        private void 結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var list = dataGridView.DataSource as IEnumerable<Item>;
            foreach (var item in list)
            {
                sb.Append(item.Name);
                sb.Append("\t");
                sb.Append(item.Count);
                sb.Append("\t");
                sb.Append(item.EachPrice.ToString());
                sb.Append("\t");
                sb.Append(item.TotalPrice);

                if (item != list.Last())
                {
                    sb.Append("\n");
                }
            }
            Clipboard.SetText(sb.ToString());
            toolStripStatusLabel.Text = "クリップボードにコピーしました。";
        }

        private void 結果をクリップボードにコピーToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var list = dataGridView.DataSource as IEnumerable<Item>;
            foreach (var item in list)
            {
                sb.Append(item.ItemId.ToString());
                sb.Append("\t");
                sb.Append(item.Name);
                sb.Append("\t");
                sb.Append(item.Count);
                sb.Append("\t");
                sb.Append(item.EachPrice.ToString());
                sb.Append("\t");
                sb.Append(item.TotalPrice);
                sb.Append("\t");
                sb.Append($"https://rotool.gungho.jp/item/{item.ItemId}/0/");
                if (item != list.Last())
                {
                    sb.Append("\n");
                }
            }
            Clipboard.SetText(sb.ToString());
            toolStripStatusLabel.Text = "クリップボードにコピーしました。";

        }

        private void 結果をCSVファイルに出力簡易ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var list = dataGridView.DataSource as IEnumerable<Item>;
            sb.AppendLine("アイテム名,個数,単体価格(中央値),合計金額");
            foreach (var item in list)
            {
                sb.Append(item.Name);
                sb.Append(",");
                sb.Append(item.Count);
                sb.Append(",");
                sb.Append(item.EachPrice.ToString());
                sb.Append(",");
                sb.Append(item.TotalPrice);
                sb.Append("\n");
            }

            OutputCsv(sb.ToString());
        }

        private void 結果をCSVファイルに出力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var list = dataGridView.DataSource as IEnumerable<Item>;

            sb.AppendLine("ItemID,アイテム名,個数,単体価格(中央値),合計金額,URL");
            foreach (var item in list)
            {
                sb.Append(item.ItemId.ToString());
                sb.Append(",");
                sb.Append(item.Name);
                sb.Append(",");
                sb.Append(item.Count);
                sb.Append(",");
                sb.Append(item.EachPrice.ToString());
                sb.Append(",");
                sb.Append(item.TotalPrice);
                sb.Append(",");
                sb.Append($"https://rotool.gungho.jp/item/{item.ItemId}/0/");
                sb.Append("\n");
            }
            OutputCsv(sb.ToString());
        }

        private void OutputCsv(string csv)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "kakaku.csv";
            dialog.InitialDirectory = Environment.CurrentDirectory;
            dialog.Filter = "CSVファイル(*.csv)|*.csv";
            dialog.FilterIndex = 1;
            dialog.Title = "保存先のファイルを指定してください。";
            dialog.RestoreDirectory = true;
            dialog.OverwritePrompt = true;
            dialog.CheckPathExists = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var writer = new StreamWriter(dialog.FileName, false, Encoding.UTF8))
                    {
                        writer.Write(csv);
                        toolStripStatusLabel.Text = "ファイルを出力しました。";
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
