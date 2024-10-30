namespace Hesap.Forms.Kartlar
{
    partial class FrmTasiyiciKarti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTasiyiciKarti));
            this.panel3 = new System.Windows.Forms.Panel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnIleri = new DevExpress.XtraEditors.SimpleButton();
            this.btnGeri = new DevExpress.XtraEditors.SimpleButton();
            this.btnSil = new DevExpress.XtraEditors.SimpleButton();
            this.btnListe = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtDorse = new DevExpress.XtraEditors.TextEdit();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPlaka = new DevExpress.XtraEditors.TextEdit();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTC = new DevExpress.XtraEditors.TextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSoyad = new DevExpress.XtraEditors.TextEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAd = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUnvan = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDorse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaka.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnvan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.simpleButton1);
            this.panel3.Controls.Add(this.btnIleri);
            this.panel3.Controls.Add(this.btnGeri);
            this.panel3.Controls.Add(this.btnSil);
            this.panel3.Controls.Add(this.btnListe);
            this.panel3.Controls.Add(this.btnKaydet);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(723, 53);
            this.panel3.TabIndex = 4;
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
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.txtDorse);
            this.panelControl1.Controls.Add(this.label13);
            this.panelControl1.Controls.Add(this.txtPlaka);
            this.panelControl1.Controls.Add(this.label12);
            this.panelControl1.Controls.Add(this.txtTC);
            this.panelControl1.Controls.Add(this.label11);
            this.panelControl1.Controls.Add(this.txtSoyad);
            this.panelControl1.Controls.Add(this.label8);
            this.panelControl1.Controls.Add(this.txtAd);
            this.panelControl1.Controls.Add(this.label6);
            this.panelControl1.Controls.Add(this.txtUnvan);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 53);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(723, 387);
            this.panelControl1.TabIndex = 5;
            // 
            // txtDorse
            // 
            this.txtDorse.Location = new System.Drawing.Point(106, 132);
            this.txtDorse.Name = "txtDorse";
            this.txtDorse.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtDorse.Properties.Appearance.Options.UseFont = true;
            this.txtDorse.Size = new System.Drawing.Size(155, 24);
            this.txtDorse.TabIndex = 10007;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label13.Location = new System.Drawing.Point(9, 134);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(93, 21);
            this.label13.TabIndex = 10009;
            this.label13.Text = "Dorse :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPlaka
            // 
            this.txtPlaka.Location = new System.Drawing.Point(106, 107);
            this.txtPlaka.Name = "txtPlaka";
            this.txtPlaka.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtPlaka.Properties.Appearance.Options.UseFont = true;
            this.txtPlaka.Size = new System.Drawing.Size(155, 24);
            this.txtPlaka.TabIndex = 10008;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label12.Location = new System.Drawing.Point(9, 109);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 21);
            this.label12.TabIndex = 10010;
            this.label12.Text = "Plaka :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTC
            // 
            this.txtTC.Location = new System.Drawing.Point(106, 82);
            this.txtTC.Name = "txtTC";
            this.txtTC.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtTC.Properties.Appearance.Options.UseFont = true;
            this.txtTC.Size = new System.Drawing.Size(155, 24);
            this.txtTC.TabIndex = 10006;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.Location = new System.Drawing.Point(9, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 21);
            this.label11.TabIndex = 10011;
            this.label11.Text = "T.C. No :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSoyad
            // 
            this.txtSoyad.Location = new System.Drawing.Point(106, 56);
            this.txtSoyad.Name = "txtSoyad";
            this.txtSoyad.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtSoyad.Properties.Appearance.Options.UseFont = true;
            this.txtSoyad.Size = new System.Drawing.Size(155, 24);
            this.txtSoyad.TabIndex = 10005;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(9, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 21);
            this.label8.TabIndex = 10012;
            this.label8.Text = "Soyad :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAd
            // 
            this.txtAd.Location = new System.Drawing.Point(106, 31);
            this.txtAd.Name = "txtAd";
            this.txtAd.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtAd.Properties.Appearance.Options.UseFont = true;
            this.txtAd.Size = new System.Drawing.Size(155, 24);
            this.txtAd.TabIndex = 10004;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(9, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 21);
            this.label6.TabIndex = 10013;
            this.label6.Text = "Ad :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUnvan
            // 
            this.txtUnvan.Location = new System.Drawing.Point(106, 6);
            this.txtUnvan.Name = "txtUnvan";
            this.txtUnvan.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtUnvan.Properties.Appearance.Options.UseFont = true;
            this.txtUnvan.Size = new System.Drawing.Size(155, 24);
            this.txtUnvan.TabIndex = 10003;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 21);
            this.label4.TabIndex = 10014;
            this.label4.Text = "Taşyc. Ünvan :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmTasiyiciKarti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 440);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel3);
            this.IconOptions.Image = global::Hesap.Properties.Resources.Bookmark;
            this.Name = "FrmTasiyiciKarti";
            this.Text = "Taşıyıcı Kartı";
            this.Load += new System.EventHandler(this.FrmTasiyiciKarti_Load);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDorse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlaka.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUnvan.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btnIleri;
        private DevExpress.XtraEditors.SimpleButton btnGeri;
        private DevExpress.XtraEditors.SimpleButton btnSil;
        private DevExpress.XtraEditors.SimpleButton btnListe;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtDorse;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.TextEdit txtPlaka;
        private System.Windows.Forms.Label label12;
        private DevExpress.XtraEditors.TextEdit txtTC;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.TextEdit txtSoyad;
        private System.Windows.Forms.Label label8;
        private DevExpress.XtraEditors.TextEdit txtAd;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txtUnvan;
        private System.Windows.Forms.Label label4;
    }
}