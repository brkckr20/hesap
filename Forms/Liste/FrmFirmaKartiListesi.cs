using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Forms.Liste
{
    public partial class FrmFirmaKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        Baglanti _baglanti;
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public string FirmaKodu, FirmaUnvan, Adres1, Adres2, Adres3;
        public int Id;

        private void excelDosyasıxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"Firma Kartları");
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1,this.Text);
        }

        public FrmFirmaKartiListesi()
        {
            InitializeComponent();
            _baglanti = new Baglanti();
        }

        private void FrmFirmaKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = "SELECT * FROM FirmaKarti";
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            FirmaKodu = gridView.GetFocusedRowCellValue("FirmaKodu").ToString();
            FirmaUnvan = gridView.GetFocusedRowCellValue("FirmaUnvan").ToString();
            Adres1 = gridView.GetFocusedRowCellValue("Adres1").ToString();
            Adres2 = gridView.GetFocusedRowCellValue("Adres2").ToString();
            Adres3 = gridView.GetFocusedRowCellValue("Adres3").ToString();
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}