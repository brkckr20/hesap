namespace Hesap.Forms.Liste
{
    partial class FrmUrunKartiListesi
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dışarıAktarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelDosyasıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satırİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satırİşlemleriToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sütunSeçimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(683, 458);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dışarıAktarToolStripMenuItem,
            this.satırİşlemleriToolStripMenuItem,
            this.satırİşlemleriToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(205, 92);
            // 
            // dışarıAktarToolStripMenuItem
            // 
            this.dışarıAktarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelDosyasıToolStripMenuItem});
            this.dışarıAktarToolStripMenuItem.Name = "dışarıAktarToolStripMenuItem";
            this.dışarıAktarToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.dışarıAktarToolStripMenuItem.Text = "Dışarı Aktar";
            // 
            // excelDosyasıToolStripMenuItem
            // 
            this.excelDosyasıToolStripMenuItem.Name = "excelDosyasıToolStripMenuItem";
            this.excelDosyasıToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.excelDosyasıToolStripMenuItem.Text = "Excel Dosyası";
            this.excelDosyasıToolStripMenuItem.Click += new System.EventHandler(this.excelDosyasıToolStripMenuItem_Click);
            // 
            // satırİşlemleriToolStripMenuItem
            // 
            this.satırİşlemleriToolStripMenuItem.Name = "satırİşlemleriToolStripMenuItem";
            this.satırİşlemleriToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.satırİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.satırİşlemleriToolStripMenuItem.Text = "Dizayn Kaydet";
            this.satırİşlemleriToolStripMenuItem.Click += new System.EventHandler(this.satırİşlemleriToolStripMenuItem_Click);
            // 
            // satırİşlemleriToolStripMenuItem1
            // 
            this.satırİşlemleriToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sütunSeçimiToolStripMenuItem});
            this.satırİşlemleriToolStripMenuItem1.Name = "satırİşlemleriToolStripMenuItem1";
            this.satırİşlemleriToolStripMenuItem1.Size = new System.Drawing.Size(204, 22);
            this.satırİşlemleriToolStripMenuItem1.Text = "Satır İşlemleri";
            // 
            // sütunSeçimiToolStripMenuItem
            // 
            this.sütunSeçimiToolStripMenuItem.Name = "sütunSeçimiToolStripMenuItem";
            this.sütunSeçimiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sütunSeçimiToolStripMenuItem.Text = "Sütun Seçimi";
            this.sütunSeçimiToolStripMenuItem.Click += new System.EventHandler(this.sütunSeçimiToolStripMenuItem_Click);
            // 
            // FrmUrunKartiListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 458);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.gridControl1);
            this.IconOptions.Image = global::Hesap.Properties.Resources.Bookmark;
            this.Name = "FrmUrunKartiListesi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ürün Kartı Listesi";
            this.Load += new System.EventHandler(this.FrmUrunKartiListesi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dışarıAktarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelDosyasıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satırİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satırİşlemleriToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem sütunSeçimiToolStripMenuItem;
    }
}