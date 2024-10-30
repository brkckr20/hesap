namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.IplikDepo
{
    partial class FrmIplikDepoListe
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
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dizaynKaydetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satırİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sütunSeçimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFilter.InHeaderSearchMode = DevExpress.XtraGrid.Views.Grid.GridInHeaderSearchMode.Disabled;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1293, 693);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // contextMenu
            // 
            this.contextMenu.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dizaynKaydetToolStripMenuItem,
            this.satırİşlemleriToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(204, 70);
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
            // FrmIplikDepoListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 693);
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.gridControl1);
            this.IconOptions.Image = global::Hesap.Properties.Resources.Bookmark;
            this.Name = "FrmIplikDepoListe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İplik Depo İşlem Listesi";
            this.Load += new System.EventHandler(this.FrmIplikDepoListe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem dizaynKaydetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satırİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sütunSeçimiToolStripMenuItem;
    }
}