using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnObserve = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFetchKakaku = new System.Windows.Forms.Button();
            this.labelApiLimit = new System.Windows.Forms.Label();
            this.comboApiLimit = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.btnHelp = new System.Windows.Forms.Button();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.countDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eachPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
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
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 460);
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
            this.btnFetchKakaku.Click += new System.EventHandler(this.btnFetchKakaku_Click);
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
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.countDataGridViewTextBoxColumn,
            this.eachPriceDataGridViewTextBoxColumn,
            this.TotalPrice,
            this.linkDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.itemBindingSource;
            this.dataGridView.Location = new System.Drawing.Point(16, 59);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(525, 393);
            this.dataGridView.TabIndex = 4;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(483, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(48, 23);
            this.btnHelp.TabIndex = 27;
            this.btnHelp.Text = "ヘルプ";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(RoItemKakakuChecker.Item);
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
            // linkDataGridViewTextBoxColumn
            // 
            this.linkDataGridViewTextBoxColumn.DataPropertyName = "Link";
            this.linkDataGridViewTextBoxColumn.FillWeight = 55F;
            this.linkDataGridViewTextBoxColumn.HeaderText = "Link";
            this.linkDataGridViewTextBoxColumn.Name = "linkDataGridViewTextBoxColumn";
            this.linkDataGridViewTextBoxColumn.ReadOnly = true;
            this.linkDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.linkDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.linkDataGridViewTextBoxColumn.Width = 55;
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
            this.Size = new System.Drawing.Size(555, 482);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
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
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.BindingSource itemBindingSource;
        private DataGridViewLinkColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn eachPriceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn TotalPrice;
        private DataGridViewLinkColumn linkDataGridViewTextBoxColumn;
    }
}
