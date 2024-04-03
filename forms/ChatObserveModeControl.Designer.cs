namespace RoItemKakakuChecker.forms
{
    partial class ChatObserveModeControl
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
            this.btnObserveChat = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dateTimeStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chatLogEntityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chatLogEntityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnObserveChat
            // 
            this.btnObserveChat.Location = new System.Drawing.Point(20, 20);
            this.btnObserveChat.Name = "btnObserveChat";
            this.btnObserveChat.Size = new System.Drawing.Size(107, 23);
            this.btnObserveChat.TabIndex = 0;
            this.btnObserveChat.Text = "チャット監視 開始";
            this.btnObserveChat.UseVisualStyleBackColor = true;
            this.btnObserveChat.Click += new System.EventHandler(this.btnObserveChat_Click);
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
            this.dateTimeStrDataGridViewTextBoxColumn,
            this.messageTypeDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.chatLogEntityBindingSource;
            this.dataGridView.Location = new System.Drawing.Point(20, 49);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(511, 434);
            this.dataGridView.TabIndex = 1;
            // 
            // dateTimeStrDataGridViewTextBoxColumn
            // 
            this.dateTimeStrDataGridViewTextBoxColumn.DataPropertyName = "DateTimeStr";
            this.dateTimeStrDataGridViewTextBoxColumn.HeaderText = "時刻";
            this.dateTimeStrDataGridViewTextBoxColumn.Name = "dateTimeStrDataGridViewTextBoxColumn";
            this.dateTimeStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.dateTimeStrDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // messageTypeDataGridViewTextBoxColumn
            // 
            this.messageTypeDataGridViewTextBoxColumn.DataPropertyName = "MessageType";
            this.messageTypeDataGridViewTextBoxColumn.HeaderText = "種別";
            this.messageTypeDataGridViewTextBoxColumn.Name = "messageTypeDataGridViewTextBoxColumn";
            this.messageTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.messageTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "メッセージ";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // chatLogEntityBindingSource
            // 
            this.chatLogEntityBindingSource.DataSource = typeof(RoItemKakakuChecker.ChatLogEntity);
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(RoItemKakakuChecker.Item);
            // 
            // ChatObserveModeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnObserveChat);
            this.Name = "ChatObserveModeControl";
            this.Size = new System.Drawing.Size(547, 502);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chatLogEntityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnObserveChat;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource chatLogEntityBindingSource;
        private System.Windows.Forms.BindingSource itemBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateTimeStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
    }
}
