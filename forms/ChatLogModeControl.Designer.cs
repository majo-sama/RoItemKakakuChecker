namespace RoItemKakakuChecker
{
    partial class ChatLogModeControl
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnFetchKakaku = new System.Windows.Forms.Button();
            this.labelApiLimit = new System.Windows.Forms.Label();
            this.comboApiLimit = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.countDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eachPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnLoadChatLog = new System.Windows.Forms.Button();
            this.btnChatDir = new System.Windows.Forms.Button();
            this.txtChatDir = new System.Windows.Forms.TextBox();
            this.labelChatDir = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 414);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "装備品はカード・エンチャントを考慮しない価格を表示しています。実際の値は公式ツールを確認してください。";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(481, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(48, 23);
            this.btnHelp.TabIndex = 20;
            this.btnHelp.Text = "ヘルプ";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnFetchKakaku
            // 
            this.btnFetchKakaku.Location = new System.Drawing.Point(319, 30);
            this.btnFetchKakaku.Name = "btnFetchKakaku";
            this.btnFetchKakaku.Size = new System.Drawing.Size(212, 23);
            this.btnFetchKakaku.TabIndex = 19;
            this.btnFetchKakaku.Text = "価格取得(RO公式ツールにアクセスします)";
            this.btnFetchKakaku.UseVisualStyleBackColor = true;
            this.btnFetchKakaku.Click += new System.EventHandler(this.btnFetchKakaku_Click);
            // 
            // labelApiLimit
            // 
            this.labelApiLimit.AutoSize = true;
            this.labelApiLimit.Location = new System.Drawing.Point(64, 35);
            this.labelApiLimit.Name = "labelApiLimit";
            this.labelApiLimit.Size = new System.Drawing.Size(253, 12);
            this.labelApiLimit.TabIndex = 18;
            this.labelApiLimit.Text = "日以内にサーバーから取得したデータは再取得しない";
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
            this.comboApiLimit.Location = new System.Drawing.Point(16, 32);
            this.comboApiLimit.Name = "comboApiLimit";
            this.comboApiLimit.Size = new System.Drawing.Size(42, 20);
            this.comboApiLimit.TabIndex = 17;
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
            this.dataGridView.Location = new System.Drawing.Point(16, 59);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(512, 345);
            this.dataGridView.TabIndex = 4;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.nameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle17;
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
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "N0";
            dataGridViewCellStyle18.NullValue = null;
            this.countDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle18;
            this.countDataGridViewTextBoxColumn.HeaderText = "個数";
            this.countDataGridViewTextBoxColumn.Name = "countDataGridViewTextBoxColumn";
            this.countDataGridViewTextBoxColumn.ReadOnly = true;
            this.countDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.countDataGridViewTextBoxColumn.Width = 78;
            // 
            // eachPriceDataGridViewTextBoxColumn
            // 
            this.eachPriceDataGridViewTextBoxColumn.DataPropertyName = "EachPrice";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle19.Format = "N0";
            dataGridViewCellStyle19.NullValue = null;
            this.eachPriceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle19;
            this.eachPriceDataGridViewTextBoxColumn.HeaderText = "単体価格(中央値)";
            this.eachPriceDataGridViewTextBoxColumn.Name = "eachPriceDataGridViewTextBoxColumn";
            this.eachPriceDataGridViewTextBoxColumn.ReadOnly = true;
            this.eachPriceDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eachPriceDataGridViewTextBoxColumn.Width = 85;
            // 
            // TotalPrice
            // 
            this.TotalPrice.DataPropertyName = "TotalPrice";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle20.Format = "N0";
            dataGridViewCellStyle20.NullValue = null;
            this.TotalPrice.DefaultCellStyle = dataGridViewCellStyle20;
            this.TotalPrice.HeaderText = "合計金額";
            this.TotalPrice.Name = "TotalPrice";
            this.TotalPrice.ReadOnly = true;
            this.TotalPrice.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TotalPrice.Width = 85;
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(RoItemKakakuChecker.Item);
            // 
            // btnLoadChatLog
            // 
            this.btnLoadChatLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLoadChatLog.Location = new System.Drawing.Point(400, 3);
            this.btnLoadChatLog.Name = "btnLoadChatLog";
            this.btnLoadChatLog.Size = new System.Drawing.Size(75, 23);
            this.btnLoadChatLog.TabIndex = 15;
            this.btnLoadChatLog.Text = "ログ読込";
            this.btnLoadChatLog.UseVisualStyleBackColor = true;
            this.btnLoadChatLog.Click += new System.EventHandler(this.btnLoadChatLog_Click);
            // 
            // btnChatDir
            // 
            this.btnChatDir.Location = new System.Drawing.Point(319, 3);
            this.btnChatDir.Name = "btnChatDir";
            this.btnChatDir.Size = new System.Drawing.Size(75, 23);
            this.btnChatDir.TabIndex = 14;
            this.btnChatDir.Text = "参照";
            this.btnChatDir.UseVisualStyleBackColor = true;
            this.btnChatDir.Click += new System.EventHandler(this.btnChatDir_Click);
            // 
            // txtChatDir
            // 
            this.txtChatDir.Location = new System.Drawing.Point(76, 3);
            this.txtChatDir.Name = "txtChatDir";
            this.txtChatDir.Size = new System.Drawing.Size(237, 19);
            this.txtChatDir.TabIndex = 13;
            // 
            // labelChatDir
            // 
            this.labelChatDir.AutoSize = true;
            this.labelChatDir.Location = new System.Drawing.Point(14, 6);
            this.labelChatDir.Name = "labelChatDir";
            this.labelChatDir.Size = new System.Drawing.Size(55, 12);
            this.labelChatDir.TabIndex = 12;
            this.labelChatDir.Text = "チャットログ";
            // 
            // ChatLogModeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnFetchKakaku);
            this.Controls.Add(this.labelApiLimit);
            this.Controls.Add(this.comboApiLimit);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnLoadChatLog);
            this.Controls.Add(this.btnChatDir);
            this.Controls.Add(this.txtChatDir);
            this.Controls.Add(this.labelChatDir);
            this.Name = "ChatLogModeControl";
            this.Size = new System.Drawing.Size(542, 438);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnFetchKakaku;
        private System.Windows.Forms.Label labelApiLimit;
        private System.Windows.Forms.ComboBox comboApiLimit;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPrice;
        private System.Windows.Forms.DataGridViewLinkColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eachPriceDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnLoadChatLog;
        private System.Windows.Forms.Button btnChatDir;
        private System.Windows.Forms.TextBox txtChatDir;
        private System.Windows.Forms.Label labelChatDir;
        private System.Windows.Forms.BindingSource itemBindingSource;

    }
}
