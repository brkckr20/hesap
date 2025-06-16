using DevExpress.XtraEditors;
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

namespace Hesap.Forms.Liste
{
    public partial class FrmBoyahaneRenkKartlariListesi : DevExpress.XtraEditors.XtraForm
    {
        public FrmBoyahaneRenkKartlariListesi(bool isCard, bool isVariant = false) //isCard parametresi eğer true ise kartta görüntülenecek ve ekleme silme güncelleme için kullanılacak
        {//varyant seçimleri için de bu ekran kullanılabilir. Revize edilmesi gerekmektedir.
            InitializeComponent();
            _isCard = isCard;
            _isVariant = isVariant;
            if (_isVariant)
            {
                this.Text = "Varyant (Renk) Kartları Listesi";
            }
        }
        public FrmBoyahaneRenkKartlariListesi(string _colorType)
        {
            InitializeComponent();
            this.ColorType = _colorType;
        }
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CrudRepository crudRepository = new CrudRepository();
        public List<Dictionary<string, object>> veriler;
        public bool _isCard, IsUse, _isVariant;
        string ColorType;
        public string Type, Code, Namee, CompanyCode, CompanyName, Date, RequestDate, ConfirmDate, PantoneNo, Price, Forex, VariantCode, VariantName; // name hata vermesine diye bu şekilde adlandırıldı
        public int Id, CompanyId, TypeNo, VariantId;
        private void FrmBoyahaneRenkKartlariListesi_Load(object sender, EventArgs e)
        {
            if (ColorType == "İplik")
            {
                RenkleriListele(2);
            }
            else if (ColorType == "Kumaş")
            {
                RenkleriListele(1);
            }
            else
            {
                Listele();
            }
        }
        void RenkleriListele(int Type)
        {
            string sql = $@"Select 
                            case 
	                            when C.Type = 1 then 'Kumaş'
	                            when C.Type = 2 Then 'İplik'
	                            end as [Türü],
	                            ISNULL(C.Code,'') Kodu,
	                            ISNULL(C.Name,'') Adı,
                                ISNULL(C2.Id,'') VaryantId,
	                            ISNULL(C2.Code,'') VaryantKodu,
								ISNULL(C2.Name,'') VaryantAdi,
	                            ISNULL(C.CompanyId,'') [Firma Id],
	                            ISNULL(CO.CompanyCode,'') [Firma Kodu],
	                            ISNULL(CO.CompanyName,'') [Firma Adı],
	                            ISNULL(C.Date,'') Tarih,
	                            ISNULL(C.RequestDate,'') [Talep Tarihi],
	                            ISNULL(C.ConfirmDate,'') [Onay Tarihi],
	                            ISNULL(C.PantoneNo,'') [Pantone No],
	                            ISNULL(C.Price,0) [Fiyat],
	                            ISNULL(C.Forex,'') [Döviz],
	                            ISNULL(C.Id,'') [Id],
                                ISNULL(C.Type,0) [Tur No],
	                            ISNULL(C.IsUse,0) Kullanimda
                            from Color C left join Company CO on C.CompanyId = CO.Id
							left join Color C2 on C.ParentId = C2.Id
                            WHERE C.Type = {Type}";
            listele.Liste(sql, gridControl1);
            crudRepository.GetUserColumns(gridView1, this.Text);

        }
        void Listele()
        {
            string sql;
            if (_isCard && !_isVariant)
            {
                sql = @"Select 
                            case 
	                            when C.Type = 1 then 'Kumaş'
	                            when C.Type = 2 Then 'İplik'
	                            end as [Türü],
	                            ISNULL(C.Code,'') Kodu,
	                            ISNULL(C.Name,'') Adı,
                                ISNULL(C2.Id,'') VaryantId,
	                            ISNULL(C2.Code,'') VaryantKodu,
								ISNULL(C2.Name,'') VaryantAdi,
	                            ISNULL(C.CompanyId,'') [Firma Id],
	                            ISNULL(CO.CompanyCode,'') [Firma Kodu],
	                            ISNULL(CO.CompanyName,'') [Firma Adı],
	                            ISNULL(C.Date,'') Tarih,
	                            ISNULL(C.RequestDate,'') [Talep Tarihi],
	                            ISNULL(C.ConfirmDate,'') [Onay Tarihi],
	                            ISNULL(C.PantoneNo,'') [Pantone No],
	                            ISNULL(C.Price,0) [Fiyat],
	                            ISNULL(C.Forex,'') [Döviz],
	                            ISNULL(C.Id,'') [Id],
                                ISNULL(C.Type,0) [Tur No],
	                            ISNULL(C.IsUse,0) Kullanimda
                            from Color C left join Company CO on C.CompanyId = CO.Id
							left join Color C2 on C.ParentId = C2.Id
                            WHERE C.IsParent = 0";
            }
            else if (_isVariant)
            {
                sql = @"Select 
	                            ISNULL(C.Code,'') Kodu,
	                            ISNULL(C.Name,'') Adı,
	                            ISNULL(C.Id,'') [Id],
	                            ISNULL(C.IsUse,0) Kullanimda
                            from Color C
							WHERE C.IsParent = 1 and C.IsUse = 1";
            }
            else
            {
                sql = @"";
            }
            listele.Liste(sql, gridControl1);
            crudRepository.GetUserColumns(gridView1, this.Text);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            if (_isCard && !_isVariant)
            {
                Type = gridView.GetFocusedRowCellValue("Türü").ToString();
                Code = gridView.GetFocusedRowCellValue("Kodu").ToString();
                Namee = gridView.GetFocusedRowCellValue("Adı").ToString();
                CompanyId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Firma Id"));
                CompanyCode = gridView.GetFocusedRowCellValue("Firma Kodu").ToString();
                CompanyName = gridView.GetFocusedRowCellValue("Firma Adı").ToString();
                Date = gridView.GetFocusedRowCellValue("Tarih").ToString();
                RequestDate = gridView.GetFocusedRowCellValue("Talep Tarihi").ToString();
                ConfirmDate = gridView.GetFocusedRowCellValue("Onay Tarihi").ToString();
                PantoneNo = gridView.GetFocusedRowCellValue("Pantone No").ToString();
                Price = gridView.GetFocusedRowCellValue("Fiyat").ToString();
                Forex = gridView.GetFocusedRowCellValue("Döviz").ToString();
                Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
                TypeNo = Convert.ToInt32(gridView.GetFocusedRowCellValue("Tur No"));
                IsUse = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Kullanimda"));
                VariantId = Convert.ToInt32(gridView1.GetFocusedRowCellValue("VaryantId"));
                VariantCode = gridView.GetFocusedRowCellValue("VaryantKodu").ToString();
                VariantName = gridView.GetFocusedRowCellValue("VaryantAdi").ToString();
            }
            Code = gridView.GetFocusedRowCellValue("Kodu").ToString();
            Namee = gridView.GetFocusedRowCellValue("Adı").ToString();
            Id = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            IsUse = Convert.ToBoolean(gridView.GetFocusedRowCellValue("Kullanimda"));
            this.Close();
        }

        private void excelDosyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1, "Boyahane Renk Kartları");
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1, this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }
    }
}