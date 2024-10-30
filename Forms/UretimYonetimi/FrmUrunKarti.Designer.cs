namespace Hesap.Forms.UretimYonetimi
{
    partial class FrmUrunKarti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUrunKarti));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.txtUrunKodu = new DevExpress.XtraEditors.ButtonEdit();
            this.chckPasif = new DevExpress.XtraEditors.CheckEdit();
            this.txtUrunAdi = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.btnIleri = new DevExpress.XtraEditors.SimpleButton();
            this.btnGeri = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUrunKodu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chckPasif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUrunAdi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.simpleButton1);
            this.panel3.Controls.Add(this.simpleButton5);
            this.panel3.Controls.Add(this.btnIleri);
            this.panel3.Controls.Add(this.btnGeri);
            this.panel3.Controls.Add(this.simpleButton2);
            this.panel3.Controls.Add(this.btnKaydet);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1272, 53);
            this.panel3.TabIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtUrunKodu);
            this.panelControl1.Controls.Add(this.chckPasif);
            this.panelControl1.Controls.Add(this.txtUrunAdi);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.label72);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 53);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1272, 82);
            this.panelControl1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 21);
            this.label1.TabIndex = 1120;
            this.label1.Text = "Ürün Adı :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.Transparent;
            this.label72.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label72.Location = new System.Drawing.Point(12, 8);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(93, 21);
            this.label72.TabIndex = 1120;
            this.label72.Text = "Ürün Kodu :";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUrunKodu
            // 
            this.txtUrunKodu.Location = new System.Drawing.Point(111, 8);
            this.txtUrunKodu.Name = "txtUrunKodu";
            this.txtUrunKodu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtUrunKodu.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_Properties_ButtonClick_1);
            this.txtUrunKodu.Size = new System.Drawing.Size(169, 22);
            this.txtUrunKodu.TabIndex = 1126;
            // 
            // chckPasif
            // 
            this.chckPasif.Location = new System.Drawing.Point(286, 8);
            this.chckPasif.Name = "chckPasif";
            this.chckPasif.Properties.Appearance.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chckPasif.Properties.Appearance.Options.UseFont = true;
            this.chckPasif.Properties.Caption = "Pasif ?";
            this.chckPasif.Size = new System.Drawing.Size(75, 20);
            this.chckPasif.TabIndex = 1125;
            // 
            // txtUrunAdi
            // 
            this.txtUrunAdi.Location = new System.Drawing.Point(111, 36);
            this.txtUrunAdi.Name = "txtUrunAdi";
            this.txtUrunAdi.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtUrunAdi.Properties.Appearance.Options.UseFont = true;
            this.txtUrunAdi.Size = new System.Drawing.Size(494, 24);
            this.txtUrunAdi.TabIndex = 1123;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton1.Location = new System.Drawing.Point(3, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 47);
            this.simpleButton1.TabIndex = 100;
            this.simpleButton1.Text = "Yeni";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton5.ImageOptions.Image")));
            this.simpleButton5.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton5.Location = new System.Drawing.Point(406, 3);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(75, 47);
            this.simpleButton5.TabIndex = 102;
            this.simpleButton5.Text = "Sil";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
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
            // simpleButton2
            // 
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton2.Location = new System.Drawing.Point(163, 3);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 47);
            this.simpleButton2.TabIndex = 102;
            this.simpleButton2.Text = "Liste";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
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
            // FrmUrunKarti
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 587);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Corbel", 8.25F);
            this.Name = "FrmUrunKarti";
            this.Text = "Ürün Kartı";
            this.Load += new System.EventHandler(this.FrmUrunKarti_Load);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUrunKodu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chckPasif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUrunAdi.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton btnIleri;
        private DevExpress.XtraEditors.SimpleButton btnGeri;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit chckPasif;
        private System.Windows.Forms.Label label72;
        private DevExpress.XtraEditors.TextEdit txtUrunAdi;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.ButtonEdit txtUrunKodu;
    }
}