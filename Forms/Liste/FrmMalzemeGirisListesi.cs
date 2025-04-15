using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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
    public partial class FrmMalzemeGirisListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public FrmMalzemeGirisListesi()
        {
            InitializeComponent();
        }

        private void FrmMalzemeGirisListesi_Load(object sender, EventArgs e)
        {
            string sql = $@"Select
                        ISNULL(R.Id,0) [Fiş Id],
	                    ISNULL(R.ReceiptDate,'') [Fiş Tari],
	                    --ISNULL(RI.ReceiptId,'') [ReceiptId], -- referans numarası
                        ISNULL(C.Id,'') [Firma Id],	                    
                        ISNULL(C.CompanyCode,'') [Firma Kodu],
	                    ISNULL(C.CompanyName,'') [Firma Adı],
	                    ISNULL(R.Explanation,'') [Açıklama],
	                    --ISNULL(R.WareHouseId,'') [Depo], -- tablosu oluşturulacak
	                    ISNULL(R.InvoiceNo,'') [Fatura No],
	                    ISNULL(R.InvoiceDate,'') [Fatura Tarihi],
	                    ISNULL(R.DispatchNo,'') [Irsaliye No],
	                    ISNULL(R.DispatchDate,'') [Irsaliye Tarihi],
	                    ISNULL(RI.TrackingNumber,'') [Takip No],
	                    ISNULL(RI.OperationType,'') [İşlem Tipi],
                        ISNULL(I.Id,'') [Malzeme Id],
	                    ISNULL(I.InventoryCode,'') [Malzeme Kodu],
	                    ISNULL(I.InventoryName,'') [Malzeme Adı],
	                    ISNULL(RI.Piece,0) [Adet],
	                    ISNULL(RI.UnitPrice,0) [Birim Fiyat],
	                    ISNULL(RI.Id,0) [Kalem Kayıt No],
	                    ISNULL(RI.UUID,'') [UUID],
                        ISNULL(RI.RowAmount,0) [Satır Tutarı],
                        ISNULL(RI.Vat,0) [KDV %]
                    from 
                    Receipt R with(nolock) 
	                    inner join ReceiptItem RI on R.Id = RI.ReceiptId
	                    left join Company C with(nolock)  on C.Id = R.CompanyId
	                    left join Inventory I with(nolock) on RI.InventoryId = I.Id
	                    where R.ReceiptType = {Convert.ToInt32(ReceiptTypes.MalzemeDepoGiris)}";
            listele.Liste(sql, gridControl1);
        }
        public List<Dictionary<string, object>> veriler = new List<Dictionary<string, object>>();
        public List<string> liste = new List<string>();
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            // buradan devam edilecek sadece üstteki sql kodu yazıldı
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
                    string TeslimAlan = Convert.ToString(gridView.GetRowCellValue(i, "Teslim Alan"));
                    liste.Add($"{MalzemeKodu};{MalzemeAdi};{kalanAdet};{IslemTipi};{UUID};{MalzemeId};{clickedId};{TeslimAlan};");
                }
            }
            Close();
            //GridView gridView = sender as GridView;
            //if (gridView == null)
            //    return;
            //int secilenId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            //veriler.Clear();
            //for (int i = 0; i < gridView.DataRowCount; i++)
            //{
            //    int id = Convert.ToInt32(gridView.GetRowCellValue(i, "ReceiptId"));

            //    if (id == secilenId)
            //    {
            //        var rowData = new Dictionary<string, object>();
            //        foreach (GridColumn column in gridView.Columns)
            //        {
            //            var columnName = column.FieldName;
            //            var cellValue = gridView.GetRowCellValue(i, columnName);
            //            rowData[columnName] = cellValue;
            //        }
            //        veriler.Add(rowData);
            //    }
            //}
            //Close();
        }
    }
}