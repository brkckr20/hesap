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

namespace Hesap.Forms.UretimYonetimi.Barkod
{
    public partial class FrmEtiketBasimListesi : DevExpress.XtraEditors.XtraForm
    {
        public FrmEtiketBasimListesi()
        {
            InitializeComponent();
        }
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public string sql;

        public List<Dictionary<string, object>> veriler = new List<Dictionary<string, object>>();

        private void FrmEtiketBasimListesi_Load(object sender, EventArgs e)
        {
            sql = @"select 
						ISNULL(e1.Id,0) Id,
						ISNULL(e1.Tarih,'') Tarih,
						ISNULL(e1.Aciklama,'') Aciklama,
						ISNULL(e1.BasimSayisi,0) BasimSayisi,
						ISNULL(e1.Yuzde,0) Yuzde,
						ISNULL(e.UrunKodu,0) [UrunKodu],
						ISNULL(e.ArtNo,0) [ArtNo],
						ISNULL(e.Sticker1,0) [Sticker1],
						ISNULL(e.Sticker2,0) [Sticker2],
						ISNULL(e.Sticker3,0) [Sticker3],
						ISNULL(e.Sticker4,0) [Sticker4],
						ISNULL(e.Sticker5,0) [Sticker5],
						ISNULL(e.Sticker6,0) [Sticker6],
						ISNULL(e.Sticker7,0) [Sticker7],
						ISNULL(e.Sticker8,0) [Sticker8],
						ISNULL(e.Sticker9,0) [Sticker9],
						ISNULL(e.Sticker10,0) [Sticker10],
						ISNULL(e.MusteriOrderNo,0) [MusteriOrderNo],
						ISNULL(e.OrderNo,0) [OrderNo],
						ISNULL(e.Varyant1,0) [Varyant1],
						ISNULL(e.Varyant2,0) [Varyant2],
						ISNULL(e.Miktar,0) [Miktar],
						ISNULL(e.Id,0) [D2Id]

					 from Etiket1 e1 inner join Etiket e on e.RefNo = e1.Id";
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1, this.Text);
        }

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