namespace Hesap.Forms.Kartlar
{
    partial class FrmDepoKarti
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDepoKarti));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnYeni = new DevExpress.XtraEditors.SimpleButton();
            this.btnIleri = new DevExpress.XtraEditors.SimpleButton();
            this.btnGeri = new DevExpress.XtraEditors.SimpleButton();
            this.btnSil = new DevExpress.XtraEditors.SimpleButton();
            this.btnListe = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.chckKullanimda = new DevExpress.XtraEditors.CheckEdit();
            this.txtDepoAdi = new DevExpress.XtraEditors.TextEdit();
            this.txtDepoKodu = new DevExpress.XtraEditors.TextEdit();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.kayıtBilgisiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chckKullanimda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepoAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepoKodu.Properties)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnYeni);
            this.panel3.Controls.Add(this.btnIleri);
            this.panel3.Controls.Add(this.btnGeri);
            this.panel3.Controls.Add(this.btnSil);
            this.panel3.Controls.Add(this.btnListe);
            this.panel3.Controls.Add(this.btnKaydet);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1099, 53);
            this.panel3.TabIndex = 4;
            // 
            // btnYeni
            // 
            this.btnYeni.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnYeni.ImageOptions.Image")));
            this.btnYeni.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnYeni.Location = new System.Drawing.Point(3, 3);
            this.btnYeni.Name = "btnYeni";
            this.btnYeni.Size = new System.Drawing.Size(75, 47);
            this.btnYeni.TabIndex = 100;
            this.btnYeni.Text = "Yeni";
            this.btnYeni.Click += new System.EventHandler(this.btnYeni_Click);
            // 
            // btnIleri
            // 
            this.btnIleri.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnIleri.ImageOptions.Image")));
            this.btnIleri.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnIleri.Location = new System.Drawing.Point(325, 3);
            this.btnIleri.Name = "btnIleri";
            this.btnIleri.Size = new System.Drawing.Size(75, 47);
            this.btnIleri.TabIndex = 102;
            this.btnIleri.Text = "İleri";
            this.btnIleri.Click += new System.EventHandler(this.btnIleri_Click);
            // 
            // btnGeri
            // 
            this.btnGeri.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGeri.ImageOptions.Image")));
            this.btnGeri.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnGeri.Location = new System.Drawing.Point(244, 3);
            this.btnGeri.Name = "btnGeri";
            this.btnGeri.Size = new System.Drawing.Size(75, 47);
            this.btnGeri.TabIndex = 102;
            this.btnGeri.Text = "Geri";
            this.btnGeri.Click += new System.EventHandler(this.btnGeri_Click);
            // 
            // btnSil
            // 
            this.btnSil.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSil.ImageOptions.Image")));
            this.btnSil.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnSil.Location = new System.Drawing.Point(406, 3);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(75, 47);
            this.btnSil.TabIndex = 102;
            this.btnSil.Text = "Sil";
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnListe
            // 
            this.btnListe.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnListe.ImageOptions.Image")));
            this.btnListe.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnListe.Location = new System.Drawing.Point(163, 3);
            this.btnListe.Name = "btnListe";
            this.btnListe.Size = new System.Drawing.Size(75, 47);
            this.btnListe.TabIndex = 102;
            this.btnListe.Text = "Liste";
            this.btnListe.Click += new System.EventHandler(this.btnListe_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnKaydet.ImageOptions.Image")));
            this.btnKaydet.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnKaydet.Location = new System.Drawing.Point(84, 3);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 47);
            this.btnKaydet.TabIndex = 101;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(14, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 21);
            this.label1.TabIndex = 1127;
            this.label1.Text = "Depo Adı";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.Transparent;
            this.label72.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label72.Location = new System.Drawing.Point(14, 60);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(93, 21);
            this.label72.TabIndex = 1128;
            this.label72.Text = "Depo Kodu:";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chckKullanimda
            // 
            this.chckKullanimda.Location = new System.Drawing.Point(113, 109);
            this.chckKullanimda.Name = "chckKullanimda";
            this.chckKullanimda.Properties.Caption = "Kullanımda";
            this.chckKullanimda.Size = new System.Drawing.Size(75, 20);
            this.chckKullanimda.TabIndex = 1129;
            // 
            // txtDepoAdi
            // 
            this.txtDepoAdi.Location = new System.Drawing.Point(113, 83);
            this.txtDepoAdi.Name = "txtDepoAdi";
            this.txtDepoAdi.Size = new System.Drawing.Size(206, 22);
            this.txtDepoAdi.TabIndex = 1120;
            // 
            // txtDepoKodu
            // 
            this.txtDepoKodu.Location = new System.Drawing.Point(113, 59);
            this.txtDepoKodu.Name = "txtDepoKodu";
            this.txtDepoKodu.Size = new System.Drawing.Size(206, 22);
            this.txtDepoKodu.TabIndex = 1119;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Corbel", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kayıtBilgisiToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48);
            //this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // kayıtBilgisiToolStripMenuItem
            // 
            this.kayıtBilgisiToolStripMenuItem.Name = "kayıtBilgisiToolStripMenuItem";
            this.kayıtBilgisiToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.kayıtBilgisiToolStripMenuItem.Text = "Kayıt Bilgisi";
            this.kayıtBilgisiToolStripMenuItem.Click += new System.EventHandler(this.kayıtBilgisiToolStripMenuItem_Click);
            // 
            // FrmDepoKarti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 504);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.chckKullanimda);
            this.Controls.Add(this.txtDepoAdi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDepoKodu);
            this.Controls.Add(this.label72);
            this.Controls.Add(this.panel3);
            this.Name = "FrmDepoKarti";
            this.Text = "Depo Kartı";
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chckKullanimda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepoAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepoKodu.Properties)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton btnYeni;
        private DevExpress.XtraEditors.SimpleButton btnIleri;
        private DevExpress.XtraEditors.SimpleButton btnGeri;
        private DevExpress.XtraEditors.SimpleButton btnSil;
        private DevExpress.XtraEditors.SimpleButton btnListe;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.TextEdit txtDepoAdi;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtDepoKodu;
        private System.Windows.Forms.Label label72;
        private DevExpress.XtraEditors.CheckEdit chckKullanimda;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem kayıtBilgisiToolStripMenuItem;
    }
}