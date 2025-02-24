namespace Hesap.Forms.Parametreler
{
    partial class frmUretimYonetimiParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUretimYonetimiParams));
            this.tabGenel = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtKasmaPayi = new DevExpress.XtraEditors.TextEdit();
            this.label72 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tabGenel)).BeginInit();
            this.tabGenel.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKasmaPayi.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabGenel
            // 
            this.tabGenel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabGenel.Location = new System.Drawing.Point(0, 0);
            this.tabGenel.Name = "tabGenel";
            this.tabGenel.SelectedTabPage = this.xtraTabPage1;
            this.tabGenel.Size = new System.Drawing.Size(1346, 685);
            this.tabGenel.TabIndex = 0;
            this.tabGenel.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.panelControl1);
            this.xtraTabPage1.Controls.Add(this.panel3);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1344, 661);
            this.xtraTabPage1.Text = "Genel";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnKaydet);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1344, 53);
            this.panel3.TabIndex = 4;
            // 
            // btnKaydet
            // 
            this.btnKaydet.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnKaydet.ImageOptions.Image")));
            this.btnKaydet.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.btnKaydet.Location = new System.Drawing.Point(3, 3);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(75, 47);
            this.btnKaydet.TabIndex = 101;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.txtKasmaPayi);
            this.panelControl1.Controls.Add(this.label72);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 53);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1344, 608);
            this.panelControl1.TabIndex = 5;
            // 
            // txtKasmaPayi
            // 
            this.txtKasmaPayi.Location = new System.Drawing.Point(140, 6);
            this.txtKasmaPayi.Name = "txtKasmaPayi";
            this.txtKasmaPayi.Size = new System.Drawing.Size(49, 22);
            this.txtKasmaPayi.TabIndex = 1;
            // 
            // label72
            // 
            this.label72.BackColor = System.Drawing.Color.Transparent;
            this.label72.Font = new System.Drawing.Font("Corbel", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label72.Location = new System.Drawing.Point(12, 7);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(122, 21);
            this.label72.TabIndex = 1118;
            this.label72.Text = "Kasma Payı (%) :";
            this.label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmUretimYonetimiParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 685);
            this.Controls.Add(this.tabGenel);
            this.Name = "frmUretimYonetimiParams";
            this.Text = "Üretim Yönetimi Parametreleri";
            this.Load += new System.EventHandler(this.frmUretimYonetimiParams_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabGenel)).EndInit();
            this.tabGenel.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtKasmaPayi.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabGenel;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtKasmaPayi;
        private System.Windows.Forms.Label label72;
    }
}