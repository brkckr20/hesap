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
    public partial class FrmMalzemeCikisListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public FrmMalzemeCikisListesi()
        {
            InitializeComponent();
        }

        private void FrmMalzemeCikisListesi_Load(object sender, EventArgs e)
        {
            string sql = @"
                        select 
                        d1.Id [Id]
                        ,Tarih
                        ,FirmaKodu
                        ,FirmaUnvani
                        ,DepoId
                        ,IrsaliyeNo
                        ,Aciklama
                        ,Yetkili
                        ,KalemIslem
                        ,MalzemeKodu
                        ,MalzemeAdi
                        ,Miktar
                        ,Birim
                        ,UUID
                        ,d2.Id [KayitNo]
                        ,d2.TeslimAlan
                         from MalzemeDepo1 d1 left join MalzemeDepo2 d2 on d1.Id = RefNo where d1.IslemCinsi = 'Çıkış' order by d1.Id";
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1, this.Text);
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

        private void sToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1,this.Text);
        }
    }
}