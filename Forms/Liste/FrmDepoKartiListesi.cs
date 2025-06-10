using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Linq;

namespace Hesap.Forms.Liste
{
    public partial class FrmDepoKartiListesi : XtraForm
    {
        CrudRepository crudRepository = new CrudRepository();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        string TableName = "WareHouse";
        public FrmDepoKartiListesi()
        {
            InitializeComponent();
        }
        public string Kodu, Adi;
        public int Id;
        public bool Kullanimda;

        private void FrmDepoKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            gridControl1.DataSource = crudRepository.GetAll<WareHouse>(this.TableName).ToList();
            crudRepository.GetUserColumns(gridView1, this.Text);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1, this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void excelxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1, "Depo Kartları Listesi");
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Kodu = gridView.GetFocusedRowCellValue("Code").ToString();
            Adi = gridView.GetFocusedRowCellValue("Name").ToString();
            Kullanimda = Convert.ToBoolean(gridView.GetFocusedRowCellValue("IsUse"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}