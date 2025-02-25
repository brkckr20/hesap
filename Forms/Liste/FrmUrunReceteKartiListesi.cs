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
        public string ReceteNo;
        private string TableName = "InventoryReceipt";
        public byte[] UrunResmi;
        CrudRepository crudRepository = new CrudRepository();
        public FrmUrunReceteKartiListesi(int _InventoryId)
        {
            InitializeComponent();
            this.InventoryId = _InventoryId;
        }
        private void FrmUrunReceteKartiListesi_Load(object sender, EventArgs e)
        {
            Listele();
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
            this.Close();
        }
    }
}