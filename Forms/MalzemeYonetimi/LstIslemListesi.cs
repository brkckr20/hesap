using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.MalzemeYonetimi
{
    public partial class LstIslemListesi : DevExpress.XtraEditors.XtraForm
    {
        int receiptTypes;
        Listele listele = new Listele();
        CrudRepository crudRepository = new CrudRepository();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public LstIslemListesi(int _receiptTypes)
        {
            InitializeComponent();
            this.receiptTypes = _receiptTypes;
        }
        string GetReceiptTypeName()
        {
            switch (receiptTypes)
            {
                case 4:
                    return "İplik Satın Alma Talimatı";
                case 5:
                    return "İplik Depo Giriş";
                case 6:
                    return "İplik Depo Çıkış";
                case 7:
                    return "Kumaş Satın Alma Talimatı";
                default:
                    return "";
            }
        }

        private void LstIslemListesi_Load(object sender, EventArgs e)
        {
            string _text = GetReceiptTypeName();
            this.Text += " [" + _text + "]";
            string sql = $@"Select
                        ISNULL(R.Id,0) [Fiş Id],
						ISNULL(R.ReceiptNo,'') [Talimat No],
	                    ISNULL(R.ReceiptDate,'') [Fiş Tarihi],
                        ISNULL(C.Id,'') [Firma Id],	                    
                        ISNULL(C.CompanyCode,'') [Firma Kodu],
	                    ISNULL(C.CompanyName,'') [Firma Adı],
	                    ISNULL(R.Explanation,'') [Açıklama],
	                    ISNULL(R.WareHouseId,'') [Depo], -- tablosu oluşturulacak
	                    ISNULL(R.InvoiceNo,'') [Fatura No],
	                    ISNULL(R.InvoiceDate,'') [Fatura Tarihi],
	                    ISNULL(R.DispatchNo,'') [Irsaliye No],
	                    ISNULL(R.DispatchDate,'') [Irsaliye Tarihi],
	                    ISNULL(RI.TrackingNumber,'') [Takip No],
	                    ISNULL(RI.OperationType,'') [İşlem Tipi],
                        ISNULL(I.Id,'') [Malzeme Id],
	                    ISNULL(I.InventoryCode,'') [Malzeme Kodu],
	                    ISNULL(I.InventoryName,'') [Malzeme Adı],
	                    ISNULL(RI.Piece,0) [Adet],
	                    ISNULL(RI.UnitPrice,0) [Birim Fiyat],
	                    ISNULL(RI.Id,0) [Kalem Kayıt No],
	                    ISNULL(RI.UUID,'') [UUID],
                        ISNULL(RI.RowAmount,0) [Satır Tutarı],
                        ISNULL(RI.Vat,0) [KDV %],
                        ISNULL(RI.Explanation,0) [Satır Açıklama],
						ISNULL(RI.GrossWeight,0) [Brüt Kg],
						ISNULL(RI.NetWeight,0) [Net Kg],
                        ISNULL(RI.MeasurementUnit,'') [Hesap Birimi],						
                        ISNULL(R.Authorized,'') [Yetkili],						
                        ISNULL(R.Maturity,'') [Vade],
						ISNULL(R.PaymentType,'') [Ödeme Şekli],
						ISNULL(R.Approved,0) [Onaylı],
						ISNULL(RI.ReceiptNo,0) [Talimat No Kalem],
						ISNULL(R.TransporterId,0) [Taşıyıcı Id],
						ISNULL(T.Title,0) [Ünvan],
						ISNULL(T.Name,0) [Taşıyıcı Adı],
						ISNULL(T.Surname,0) [Taşıyıcı Soyadı],
						ISNULL(T.NumberPlate,0) [Plaka],
						ISNULL(T.TrailerNumber,0) [Dorse],
                        ISNULL(T.TCKN,'') [TCKN],
						ISNULL(CO.Id,0) [Renk Id],
						ISNULL(CO.Code,'') [Renk Kodu],
						ISNULL(CO.Name,'') [Renk Adı]
						
                    from 
                    Receipt R with(nolock) 
	                    inner join ReceiptItem RI on R.Id = RI.ReceiptId
	                    left join Company C with(nolock)  on C.Id = R.CompanyId
	                    left join Inventory I with(nolock) on RI.InventoryId = I.Id
						left join Transporter T on R.TransporterId = T.Id
						left join Color CO on CO.Id = RI.ColorId
						where R.ReceiptType = {receiptTypes}
                        ";
            listele.Liste(sql, gridControl1);
            crudRepository.GetUserColumns(gridView1, this.Text);
        }
        public List<string> liste = new List<string>();

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            int clickedId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Fiş Id"));
            for (int i = 0; i < gridView.DataRowCount; i++)
            {
                int currentId = Convert.ToInt32(gridView.GetRowCellValue(i, "Fiş Id"));
                if (currentId == clickedId)
                {
                    DateTime Tarih = (DateTime)gridView1.GetRowCellValue(i, "Fiş Tarihi");
                    string MalzemeKodu = Convert.ToString(gridView.GetRowCellValue(i, "Malzeme Kodu"));
                    string MalzemeAdi = Convert.ToString(gridView.GetRowCellValue(i, "Malzeme Adı"));
                    int kalanAdet = Convert.ToInt32(gridView.GetRowCellValue(i, "Adet"));
                    string IslemTipi = Convert.ToString(gridView.GetRowCellValue(i, "İşlem Tipi"));
                    string UUID = Convert.ToString(gridView.GetRowCellValue(i, "UUID"));
                    int MalzemeId = Convert.ToInt32(gridView.GetRowCellValue(i, "Malzeme Id"));
                    string TeslimAlan = Convert.ToString(gridView.GetRowCellValue(i, "Teslim Alan"));
                    int FirmaId = Convert.ToInt32(gridView.GetRowCellValue(i, "Firma Id"));
                    string FirmaKodu = Convert.ToString(gridView.GetRowCellValue(i, "Firma Kodu"));
                    string FirmaAdi = Convert.ToString(gridView.GetRowCellValue(i, "Firma Adı"));
                    DateTime FaturaTarihi = (DateTime)gridView1.GetRowCellValue(i, "Fatura Tarihi");
                    string FaturaNo = Convert.ToString(gridView.GetRowCellValue(i, "Fatura No"));
                    DateTime IrsaliyeTarihi = (DateTime)gridView1.GetRowCellValue(i, "Irsaliye Tarihi");
                    string IrsaliyeNo = Convert.ToString(gridView.GetRowCellValue(i, "Irsaliye No"));
                    string Aciklama = Convert.ToString(gridView.GetRowCellValue(i, "Açıklama"));
                    decimal BirimFiyat = Convert.ToDecimal(gridView.GetRowCellValue(i, "Birim Fiyat"));
                    int Kdv = Convert.ToInt32(gridView.GetRowCellValue(i, "KDV %"));
                    int KalemKayitNo = Convert.ToInt32(gridView.GetRowCellValue(i, "Kalem Kayıt No"));
                    string SatirAciklama = Convert.ToString(gridView.GetRowCellValue(i, "Satır Açıklama"));
                    int TakipNo = Convert.ToInt32(gridView.GetRowCellValue(i, "Takip No"));
                    string TalimatNo = Convert.ToString(gridView.GetRowCellValue(i, "Talimat No"));
                    decimal BrutKg = Convert.ToDecimal(gridView.GetRowCellValue(i, "Brüt Kg"));
                    decimal NetKg = Convert.ToDecimal(gridView.GetRowCellValue(i, "Net Kg"));
                    string HesapBirimi = Convert.ToString(gridView.GetRowCellValue(i, "Hesap Birimi"));
                    string Yetkili = Convert.ToString(gridView.GetRowCellValue(i, "Yetkili"));
                    int Vade = Convert.ToInt32(gridView.GetRowCellValue(i, "Vade"));
                    string OdemeSekli = Convert.ToString(gridView.GetRowCellValue(i, "Ödeme Şekli"));
                    bool Onayli = Convert.ToBoolean(gridView.GetRowCellValue(i, "Onaylı"));
                    string TalimatNoKalem = Convert.ToString(gridView.GetRowCellValue(i, "Talimat No Kalem"));
                    int TasiyiciId = Convert.ToInt32(gridView.GetRowCellValue(i, "Taşıyıcı Id"));
                    string TasiyiciUnvan = Convert.ToString(gridView.GetRowCellValue(i, "Ünvan"));
                    string TasiyiciAd = Convert.ToString(gridView.GetRowCellValue(i, "Taşıyıcı Adı"));
                    string TasiyiciSoyad = Convert.ToString(gridView.GetRowCellValue(i, "Taşıyıcı Soyadı"));
                    string Plaka = Convert.ToString(gridView.GetRowCellValue(i, "Plaka"));
                    string Dorse = Convert.ToString(gridView.GetRowCellValue(i, "Dorse"));
                    string TCKN = Convert.ToString(gridView.GetRowCellValue(i, "TCKN"));
                    int RenkId = Convert.ToInt32(gridView.GetRowCellValue(i, "Renk Id"));
                    string RenkKodu = Convert.ToString(gridView.GetRowCellValue(i, "Renk Kodu"));
                    string RenkAdi = Convert.ToString(gridView.GetRowCellValue(i, "Renk Adı"));
                    liste.Add($"{MalzemeKodu};{MalzemeAdi};{kalanAdet};{IslemTipi};{UUID};{MalzemeId};{clickedId};{TeslimAlan};{Tarih};{FirmaId};{FirmaKodu};{FirmaAdi};{FaturaTarihi};{FaturaNo};{IrsaliyeTarihi};{IrsaliyeNo};{Aciklama};{BirimFiyat};{Kdv};{KalemKayitNo};{SatirAciklama};{TakipNo};{TalimatNo};{BrutKg};{NetKg};{HesapBirimi};{Yetkili};{Vade};{OdemeSekli};{Onayli};{TalimatNoKalem};{TasiyiciId};{TasiyiciUnvan};{TasiyiciAd};{TasiyiciSoyad};{Plaka};{Dorse};{TCKN};{RenkId};{RenkKodu};{RenkAdi}");
                }
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

        private void excelOlarakAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1, GetReceiptTypeName());
        }
    }
}