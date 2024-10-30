namespace Hesap.Forms.MalzemeYonetimi
{
    partial class FrmMalzemeKarti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMalzemeKarti));
            this.panel3 = new System.Windows.Forms.Panel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pnlGrupKodlari = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chckKullanimda = new DevExpress.XtraEditors.CheckEdit();
            this.txtGrupKodu = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAdi = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKodu = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grupKodlarınıGösterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTip = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrupKodlari)).BeginInit();
            this.pnlGrupKodlari.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chckKullanimda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrupKodu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKodu.Properties)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTip.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.simpleButton1);
            this.panel3.Controls.Add(this.simpleButton5);
            this.panel3.Controls.Add(this.simpleButton4);
            this.panel3.Controls.Add(this.simpleButton3);
            this.panel3.Controls.Add(this.simpleButton2);
            this.panel3.Controls.Add(this.btnKaydet);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(934, 53);
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
            // simpleButton4
            // 
            this.simpleButton4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.ImageOptions.Image")));
            this.simpleButton4.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton4.Location = new System.Drawing.Point(325, 3);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(75, 47);
            this.simpleButton4.TabIndex = 102;
            this.simpleButton4.Text = "İleri";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));
            this.simpleButton3.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.simpleButton3.Location = new System.Drawing.Point(244, 3);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(75, 47);
            this.simpleButton3.TabIndex = 102;
            this.simpleButton3.Text = "Geri";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
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
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cmbTip);
            this.panelControl1.Controls.Add(this.pnlGrupKodlari);
            this.panelControl1.Controls.Add(this.chckKullanimda);
            this.panelControl1.Controls.Add(this.txtGrupKodu);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.txtAdi);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.txtKodu);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 53);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(934, 503);
            this.panelControl1.TabIndex = 5;
            // 
            // pnlGrupKodlari
            // 
            this.pnlGrupKodlari.Controls.Add(this.labelControl2);
            this.pnlGrupKodlari.Controls.Add(this.labelControl1);
            this.pnlGrupKodlari.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGrupKodlari.Location = new System.Drawing.Point(2, 236);
            this.pnlGrupKodlari.Name = "pnlGrupKodlari";
            this.pnlGrupKodlari.Size = new System.Drawing.Size(930, 265);
            this.pnlGrupKodlari.TabIndex = 1127;
            this.pnlGrupKodlari.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(33, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = " - Toner Grubu";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "01";
            // 
            // chckKullanimda
            // 
            this.chckKullanimda.EditValue = true;
            this.chckKullanimda.Location = new System.Drawing.Point(304, 8);
            this.chckKullanimda.Name = "chckKullanimda";
            this.chckKullanimda.Properties.Caption = "Kullanımda";
            this.chckKullanimda.Size = new System.Drawing.Size(75, 20);
            this.chckKullanimda.TabIndex = 1126;
            // 
            // txtGrupKodu
            // 
            this.txtGrupKodu.Location = new System.Drawing.Point(142, 58);
            this.txtGrupKodu.Name = "txtGrupKodu";
            this.txtGrupKodu.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtGrupKodu.Properties.Appearance.Options.UseFont = true;
            this.txtGrupKodu.Size = new System.Drawing.Size(155, 24);
            this.txtGrupKodu.TabIndex = 1125;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(15, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 21);
            this.label3.TabIndex = 1124;
            this.label3.Text = "Grup :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAdi
            // 
            this.txtAdi.Location = new System.Drawing.Point(142, 32);
            this.txtAdi.Name = "txtAdi";
            this.txtAdi.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtAdi.Properties.Appearance.Options.UseFont = true;
            this.txtAdi.Size = new System.Drawing.Size(155, 24);
            this.txtAdi.TabIndex = 1125;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(15, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 21);
            this.label2.TabIndex = 1124;
            this.label2.Text = "Malzeme Adı :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKodu
            // 
            this.txtKodu.Location = new System.Drawing.Point(142, 6);
            this.txtKodu.Name = "txtKodu";
            this.txtKodu.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtKodu.Properties.Appearance.Options.UseFont = true;
            this.txtKodu.Size = new System.Drawing.Size(155, 24);
            this.txtKodu.TabIndex = 1125;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(15, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 21);
            this.label1.TabIndex = 1124;
            this.label1.Text = "Malzeme Kodu :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grupKodlarınıGösterToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(188, 26);
            // 
            // grupKodlarınıGösterToolStripMenuItem
            // 
            this.grupKodlarınıGösterToolStripMenuItem.Name = "grupKodlarınıGösterToolStripMenuItem";
            this.grupKodlarınıGösterToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.grupKodlarınıGösterToolStripMenuItem.Text = "Grup Kodlarını Göster";
            this.grupKodlarınıGösterToolStripMenuItem.Click += new System.EventHandler(this.grupKodlarınıGösterToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(14, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 21);
            this.label4.TabIndex = 1124;
            this.label4.Text = "Tip :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbTip
            // 
            this.cmbTip.EditValue = "Malzeme";
            this.cmbTip.Location = new System.Drawing.Point(142, 84);
            this.cmbTip.Name = "cmbTip";
            this.cmbTip.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTip.Properties.Items.AddRange(new object[] {
            "Malzeme",
            "Hizmet",
            "Sabit Kıymet"});
            this.cmbTip.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbTip.Size = new System.Drawing.Size(155, 22);
            this.cmbTip.TabIndex = 1128;
            // 
            // FrmMalzemeKarti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 556);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel3);
            this.Name = "FrmMalzemeKarti";
            this.Text = "Malzeme Kartı";
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlGrupKodlari)).EndInit();
            this.pnlGrupKodlari.ResumeLayout(false);
            this.pnlGrupKodlari.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chckKullanimda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrupKodu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKodu.Properties)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbTip.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit chckKullanimda;
        private DevExpress.XtraEditors.TextEdit txtGrupKodu;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtAdi;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtKodu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem grupKodlarınıGösterToolStripMenuItem;
        private DevExpress.XtraEditors.PanelControl pnlGrupKodlari;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTip;
    }
}