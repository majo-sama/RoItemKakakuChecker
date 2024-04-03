using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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



        public MainForm()
        {
            settings = new AppSettings();

            InitializeComponent();
            //this.TopMost = true;
            this.SizeGripStyle = SizeGripStyle.Show;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
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
                }
                else if (radio.Name == "radioButton_storage")
                {
                    this.storageObserveModeControl1.Show();
                    this.chatLogModeControl.Hide();
                    this.chatObserveModeControl1.Hide();
                }
                else
                {
                    this.chatObserveModeControl1.Show();
                    this.storageObserveModeControl1.Hide();
                    this.chatLogModeControl.Hide();
                }
            }
            return;
        }



        public void UpdateToolStripLabel(string text)
        {
            toolStripStatusLabel.Text = text;
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
    }

}
