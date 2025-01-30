using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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

namespace Hesap.Forms.Liste
{
    public partial class FrmKullaniciListesi : DevExpress.XtraEditors.XtraForm
    {
        public FrmKullaniciListesi()
        {
            InitializeComponent();
        }
        Listele listele = new Listele();
        public string Kodu, AdSoyad, Sifre, Departman;
        public int Id;
        private void FrmKullaniciListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = "SELECT * FROM Users";
            listele.Liste(sql, gridControl1);
        }
        
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Kodu = gridView.GetFocusedRowCellValue("Kodu").ToString();
            AdSoyad = gridView.GetFocusedRowCellValue("AdSoyad").ToString();
            Sifre = gridView.GetFocusedRowCellValue("Sifre").ToString();
            Departman = gridView.GetFocusedRowCellValue("DepartmanId").ToString();
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}