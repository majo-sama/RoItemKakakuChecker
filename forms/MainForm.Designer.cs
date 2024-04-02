namespace RoItemKakakuChecker
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStop = new System.Windows.Forms.Button();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.radioButton_chatLog = new System.Windows.Forms.RadioButton();
            this.radioButton_storage = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.結果をCSVファイルに出力_簡易 = new System.Windows.Forms.ToolStripMenuItem();
            this.結果をCSVファイルに出力_通常 = new System.Windows.Forms.ToolStripMenuItem();
            this.storageObserveModeControl1 = new RoItemKakakuChecker.StorageObserveModeControl(this);
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnStop.Location = new System.Drawing.Point(493, 462);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(37, 20);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "中止";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.toolStripStatusLabel.Size = new System.Drawing.Size(138, 17);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Margin = new System.Windows.Forms.Padding(1, 3, 45, 3);
            this.toolStripProgressBar.MergeIndex = -2;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar,
            this.toolStripDropDownButton});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(542, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDropDownButton
            // 
            this.toolStripDropDownButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.結果をCSVファイルに出力_通常,
            this.結果をCSVファイルに出力_簡易});
            this.toolStripDropDownButton.Margin = new System.Windows.Forms.Padding(0, 2, 10, 0);
            this.toolStripDropDownButton.Name = "toolStripDropDownButton";
            this.toolStripDropDownButton.Size = new System.Drawing.Size(44, 20);
            this.toolStripDropDownButton.Text = "出力";
            this.toolStripDropDownButton.Click += new System.EventHandler(this.toolStripDropDownButton_Click);
            // 
            // radioButton_chatLog
            // 
            this.radioButton_chatLog.AutoSize = true;
            this.radioButton_chatLog.Location = new System.Drawing.Point(23, 15);
            this.radioButton_chatLog.Name = "radioButton_chatLog";
            this.radioButton_chatLog.Size = new System.Drawing.Size(115, 16);
            this.radioButton_chatLog.TabIndex = 12;
            this.radioButton_chatLog.TabStop = true;
            this.radioButton_chatLog.Text = "チャットログから確認";
            this.radioButton_chatLog.UseVisualStyleBackColor = true;
            // 
            // radioButton_storage
            // 
            this.radioButton_storage.AutoSize = true;
            this.radioButton_storage.Location = new System.Drawing.Point(146, 15);
            this.radioButton_storage.Name = "radioButton_storage";
            this.radioButton_storage.Size = new System.Drawing.Size(71, 16);
            this.radioButton_storage.TabIndex = 13;
            this.radioButton_storage.TabStop = true;
            this.radioButton_storage.Text = "倉庫監視";
            this.radioButton_storage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_chatLog);
            this.groupBox1.Controls.Add(this.radioButton_storage);
            this.groupBox1.Location = new System.Drawing.Point(18, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 38);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "モード選択";
            // 
            // storageObserveModeControl1
            // 
            this.storageObserveModeControl1.Location = new System.Drawing.Point(0, 52);
            this.storageObserveModeControl1.Name = "storageObserveModeControl1";
            this.storageObserveModeControl1.Size = new System.Drawing.Size(542, 412);
            this.storageObserveModeControl1.TabIndex = 15;
            // 
            // 結果をCSVファイルに出力_簡易
            // 
            this.結果をCSVファイルに出力_簡易.Name = "結果をCSVファイルに出力_簡易";
            this.結果をCSVファイルに出力_簡易.Size = new System.Drawing.Size(218, 22);
            this.結果をCSVファイルに出力_簡易.Text = "結果をCSVファイルに出力(簡易)";
            // 
            // 結果をCSVファイルに出力_通常
            // 
            this.結果をCSVファイルに出力_通常.Name = "結果をCSVファイルに出力_通常";
            this.結果をCSVファイルに出力_通常.Size = new System.Drawing.Size(32, 19);
            this.結果をCSVファイルに出力_通常.Text = "結果をCSVファイルに出力";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 482);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "RO価格確認機";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem 結果をCSVファイルに出力ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 結果をクリップボードにコピーToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 結果をCSVファイルに出力簡易ToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButton_chatLog;
        private System.Windows.Forms.RadioButton radioButton_storage;
        private System.Windows.Forms.GroupBox groupBox1;
        private ChatLogModeControl chatLogModeControl;
        private StorageObserveModeControl storageObserveModeControl1;
        private System.Windows.Forms.ToolStripMenuItem 結果をCSVファイルに出力_簡易;
        private System.Windows.Forms.ToolStripMenuItem 結果をCSVファイルに出力_通常;
    }
}

