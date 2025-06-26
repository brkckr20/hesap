using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.IplikDepo
{
    public partial class FrmTalimatlar : XtraForm
    {
		Listele listele = new Listele();
        public List<string> satinAlmaListesi = new List<string>();
        CrudRepository crudRepository = new CrudRepository();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        //Kumaş satın alma ekranında da diğer ekranlarda da bu kullanılacak
        public int _receiptType,_entryReceiptType;
        public FrmTalimatlar(ReceiptTypes receiptTypes)
        {
            InitializeComponent();
            this._receiptType = Convert.ToInt32(receiptTypes);
            SetText();
            SetEntryReceiptType();
        }
        void SetEntryReceiptType() // bu metodun amacı iplik depo giriş ise talimata göre hesaplama yapacak, AllTypes.cs den incelenebilir.
        {
            switch (this._receiptType)
            {
                case 4: //talimat no iplik satın alma ise 
                    _entryReceiptType = 5; // giriş deposu iplik giriş
                    break;
                case 7: //kumas s.a talimatı
                    _entryReceiptType = 8; // ham depo girişi
                    break;


                default:
                    break;
            }
        }

        void SetText()
        {
            switch (this._receiptType)
            {
                case 4:
                    this.Text += " [ İplik ]";
                    break;
                case 7:
                    this.Text += " [ Kumaş ]";
                    break;
                default:
                    break;
            }
        }

        private void FrmTalimatlar_Load(object sender, EventArgs e)
        {
			
			string sql = $@"SELECT
	                        ISNULL(d1.ReceiptNo, '') AS TalimatNo,	
	                        ISNULL(d1.ReceiptDate, '') AS Tarih,
                            ISNULL(fk.Id, 0) AS FirmaId,
	                        ISNULL(fk.CompanyCode, '') AS FirmaKodu,
                            ISNULL(fk.CompanyName, '') AS FirmaUnvan,
	                        ISNULL(d2.InventoryId, '') AS IplikId,
	                        ISNULL(ik.InventoryCode, '') AS IplikKodu,
	                        ISNULL(ik.InventoryName, '') AS IplikAdi,
							ISNULL(d2.UnitPrice, 0) AS BirimFiyat,
							ISNULL(d2.MeasurementUnit, '') AS HesapBirimi,
	                         ISNULL(SUM(d2.NetWeight), 0) - (select ISNULL(sum(y.GrossWeight),0) from Receipt x inner join ReceiptItem y on x.Id = y.ReceiptId where x.ReceiptType = {_entryReceiptType} and y.TrackingNumber = d2.Id) [Kalan Kg],
                            ISNULL(d2.Vat, '') AS [KDV %],
	                         ISNULL(d2.Id,0) TakipNo,
							 ISNULL(CO.Id,0) [Renk Id],
							 ISNULL(CO.Code,'') [Renk No],
							 ISNULL(CO.Name,'') [Renk Adı]
                        FROM 
                            Receipt d1 
                            INNER JOIN ReceiptItem d2 ON d1.Id = d2.ReceiptId
                            left JOIN Company fk ON d1.CompanyId = fk.Id
	                        left join Inventory ik on ik.Id = d2.InventoryId
							left join Color CO on CO.Id = d2.ColorId
	                        where d1.ReceiptType = {Convert.ToInt32(this._receiptType)} and d1.Approved = 1 and d1.IsFinished = 0
                        GROUP BY 
                            ISNULL(d1.ReceiptNo, ''),
	                        ISNULL(d1.ReceiptDate, ''),
                            ISNULL(fk.Id, 0),
                            ISNULL(fk.CompanyCode, ''),
                            ISNULL(fk.CompanyName, ''),
                            ISNULL(d2.InventoryId, ''),
	                        ISNULL(ik.InventoryCode, ''),
	                        ISNULL(ik.InventoryName, '')
	                        ,d2.Id
	                        ,ISNULL(d2.Id,0)
							,ISNULL(d2.UnitPrice, 0)
							,ISNULL(d2.MeasurementUnit, '')
                            ,ISNULL(d2.Vat, '')
							,ISNULL(CO.Id,0)
							,ISNULL(CO.Code,'')
							,ISNULL(CO.Name,'')
	                        HAVING 
	                         ISNULL(SUM(d2.NetWeight), 0) - (select ISNULL(sum(y.GrossWeight),0) from Receipt x inner join ReceiptItem y on x.Id = y.ReceiptId where x.ReceiptType = {_entryReceiptType} and y.TrackingNumber = d2.Id) > 0
";
            listele.Liste(sql, gridControl1);
            crudRepository.GetUserColumns(gridView1,this.Text);
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            //bazı alanlar sorgudan kaldırıldı fakat alt kısım sorgudan bağımsız kendi sıralamasına göre çalıştığı için problem olmadı
            foreach (int rowHandle in selectedRows)
            {
                string TalimatNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "TalimatNo"));
				int FirmaId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "FirmaId"));
                string FirmaKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaKodu"));
                string FirmaUnvan = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaUnvan"));
				int TakipNo = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "TakipNo"));
				int IplikId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "IplikId"));
                string IplikKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikKodu"));
                string IplikAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikAdi"));
				decimal BrutKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Kalan Kg"));
				decimal NetKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "NetKg"));
				decimal Fiyat = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Fiyat"));
                string DovizCinsi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "DovizCinsi"));
                string OrganikSertifikaNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "OrganikSertifikaNo"));
                string Marka = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Marka"));
				int IplikRenkId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "IplikRenkId"));
                string IplkiRenkKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikRenkKodu"));
                string IplikRenkAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "IplikRenkAdi"));
                decimal BirimFiyat = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "BirimFiyat"));
                string HesapBirimi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "HesapBirimi"));
				int KDV = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "KDV %"));
                int RenkId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Renk Id"));
                string RenkNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Renk No"));
                string RenkAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Renk Adı"));
                satinAlmaListesi.Add($"{TalimatNo};{FirmaId};{FirmaKodu};{FirmaUnvan};{TakipNo};{IplikId};{IplikKodu};{IplikAdi};{BrutKg};{Fiyat};{DovizCinsi};" +
					$"{OrganikSertifikaNo};{Marka};{IplikRenkId};{IplkiRenkKodu};{IplikRenkAdi};{NetKg};{BirimFiyat};{HesapBirimi};{KDV};{RenkId};{RenkNo};{RenkAdi}");
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
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"Talimat Listesi");
        }
    }
}