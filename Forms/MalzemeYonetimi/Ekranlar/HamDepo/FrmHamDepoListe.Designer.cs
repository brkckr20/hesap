﻿namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.HamDepo
{
    partial class FrmHamDepoListe
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
            this.contextMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dizaynKaydetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satırİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sütunSeçimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dışarıAktarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelAktarxlsxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.contextMenu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1428, 698);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
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
            // 
            // contextMenu1
            // 
            this.contextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dışarıAktarToolStripMenuItem,
            this.dizaynKaydetToolStripMenuItem,
            this.satırİşlemleriToolStripMenuItem});
            this.contextMenu1.Name = "contextMenu1";
            this.contextMenu1.Size = new System.Drawing.Size(203, 92);
            // 
            // dizaynKaydetToolStripMenuItem
            // 
            this.dizaynKaydetToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dizaynKaydetToolStripMenuItem.Name = "dizaynKaydetToolStripMenuItem";
            this.dizaynKaydetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.dizaynKaydetToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.dizaynKaydetToolStripMenuItem.Text = "Dizayn Kaydet";
            this.dizaynKaydetToolStripMenuItem.Click += new System.EventHandler(this.dizaynKaydetToolStripMenuItem_Click);
            // 
            // satırİşlemleriToolStripMenuItem
            // 
            this.satırİşlemleriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sütunSeçimiToolStripMenuItem});
            this.satırİşlemleriToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.satırİşlemleriToolStripMenuItem.Name = "satırİşlemleriToolStripMenuItem";
            this.satırİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.satırİşlemleriToolStripMenuItem.Text = "Satır İşlemleri";
            // 
            // sütunSeçimiToolStripMenuItem
            // 
            this.sütunSeçimiToolStripMenuItem.Name = "sütunSeçimiToolStripMenuItem";
            this.sütunSeçimiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sütunSeçimiToolStripMenuItem.Text = "Sütun Seçimi";
            this.sütunSeçimiToolStripMenuItem.Click += new System.EventHandler(this.sütunSeçimiToolStripMenuItem_Click);
            // 
            // dışarıAktarToolStripMenuItem
            // 
            this.dışarıAktarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelAktarxlsxToolStripMenuItem});
            this.dışarıAktarToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dışarıAktarToolStripMenuItem.Name = "dışarıAktarToolStripMenuItem";
            this.dışarıAktarToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.dışarıAktarToolStripMenuItem.Text = "Dışarı Aktar";
            // 
            // excelAktarxlsxToolStripMenuItem
            // 
            this.excelAktarxlsxToolStripMenuItem.Name = "excelAktarxlsxToolStripMenuItem";
            this.excelAktarxlsxToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.excelAktarxlsxToolStripMenuItem.Text = "Excel Aktar (.xlsx)";
            this.excelAktarxlsxToolStripMenuItem.Click += new System.EventHandler(this.excelAktarxlsxToolStripMenuItem_Click);
            // 
            // FrmHamDepoListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 698);
            this.ContextMenuStrip = this.contextMenu1;
            this.Controls.Add(this.gridControl1);
            this.IconOptions.Image = global::Hesap.Properties.Resources.Bookmark;
            this.Name = "FrmHamDepoListe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ham Depo Liste";
            this.Load += new System.EventHandler(this.FrmHamDepoListe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.contextMenu1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenu1;
        private System.Windows.Forms.ToolStripMenuItem dizaynKaydetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satırİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sütunSeçimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dışarıAktarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelAktarxlsxToolStripMenuItem;
    }
}