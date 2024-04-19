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
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.labelYomiage = new System.Windows.Forms.Label();
            this.checkBoxPublic = new System.Windows.Forms.CheckBox();
            this.checkBoxParty = new System.Windows.Forms.CheckBox();
            this.checkBoxGuild = new System.Windows.Forms.CheckBox();
            this.checkBoxWhisper = new System.Windows.Forms.CheckBox();
            this.checkBoxWord = new System.Windows.Forms.CheckBox();
            this.textBoxWord = new System.Windows.Forms.TextBox();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonYomiageSetting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chatLogEntityBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnObserveChat
            // 
            this.btnObserveChat.Location = new System.Drawing.Point(20, 9);
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
            this.dataGridView.Location = new System.Drawing.Point(20, 38);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.Size = new System.Drawing.Size(511, 436);
            this.dataGridView.TabIndex = 1;
            // 
            // dateTimeStrDataGridViewTextBoxColumn
            // 
            this.dateTimeStrDataGridViewTextBoxColumn.DataPropertyName = "DateTimeStr";
            this.dateTimeStrDataGridViewTextBoxColumn.FillWeight = 120F;
            this.dateTimeStrDataGridViewTextBoxColumn.HeaderText = "時刻";
            this.dateTimeStrDataGridViewTextBoxColumn.Name = "dateTimeStrDataGridViewTextBoxColumn";
            this.dateTimeStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.dateTimeStrDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dateTimeStrDataGridViewTextBoxColumn.Width = 120;
            // 
            // messageTypeDataGridViewTextBoxColumn
            // 
            this.messageTypeDataGridViewTextBoxColumn.DataPropertyName = "MessageType";
            this.messageTypeDataGridViewTextBoxColumn.FillWeight = 55F;
            this.messageTypeDataGridViewTextBoxColumn.HeaderText = "種別";
            this.messageTypeDataGridViewTextBoxColumn.Name = "messageTypeDataGridViewTextBoxColumn";
            this.messageTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.messageTypeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.messageTypeDataGridViewTextBoxColumn.Width = 55;
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
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(456, 9);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 2;
            this.btnHelp.Text = "ヘルプ";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(133, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "表示のクリア";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // labelYomiage
            // 
            this.labelYomiage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelYomiage.AutoSize = true;
            this.labelYomiage.Location = new System.Drawing.Point(20, 483);
            this.labelYomiage.Name = "labelYomiage";
            this.labelYomiage.Size = new System.Drawing.Size(50, 12);
            this.labelYomiage.TabIndex = 4;
            this.labelYomiage.Text = "読み上げ";
            // 
            // checkBoxPublic
            // 
            this.checkBoxPublic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxPublic.AutoSize = true;
            this.checkBoxPublic.Location = new System.Drawing.Point(76, 482);
            this.checkBoxPublic.Name = "checkBoxPublic";
            this.checkBoxPublic.Size = new System.Drawing.Size(55, 16);
            this.checkBoxPublic.TabIndex = 5;
            this.checkBoxPublic.Text = "Public";
            this.checkBoxPublic.UseVisualStyleBackColor = true;
            // 
            // checkBoxParty
            // 
            this.checkBoxParty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxParty.AutoSize = true;
            this.checkBoxParty.Location = new System.Drawing.Point(137, 482);
            this.checkBoxParty.Name = "checkBoxParty";
            this.checkBoxParty.Size = new System.Drawing.Size(51, 16);
            this.checkBoxParty.TabIndex = 6;
            this.checkBoxParty.Text = "Party";
            this.checkBoxParty.UseVisualStyleBackColor = true;
            // 
            // checkBoxGuild
            // 
            this.checkBoxGuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxGuild.AutoSize = true;
            this.checkBoxGuild.Location = new System.Drawing.Point(194, 482);
            this.checkBoxGuild.Name = "checkBoxGuild";
            this.checkBoxGuild.Size = new System.Drawing.Size(50, 16);
            this.checkBoxGuild.TabIndex = 7;
            this.checkBoxGuild.Text = "Guild";
            this.checkBoxGuild.UseVisualStyleBackColor = true;
            // 
            // checkBoxWhisper
            // 
            this.checkBoxWhisper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxWhisper.AutoSize = true;
            this.checkBoxWhisper.Location = new System.Drawing.Point(250, 482);
            this.checkBoxWhisper.Name = "checkBoxWhisper";
            this.checkBoxWhisper.Size = new System.Drawing.Size(64, 16);
            this.checkBoxWhisper.TabIndex = 8;
            this.checkBoxWhisper.Text = "Whisper";
            this.checkBoxWhisper.UseVisualStyleBackColor = true;
            // 
            // checkBoxWord
            // 
            this.checkBoxWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxWord.AutoSize = true;
            this.checkBoxWord.Location = new System.Drawing.Point(320, 482);
            this.checkBoxWord.Name = "checkBoxWord";
            this.checkBoxWord.Size = new System.Drawing.Size(76, 16);
            this.checkBoxWord.TabIndex = 9;
            this.checkBoxWord.Text = "指定ワード";
            this.checkBoxWord.UseVisualStyleBackColor = true;
            // 
            // textBoxWord
            // 
            this.textBoxWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWord.Location = new System.Drawing.Point(402, 480);
            this.textBoxWord.Name = "textBoxWord";
            this.textBoxWord.Size = new System.Drawing.Size(129, 19);
            this.textBoxWord.TabIndex = 10;
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(RoItemKakakuChecker.Item);
            // 
            // buttonYomiageSetting
            // 
            this.buttonYomiageSetting.Location = new System.Drawing.Point(375, 9);
            this.buttonYomiageSetting.Name = "buttonYomiageSetting";
            this.buttonYomiageSetting.Size = new System.Drawing.Size(75, 23);
            this.buttonYomiageSetting.TabIndex = 11;
            this.buttonYomiageSetting.Text = "読上設定";
            this.buttonYomiageSetting.UseVisualStyleBackColor = true;
            this.buttonYomiageSetting.Click += new System.EventHandler(this.buttonYomiageSetting_Click);
            // 
            // ChatObserveModeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonYomiageSetting);
            this.Controls.Add(this.textBoxWord);
            this.Controls.Add(this.checkBoxWord);
            this.Controls.Add(this.checkBoxWhisper);
            this.Controls.Add(this.checkBoxGuild);
            this.Controls.Add(this.checkBoxParty);
            this.Controls.Add(this.checkBoxPublic);
            this.Controls.Add(this.labelYomiage);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnObserveChat);
            this.Name = "ChatObserveModeControl";
            this.Size = new System.Drawing.Size(547, 502);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chatLogEntityBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnObserveChat;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource chatLogEntityBindingSource;
        private System.Windows.Forms.BindingSource itemBindingSource;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateTimeStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label labelYomiage;
        private System.Windows.Forms.CheckBox checkBoxPublic;
        private System.Windows.Forms.CheckBox checkBoxParty;
        private System.Windows.Forms.CheckBox checkBoxGuild;
        private System.Windows.Forms.CheckBox checkBoxWhisper;
        private System.Windows.Forms.CheckBox checkBoxWord;
        private System.Windows.Forms.TextBox textBoxWord;
        private System.Windows.Forms.Button buttonYomiageSetting;
    }
}
