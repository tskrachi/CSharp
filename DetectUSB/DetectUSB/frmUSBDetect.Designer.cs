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
            this.lvwUsbDevice = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // txtUSBInfo
            // 
            this.txtUSBInfo.Location = new System.Drawing.Point(12, 185);
            this.txtUSBInfo.Multiline = true;
            this.txtUSBInfo.Name = "txtUSBInfo";
            this.txtUSBInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUSBInfo.Size = new System.Drawing.Size(469, 79);
            this.txtUSBInfo.TabIndex = 0;
            // 
            // lvwUsbDevice
            // 
            this.lvwUsbDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwUsbDevice.Location = new System.Drawing.Point(12, 12);
            this.lvwUsbDevice.MultiSelect = false;
            this.lvwUsbDevice.Name = "lvwUsbDevice";
            this.lvwUsbDevice.Size = new System.Drawing.Size(469, 167);
            this.lvwUsbDevice.TabIndex = 1;
            this.lvwUsbDevice.UseCompatibleStateImageBehavior = false;
            this.lvwUsbDevice.View = System.Windows.Forms.View.Details;
            // 
            // frmUSBDetect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 276);
            this.Controls.Add(this.lvwUsbDevice);
            this.Controls.Add(this.txtUSBInfo);
            this.Name = "frmUSBDetect";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUSBInfo;
        private System.Windows.Forms.ListView lvwUsbDevice;
    }
}

