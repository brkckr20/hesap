using Hesap.Utils;
using System;
using System.Collections.Generic;
using Hesap.DataAccess;

namespace Hesap.Forms.MalzemeYonetimi
{
    public partial class FrmIslemBekleyenler : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        CrudRepository crudRepository = new CrudRepository();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public FrmIslemBekleyenler()
        {
            InitializeComponent();
        }

        private void FrmIslemBekleyenler_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT 
                        ISNULL(d1.ReceiptDate,'') [Fiş Tarihi],
						ISNULL(d1.CompanyId,0) [Firma Id],
						ISNULL(c.CompanyCode,'') [Firma Kodu],
                        ISNULL(c.CompanyName,'') [Firma Adı],
                        ISNULL(d2.OperationType,'') [Kalem İşlem],
                        ISNULL(d2.InventoryId,0) [Malzeme Id],
						ISNULL(d2.UUID,0) [UUID],
						ISNULL(MK.InventoryCode,'') [Malzeme Kodu],
						ISNULL(MK.InventoryName,'') [Malzeme Adı],
                        ISNULL(SUM(d2.Piece) - COALESCE((
                            SELECT SUM(y.Piece)
                            FROM Receipt x
                            INNER JOIN ReceiptItem y ON x.Id = y.ReceiptId
                            WHERE x.ReceiptType = 1 AND d2.Id = y.TrackingNumber
                        ), 0), 0) AS Kalan,
                        d2.ReceiptId [Fiş Id],
                        d2.Id [Takip No]
                    FROM Receipt d1
                    LEFT JOIN ReceiptItem d2 ON d1.Id = d2.ReceiptId
					left join Company C on C.Id = d1.CompanyId
					LEFT join Inventory MK on MK.Id= d2.InventoryId
                    WHERE d1.ReceiptType = 3 AND d2.OperationType IN ('Dolum','Tamir')
                    GROUP BY
                        ISNULL(d1.ReceiptDate,''),
						ISNULL(d1.CompanyId,0),
                        ISNULL(c.CompanyCode,''),
                        ISNULL(c.CompanyName,''),
						d2.Id,
						d2.ReceiptId,
						ISNULL(d2.OperationType,''),
                        ISNULL(d2.InventoryId,0),
						ISNULL(MK.InventoryCode,''),
						ISNULL(MK.InventoryName,''),
						ISNULL(d2.UUID,0)
                    HAVING ISNULL(SUM(d2.Piece) - COALESCE((
                        SELECT SUM(y.Piece)
                        FROM Receipt x
                        INNER JOIN ReceiptItem y ON x.Id = y.ReceiptId
                        WHERE x.ReceiptType = 1 AND d2.Id = y.TrackingNumber
                    ), 0), 0) <> 0;";
            listele.Liste(sql, gridControl1);
            crudRepository.GetUserColumns(gridView1,this.Text);
        }
        public List<string> malzemeBilgileri = new List<string>();

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (int rowHandle in selectedRows)
            {
                string KalemIslem = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Kalem İşlem")); //OperationType
                string MalzemeKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Malzeme Kodu")); // InventoryCode
                string MalzemeAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Malzeme Adı")); //InventoryName
                int MalzemeId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Malzeme Id")); // InventoryId
                int Kalan = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Kalan")); // Piece - kalan
                int TakipNo = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Takip No"));
                malzemeBilgileri.Add($"{KalemIslem};{MalzemeKodu};{MalzemeAdi};{MalzemeId};{Kalan};{TakipNo}");
            }
            Close();
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void excelOlarakAktarxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"İşlemler Bekleyenler Listesi");
        }
    }
}