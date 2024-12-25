using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.Forms.OrderYonetimi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    public partial class FrmOrderRenkSecimi : DevExpress.XtraEditors.XtraForm
    {
        public FrmOrderRenkSecimi()
        {
            InitializeComponent();
        }

        private void FrmOrderRenkSecimi_Load(object sender, EventArgs e)
        {
            deneme();
            gridView1.DoubleClick += GridView1_DoubleClick;
        }
        public string selectedColor;
        void deneme()
        {
            var renkler = new List<string> { "Beyaz", "Siyah", "Kırmızı" };
            gridControl1.DataSource = renkler;
            GridView gridView = gridControl1.MainView as GridView;
            if (gridView != null)
            {
                gridView.Columns[0].Caption = "Renk";
            }
        }
        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            // Seçili satırdaki veriyi alıyoruz
            GridView gridView = sender as GridView;
            if (gridView != null)
            {
                int rowHandle = gridView.FocusedRowHandle;  // Seçilen satırın handle'ı
                if (rowHandle >= 0) // Satır geçerliyse
                {
                    selectedColor = gridView.GetRowCellValue(rowHandle, gridView.Columns[0]).ToString();
                    this.Close();
                }
            }
        }
    }
}