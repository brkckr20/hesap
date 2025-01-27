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
                        ISNULL(R.Id,0) [Id],
	                    ISNULL(R.ReceiptDate,'') [ReceiptDate],
	                    ISNULL(RI.ReceiptId,'') [ReceiptId], -- referans numarası
                        ISNULL(C.Id,'') [CompanyId],	                    
                        ISNULL(C.CompanyCode,'') [CompanyCode],
	                    ISNULL(C.CompanyName,'') [CompanyName],
	                    ISNULL(R.Explanation,'') [Explanation],
	                    --ISNULL(R.WareHouseId,'') [Depo], -- tablosu oluşturulacak
	                    ISNULL(R.InvoiceNo,'') [InvoiceNo],
	                    ISNULL(R.InvoiceDate,'') [InvoiceDate],
	                    ISNULL(R.DispatchNo,'') [DispatchNo],
	                    ISNULL(R.DispatchDate,'') [DispatchDate],
	                    ISNULL(RI.TrackingNumber,'') [TrackingNumber],
	                    ISNULL(RI.OperationType,'') [OperationType],
                        ISNULL(I.Id,'') [InventoryId],
	                    ISNULL(I.InventoryCode,'') [InventoryCode],
	                    ISNULL(I.InventoryName,'') [InventoryName],
	                    ISNULL(RI.Piece,0) [Piece],
	                    ISNULL(RI.UnitPrice,0) [UnitPrice],
	                    ISNULL(RI.Id,0) [ReceiptItemId],
	                    ISNULL(RI.UUID,'') [UUID]
                    from 
                    Receipt R with(nolock) 
	                    inner join ReceiptItem RI on R.Id = RI.ReceiptId
	                    left join Company C with(nolock)  on C.Id = R.CompanyId
	                    left join Inventory I with(nolock) on RI.InventoryId = I.Id
	                    where R.ReceiptType = {Convert.ToInt32(ReceiptTypes.MalzemeDepoGiris)}";
            listele.Liste(sql, gridControl1);
        }
        public List<Dictionary<string, object>> veriler = new List<Dictionary<string, object>>();
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView == null)
                return;
            int secilenId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            veriler.Clear();
            for (int i = 0; i < gridView.DataRowCount; i++)
            {
                int id = Convert.ToInt32(gridView.GetRowCellValue(i, "ReceiptId"));

                if (id == secilenId)
                {
                    var rowData = new Dictionary<string, object>();
                    foreach (GridColumn column in gridView.Columns)
                    {
                        var columnName = column.FieldName;
                        var cellValue = gridView.GetRowCellValue(i, columnName);
                        rowData[columnName] = cellValue;
                    }
                    veriler.Add(rowData);
                }
            }
            Close();
        }
    }
}