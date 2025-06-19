using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.IplikDepo
{
    public partial class FrmFasonaGönderilenler : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public List<string> listem = new List<string>();
        int _firmaId;
        public FrmFasonaGönderilenler(int FirmaId)
        {
            InitializeComponent();
            this._firmaId = FirmaId;
        }

        private void FrmFasonaGönderilenler_Load(object sender, EventArgs e)
        {
            string sql;
            if (this._firmaId !=0)
            {
                sql = $@"SELECT 
    ISNULL(d1.ReceiptDate, '') AS Tarih,
    ISNULL(d2.Id, '') AS TakipNo,
    ISNULL(d2.OperationType, '') AS KalemIslem,
    ISNULL(fk.Id, 0) AS FirmaId,
    ISNULL(fk.CompanyCode, '') AS FirmaKodu,
    ISNULL(fk.CompanyName, '') AS FirmaUnvan,
    ISNULL(ik.Id, '') AS IplikId,
    ISNULL(ik.InventoryCode, '') AS IplikKodu,
    ISNULL(ik.InventoryName, '') AS IplikAdi,
    ISNULL(d2.Brand, '') AS Marka,
    ISNULL(brk.Id, '') AS IplikRenkId,
    ISNULL(brk.Code, '') AS IplikRenkKodu,
    ISNULL(brk.Name, '') AS IplikRenkAdi,
    ISNULL(SUM(d2.NetWeight), 0) - 
        (SELECT ISNULL(SUM(y.GrossWeight), 0) 
         FROM Receipt x 
         INNER JOIN ReceiptItem y ON x.Id = y.ReceiptId 
         WHERE x.ReceiptType = 5 AND y.TrackingNumber = ISNULL(d2.Id, '')) AS KalanKg

FROM 
    Receipt d1 
INNER JOIN 
    ReceiptItem d2 ON d1.Id = d2.ReceiptId
LEFT JOIN 
    Company fk ON d1.CompanyId = fk.Id
LEFT JOIN 
    Inventory ik ON ik.Id = d2.InventoryId
LEFT JOIN 
    Color brk ON brk.Id = d2.ColorId
WHERE 
    d1.ReceiptType = 6
    AND d2.OperationType NOT IN ('Satış','Fason İade','Alış İade') 
     AND d1.CompanyId = '{this._firmaId}'
GROUP BY
    ISNULL(d1.ReceiptDate, ''),
    ISNULL(d2.Id, ''),
    ISNULL(d2.OperationType, ''),
    ISNULL(fk.Id, 0),
    ISNULL(fk.CompanyCode, ''),
    ISNULL(fk.CompanyName, ''),
    ISNULL(ik.Id, ''),
    ISNULL(ik.InventoryCode, ''),
    ISNULL(ik.InventoryName, ''),
    ISNULL(d2.Brand, ''),
    ISNULL(brk.Id, ''),
    ISNULL(brk.Code, ''),
    ISNULL(brk.Name, '')
HAVING 
    ISNULL(SUM(d2.NetWeight), 0) - 
    (SELECT ISNULL(SUM(y.GrossWeight), 0) 
     FROM Receipt x 
     INNER JOIN ReceiptItem y ON x.Id = y.ReceiptId
     WHERE x.ReceiptType = 5 AND y.TrackingNumber = ISNULL(d2.Id, '')) > 0

					";
            }
            else
            {
                sql = "select 'Lütfen firma seçimi yapınız' [Açıklama]";
            }
            
								listele.Liste(sql, gridControl1);
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (int rowHandle in selectedRows)
            {
                int FirmaId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "FirmaId"));
                string FirmaKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaKodu"));
                string KalemIslem = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KalemIslem"));
                string FirmaUnvan = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaUnvan"));
                int TakipNo = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "TakipNo"));
                int IplikId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "IplikId"));
                string IplikKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikKodu"));
                string IplikAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikAdi"));
                decimal BrutKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "BrutKg"));
                decimal NetKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "KalanKg"));
                decimal Fiyat = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Fiyat"));
                string DovizCinsi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "DovizCinsi"));
                string OrganikSertifikaNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "OrganikSertifikaNo"));
                string Marka = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Marka"));
                int IplikRenkId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "IplikRenkId"));
                string IplkiRenkKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikRenkKodu"));
                string IplikRenkAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikRenkAdi"));

                listem.Add($"{FirmaId};{FirmaKodu};{FirmaUnvan};{TakipNo};{IplikId};{IplikKodu};{IplikAdi};{BrutKg};{Fiyat};{DovizCinsi};" +
                    $"{OrganikSertifikaNo};{Marka};{IplikRenkId};{IplkiRenkKodu};{IplikRenkAdi};{NetKg};{KalemIslem}");
            }
            Close();
        }
    }
}