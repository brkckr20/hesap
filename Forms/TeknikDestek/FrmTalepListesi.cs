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

namespace Hesap.Forms.TeknikDestek
{
    public partial class FrmTalepListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public string sql;
        public FrmTalepListesi()
        {
            InitializeComponent();
        }
        public List<Dictionary<string, object>> veriler = new List<Dictionary<string, object>>();      
        private void FrmTalepListesi_Load(object sender, EventArgs e)
        {
            sql = @"select 
                            ISNULL(T.Id,'') Id,
                            ISNULL(T.Tarih,'') Tarih,
                            ISNULL(T.Departman,'') Departman,
                            ISNULL(T.Baslik,'') Baslik,
                            ISNULL(T.Aciklama,'') Aciklama,
                            ISNULL(T.Ek,'') Ek,
                            ISNULL(T.Durum,'') Durum,
                            ISNULL(T.GorusmeId,'') GorusmeId,
                            ISNULL(T.TamamlanmaTarihi,'') TamamlanmaTarihi,
                            ISNULL(T.Kullanici,'') Kullanici,
                            CONVERT(varbinary(max), T.Resim) Resim,
							ISNULL(TG.SiraNo,0) [SiraNo],
							ISNULL(TG.GorusmeTarihi,'') [GorusmeTarihi],
							ISNULL(TG.GorusmeNotu,'') [GorusmeNotu],
							ISNULL(TG.Not1,'') [Not1],
							ISNULL(TG.Not2,'') [Not2],
							ISNULL(TG.Not3,'') [Not3],
							ISNULL(TG.Id,'') [D2Id]
                            from Talepler T left join TaleplerGorusme TG on T.Id = TG.RefNo
                            order by TG.RefNo,T.Tarih asc
";
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);

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
        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1,this.Text);
        }
        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void excelxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"Talepler Listesi");
        }
    }
}