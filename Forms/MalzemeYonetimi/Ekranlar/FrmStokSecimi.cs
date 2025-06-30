using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar
{
    public partial class FrmStokSecimi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        public List<string> stokListesi = new List<string>();
        CrudRepository crudRepository = new CrudRepository();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public int _receiptType, _entryReceiptType;
        public FrmStokSecimi(ReceiptTypes receiptTypes)
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
                case 8: //ham kumas gişi
                    _entryReceiptType = 9; // ham depo çıkışı
                    break;


                default:
                    break;
            }
        }

        private void FrmStokSecimi_Load(object sender, EventArgs e)
        {
            string sql = $@"SELECT
	                        ISNULL(d1.ReceiptNo, '') AS TalimatNo,	
	                        ISNULL(d1.ReceiptDate, '') AS Tarih,
                            ISNULL(fk.Id, 0) AS FirmaId,
	                        ISNULL(fk.CompanyCode, '') AS FirmaKodu,
                            ISNULL(fk.CompanyName, '') AS FirmaUnvan,
	                        ISNULL(d2.InventoryId, '') AS MalzemeId,
	                        ISNULL(ik.InventoryCode, '') AS MalzemeKodu,
	                        ISNULL(ik.InventoryName, '') AS MalzemeAdi,
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
	                        where d1.ReceiptType = {Convert.ToInt32(this._receiptType)} --and d1.Approved = 1 and d1.IsFinished = 0
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
            crudRepository.GetUserColumns(gridView1, this.Text);
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            //bazı alanlar sorgudan kaldırıldı fakat alt kısım sorgudan bağımsız kendi sıralamasına göre çalıştığı için problem olmadı
            foreach (int rowHandle in selectedRows)
            {
                string TalimatNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "TalimatNo"));//0
                int FirmaId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "FirmaId"));//1
                string FirmaKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaKodu"));//2
                string FirmaUnvan = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaUnvan"));//3
                int TakipNo = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "TakipNo"));//4
                int IplikId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "MalzemeId"));//5
                string IplikKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "MalzemeKodu"));//6
                string IplikAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "MalzemeAdi"));//7
                decimal BrutKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Kalan Kg"));//8
                decimal NetKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "NetKg"));//9
                decimal Fiyat = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Fiyat"));//10
                string DovizCinsi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "DovizCinsi"));//11
                string Marka = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Marka"));//12
                int RenkId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Renk Id"));//13
                string RenkNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Renk No"));//14
                string RenkAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Renk Adı"));//15
                decimal BirimFiyat = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "BirimFiyat"));//16
                string HesapBirimi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "HesapBirimi"));//17
                int KDV = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "KDV %"));//18
                stokListesi.Add($"{TalimatNo};{FirmaId};{FirmaKodu};{FirmaUnvan};{TakipNo};{IplikId};{IplikKodu};{IplikAdi};{BrutKg};{NetKg};{Fiyat};{DovizCinsi};" +
                    $"{Marka};{RenkId};{RenkNo};{RenkAdi};{BirimFiyat};{HesapBirimi};{KDV};{RenkId};{RenkNo};{RenkAdi}");
            }
            Close();
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
    }
}