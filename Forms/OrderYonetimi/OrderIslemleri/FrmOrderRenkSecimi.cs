using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.Forms.OrderYonetimi.Models;
using Hesap.Utils;
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
            GetColorList();
            gridView1.DoubleClick += GridView1_DoubleClick;
        }
        public string selectedColor;
        Listele listele = new Listele();
        
        void GetColorList()
        {
            string sql = @"select Kodu, Adi from RenkKarti where Kullanimda=1";
            listele.Liste(sql,gridControl1);
        }
        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView != null)
            {
                int rowHandle = gridView.FocusedRowHandle;
                if (rowHandle >= 0)
                {
                    selectedColor = gridView.GetRowCellValue(rowHandle, gridView.Columns[0]).ToString();
                    this.Close();
                }
            }
        }
    }
}