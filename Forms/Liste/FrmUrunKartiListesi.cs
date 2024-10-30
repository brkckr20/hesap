using Dapper;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Forms.Liste
{
    public partial class FrmUrunKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public string UrunKodu, UrunAdi;
        public bool Pasif;
        public int Id;
        public FrmUrunKartiListesi()
        {
            InitializeComponent();
        }

        private void FrmUrunKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = "SELECT Id,UrunKodu,UrunAdi,Pasif FROM UrunKarti where Pasif = 0";
            listele.Liste(sql, gridControl1);
        }

        private void excelDosyasıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1, this.Text);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

            GridView gridView = sender as GridView;
            UrunKodu = gridView.GetFocusedRowCellValue("UrunKodu").ToString();
            UrunAdi = gridView.GetFocusedRowCellValue("UrunAdi").ToString();
            Pasif = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Pasif"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}