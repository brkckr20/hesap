namespace Hesap.Forms.Kartlar
{
    partial class FrmKullaniciKarti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKullaniciKarti));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnYeni = new DevExpress.XtraEditors.SimpleButton();
            this.btnIleri = new DevExpress.XtraEditors.SimpleButton();
            this.btnGeri = new DevExpress.XtraEditors.SimpleButton();
            this.btnSil = new DevExpress.XtraEditors.SimpleButton();
            this.btnListe = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chckKullanimda = new DevExpress.XtraEditors.CheckEdit();
            this.txtSifre = new DevExpress.XtraEditors.TextEdit();
            this.txtSoyad = new DevExpress.XtraEditors.TextEdit();
            this.txtAd = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKodu = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.grdKullaniciYetkileri = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.RecId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ScreenName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CanAccess = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CanSave = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CanUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CanDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Tag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.grdButtons = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ButtonId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ButtonName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ButtonText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.IsVisible = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chckKullanimda.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSifre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKodu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdKullaniciYetkileri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
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
            this.panel3.Size = new System.Drawing.Size(1123, 53);
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
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.chckKullanimda);
            this.panelControl1.Controls.Add(this.txtSifre);
            this.panelControl1.Controls.Add(this.txtSoyad);
            this.panelControl1.Controls.Add(this.txtAd);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.txtKodu);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.label72);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 53);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1123, 91);
            this.panelControl1.TabIndex = 5;
            // 
            // chckKullanimda
            // 
            this.chckKullanimda.Location = new System.Drawing.Point(325, 7);
            this.chckKullanimda.Name = "chckKullanimda";
            this.chckKullanimda.Properties.Caption = "Kullanımda";
            this.chckKullanimda.Size = new System.Drawing.Size(87, 20);
            this.chckKullanimda.TabIndex = 1125;
            // 
            // txtSifre
            // 
            this.txtSifre.Location = new System.Drawing.Point(113, 54);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(206, 22);
            this.txtSifre.TabIndex = 1124;
            // 
            // txtSoyad
            // 
            this.txtSoyad.Location = new System.Drawing.Point(218, 30);
            this.txtSoyad.Name = "txtSoyad";
            this.txtSoyad.Size = new System.Drawing.Size(100, 22);
            this.txtSoyad.TabIndex = 1123;
            // 
            // txtAd
            // 
            this.txtAd.Location = new System.Drawing.Point(113, 30);
            this.txtAd.Name = "txtAd";
            this.txtAd.Size = new System.Drawing.Size(100, 22);
            this.txtAd.TabIndex = 1122;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(13, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 21);
            this.label2.TabIndex = 1126;
            this.label2.Text = "Şifre :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtKodu
            // 
            this.txtKodu.Location = new System.Drawing.Point(113, 6);
            this.txtKodu.Name = "txtKodu";
            this.txtKodu.Size = new System.Drawing.Size(206, 22);
            this.txtKodu.TabIndex = 1121;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 21);
            this.label1.TabIndex = 1127;
            this.label1.Text = "Ad Soyad :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.Transparent;
            this.label72.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label72.Location = new System.Drawing.Point(13, 7);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(93, 21);
            this.label72.TabIndex = 1128;
            this.label72.Text = "Kodu :";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grdKullaniciYetkileri
            // 
            this.grdKullaniciYetkileri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKullaniciYetkileri.Location = new System.Drawing.Point(0, 0);
            this.grdKullaniciYetkileri.MainView = this.gridView1;
            this.grdKullaniciYetkileri.Name = "grdKullaniciYetkileri";
            this.grdKullaniciYetkileri.Size = new System.Drawing.Size(1121, 493);
            this.grdKullaniciYetkileri.TabIndex = 0;
            this.grdKullaniciYetkileri.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.RecId,
            this.ScreenName,
            this.CanAccess,
            this.CanSave,
            this.CanUpdate,
            this.CanDelete,
            this.Tag});
            this.gridView1.GridControl = this.grdKullaniciYetkileri;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // RecId
            // 
            this.RecId.Caption = "Kayıt No";
            this.RecId.FieldName = "Id";
            this.RecId.Name = "RecId";
            this.RecId.Visible = true;
            this.RecId.VisibleIndex = 0;
            // 
            // ScreenName
            // 
            this.ScreenName.Caption = "Ekran Adı";
            this.ScreenName.FieldName = "ScreenName";
            this.ScreenName.Name = "ScreenName";
            this.ScreenName.Visible = true;
            this.ScreenName.VisibleIndex = 1;
            // 
            // CanAccess
            // 
            this.CanAccess.Caption = "Giriş";
            this.CanAccess.FieldName = "CanAccess";
            this.CanAccess.Name = "CanAccess";
            this.CanAccess.Visible = true;
            this.CanAccess.VisibleIndex = 2;
            // 
            // CanSave
            // 
            this.CanSave.Caption = "Kayıt";
            this.CanSave.FieldName = "CanSave";
            this.CanSave.Name = "CanSave";
            this.CanSave.Visible = true;
            this.CanSave.VisibleIndex = 3;
            // 
            // CanUpdate
            // 
            this.CanUpdate.Caption = "Güncelleme";
            this.CanUpdate.FieldName = "CanUpdate";
            this.CanUpdate.Name = "CanUpdate";
            this.CanUpdate.Visible = true;
            this.CanUpdate.VisibleIndex = 5;
            // 
            // CanDelete
            // 
            this.CanDelete.Caption = "Sime";
            this.CanDelete.FieldName = "CanDelete";
            this.CanDelete.Name = "CanDelete";
            this.CanDelete.Visible = true;
            this.CanDelete.VisibleIndex = 4;
            // 
            // Tag
            // 
            this.Tag.Caption = "Etiket";
            this.Tag.FieldName = "Tag";
            this.Tag.Name = "Tag";
            this.Tag.Visible = true;
            this.Tag.VisibleIndex = 6;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 144);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1123, 517);
            this.xtraTabControl1.TabIndex = 7;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grdKullaniciYetkileri);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1121, 493);
            this.xtraTabPage1.Text = "Kayıt Yetkileri";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grdButtons);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1121, 493);
            this.xtraTabPage2.Text = "Ekran Görünürlük Yetkileri";
            // 
            // grdButtons
            // 
            this.grdButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdButtons.Location = new System.Drawing.Point(0, 0);
            this.grdButtons.MainView = this.gridView2;
            this.grdButtons.Name = "grdButtons";
            this.grdButtons.Size = new System.Drawing.Size(1121, 493);
            this.grdButtons.TabIndex = 1;
            this.grdButtons.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ButtonId,
            this.ButtonName,
            this.ButtonText,
            this.IsVisible});
            this.gridView2.GridControl = this.grdButtons;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // ButtonId
            // 
            this.ButtonId.Caption = "Kayıt No";
            this.ButtonId.FieldName = "Id";
            this.ButtonId.Name = "ButtonId";
            this.ButtonId.Visible = true;
            this.ButtonId.VisibleIndex = 0;
            // 
            // ButtonName
            // 
            this.ButtonName.Caption = "Button Name";
            this.ButtonName.FieldName = "ButtonName";
            this.ButtonName.Name = "ButtonName";
            this.ButtonName.Visible = true;
            this.ButtonName.VisibleIndex = 1;
            // 
            // ButtonText
            // 
            this.ButtonText.Caption = "Button Yazısı";
            this.ButtonText.FieldName = "ButtonText";
            this.ButtonText.Name = "ButtonText";
            this.ButtonText.Visible = true;
            this.ButtonText.VisibleIndex = 3;
            this.ButtonText.Width = 374;
            // 
            // IsVisible
            // 
            this.IsVisible.Caption = "Gizle / Göster ?";
            this.IsVisible.FieldName = "IsVisible";
            this.IsVisible.Name = "IsVisible";
            this.IsVisible.Visible = true;
            this.IsVisible.VisibleIndex = 2;
            this.IsVisible.Width = 117;
            // 
            // FrmKullaniciKarti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 661);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel3);
            this.IconOptions.Image = global::Hesap.Properties.Resources.Bookmark;
            this.Name = "FrmKullaniciKarti";
            this.Text = "Kullanıcı Kartı";
            this.Load += new System.EventHandler(this.FrmKullaniciKarti_Load);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chckKullanimda.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSifre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKodu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdKullaniciYetkileri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
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
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit chckKullanimda;
        private DevExpress.XtraEditors.TextEdit txtSifre;
        private DevExpress.XtraEditors.TextEdit txtSoyad;
        private DevExpress.XtraEditors.TextEdit txtAd;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtKodu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label72;
        private DevExpress.XtraGrid.GridControl grdKullaniciYetkileri;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn RecId;
        private DevExpress.XtraGrid.Columns.GridColumn ScreenName;
        private DevExpress.XtraGrid.Columns.GridColumn CanAccess;
        private DevExpress.XtraGrid.Columns.GridColumn CanSave;
        private DevExpress.XtraGrid.Columns.GridColumn CanDelete;
        private DevExpress.XtraGrid.Columns.GridColumn CanUpdate;
        private DevExpress.XtraGrid.Columns.GridColumn Tag;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraGrid.GridControl grdButtons;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn ButtonId;
        private DevExpress.XtraGrid.Columns.GridColumn ButtonName;
        private DevExpress.XtraGrid.Columns.GridColumn IsVisible;
        private DevExpress.XtraGrid.Columns.GridColumn ButtonText;
    }
}