using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
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
    public partial class FrmUrunReceteKartiListesi : DevExpress.XtraEditors.XtraForm
    {
        public int Id, InventoryId;
        public float HamGr_M2, HamEn, HamBoy, MamulEn, MamulBoy, MamulGr_M2;
        public bool IpligiBoyali;
        public string ReceteNo,Aciklama;
        private string TableName = "InventoryReceipt";
        public byte[] UrunResmi;
        CrudRepository crudRepository = new CrudRepository();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();

        private void excelAktarxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"Ürün Reçete Listesi");
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1, this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }


        public FrmUrunReceteKartiListesi(int _InventoryId)
        {
            InitializeComponent();
            this.InventoryId = _InventoryId;
        }
        private void FrmUrunReceteKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
            crudRepository.GetUserColumns(gridView1,this.Text);
        }
        void Listele()
        {
            if (this.InventoryId != 0)
            {
                gridControl1.DataSource = crudRepository.GetAll<InventoryReceipt>(this.TableName).Where(rec => rec.InventoryId == this.InventoryId);
            }
            else
            {
                gridControl1.DataSource = crudRepository.GetAll<InventoryReceipt>(this.TableName);
            }
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            ReceteNo = gridView.GetFocusedRowCellValue("ReceiptNo").ToString();
            HamGr_M2 = Convert.ToSingle(gridView.GetFocusedRowCellValue("RawGrammage"));
            MamulGr_M2 = Convert.ToSingle(gridView.GetFocusedRowCellValue("ProductGrammage"));
            HamEn = Convert.ToSingle(gridView.GetFocusedRowCellValue("RawWidth"));
            HamBoy = Convert.ToSingle(gridView.GetFocusedRowCellValue("RawHeight"));
            MamulEn = Convert.ToSingle(gridView.GetFocusedRowCellValue("ProductWidth"));
            MamulBoy = Convert.ToSingle(gridView.GetFocusedRowCellValue("ProductHeight"));
            UrunResmi = (byte[])gridView.GetFocusedRowCellValue("ReceiptImage1");
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            InventoryId = Convert.ToInt32(gridView.GetFocusedRowCellValue("InventoryId"));
            Aciklama = gridView.GetFocusedRowCellValue("Explanation").ToString();
            IpligiBoyali = Convert.ToBoolean(gridView.GetFocusedRowCellValue("YarnDyed"));
            this.Close();
        }
    }
}