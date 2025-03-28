namespace Hesap.Forms.Diger
{
    partial class FrmNumerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNumerator));
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnYeni = new DevExpress.XtraEditors.SimpleButton();
            this.btnIleri = new DevExpress.XtraEditors.SimpleButton();
            this.btnGeri = new DevExpress.XtraEditors.SimpleButton();
            this.btnSil = new DevExpress.XtraEditors.SimpleButton();
            this.btnListe = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            this.panel3.Size = new System.Drawing.Size(1170, 53);
            this.panel3.TabIndex = 5;
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
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 53);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1170, 559);
            this.panelControl1.TabIndex = 6;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1166, 555);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // FrmNumerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 612);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel3);
            this.Name = "FrmNumerator";
            this.Text = "Numaratör";
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}