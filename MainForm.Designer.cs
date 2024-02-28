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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelChatDir = new System.Windows.Forms.Label();
            this.txtChatDir = new System.Windows.Forms.TextBox();
            this.btnChatDir = new System.Windows.Forms.Button();
            this.btnLoadChatLog = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboApiLimit = new System.Windows.Forms.ComboBox();
            this.labelApiLimit = new System.Windows.Forms.Label();
            this.btnFetchKakaku = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.結果をクリップボードにコピーToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.結果をCSVファイルに出力簡易ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.結果をCSVファイルに出力ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHelp = new System.Windows.Forms.Button();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.countDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eachPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // labelChatDir
            // 
            this.labelChatDir.AutoSize = true;
            this.labelChatDir.Location = new System.Drawing.Point(14, 19);
            this.labelChatDir.Name = "labelChatDir";
            this.labelChatDir.Size = new System.Drawing.Size(55, 12);
            this.labelChatDir.TabIndex = 0;
            this.labelChatDir.Text = "チャットログ";
            // 
            // txtChatDir
            // 
            this.txtChatDir.Location = new System.Drawing.Point(73, 15);
            this.txtChatDir.Name = "txtChatDir";
            this.txtChatDir.Size = new System.Drawing.Size(237, 19);
            this.txtChatDir.TabIndex = 1;
            // 
            // btnChatDir
            // 
            this.btnChatDir.Location = new System.Drawing.Point(316, 13);
            this.btnChatDir.Name = "btnChatDir";
            this.btnChatDir.Size = new System.Drawing.Size(75, 23);
            this.btnChatDir.TabIndex = 2;
            this.btnChatDir.Text = "参照";
            this.btnChatDir.UseVisualStyleBackColor = true;
            this.btnChatDir.Click += new System.EventHandler(this.btnChatDir_Click);
            // 
            // btnLoadChatLog
            // 
            this.btnLoadChatLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLoadChatLog.Location = new System.Drawing.Point(397, 13);
            this.btnLoadChatLog.Name = "btnLoadChatLog";
            this.btnLoadChatLog.Size = new System.Drawing.Size(75, 23);
            this.btnLoadChatLog.TabIndex = 3;
            this.btnLoadChatLog.Text = "ログ読込";
            this.btnLoadChatLog.UseVisualStyleBackColor = true;
            this.btnLoadChatLog.Click += new System.EventHandler(this.btnLoadChatLog_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.countDataGridViewTextBoxColumn,
            this.eachPriceDataGridViewTextBoxColumn,
            this.TotalPrice});
            this.dataGridView.DataSource = this.itemBindingSource;
            this.dataGridView.Location = new System.Drawing.Point(16, 91);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(512, 313);
            this.dataGridView.TabIndex = 4;
            // 
            // TotalPrice
            // 
            this.TotalPrice.DataPropertyName = "TotalPrice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.TotalPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalPrice.HeaderText = "合計金額";
            this.TotalPrice.Name = "TotalPrice";
            this.TotalPrice.ReadOnly = true;
            this.TotalPrice.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TotalPrice.Width = 85;
            // 
            // comboApiLimit
            // 
            this.comboApiLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboApiLimit.FormattingEnabled = true;
            this.comboApiLimit.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "5",
            "10",
            "20",
            "30"});
            this.comboApiLimit.Location = new System.Drawing.Point(16, 55);
            this.comboApiLimit.Name = "comboApiLimit";
            this.comboApiLimit.Size = new System.Drawing.Size(42, 20);
            this.comboApiLimit.TabIndex = 5;
            // 
            // labelApiLimit
            // 
            this.labelApiLimit.AutoSize = true;
            this.labelApiLimit.Location = new System.Drawing.Point(64, 58);
            this.labelApiLimit.Name = "labelApiLimit";
            this.labelApiLimit.Size = new System.Drawing.Size(253, 12);
            this.labelApiLimit.TabIndex = 6;
            this.labelApiLimit.Text = "日以内にサーバーから取得したデータは再取得しない";
            // 
            // btnFetchKakaku
            // 
            this.btnFetchKakaku.Location = new System.Drawing.Point(316, 52);
            this.btnFetchKakaku.Name = "btnFetchKakaku";
            this.btnFetchKakaku.Size = new System.Drawing.Size(212, 23);
            this.btnFetchKakaku.TabIndex = 7;
            this.btnFetchKakaku.Text = "価格取得(RO公式ツールにアクセスします)";
            this.btnFetchKakaku.UseVisualStyleBackColor = true;
            this.btnFetchKakaku.Click += new System.EventHandler(this.btnFetchKakaku_Click);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnStop.Location = new System.Drawing.Point(445, 429);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(37, 20);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "中止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
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
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripDropDownButton,
            this.toolStripProgressBar});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(542, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripDropDownButton
            // 
            this.toolStripDropDownButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem,
            this.結果をクリップボードにコピーToolStripMenuItem,
            this.結果をCSVファイルに出力簡易ToolStripMenuItem,
            this.結果をCSVファイルに出力ToolStripMenuItem});
            this.toolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton.Image")));
            this.toolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton.Name = "toolStripDropDownButton";
            this.toolStripDropDownButton.Size = new System.Drawing.Size(44, 20);
            this.toolStripDropDownButton.Text = "出力";
            // 
            // 結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem
            // 
            this.結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem.Name = "結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem";
            this.結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem.Size = new System.Drawing.Size(309, 22);
            this.結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem.Text = "結果をExcel形式でクリップボードにコピー（簡易）";
            this.結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem.Click += new System.EventHandler(this.結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem_Click);
            // 
            // 結果をクリップボードにコピーToolStripMenuItem
            // 
            this.結果をクリップボードにコピーToolStripMenuItem.Name = "結果をクリップボードにコピーToolStripMenuItem";
            this.結果をクリップボードにコピーToolStripMenuItem.Size = new System.Drawing.Size(309, 22);
            this.結果をクリップボードにコピーToolStripMenuItem.Text = "結果をExcel形式でクリップボードにコピー（Full）";
            this.結果をクリップボードにコピーToolStripMenuItem.Click += new System.EventHandler(this.結果をクリップボードにコピーToolStripMenuItem_Click);
            // 
            // 結果をCSVファイルに出力簡易ToolStripMenuItem
            // 
            this.結果をCSVファイルに出力簡易ToolStripMenuItem.Name = "結果をCSVファイルに出力簡易ToolStripMenuItem";
            this.結果をCSVファイルに出力簡易ToolStripMenuItem.Size = new System.Drawing.Size(309, 22);
            this.結果をCSVファイルに出力簡易ToolStripMenuItem.Text = "結果をCSVファイルに出力（簡易）";
            this.結果をCSVファイルに出力簡易ToolStripMenuItem.Click += new System.EventHandler(this.結果をCSVファイルに出力簡易ToolStripMenuItem_Click);
            // 
            // 結果をCSVファイルに出力ToolStripMenuItem
            // 
            this.結果をCSVファイルに出力ToolStripMenuItem.Name = "結果をCSVファイルに出力ToolStripMenuItem";
            this.結果をCSVファイルに出力ToolStripMenuItem.Size = new System.Drawing.Size(309, 22);
            this.結果をCSVファイルに出力ToolStripMenuItem.Text = "結果をCSVファイルに出力（Full）";
            this.結果をCSVファイルに出力ToolStripMenuItem.Click += new System.EventHandler(this.結果をCSVファイルに出力ToolStripMenuItem_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(479, 13);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(48, 23);
            this.btnHelp.TabIndex = 10;
            this.btnHelp.Text = "ヘルプ";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.nameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.nameDataGridViewTextBoxColumn.HeaderText = "アイテム名";
            this.nameDataGridViewTextBoxColumn.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.nameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.nameDataGridViewTextBoxColumn.TrackVisitedState = false;
            // 
            // countDataGridViewTextBoxColumn
            // 
            this.countDataGridViewTextBoxColumn.DataPropertyName = "Count";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.countDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.countDataGridViewTextBoxColumn.HeaderText = "個数";
            this.countDataGridViewTextBoxColumn.Name = "countDataGridViewTextBoxColumn";
            this.countDataGridViewTextBoxColumn.ReadOnly = true;
            this.countDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.countDataGridViewTextBoxColumn.Width = 78;
            // 
            // eachPriceDataGridViewTextBoxColumn
            // 
            this.eachPriceDataGridViewTextBoxColumn.DataPropertyName = "EachPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.eachPriceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.eachPriceDataGridViewTextBoxColumn.HeaderText = "単体価格(中央値)";
            this.eachPriceDataGridViewTextBoxColumn.Name = "eachPriceDataGridViewTextBoxColumn";
            this.eachPriceDataGridViewTextBoxColumn.ReadOnly = true;
            this.eachPriceDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eachPriceDataGridViewTextBoxColumn.Width = 85;
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(RoItemKakakuChecker.Item);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 450);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnFetchKakaku);
            this.Controls.Add(this.labelApiLimit);
            this.Controls.Add(this.comboApiLimit);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnLoadChatLog);
            this.Controls.Add(this.btnChatDir);
            this.Controls.Add(this.txtChatDir);
            this.Controls.Add(this.labelChatDir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "RO価格確認機";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelChatDir;
        private System.Windows.Forms.TextBox txtChatDir;
        private System.Windows.Forms.Button btnChatDir;
        private System.Windows.Forms.Button btnLoadChatLog;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource itemBindingSource;
        private System.Windows.Forms.ComboBox comboApiLimit;
        private System.Windows.Forms.Label labelApiLimit;
        private System.Windows.Forms.Button btnFetchKakaku;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.DataGridViewLinkColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eachPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPrice;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem 結果をCSVファイルに出力ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 結果をクリップボードにコピーToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 結果をExcel形式でクリップボードにコピー簡易ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 結果をCSVファイルに出力簡易ToolStripMenuItem;
    }
}

