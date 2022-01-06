namespace DetectUSB
{
    partial class frmUSBDetect
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.txtUSBInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtUSBInfo
            // 
            this.txtUSBInfo.Location = new System.Drawing.Point(12, 80);
            this.txtUSBInfo.Multiline = true;
            this.txtUSBInfo.Name = "txtUSBInfo";
            this.txtUSBInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUSBInfo.Size = new System.Drawing.Size(469, 79);
            this.txtUSBInfo.TabIndex = 0;
            // 
            // frmUSBDetect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 213);
            this.Controls.Add(this.txtUSBInfo);
            this.Name = "frmUSBDetect";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUSBInfo;
    }
}

