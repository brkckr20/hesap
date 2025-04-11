using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
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
        CrudRepository crudRepository = new CrudRepository();
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
            yardimciAraclar.ArkaPlaniDegistir(e, "Kalan Adet");
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        public FrmMalzemeDepoStok()
        {
            InitializeComponent();
            crudRepository.GetUserColumns(gridView1,this.Text);
        }
        private void FrmMalzemeDepoStok_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT
                        d1.Id as Id,
                        d1.ReceiptDate,
						d2.OperationType,
                        d1.CompanyId,
						C.CompanyName,
						d2.InventoryId,
						MK.InventoryCode,
						MK.InventoryName,
                        ISNULL(SUM(d2.Piece) - COALESCE((
                            SELECT SUM(y.Piece)
                            FROM Receipt x
                            INNER JOIN ReceiptItem y ON x.Id = y.ReceiptId
                            WHERE y.UUID = d2.UUID AND x.ReceiptType = 3
                        ), 0), 0) AS [Kalan Adet],
                        d2.UUID
                    FROM Receipt d1
                    INNER JOIN ReceiptItem d2 ON d1.Id = d2.ReceiptId
					left join Inventory MK on MK.Id = d2.InventoryId
					left join Company C on C.Id = d1.CompanyId
                    WHERE d1.ReceiptType = 1
                    GROUP BY
                        d1.Id,
                        d1.ReceiptDate,
						d2.OperationType,
                        d1.CompanyId,
						d2.InventoryId,
						MK.InventoryCode,
						MK.InventoryName,
						C.CompanyName,
                        d2.UUID
					having ISNULL(SUM(d2.Piece) - COALESCE((
                            SELECT SUM(y.Piece)
                            FROM Receipt x
                            INNER JOIN ReceiptItem y ON x.Id = y.ReceiptId
                            WHERE y.UUID = d2.UUID AND x.ReceiptType = 3
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