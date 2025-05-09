namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.Talimatlar
{
    partial class FrmTalimatOnaylama
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnOnaysiz = new DevExpress.XtraEditors.SimpleButton();
            this.btnOnayli = new DevExpress.XtraEditors.SimpleButton();
            this.btnTumu = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.onayİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onaylaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onayıKaldırToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnOnaysiz);
            this.panelControl1.Controls.Add(this.btnOnayli);
            this.panelControl1.Controls.Add(this.btnTumu);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1100, 35);
            this.panelControl1.TabIndex = 0;
            // 
            // btnOnaysiz
            // 
            this.btnOnaysiz.Location = new System.Drawing.Point(174, 5);
            this.btnOnaysiz.Name = "btnOnaysiz";
            this.btnOnaysiz.Size = new System.Drawing.Size(75, 23);
            this.btnOnaysiz.TabIndex = 3;
            this.btnOnaysiz.Text = "Onaysız";
            this.btnOnaysiz.Click += new System.EventHandler(this.btnOnaysiz_Click);
            // 
            // btnOnayli
            // 
            this.btnOnayli.Location = new System.Drawing.Point(93, 5);
            this.btnOnayli.Name = "btnOnayli";
            this.btnOnayli.Size = new System.Drawing.Size(75, 23);
            this.btnOnayli.TabIndex = 2;
            this.btnOnayli.Text = "Onaylı";
            this.btnOnayli.Click += new System.EventHandler(this.btnOnayli_Click);
            // 
            // btnTumu
            // 
            this.btnTumu.Location = new System.Drawing.Point(12, 5);
            this.btnTumu.Name = "btnTumu";
            this.btnTumu.Size = new System.Drawing.Size(75, 23);
            this.btnTumu.TabIndex = 1;
            this.btnTumu.Text = "Tümü";
            this.btnTumu.Click += new System.EventHandler(this.btnTumu_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 35);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1100, 655);
            this.panelControl2.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1096, 651);
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
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onayİşlemleriToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 26);
            // 
            // onayİşlemleriToolStripMenuItem
            // 
            this.onayİşlemleriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onaylaToolStripMenuItem,
            this.onayıKaldırToolStripMenuItem});
            this.onayİşlemleriToolStripMenuItem.Name = "onayİşlemleriToolStripMenuItem";
            this.onayİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.onayİşlemleriToolStripMenuItem.Text = "Onay İşlemleri";
            // 
            // onaylaToolStripMenuItem
            // 
            this.onaylaToolStripMenuItem.Name = "onaylaToolStripMenuItem";
            this.onaylaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.onaylaToolStripMenuItem.Text = "Onayla";
            this.onaylaToolStripMenuItem.Click += new System.EventHandler(this.onaylaToolStripMenuItem_Click);
            // 
            // onayıKaldırToolStripMenuItem
            // 
            this.onayıKaldırToolStripMenuItem.Name = "onayıKaldırToolStripMenuItem";
            this.onayıKaldırToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.onayıKaldırToolStripMenuItem.Text = "Onayı Kaldır";
            this.onayıKaldırToolStripMenuItem.Click += new System.EventHandler(this.onayıKaldırToolStripMenuItem_Click);
            // 
            // FrmTalimatOnaylama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 690);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmTalimatOnaylama";
            this.Text = "Talimat Onaylama";
            this.Load += new System.EventHandler(this.FrmTalimatOnaylama_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnOnaysiz;
        private DevExpress.XtraEditors.SimpleButton btnOnayli;
        private DevExpress.XtraEditors.SimpleButton btnTumu;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem onayİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onaylaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onayıKaldırToolStripMenuItem;
    }
}