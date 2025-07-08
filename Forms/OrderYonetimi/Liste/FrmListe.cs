using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Linq;

namespace Hesap.Forms.OrderYonetimi.Liste
{
    public partial class FrmListe : DevExpress.XtraEditors.XtraForm
    {
        public FrmListe()
        {
            InitializeComponent();
        }
        string _ekranAdi;
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CrudRepository crudRepository = new CrudRepository();
        public int Id,Tip;
        public string Adi, OrjAdi;
        public bool Kullanimda;
        public int where_sarti;
        public FrmListe(string ekranAdi)
        {
            InitializeComponent();
            this.Text = ekranAdi + " Listesi";
            this._ekranAdi = ekranAdi;
        }
        void Listele()
        {
            if (this._ekranAdi == "Kategori Kartı")
            {
                where_sarti = Convert.ToInt32(LookupTypes.Kategori);
            }
            else if(this._ekranAdi == "Cinsi Kartı")
            {
                where_sarti = Convert.ToInt32(LookupTypes.Cinsi);
            }
            gridControl1.DataSource = crudRepository.GetAll<Lookup>("Lookup").Where(x => x.Type == where_sarti).ToList();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Adi = gridView.GetFocusedRowCellValue("Name").ToString();
            OrjAdi = gridView.GetFocusedRowCellValue("OriginalName").ToString();
            //Kullanimda = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Kullanimda"));
            Tip = Convert.ToInt32(gridView.GetFocusedRowCellValue("Type"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1,this.Text);
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
            Listele();
            crudRepository.GetUserColumns(gridView1,this.Text);
        }
        
    }
}