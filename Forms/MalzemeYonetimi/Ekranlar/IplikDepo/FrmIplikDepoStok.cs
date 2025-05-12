using DevExpress.XtraEditors;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.IplikDepo
{
    public partial class FrmIplikDepoStok : XtraForm
    {
        Listele listele = new Listele();
        public List<string> stokListesi = new List<string>();
        public FrmIplikDepoStok()
        {
            InitializeComponent();
        }

        private void FrmIplikDepoStok_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT 
                            ISNULL(d2.Id, 0) AS [TakipNo],
                            ISNULL(d2.OperationType, '') AS [KalemIslem],
                            ISNULL(d2.InventoryId, '') AS [IplikId],
                            ISNULL(ik.InventoryCode, '') AS [IplikKodu],
                            ISNULL(ik.InventoryName, '') AS [IplikAdi],
                            --ISNULL(ik.Organik, '') AS [Organik],
                            ISNULL(d2.Brand, '') AS [Marka],
                            --ISNULL(d2.PartiNo, '') AS [PartiNo],
                            ISNULL(d2.ColorId, 0) AS [IplikRenkId],
                            --ISNULL(brk.BoyahaneRenkKodu, '') AS [IplikRenkKodu],
                            --ISNULL(brk.BoyahaneRenkAdi, '') AS [IplikRenkAdi],
                            --SUM(ISNULL(d2.NetKg, 0)) AS [NetKg],
                            ISNULL(SUM(d2.NetWeight), 0) - 
                                (SELECT ISNULL(SUM(y.NetWeight), 0) 
                                 FROM Receipt x 
                                 INNER JOIN ReceiptItem y ON x.Id = y.ReceiptId 
                                 WHERE x.ReceiptType = 6 AND y.TrackingNumber = ISNULL(d2.Id, 0)) AS NetKg
                        FROM 
                            Receipt d1 
                        INNER JOIN 
                            ReceiptItem d2 ON d1.Id = d2.ReceiptId
                        LEFT JOIN 
                            Company fk ON d1.CompanyId = fk.Id
                        LEFT JOIN 
                            Inventory ik ON ik.Id = d2.InventoryId
                        --LEFT JOIN 
                        --    BoyahaneRenkKartlari brk ON brk.Id = d2.IplikRenkId
                        --LEFT JOIN 
                        --    OzellikKodlama ok ON ok.Id = ik.IplikNo
                        WHERE 
                            d1.ReceiptType = 5
                        GROUP BY
                            ISNULL(d2.OperationType, ''),
                            ISNULL(ik.InventoryCode, ''),
                            ISNULL(d2.InventoryId, ''),
                            ISNULL(ik.InventoryName, ''),
                            --ISNULL(ik.Organik, ''),
                            ISNULL(d2.Brand, ''),
                            --ISNULL(d2.PartiNo, ''),
                            ISNULL(d2.ColorId, 0),
                            --ISNULL(brk.BoyahaneRenkKodu, ''),
                            --ISNULL(brk.BoyahaneRenkAdi, ''),
                            ISNULL(d2.Id, 0)
                        HAVING
                            ISNULL(SUM(d2.NetWeight), 0) - 
                            (SELECT ISNULL(SUM(y.NetWeight), 0) 
                             FROM Receipt x 
                             INNER JOIN ReceiptItem y ON x.Id = y.ReceiptId
                             WHERE x.ReceiptType = 6 AND y.TrackingNumber = ISNULL(d2.Id, 0)) <> 0

";
            listele.Liste(sql, gridControl1);
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                string KalemIslem = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KalemIslem"));
                string IplikKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikKodu"));
                int IplikId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "IplikId"));
                string IplikAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikAdi"));
                bool Organik = Convert.ToBoolean(gridView1.GetRowCellValue(rowHandle, "Organik"));
                string Marka = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Marka"));
                string PartiNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "PartiNo"));
                int IplikRenkId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "IplikRenkId"));
                string IplkiRenkKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikRenkKodu"));
                string IplikRenkAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikRenkAdi"));
                decimal NetKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "NetKg"));
                string TakipNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "TakipNo"));


                stokListesi.Add($"{KalemIslem};{IplikKodu};{IplikId};{IplikAdi};{Organik};{Marka};{PartiNo};{IplikRenkId};{IplkiRenkKodu};" +
                    $"{IplikRenkAdi};{NetKg};{TakipNo}");
            }
            Close();
        }
    }
}