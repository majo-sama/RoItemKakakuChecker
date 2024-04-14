using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Speech.Synthesis;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace RoItemKakakuChecker
{
    public partial class MainForm : Form
    {
        public AppSettings settings;
        public bool stopFlag = false;
        public bool isFetching = false;
        public ItemIdNameMap itemIdNameMap = new ItemIdNameMap();
        public OptionIdNameMap optionIdNameMap = new OptionIdNameMap();
        public Speaker speaker;


        public MainForm()
        {
            settings = new AppSettings();

            InitializeComponent();
            //this.TopMost = true;
            this.SizeGripStyle = SizeGripStyle.Show;
            chatObserveModeControl1.SetMainForm(this);
            speaker = new Speaker();

            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            speaker.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            storageObserveModeControl1.SetMainForm(this);


            // 
            // chatLogModeControl
            // 
            this.chatLogModeControl = new RoItemKakakuChecker.ChatLogModeControl(this);
            this.chatLogModeControl.Location = new System.Drawing.Point(0, 52);
            this.chatLogModeControl.Name = "chatLogModeControl";
            this.chatLogModeControl.Size = new System.Drawing.Size(542, 412);
            this.chatLogModeControl.TabIndex = 15;

            ToolTip toolTipChatLog = new ToolTip();
            toolTipChatLog.AutoPopDelay = 5000;
            toolTipChatLog.InitialDelay = 100;
            toolTipChatLog.ReshowDelay = 100;
            toolTipChatLog.SetToolTip(this.radioButton_chatLog, "/savechatにより出力されたチャットログに含まれるアイテム獲得メッセージを解析するモードです。");
            ToolTip toolTipObserveStorage = new ToolTip();
            toolTipObserveStorage.AutoPopDelay = 5000;
            toolTipObserveStorage.InitialDelay = 100;
            toolTipObserveStorage.ReshowDelay = 100;
            toolTipObserveStorage.SetToolTip(this.radioButton_storage, "倉庫を開いた際の通信パケットを監視し解析するモードです。");


            this.radioButton_chatLog.CheckedChanged += RadioButton_chatLog_CheckedChanged;
            this.radioButton_storage.CheckedChanged += RadioButton_chatLog_CheckedChanged;
            this.radioChatMessage.CheckedChanged += RadioButton_chatLog_CheckedChanged;

            this.radioButton_chatLog.Checked = false;
            this.radioButton_chatLog.Checked = true;


            this.Controls.Add(this.chatLogModeControl);
            this.Controls.Add(this.storageObserveModeControl1);
            this.Controls.Add(this.chatObserveModeControl1);

            btnStop.Click += BtnStop_Click;


            string cmdDelete = "netsh advfirewall firewall delete rule name=\"RoItemKakakuChecker\"";
            string exePath = Assembly.GetEntryAssembly().Location;
            string cmdAdd = $"netsh advfirewall firewall add rule name=\"RoItemKakakuChecker\" dir=in action=allow program=\"{exePath}\" enable=yes";

            //Processオブジェクトを作成
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //ComSpec(cmd.exe)のパスを取得して、FileNameプロパティに指定
            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            //出力を読み取れるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;
            //ウィンドウを表示しないようにする
            p.StartInfo.CreateNoWindow = true;
            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            p.StartInfo.Arguments = $@"/c {cmdDelete} & {cmdAdd}";

            //起動
            p.Start();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (isFetching)
            {
                stopFlag = true;
                isFetching = false;
            }
        }

        private void RadioButton_chatLog_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                if (radio.Name == "radioButton_chatLog")
                {
                    this.chatLogModeControl.Show();
                    this.storageObserveModeControl1.Hide();
                    this.chatObserveModeControl1.Hide();
                    btnStop.Show();
                    toolStripDropDownButton.Enabled = true;
                }
                else if (radio.Name == "radioButton_storage")
                {
                    this.storageObserveModeControl1.Show();
                    this.chatLogModeControl.Hide();
                    this.chatObserveModeControl1.Hide();
                    btnStop.Show();
                    toolStripDropDownButton.Enabled = true;
                }
                else
                {
                    this.chatObserveModeControl1.Show();
                    this.storageObserveModeControl1.Hide();
                    this.chatLogModeControl.Hide();
                    btnStop.Hide();
                    toolStripDropDownButton.Enabled = false;
                }
            }
            return;
        }



        public void UpdateToolStripLabel(string text)
        {
            this.Invoke(new Action(() => { toolStripStatusLabel.Text = text; }));
        }

        public void UpdateToolStripProgressBarSetting(int min, int max)
        {
            toolStripProgressBar.Minimum = min;
            toolStripProgressBar.Maximum = max;
            toolStripProgressBar.Value = 0;
        }

        public void UpdateToolStripProgressBarValue(int value)
        {
            toolStripProgressBar.Value = value;
        }

        public void IncrementToolStripProgressBarValue()
        {
            int nextValue = toolStripProgressBar.Value + 1;
            if (nextValue < toolStripProgressBar.Maximum)
            {
                toolStripProgressBar.Value = nextValue + 1;
                toolStripProgressBar.Value = nextValue;
            }
            else
            {
                toolStripProgressBar.Maximum++;
                toolStripProgressBar.Value = nextValue + 1;
                toolStripProgressBar.Value = nextValue;
                toolStripProgressBar.Maximum--;
            }
        }

        private void toolStripDropDownButton_Click(object sender, EventArgs e)
        {

        }

        public void LogError(string str)
        {
            var dir = "ErrorLog";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var fileName = $"errorlog_{date}.txt";
            Directory.CreateDirectory(dir);
            using (var writer = new StreamWriter($@"{dir}\{fileName}", true, Encoding.UTF8))
            {
                string text = $"{DateTime.Now.ToString()} {str}";
                writer.WriteLine(text);
                UpdateToolStripLabel($@"エラーログが {dir}\{fileName} に出力されました。");
            }
        }

        private void 結果をCSVファイルに出力_通常_Click(object sender, EventArgs e)
        {
            if (radioButton_chatLog.Checked || radioButton_storage.Checked)
            {
                StringBuilder sb = new StringBuilder();
                IEnumerable<Item> list;
                if (radioButton_chatLog.Checked)
                {
                    list = chatLogModeControl.dataGridView.DataSource as IEnumerable<Item>;
                }
                else
                {
                    list = storageObserveModeControl1.dataGridView.DataSource as IEnumerable<Item>;
                }

                if (list == null || list.Count() == 0)
                {
                    UpdateToolStripLabel($@"エラー: 出力対象のデータが存在しません。");
                    return;
                }

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



        }

        private void 結果をCSVファイルに出力_簡易_Click(object sender, EventArgs e)
        {
            if (radioButton_chatLog.Checked || radioButton_storage.Checked)
            {
                StringBuilder sb = new StringBuilder();
                IEnumerable<Item> list;
                if (radioButton_chatLog.Checked)
                {
                    list = chatLogModeControl.dataGridView.DataSource as IEnumerable<Item>;
                }
                else
                {
                    list = storageObserveModeControl1.dataGridView.DataSource as IEnumerable<Item>;
                }
                if (list == null || list.Count() == 0)
                {
                    UpdateToolStripLabel($@"エラー: 出力対象のデータが存在しません。");
                    return;
                }
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
        }

        private void 結果をExcel形式でクリップボードにコピー_通常_Click(object sender, EventArgs e)
        {
            if (radioButton_chatLog.Checked || radioButton_storage.Checked)
            {
                StringBuilder sb = new StringBuilder();
                IEnumerable<Item> list;
                if (radioButton_chatLog.Checked)
                {
                    list = chatLogModeControl.dataGridView.DataSource as IEnumerable<Item>;
                }
                else
                {
                    list = storageObserveModeControl1.dataGridView.DataSource as IEnumerable<Item>;
                }
                if (list == null || list.Count() == 0)
                {
                    UpdateToolStripLabel($@"エラー: 出力対象のデータが存在しません。");
                    return;
                }

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
                UpdateToolStripLabel("クリップボードにコピーしました。");
            }
        }

        private void 結果をExcel形式でクリップボードにコピー_簡易_Click(object sender, EventArgs e)
        {
            if (radioButton_chatLog.Checked || radioButton_storage.Checked)
            {
                StringBuilder sb = new StringBuilder();
                IEnumerable<Item> list;
                if (radioButton_chatLog.Checked)
                {
                    list = chatLogModeControl.dataGridView.DataSource as IEnumerable<Item>;
                }
                else
                {
                    list = storageObserveModeControl1.dataGridView.DataSource as IEnumerable<Item>;
                }
                if (list == null || list.Count() == 0)
                {
                    UpdateToolStripLabel($@"エラー: 出力対象のデータが存在しません。");
                    return;
                }
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
                UpdateToolStripLabel("クリップボードにコピーしました。");
            }
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
                        UpdateToolStripLabel("ファイルを出力しました。");
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
