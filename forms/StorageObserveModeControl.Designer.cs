namespace RoItemKakakuChecker
{
    partial class StorageObserveModeControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnObserve = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFetchKakaku = new System.Windows.Forms.Button();
            this.labelApiLimit = new System.Windows.Forms.Label();
            this.comboApiLimit = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnObserve
            // 
            this.btnObserve.Location = new System.Drawing.Point(16, 3);
            this.btnObserve.Name = "btnObserve";
            this.btnObserve.Size = new System.Drawing.Size(101, 23);
            this.btnObserve.TabIndex = 0;
            this.btnObserve.Text = "倉庫監視 開始";
            this.btnObserve.UseVisualStyleBackColor = true;
            this.btnObserve.Click += new System.EventHandler(this.btnObserve_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 414);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "装備品はカード・エンチャントを考慮しない価格を表示しています。実際の値は公式ツールを確認してください。";
            // 
            // btnFetchKakaku
            // 
            this.btnFetchKakaku.Location = new System.Drawing.Point(319, 30);
            this.btnFetchKakaku.Name = "btnFetchKakaku";
            this.btnFetchKakaku.Size = new System.Drawing.Size(212, 23);
            this.btnFetchKakaku.TabIndex = 25;
            this.btnFetchKakaku.Text = "価格取得(RO公式ツールにアクセスします)";
            this.btnFetchKakaku.UseVisualStyleBackColor = true;
            // 
            // labelApiLimit
            // 
            this.labelApiLimit.AutoSize = true;
            this.labelApiLimit.Location = new System.Drawing.Point(64, 35);
            this.labelApiLimit.Name = "labelApiLimit";
            this.labelApiLimit.Size = new System.Drawing.Size(253, 12);
            this.labelApiLimit.TabIndex = 24;
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
            this.comboApiLimit.TabIndex = 23;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TotalPrice});
            this.dataGridView.Location = new System.Drawing.Point(16, 59);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(512, 345);
            this.dataGridView.TabIndex = 22;
            // 
            // TotalPrice
            // 
            this.TotalPrice.DataPropertyName = "TotalPrice";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N0";
            dataGridViewCellStyle14.NullValue = null;
            this.TotalPrice.DefaultCellStyle = dataGridViewCellStyle14;
            this.TotalPrice.HeaderText = "合計金額";
            this.TotalPrice.Name = "TotalPrice";
            this.TotalPrice.ReadOnly = true;
            this.TotalPrice.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TotalPrice.Width = 85;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(483, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(48, 23);
            this.btnHelp.TabIndex = 27;
            this.btnHelp.Text = "ヘルプ";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // StorageObserveModeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFetchKakaku);
            this.Controls.Add(this.labelApiLimit);
            this.Controls.Add(this.comboApiLimit);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnObserve);
            this.Name = "StorageObserveModeControl";
            this.Size = new System.Drawing.Size(542, 438);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnObserve;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFetchKakaku;
        private System.Windows.Forms.Label labelApiLimit;
        private System.Windows.Forms.ComboBox comboApiLimit;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPrice;
        private System.Windows.Forms.Button btnHelp;
    }
}
