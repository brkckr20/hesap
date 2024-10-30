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
    public partial class FrmDepoKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();

        public FrmDepoKartiListesi()
        {
            InitializeComponent();
        }
        public string Kodu, Adi, KayitEden, Guncelleyen;
        public bool Kullanimda;
        public DateTime? KayitTarihi, GuncellemeTarihi;
        public int Id;

        private void FrmDepoKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = "SELECT * FROM DepoKarti";
            listele.Liste(sql, gridControl1);
            gridView1.Columns["KayitTarihi"].Visible = false;
            gridView1.Columns["KayitEden"].Visible = false;
            gridView1.Columns["GuncellemeTarihi"].Visible = false;
            gridView1.Columns["Guncelleyen"].Visible = false;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Kodu = gridView.GetFocusedRowCellValue("Kodu").ToString();
            Adi = gridView.GetFocusedRowCellValue("Adi").ToString();
            Kullanimda = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Kullanimda"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            KayitEden= gridView.GetFocusedRowCellValue("KayitEden").ToString();
            Guncelleyen = gridView.GetFocusedRowCellValue("Guncelleyen").ToString();
            var kayitTarihiObj = gridView.GetFocusedRowCellValue("KayitTarihi");
            KayitTarihi = kayitTarihiObj as DateTime?;
            var guncellemeTarihiObj = gridView.GetFocusedRowCellValue("GuncellemeTarihi");
            GuncellemeTarihi = guncellemeTarihiObj as DateTime?;
            this.Close();
        }
    }
}