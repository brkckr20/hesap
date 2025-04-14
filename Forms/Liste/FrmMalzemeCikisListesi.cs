using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Hesap.Forms.Liste
{
    public partial class FrmMalzemeCikisListesi : XtraForm
    {
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public List<string> liste = new List<string>();
        CrudRepository crudRepository = new CrudRepository();
        public FrmMalzemeCikisListesi()
        {
            InitializeComponent();
        }

        private void FrmMalzemeCikisListesi_Load(object sender, EventArgs e)
        {
            string sql = @"
                        select 
	                        ISNULL(R.Id,0) [Fiş Id]
	                        ,ISNULL(R.ReceiptDate,'') [Tarih]
	                        ,ISNULL(C.Id,'') [Firma Id]
	                        ,ISNULL(C.CompanyCode,'') [Firma Kodu]
	                        ,ISNULL(C.CompanyName,'') [Firma Adı]
	                        ,ISNULL(R.WareHouseId,'') [Depo Id]
	                        ,ISNULL(R.ReceiptNo,'') [Irsaliye No]
	                        ,ISNULL(R.Explanation,'') [Açıklama]
	                        ,ISNULL(R.Authorized,'') [Yetkili]
	                        ,ISNULL(RI.OperationType,'') [İşlem Tipi]
	                        ,ISNULL(RI.InventoryId,'') [Malzeme Id]
	                        ,ISNULL(I.InventoryCode,'') [Malzeme Kodu]
	                        ,ISNULL(I.InventoryName,'') [Malzeme Adı]
	                        ,ISNULL(RI.Piece,0) [Adet]
	                        ,ISNULL(RI.UUID,'') [UUID]
                            ,ISNULL(RI.Receiver,'') [Teslim Alan]
	                        ,ISNULL(RI.Id,'') [Kalem Kayıt No]
                        from Receipt R 
                        left join ReceiptItem RI on R.Id = RI.ReceiptId
                        left join Company C on C.Id = R.CompanyId
                        left join Inventory I on I.Id = RI.InventoryId
                        where R.ReceiptType = 3";
            listele.Liste(sql, gridControl1);
            crudRepository.GetUserColumns(gridView1, this.Text);
        }
        public List<Dictionary<string, object>> veriler = new List<Dictionary<string, object>>();
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            int clickedId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Fiş Id"));
            for (int i = 0; i < gridView.DataRowCount; i++)
            {
                int currentId = Convert.ToInt32(gridView.GetRowCellValue(i, "Fiş Id"));
                if (currentId == clickedId)
                {
                    string MalzemeKodu = Convert.ToString(gridView.GetRowCellValue(i, "Malzeme Kodu"));
                    string MalzemeAdi = Convert.ToString(gridView.GetRowCellValue(i, "Malzeme Adı"));
                    int kalanAdet = Convert.ToInt32(gridView.GetRowCellValue(i, "Adet"));
                    string IslemTipi = Convert.ToString(gridView.GetRowCellValue(i, "İşlem Tipi"));
                    string UUID = Convert.ToString(gridView.GetRowCellValue(i, "UUID"));
                    int MalzemeId = Convert.ToInt32(gridView.GetRowCellValue(i, "Malzeme Id"));
                    string TeslimAlan= Convert.ToString(gridView.GetRowCellValue(i, "Teslim Alan"));
                    liste.Add($"{MalzemeKodu};{MalzemeAdi};{kalanAdet};{IslemTipi};{UUID};{MalzemeId};{clickedId};{TeslimAlan};");
                }
            }
            Close();
        }

        private void sToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1,this.Text);
        }
    }
}