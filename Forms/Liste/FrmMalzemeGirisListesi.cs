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
            string sql = @"SELECT 
	                        M1.Id,
	                        M1.Tarih,
	                        ISNULL(M1.FirmaKodu,'') [FirmaKodu],
	                        ISNULL(M1.FirmaUnvani,'') [FirmaUnvani],
	                        ISNULL(M1.Aciklama,'') [Aciklama],
	                        ISNULL(M1.DepoId,'') [DepoId],
	                        ISNULL(M1.FaturaNo,'') [FaturaNo],
	                        ISNULL(M1.FaturaTarihi,'') [FaturaTarihi],
	                        ISNULL(M1.IrsaliyeNo,'') [IrsaliyeNo],
	                        ISNULL(M2.TakipNo,'') [TakipNo],
	                        ISNULL(M2.KalemIslem,'') [KalemIslem],
	                        ISNULL(M2.MalzemeKodu,'') [MalzemeKodu],
	                        ISNULL(M2.MalzemeAdi,'') [MalzemeAdi],
	                        ISNULL(M2.Miktar,0) [Miktar],
	                        ISNULL(M2.Birim,'') [Birim],
	                        ISNULL(M2.BirimFiyat,0) [BirimFiyat],
	                        ISNULL(M2.UUID,'') [UUID],
							ISNULL(M2.Id,'') [D2Id]
                        FROM MalzemeDepo1 M1 INNER JOIN MalzemeDepo2 M2 on M1.Id = M2.RefNo where M1.IslemCinsi = 'Giriş' order by M1.Tarih";
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
                int id = Convert.ToInt32(gridView.GetRowCellValue(i, "Id"));

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