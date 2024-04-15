using Semver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using NetFwTypeLib;

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

        private const string VERSION = "1.3.2";


        public MainForm()
        {
            settings = new AppSettings();

            InitializeComponent();
            //this.TopMost = true;
            this.SizeGripStyle = SizeGripStyle.Show;
            chatObserveModeControl1.SetMainForm(this);
            speaker = new Speaker();

            System.Windows.Forms.Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            speaker.Dispose();
        }

        private async void Form1_Load(object sender, EventArgs e)
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


            this.Size = new Size(690, 600);

            btnStop.Click += BtnStop_Click;

            //RemoveFirewallException();
            ////CheckFirewallException();
            AddFirewallException();

            this.Text = "RO価格確認機 v" + VERSION;


            await AutoUpdate();
        }


        static bool CheckFirewallException()
        {
            var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Type NetFwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
            INetFwMgr manager = (INetFwMgr)Activator.CreateInstance(NetFwMgrType);
            bool isExceptionAllowed = false;

            // ファイアーウォールの例外リストを取得
            foreach (INetFwAuthorizedApplication app in manager.LocalPolicy.CurrentProfile.AuthorizedApplications)
            {
                if (app.ProcessImageFileName.ToLower() == exePath.ToLower())
                {
                    isExceptionAllowed = true;
                    break;
                }
            }

            return isExceptionAllowed;
        }

        static void RemoveFirewallException()
        {
            var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var appName = "RoItemKakakuChecker";
            Type NetFwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr", false);
            INetFwMgr manager = (INetFwMgr)Activator.CreateInstance(NetFwMgrType);

            // ファイアーウォールの例外リストから指定されたアプリケーションを削除
            INetFwAuthorizedApplications apps = manager.LocalPolicy.CurrentProfile.AuthorizedApplications;
            foreach (INetFwAuthorizedApplication app in apps)
            {
                if (app.Name == appName)
                {
                    apps.Remove(app.ProcessImageFileName);
                }
            }
        }

        static void AddFirewallException()
        {

            //Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            //INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);
            //var currentProfiles = fwPolicy2.CurrentProfileTypes;

            // Let's create a new rule

            INetFwRule2 inboundRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            inboundRule.Enabled = true;
            //inboundRule.Profiles = currentProfiles;
            inboundRule.Profiles = 2 | 4;
            inboundRule.Name = "RoItemKakakuChecker";
            inboundRule.ApplicationName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            // Now add the rule

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallPolicy.Rules.Remove("RoItemkakakuChecker");
            firewallPolicy.Rules.Add(inboundRule);
        }

        private async Task<string> AutoUpdate()
        {
            var currentSemVer = SemVersion.Parse(VERSION, SemVersionStyles.Strict);


            try
            {

                string zipUrl = null;

                using(var client = new HttpClient())
                {

                    var url = "https://api.github.com/repos/majo-sama/RoItemKakakuChecker/releases/latest";

                    client.DefaultRequestHeaders.Accept.Clear();
                    //var byteArray = Encoding.ASCII.GetBytes(":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");
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "RoItemKakakuChecker");

                    HttpResponseMessage response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "failed";
                    }
                    string json = await response.Content.ReadAsStringAsync();
                    var parsedJson = JsonObject.Parse(json);
                    var latestVersion = parsedJson["tag_name"].ToString();
                    zipUrl = parsedJson["assets"][0]["browser_download_url"].ToString();

                    var latestSemVer = SemVersion.Parse(latestVersion, SemVersionStyles.Strict);

                    if (currentSemVer.CompareSortOrderTo(latestSemVer) >= 0)
                    {
                        return "no_needed";
                    }

                }


                // 保存するパスを指定
                string autoUpdateDir = "auto_update";
                string zipFileName = "archive.zip";

                if (Directory.Exists(autoUpdateDir))
                {
                    foreach (var f in Directory.EnumerateFiles(autoUpdateDir))
                    {
                        File.Delete(f);
                    }
                }
                Directory.CreateDirectory("auto_update");
                string filePath = $@"{autoUpdateDir}\{zipFileName}";

                using (HttpClient zipClient = new HttpClient())
                {
                    zipClient.DefaultRequestHeaders.Accept.Clear();
                    //var bytes = Encoding.ASCII.GetBytes(":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");
                    //zipClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
                    zipClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                    zipClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "RoItemKakakuChecker");
                    using (HttpResponseMessage zipResponse = await zipClient.GetAsync(zipUrl))
                    {
                        using (Stream streamToReadFrom = await zipResponse.Content.ReadAsStreamAsync())
                        {
                            using (Stream streamToWriteTo = File.Open(filePath, FileMode.Create))
                            {
                                await streamToReadFrom.CopyToAsync(streamToWriteTo);
                            }
                        }
                    }
                }


                using (ZipArchive archive = ZipFile.OpenRead(filePath))
                {
                    var entries = archive.Entries.Where(e => e.FullName.EndsWith(".txt") || e.FullName.EndsWith(".exe") || e.FullName.EndsWith(".html"));
                    foreach (ZipArchiveEntry entry in entries)
                    {
                        entry.ExtractToFile(Path.Combine(autoUpdateDir, entry.Name));
                    }

                }

                // Batファイル出力
                string text = "";
                text += @"@echo Updating..." + "\r\n";
                text += @"@echo off" + "\r\n";
                text += @"timeout /t 5 /nobreak > nul" + "\r\n";
                text += @"del " + System.Reflection.Assembly.GetExecutingAssembly().Location + " > nul" + "\r\n";
                text += @"copy auto_update\RoItemKakakuChecker.exe RoItemKakakuChecker.exe" + "\r\n";
                text += @"copy auto_update\readme.html readme.html" + "\r\n";
                text += @"start RoItemKakakuChecker.exe" + "\r\n";
                text += @"del patch.bat > nul" + "\r\n";
                text += @"exit" + "\r\n";
                StreamWriter sw = new StreamWriter(@"auto_update\patch.bat", false, Encoding.GetEncoding("Shift_JIS"));
                sw.Write(text);
                sw.Close();

                // exe更新
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = @"auto_update\patch.bat";
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                Application.Exit();
                p.Close();
                p = null;

                //Directory.Delete(autoUpdateDir, true);

                return "succeeded";
            }
            catch (Exception ex)
            {
                return "failed";
            }
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
                    toolStripDropDownButton.Visible = true;
                    toolStripProgressBar.Visible = true;

                }
                else if (radio.Name == "radioButton_storage")
                {
                    this.storageObserveModeControl1.Show();
                    this.chatLogModeControl.Hide();
                    this.chatObserveModeControl1.Hide();
                    btnStop.Show();
                    toolStripDropDownButton.Enabled = true;
                    toolStripDropDownButton.Visible = true;
                    toolStripProgressBar.Visible = true;


                }
                else
                {
                    this.chatObserveModeControl1.Show();
                    this.storageObserveModeControl1.Hide();
                    this.chatLogModeControl.Hide();
                    btnStop.Hide();
                    toolStripDropDownButton.Enabled = false;
                    toolStripDropDownButton.Visible = false;
                    toolStripProgressBar.Visible = false;

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
