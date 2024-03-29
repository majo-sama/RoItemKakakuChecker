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
            this.btnObserve = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnObserve
            // 
            this.btnObserve.Location = new System.Drawing.Point(56, 57);
            this.btnObserve.Name = "btnObserve";
            this.btnObserve.Size = new System.Drawing.Size(75, 23);
            this.btnObserve.TabIndex = 0;
            this.btnObserve.Text = "button1";
            this.btnObserve.UseVisualStyleBackColor = true;
            this.btnObserve.Click += new System.EventHandler(this.btnObserve_Click);
            // 
            // StorageObserveModeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnObserve);
            this.Name = "StorageObserveModeControl";
            this.Size = new System.Drawing.Size(512, 508);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnObserve;
    }
}
