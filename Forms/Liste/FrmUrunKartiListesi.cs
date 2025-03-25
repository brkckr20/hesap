using Dapper;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
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
        CrudRepository crudRepository = new CrudRepository();
        public string UrunKodu, UrunAdi;
        public bool Pasif;
        public int Id,InventoryType;
        public FrmUrunKartiListesi(int inv_type)
        {
            InitializeComponent();
            this.InventoryType = inv_type;
        }

        private void FrmUrunKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
            crudRepository.GetUserColumns(gridView1,this.Text);
        }
        void Listele()
        {
            gridControl1.DataSource = crudRepository.GetAll<Inventory>("Inventory")
                .Where(inv => inv.IsPrefix == false && inv.Type == InventoryType)
                .Select(inv => new { 
                    UrunKodu =inv.InventoryCode, 
                    UrunAdi =inv.InventoryName,
                    Kullanimda = inv.IsUse,
                    inv.Id })
                .ToList();
        }

        private void satırİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
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
            Pasif = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Kullanimda"));
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            this.Close();
        }
    }
}