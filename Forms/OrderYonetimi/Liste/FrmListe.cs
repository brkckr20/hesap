using Dapper;
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

namespace Hesap.Forms.OrderYonetimi.Liste
{
    public partial class FrmListe : DevExpress.XtraEditors.XtraForm
    {
        public FrmListe()
        {
            InitializeComponent();
        }
        string _ekranAdi;
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public int Id;
        public string Adi, OrjAdi;
        public bool Kullanimda;
        public FrmListe(string ekranAdi)
        {
            InitializeComponent();
            this.Text = ekranAdi + " Listesi";
            this._ekranAdi = ekranAdi;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Adi = gridView.GetFocusedRowCellValue("Adi").ToString();
            OrjAdi = gridView.GetFocusedRowCellValue("OrjAdi").ToString();
            Kullanimda = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Kullanimda"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void excelAktarxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"Kategori Kartları Listesi");
        }

        private void FrmListe_Load(object sender, EventArgs e)
        {
            string sql = $"select * from OzellikKarti where EkranAdi = '{this._ekranAdi}'";
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
        }
        
    }
}