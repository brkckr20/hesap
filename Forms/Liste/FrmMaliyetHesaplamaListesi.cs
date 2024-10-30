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
    public partial class FrmMaliyetHesaplamaListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public FrmMaliyetHesaplamaListesi()
        {
            InitializeComponent();
        }

        private void FrmMaliyetHesaplamaListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = @"select * from UretimBaslik UBA left join UretimBilgileri UBI on UBI.RefNo = UBA.Id
                            left join UretimHesaplama UHE on UHE.RefNo = UBA.Id
                            left join MaliyetHesaplama MHE on MHE.RefNo = UBA.Id
							inner join FirmaKarti FK on FK.FirmaKodu = UBA.FirmaKodu";
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