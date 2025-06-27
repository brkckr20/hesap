using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.HamDepo
{
    public partial class FrmHamDepoStok : DevExpress.XtraEditors.XtraForm
    {
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public List<string> stokListesi = new List<string>();
        Listele listele = new Listele();
        public FrmHamDepoStok()
        {
            InitializeComponent();
        }

        private void FrmHamDepoStok_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT ISNULL(d2.Id, 0) AS [TakipNo], ISNULL(d2.OperationType, '') AS [KalemIslem], ISNULL(d2.InventoryId, '') AS [IplikId], ISNULL(ik.InventoryCode, '') AS [IplikKodu], ISNULL(ik.InventoryName, '') AS [IplikAdi], ISNULL(d2.Brand, '') AS [Marka], ISNULL(brk.Id, 0) AS [IplikRenkId], ISNULL(brk.Code, '') AS [IplikRenkKodu], ISNULL(brk.Name, '') AS [IplikRenkAdi], ISNULL(SUM(d2.NetWeight), 0) - (SELECT ISNULL(SUM(y.GrossWeight), 0) FROM Receipt x INNER JOIN ReceiptItem y ON x.Id = y.ReceiptId WHERE x.ReceiptType = 9 AND y.TrackingNumber = ISNULL(d2.Id, 0)) AS [Kalan Kg] FROM Receipt d1 INNER JOIN ReceiptItem d2 ON d1.Id = d2.ReceiptId LEFT JOIN Company fk ON d1.CompanyId = fk.Id LEFT JOIN Inventory ik ON ik.Id = d2.InventoryId LEFT JOIN Color brk ON brk.Id = d2.ColorId WHERE d1.ReceiptType = 8 GROUP BY ISNULL(d2.OperationType, ''), ISNULL(ik.InventoryCode, ''), ISNULL(d2.InventoryId, ''), ISNULL(ik.InventoryName, ''), ISNULL(d2.Brand, ''), ISNULL(brk.Id, 0), ISNULL(brk.Code, ''), ISNULL(brk.Name, ''), ISNULL(d2.Id, 0) HAVING ISNULL(SUM(d2.NetWeight), 0) - (SELECT ISNULL(SUM(y.GrossWeight), 0) FROM Receipt x INNER JOIN ReceiptItem y ON x.Id = y.ReceiptId WHERE x.ReceiptType = 9 AND y.TrackingNumber = ISNULL(d2.Id, 0)) <> 0";
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1, this.Text);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1, this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                string KumasKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KumasKodu"));
                string KumasAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KumasAdi"));
                int GrM2 = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "GrM2"));
                decimal BrutKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "BrutKg"));
                decimal NetKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "KalanNetKg"));
                decimal NetMt = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "NetMt"));
                int BoyahaneRenkId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "BoyahaneRenkId"));
                string BoyahaneRenkKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "BoyahaneRenkKodu"));
                string BoyahaneRenkAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "BoyahaneRenkAdi"));
                string TakipNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "TakipNo"));
                int KumasId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "KumasId"));


                stokListesi.Add($"{KumasKodu};{KumasAdi};{GrM2};{BrutKg};{NetKg};{NetMt};{BoyahaneRenkId};{BoyahaneRenkKodu};{BoyahaneRenkAdi};" +
                    $"{TakipNo};{KumasId}");
            }
            Close();
        }
    }
}