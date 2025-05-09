namespace Hesap.Forms.Diger
{
    partial class FrmSaveInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblKayitTarihi = new DevExpress.XtraEditors.LabelControl();
            this.lblKayitEden = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.lblGuncellemeTarihi = new DevExpress.XtraEditors.LabelControl();
            this.lblGuncelleyen = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblKayitTarihi);
            this.groupControl1.Controls.Add(this.lblKayitEden);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(203, 84);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Kaydeden";
            // 
            // lblKayitTarihi
            // 
            this.lblKayitTarihi.Location = new System.Drawing.Point(16, 56);
            this.lblKayitTarihi.Name = "lblKayitTarihi";
            this.lblKayitTarihi.Size = new System.Drawing.Size(61, 13);
            this.lblKayitTarihi.TabIndex = 0;
            this.lblKayitTarihi.Text = "labelControl1";
            // 
            // lblKayitEden
            // 
            this.lblKayitEden.Location = new System.Drawing.Point(16, 37);
            this.lblKayitEden.Name = "lblKayitEden";
            this.lblKayitEden.Size = new System.Drawing.Size(61, 13);
            this.lblKayitEden.TabIndex = 0;
            this.lblKayitEden.Text = "labelControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.lblGuncellemeTarihi);
            this.groupControl2.Controls.Add(this.lblGuncelleyen);
            this.groupControl2.Location = new System.Drawing.Point(221, 12);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(203, 84);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Güncelleyen";
            // 
            // lblGuncellemeTarihi
            // 
            this.lblGuncellemeTarihi.Location = new System.Drawing.Point(16, 56);
            this.lblGuncellemeTarihi.Name = "lblGuncellemeTarihi";
            this.lblGuncellemeTarihi.Size = new System.Drawing.Size(61, 13);
            this.lblGuncellemeTarihi.TabIndex = 0;
            this.lblGuncellemeTarihi.Text = "labelControl1";
            // 
            // lblGuncelleyen
            // 
            this.lblGuncelleyen.Location = new System.Drawing.Point(16, 37);
            this.lblGuncelleyen.Name = "lblGuncelleyen";
            this.lblGuncelleyen.Size = new System.Drawing.Size(61, 13);
            this.lblGuncelleyen.TabIndex = 0;
            this.lblGuncelleyen.Text = "labelControl1";
            // 
            // FrmSaveInfo
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 110);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.IconOptions.Image = global::Hesap.Properties.Resources.AppIcon;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSaveInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kayıt Bilgisi";
            this.Load += new System.EventHandler(this.FrmSaveInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblKayitTarihi;
        private DevExpress.XtraEditors.LabelControl lblKayitEden;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl lblGuncellemeTarihi;
        private DevExpress.XtraEditors.LabelControl lblGuncelleyen;
    }
}