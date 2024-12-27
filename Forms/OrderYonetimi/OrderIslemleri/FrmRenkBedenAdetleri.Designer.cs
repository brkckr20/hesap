namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    partial class FrmRenkBedenAdetleri
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Siparis_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Model_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Beden_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Renk_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Miktar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Beden = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnKayit = new DevExpress.XtraEditors.SimpleButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dizaynKaydetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satırİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sütunSeçimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(686, 436);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Id,
            this.Siparis_Id,
            this.Model_Id,
            this.Beden_Id,
            this.Renk_Id,
            this.Miktar,
            this.Beden});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // Id
            // 
            this.Id.Caption = "Id";
            this.Id.FieldName = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = true;
            this.Id.VisibleIndex = 0;
            // 
            // Siparis_Id
            // 
            this.Siparis_Id.Caption = "Sipariş Id";
            this.Siparis_Id.FieldName = "SiparisId";
            this.Siparis_Id.Name = "Siparis_Id";
            this.Siparis_Id.Visible = true;
            this.Siparis_Id.VisibleIndex = 1;
            // 
            // Model_Id
            // 
            this.Model_Id.Caption = "Model Id";
            this.Model_Id.FieldName = "ModelId";
            this.Model_Id.Name = "Model_Id";
            this.Model_Id.Visible = true;
            this.Model_Id.VisibleIndex = 2;
            // 
            // Beden_Id
            // 
            this.Beden_Id.Caption = "Beden Id";
            this.Beden_Id.FieldName = "BedenId";
            this.Beden_Id.Name = "Beden_Id";
            this.Beden_Id.Visible = true;
            this.Beden_Id.VisibleIndex = 3;
            // 
            // Renk_Id
            // 
            this.Renk_Id.Caption = "Renk Id";
            this.Renk_Id.FieldName = "RenkId";
            this.Renk_Id.Name = "Renk_Id";
            this.Renk_Id.Visible = true;
            this.Renk_Id.VisibleIndex = 4;
            // 
            // Miktar
            // 
            this.Miktar.Caption = "Miktar";
            this.Miktar.FieldName = "Miktar";
            this.Miktar.Name = "Miktar";
            this.Miktar.Visible = true;
            this.Miktar.VisibleIndex = 5;
            // 
            // Beden
            // 
            this.Beden.Caption = "Beden";
            this.Beden.FieldName = "Beden";
            this.Beden.Name = "Beden";
            this.Beden.Visible = true;
            this.Beden.VisibleIndex = 6;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(690, 440);
            this.panelControl1.TabIndex = 2;
            this.panelControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControl1_Paint);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Controls.Add(this.btnKayit);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 394);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(690, 46);
            this.panelControl2.TabIndex = 3;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(113, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 37);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "İptal";
            // 
            // btnKayit
            // 
            this.btnKayit.Location = new System.Drawing.Point(8, 5);
            this.btnKayit.Name = "btnKayit";
            this.btnKayit.Size = new System.Drawing.Size(99, 37);
            this.btnKayit.TabIndex = 0;
            this.btnKayit.Text = "Kaydet ve Kapat";
            this.btnKayit.Click += new System.EventHandler(this.btnKayit_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dizaynKaydetToolStripMenuItem,
            this.satırİşlemleriToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(204, 48);
            // 
            // dizaynKaydetToolStripMenuItem
            // 
            this.dizaynKaydetToolStripMenuItem.Name = "dizaynKaydetToolStripMenuItem";
            this.dizaynKaydetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.dizaynKaydetToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.dizaynKaydetToolStripMenuItem.Text = "Dizayn Kaydet";
            this.dizaynKaydetToolStripMenuItem.Click += new System.EventHandler(this.dizaynKaydetToolStripMenuItem_Click);
            // 
            // satırİşlemleriToolStripMenuItem
            // 
            this.satırİşlemleriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sütunSeçimiToolStripMenuItem});
            this.satırİşlemleriToolStripMenuItem.Name = "satırİşlemleriToolStripMenuItem";
            this.satırİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.satırİşlemleriToolStripMenuItem.Text = "Satır İşlemleri";
            // 
            // sütunSeçimiToolStripMenuItem
            // 
            this.sütunSeçimiToolStripMenuItem.Name = "sütunSeçimiToolStripMenuItem";
            this.sütunSeçimiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sütunSeçimiToolStripMenuItem.Text = "Sütun Seçimi";
            this.sütunSeçimiToolStripMenuItem.Click += new System.EventHandler(this.sütunSeçimiToolStripMenuItem_Click);
            // 
            // FrmRenkBedenAdetleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 440);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = global::Hesap.Properties.Resources.Bookmark;
            this.Name = "FrmRenkBedenAdetleri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Renk Beden Adetleri";
            this.Load += new System.EventHandler(this.FrmRenkBedenAdetleri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnKayit;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn Id;
        private DevExpress.XtraGrid.Columns.GridColumn Siparis_Id;
        private DevExpress.XtraGrid.Columns.GridColumn Model_Id;
        private DevExpress.XtraGrid.Columns.GridColumn Beden_Id;
        private DevExpress.XtraGrid.Columns.GridColumn Renk_Id;
        private DevExpress.XtraGrid.Columns.GridColumn Miktar;
        private DevExpress.XtraGrid.Columns.GridColumn Beden;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dizaynKaydetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satırİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sütunSeçimiToolStripMenuItem;
    }
}