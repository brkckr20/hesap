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
            this.cmbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chckIsActive = new DevExpress.XtraEditors.CheckEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIsim = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumara = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOnEk = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chckIsActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIsim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumara.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOnEk.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
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
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cmbType);
            this.panelControl1.Controls.Add(this.chckIsActive);
            this.panelControl1.Controls.Add(this.label5);
            this.panelControl1.Controls.Add(this.label4);
            this.panelControl1.Controls.Add(this.txtIsim);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.txtNumara);
            this.panelControl1.Controls.Add(this.label2);
            this.panelControl1.Controls.Add(this.txtOnEk);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 53);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1170, 161);
            this.panelControl1.TabIndex = 6;
            // 
            // cmbType
            // 
            this.cmbType.Location = new System.Drawing.Point(111, 102);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Properties.Items.AddRange(new object[] {
            "",
            "Kumaş",
            "İplik",
            "Aksesuar"});
            this.cmbType.Size = new System.Drawing.Size(155, 22);
            this.cmbType.TabIndex = 4;
            // 
            // chckIsActive
            // 
            this.chckIsActive.Location = new System.Drawing.Point(111, 130);
            this.chckIsActive.Name = "chckIsActive";
            this.chckIsActive.Properties.Caption = "";
            this.chckIsActive.Size = new System.Drawing.Size(30, 20);
            this.chckIsActive.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(12, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 21);
            this.label5.TabIndex = 1121;
            this.label5.Text = "Malzeme Türü :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(12, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 21);
            this.label4.TabIndex = 1121;
            this.label4.Text = "Aktif :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIsim
            // 
            this.txtIsim.EditValue = "";
            this.txtIsim.Location = new System.Drawing.Point(111, 72);
            this.txtIsim.Name = "txtIsim";
            this.txtIsim.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtIsim.Properties.Appearance.Options.UseFont = true;
            this.txtIsim.Size = new System.Drawing.Size(155, 24);
            this.txtIsim.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(12, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 21);
            this.label3.TabIndex = 1121;
            this.label3.Text = "İsim :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNumara
            // 
            this.txtNumara.EditValue = "1";
            this.txtNumara.Location = new System.Drawing.Point(111, 42);
            this.txtNumara.Name = "txtNumara";
            this.txtNumara.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtNumara.Properties.Appearance.Options.UseFont = true;
            this.txtNumara.Size = new System.Drawing.Size(155, 24);
            this.txtNumara.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 21);
            this.label2.TabIndex = 1121;
            this.label2.Text = "Numara :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOnEk
            // 
            this.txtOnEk.Location = new System.Drawing.Point(111, 12);
            this.txtOnEk.Name = "txtOnEk";
            this.txtOnEk.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 10F);
            this.txtOnEk.Properties.Appearance.Options.UseFont = true;
            this.txtOnEk.Size = new System.Drawing.Size(155, 24);
            this.txtOnEk.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 21);
            this.label1.TabIndex = 1121;
            this.label1.Text = "Ön Ek :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 214);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1170, 398);
            this.panelControl2.TabIndex = 7;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1166, 394);
            this.gridControl1.TabIndex = 2;
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
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            // 
            // FrmNumerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 612);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel3);
            this.Name = "FrmNumerator";
            this.Text = "Numaratör";
            this.Load += new System.EventHandler(this.FrmNumerator_Load);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chckIsActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIsim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumara.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOnEk.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
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
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtIsim;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txtNumara;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtOnEk;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit cmbType;
        private DevExpress.XtraEditors.CheckEdit chckIsActive;
        private System.Windows.Forms.Label label5;
    }
}