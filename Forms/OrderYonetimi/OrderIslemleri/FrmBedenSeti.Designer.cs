namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    partial class FrmBedenSeti
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
            this.lstBedenler = new DevExpress.XtraEditors.ListBoxControl();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.txtBedenSeti = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lstBedenler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBedenSeti.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lstBedenler
            // 
            this.lstBedenler.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstBedenler.Location = new System.Drawing.Point(0, 0);
            this.lstBedenler.Name = "lstBedenler";
            this.lstBedenler.Size = new System.Drawing.Size(209, 264);
            this.lstBedenler.TabIndex = 0;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(115, 270);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(86, 22);
            this.btnKaydet.TabIndex = 1;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // txtBedenSeti
            // 
            this.txtBedenSeti.Location = new System.Drawing.Point(9, 270);
            this.txtBedenSeti.Name = "txtBedenSeti";
            this.txtBedenSeti.Size = new System.Drawing.Size(100, 22);
            this.txtBedenSeti.TabIndex = 2;
            this.txtBedenSeti.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBedenSeti_KeyPress);
            // 
            // FrmBedenSeti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 297);
            this.Controls.Add(this.txtBedenSeti);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.lstBedenler);
            this.IconOptions.Image = global::Hesap.Properties.Resources.AppIcon;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(211, 327);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(211, 327);
            this.Name = "FrmBedenSeti";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bedenler";
            ((System.ComponentModel.ISupportInitialize)(this.lstBedenler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBedenSeti.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lstBedenler;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.TextEdit txtBedenSeti;
    }
}