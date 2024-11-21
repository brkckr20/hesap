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

namespace Hesap.Forms.MalzemeYonetimi
{
    public partial class FrmMalzemeDepoStok : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public string MalzemeKodu, MalzemeAdi, UUID, Birim;
        public List<string> malzemeBilgileri = new List<string>();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
           
            foreach (int rowHandle in selectedRows)
            {
                string MalzemeKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "MalzemeKodu"));
                string MalzemeAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "MalzemeAdi"));
                string UUID = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "UUID"));
                string birim = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Birim"));
                malzemeBilgileri.Add($"{MalzemeKodu};{MalzemeAdi};{UUID};{birim}");
            }
            Close();
        }

        private void excelxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"Malzeme Stok Listesi");
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            yardimciAraclar.ArkaPlaniDegistir(e, "Kalan");
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        public FrmMalzemeDepoStok()
        {
            InitializeComponent();
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
        }
        private void FrmMalzemeDepoStok_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT
                        d1.Id as Id,
                        d1.Tarih,
                        d1.FirmaKodu,
						d2.MalzemeKodu,
						d2.MalzemeAdi,
                        ISNULL(SUM(d2.Miktar) - COALESCE((
                            SELECT SUM(y.Miktar)
                            FROM MalzemeDepo1 x
                            INNER JOIN MalzemeDepo2 y ON x.Id = y.RefNo
                            WHERE y.UUID = d2.UUID AND x.IslemCinsi = 'Çıkış'
                        ), 0), 0) AS Kalan,
                        d2.Birim,
                        d2.UUID
                    FROM MalzemeDepo1 d1
                    INNER JOIN MalzemeDepo2 d2 ON d1.Id = d2.RefNo
					left join MalzemeKarti MK on MK.Kodu = d2.MalzemeKodu
                    WHERE d1.IslemCinsi = 'Giriş' and MK.Tip <> 1
                    GROUP BY
                        d1.Id,
                        d1.Tarih,
                        d1.FirmaKodu,
						d2.MalzemeKodu,
						d2.MalzemeAdi,
                        d2.Birim,
                        d2.UUID
					having ISNULL(SUM(d2.Miktar) - COALESCE((
                            SELECT SUM(y.Miktar)
                            FROM MalzemeDepo1 x
                            INNER JOIN MalzemeDepo2 y ON x.Id = y.RefNo
                            WHERE y.UUID = d2.UUID AND x.IslemCinsi = 'Çıkış'
                        ), 0), 0) <> 0";
            listele.Liste(sql, gridControl1);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            int focusedRowHandle = gridView.FocusedRowHandle;
            if (focusedRowHandle < 0)
                return;
            MalzemeKodu = gridView.GetRowCellValue(focusedRowHandle, "MalzemeKodu").ToString();
            MalzemeAdi = gridView.GetRowCellValue(focusedRowHandle, "MalzemeAdi").ToString();
            UUID = gridView.GetRowCellValue(focusedRowHandle, "UUID").ToString();
            Birim = gridView.GetRowCellValue(focusedRowHandle, "Birim").ToString();
            Close();
        }
    }
}