using Dapper;
using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Forms.Liste;
using Hesap.Helpers;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;

namespace Hesap
{
    public partial class Form1 : XtraForm
    {
        Bildirim bildirim = new Bildirim();
        HesaplaVeYansit hesapla = new HesaplaVeYansit();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Ayarlar ayarlar = new Ayarlar();
        CrudRepository crudRepository = new CrudRepository();
        Numarator numarator = new Numarator();
        int Id = 0, CompanyId = 0, InventoryId = 0, RecipeId = 0, InventoryType = Convert.ToInt32(InventoryTypes.Kumas);
        string TableName1 = "Cost", TableName2 = "CostProductionInformation", TableName3 = "CostProductionCalculate", TableName4 = "CostCostCalculate";
        public Form1()
        {
            InitializeComponent();
            RegisterEvent();
        }
        private void RegisterEvent()
        {
            var hesaplamalar = new List<(TextBox, TextBox, TextBox, string)>
            {
                // üretim bilgileri
                    // - iplik bilgileri alanıdır
                (txtCozgu1, txtCozgu1Bolen, lblCozgu1Uretim, "Bolme"),
                (txtCozgu2, txtCozgu2Bolen, lblCozgu2Uretim, "Bolme"),
                (txtAtki1, txtAtki1Bolen, lblAtki1Uretim, "Bolme"),
                (txtAtki2, txtAtki2Bolen, lblAtki2Uretim, "Bolme"),
                (txtAtki3, txtAtki3Bolen, lblAtki3Uretim, "Bolme"),
                (txtAtki4, txtAtki4Bolen, lblAtki4Uretim, "Bolme"),
                    //  - dokuma bilgileri alanıdır
                (txtTarakNo1, txtTarakNo1Bolen, lblTarakNo1Uretim, "Carpma"),
                (txtTarakNo2, txtTarakNo2Bolen, lblTarakNo2Uretim, "Carpma"),
                    // - tel sayilari
                (lblTarakNo1Uretim, txtTarakEn, txtCozgu1TelSayisi, "Carpma"),
                (lblTarakNo2Uretim, txtTarakEn, txtCozgu2TelSayisi, "Carpma"),
                (txtAtki1Siklik, txtHamBoy, txtAtki1TelSayisi, "Carpma"),
                (txtAtki2Siklik, txtHamBoy, txtAtki2TelSayisi, "Carpma"),
                (txtAtki3Siklik, txtHamBoy, txtAtki3TelSayisi, "Carpma"),
                (txtAtki4Siklik, txtHamBoy, txtAtki4TelSayisi, "Carpma"),
                //Maliyet Hesaplama - Çözgü
                (txtHamBoy, txtCozguMH, txtCozguDMMH, "CozguMaliyet"),
                //Ham kumaş maliyeti TL karşılığı
                (txtKurMH, txtFireliForexHKMMH, txtFireliTlHKMMH, "Carpma"),
                //parça yıkama                
                (txtParcaYikamaMH, txtGramajToplam, txtParcaYikama, "Carpma"),
                //txteuro hesaplaması
                (txtKumasBoyamaMH, txtPariteMH, txtEuro, "Carpma"),
                //dik.ür-konf-maliyeti
                (txtBoyaliKumas, txtKonfMaliyetMH, txtKonfeksiyonMaliyeti, "Topla"),
                //dik.ürün karli - tl
                (txtKarliDU, txtKurMH, txtDUKTL, "Carpma"),
                //dik.ürün ldv dahil - tl
                (txtKDVDahilFiyat, txtKurMH, txtKDVDUKTL, "Carpma"),
                //belirlenen fiyat
                (txtBelirliFiyatForex, txtKurMH, txtbelirliFiyatTl, "Carpma"),
                //belirlenen fiyat - kdv dahil
                (txtKDVliFiyatForex, txtKurMH, txtKDVliFiyatTl, "Carpma"),
            };
            var direkYansimalar = new List<(TextBox, TextBox)>
            {
                (lblTarakNo1Uretim, txtCozgu1Siklik),
                (lblTarakNo2Uretim, txtCozgu2Siklik),
                (txtIpMaliyetToplam, txtIplikMaliyetMH),
                (txtFireliYBMMH, txtBoyaliKumas),
            };
            foreach (var (txt1, txt2, hedef, islem) in hesaplamalar)
            {
                txt1.TextChanged += (s, e) => CostCalculationHelper.TextboxAndTextboxToTextbox(txt1, txt2, hedef, islem);
                txt2.TextChanged += (s, e) => CostCalculationHelper.TextboxAndTextboxToTextbox(txt1, txt2, hedef, islem);
            }

            foreach (var (kaynak, hedef) in direkYansimalar)
            {
                kaynak.TextChanged += (s, e) => CostCalculationHelper.DirectReflection(kaynak, hedef);
            }

            // ham en - tek bir textboxtan hesaplama için eklendi
            txtTarakEn.TextChanged += (s, e) => CostCalculationHelper.CalculateWithStaticNumber(txtTarakEn, txtHamEn);
            // Çözgü 1
            RegisterTextChanged(new[] { txtHamBoy, txtBoySacak, txtCozgu1TelSayisi, txtCozgu1 },
                () => CostCalculationHelper.CalculateGrammageCozgu1(txtHamBoy, txtBoySacak, txtCozgu1TelSayisi, txtCozgu1, txtCozgu1UH));

            // Çözgü 2
            RegisterTextChanged(new[] { txtHamBoy, txtCozgu2TelSayisi, lblCozgu2Uretim },
                () => CostCalculationHelper.CalculateGrammageCozgu2(txtHamBoy, txtCozgu2TelSayisi, lblCozgu2Uretim, txtCozgu2UH));

            // Atkı 1-4
            RegisterTextChanged(new[] { txtAtki1Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki1Uretim },
                () => CostCalculationHelper.CalculateGrammageAtki(txtAtki1Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki1Uretim, txtAtki1UH));

            RegisterTextChanged(new[] { txtAtki2Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki2Uretim },
                () => CostCalculationHelper.CalculateGrammageAtki(txtAtki2Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki2Uretim, txtAtki2UH));

            RegisterTextChanged(new[] { txtAtki3Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki3Uretim },
                () => CostCalculationHelper.CalculateGrammageAtki(txtAtki3Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki3Uretim, txtAtki3UH));

            RegisterTextChanged(new[] { txtAtki4Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki4Uretim },
                () => CostCalculationHelper.CalculateGrammageAtki(txtAtki4Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki4Uretim, txtAtki4UH));

            // Gramaj Toplamı
            RegisterTextChanged(new[] { txtCozgu1UH, txtCozgu2UH, txtAtki1UH, txtAtki2UH, txtAtki3UH, txtAtki4UH },
                () => CostCalculationHelper.CalculateGrammageSum(txtCozgu1UH, txtCozgu2UH, txtAtki1UH, txtAtki2UH, txtAtki3UH, txtAtki4UH, txtGramajToplam));
            //iplik boyama gr sonuç
            txtGramajToplam.TextChanged += (s, e) => CostCalculationHelper.CalculateYarnGrammage(txtGramajToplam, txtHamEn, txtIpBoyamaSonuc);
            txtHamEn.TextChanged += (s, e) => CostCalculationHelper.CalculateYarnGrammage(txtGramajToplam, txtHamEn, txtIpBoyamaSonuc);
            // üretim hesaplama - iplik maliyet toplamı
            RegisterTextChanged(new[] { txtCozgu1IM, txtCozgu2IM, txtAtki1IM, txtAtki2IM, txtAtki3IM, txtAtki4IM },
                () => CostCalculationHelper.CalculateCostYarnAmount(txtCozgu1IM, txtCozgu2IM, txtAtki1IM, txtAtki2IM, txtAtki3IM, txtAtki4IM, txtIpMaliyetToplam));
            //iplik maliyet hesap
            RegisterTextChanged(new[] { txtCozgu1IB, txtCozgu1IF, txtCozgu1UH },
                () => CostCalculationHelper.CalculateYarnAmount(txtCozgu1IB, txtCozgu1IF, txtCozgu1UH, txtCozgu1IM));
            RegisterTextChanged(new[] { txtCozgu2IB, txtCozgu2IF, txtCozgu2UH },
                () => CostCalculationHelper.CalculateYarnAmount(txtCozgu2IB, txtCozgu2IF, txtCozgu2UH, txtCozgu2IM));
            RegisterTextChanged(new[] { txtAtki1IB, txtAtki1IF, txtAtki1UH },
                () => CostCalculationHelper.CalculateYarnAmount(txtAtki1IB, txtAtki1IF, txtAtki1UH, txtAtki1IM));
            RegisterTextChanged(new[] { txtAtki2IB, txtAtki2IF, txtAtki2UH },
                () => CostCalculationHelper.CalculateYarnAmount(txtAtki2IB, txtAtki2IF, txtAtki2UH, txtAtki2IM));
            RegisterTextChanged(new[] { txtAtki3IB, txtAtki3IF, txtAtki3UH },
                () => CostCalculationHelper.CalculateYarnAmount(txtAtki3IB, txtAtki3IF, txtAtki3UH, txtAtki3IM));
            RegisterTextChanged(new[] { txtAtki4IB, txtAtki4IF, txtAtki4UH },
                () => CostCalculationHelper.CalculateYarnAmount(txtAtki4IB, txtAtki4IF, txtAtki4UH, txtAtki4IM));

            /*maliyet hesaplama - dokuma maliyetinde kalındı*/
            RegisterTextChanged(new[] { txtHamBoy, txtBoySacak, txtAtki1Siklik, txtAtki2Siklik, txtAtki3Siklik, txtAtki4Siklik, txtAtkiMH, txtKurMH },
                () => CostCalculationHelper.CalculateCostWeaving(txtHamBoy, txtBoySacak, txtAtki1Siklik, txtAtki2Siklik, txtAtki3Siklik, txtAtki4Siklik, txtAtkiMH, txtKurMH, txtDokumaMH));
            txtDokumaMH.TextChanged += (s, e) => CostCalculationHelper.CalculateCostProductionSum(txtDokumaMH, txtCozguDMMH, txtIplikMaliyetMH, txtToplamUMMH);
            txtCozguDMMH.TextChanged += (s, e) => CostCalculationHelper.CalculateCostProductionSum(txtDokumaMH, txtCozguDMMH, txtIplikMaliyetMH, txtToplamUMMH);
            txtIplikMaliyetMH.TextChanged += (s, e) => CostCalculationHelper.CalculateCostProductionSum(txtDokumaMH, txtCozguDMMH, txtIplikMaliyetMH, txtToplamUMMH);
            //fireli üretim maliyet
            txtToplamUMMH.TextChanged += (s, e) => CostCalculationHelper.CalculateCostProductionSumWithWastage(txtToplamUMMH, txtDokumaFiresiMH, txtToplamUMMH, txtFireliMH);
            txtDokumaFiresiMH.TextChanged += (s, e) => CostCalculationHelper.CalculateCostProductionSumWithWastage(txtToplamUMMH, txtDokumaFiresiMH, txtToplamUMMH, txtFireliMH);
            //ham kumaş maliyeti kârlı
            txtFireliMH.TextChanged += (s, e) => CostCalculationHelper.CalculateCostProductionSumWithWastage(txtFireliMH, txtKarMH, txtFireliMH, txtFireliForexHKMMH);
            txtKarMH.TextChanged += (s, e) => CostCalculationHelper.CalculateCostProductionSumWithWastage(txtFireliMH, txtKarMH, txtFireliMH, txtFireliForexHKMMH);

            txtGramajToplam.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedFabric(txtEuro, txtGramajToplam, txtFireliMH, txtBoyanmisKumas);
            txtFireliMH.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedFabric(txtEuro, txtGramajToplam, txtFireliMH, txtBoyanmisKumas);
            //yıkama ve boyahane maliyet - fireli
            txtParcaYikamaMH.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedWastage(txtParcaYikama, txtBoyanmisKumas, txtBoyahaneFiresiMH, txtFireliYBMMH);
            txtBoyanmisKumas.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedWastage(txtParcaYikama, txtBoyanmisKumas, txtBoyahaneFiresiMH, txtFireliYBMMH);
            txtBoyahaneFiresiMH.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedWastage(txtParcaYikama, txtBoyanmisKumas, txtBoyahaneFiresiMH, txtFireliYBMMH);
            //üsttekinin kârlısı
            txtFireliYBMMH.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedBeneficial(txtFireliYBMMH, txtKarMH, txtKarliYBMMH);
            txtKarMH.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedBeneficial(txtFireliYBMMH, txtKarMH, txtKarliYBMMH);
            //2.kalite maliyeti dik.ür
            txtKonfeksiyonMaliyeti.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedBeneficial(txtKonfeksiyonMaliyeti, txt2KaliteMaliyetMH, txt2KaliteMaliyeti);
            txt2KaliteMaliyetMH.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedBeneficial(txtKonfeksiyonMaliyeti, txt2KaliteMaliyetMH, txt2KaliteMaliyeti);
            //dikilmis-urun karli
            txt2KaliteMaliyeti.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedBeneficial(txt2KaliteMaliyeti, txtKarMH, txtKarliDU);
            txtKarMH.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedBeneficial(txt2KaliteMaliyeti, txtKarMH, txtKarliDU);
            //dik.ürün kdv dahil fiyat
            txtKarliDU.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedBeneficial(txtKarliDU, txtKDVMH, txtKDVDahilFiyat);
            txtKDVMH.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedBeneficial(txtKarliDU, txtKDVMH, txtKDVDahilFiyat);
            //KDV DAHİL BELİRLENEN FİYAT
            txtBelirliFiyatForex.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedBeneficial(txtBelirliFiyatForex, txtKDVMH, txtKDVliFiyatForex);
            txtKDVMH.TextChanged += (s, e) => CostCalculationHelper.CalculatePaintedBeneficial(txtBelirliFiyatForex, txtKDVMH, txtKDVliFiyatForex);


        }
        private void RegisterTextChanged(TextBox[] textBoxes, Action action)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.TextChanged += (s, e) => action();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DolarKuruGetir("USD_ALIS");
            AttachEnterKeyHandler(this);
            dateTimePicker1.EditValue = DateTime.Now;
            lblFisNo.Text = numarator.NumaraVer("Maliyet");
        }
        private void materialLabel6_Click(object sender, EventArgs e)
        {

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }
        decimal VirgulKaldir(TextBox textBox, int decimalPlaces = 3)
        {
            string text = textBox.Text.Replace(",", ".");
            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal decimalValue))
            {
                decimalValue = Math.Round(decimalValue, decimalPlaces);
                return decimalValue;
            }

            return 0;
        }
        void Kaydet()
        {
            var costParams = new Dictionary<string, object>
            {
                {"CompanyId",CompanyId},
                {"Date", dateTimePicker1.EditValue},
                {"InventoryId", InventoryId},
                {"RecipeId", RecipeId},
                {"OrderNo", lblFisNo.Text},
            };
            if (this.Id == 0)
            {
                Id = crudRepository.Insert(this.TableName1, costParams);
                var CPI_params = new Dictionary<string, object>
                { // ilgili alanlara ihtiyaç olması durumunda VirgulKaldır metodu eklenebilir
                    {"CostId", this.Id},{"YI_Warp1", txtCozgu1.Text},{"YI_Warp1Divider", txtCozgu1Bolen.Text},{"YI_Warp1Result", lblCozgu1Uretim.Text},{"YI_Warp2", txtCozgu2.Text},{"YI_Warp2Divider", txtCozgu2Bolen.Text},{"YI_Warp2Result", lblCozgu2Uretim.Text},{"YI_Scarf1", txtAtki1.Text},{"YI_Scarf1Divider", txtAtki1Bolen.Text},{"YI_Scarf1Result", lblAtki1Uretim.Text},{"YI_Scarf2", txtAtki2.Text},{"YI_Scarf2Divider", txtAtki2Bolen.Text},{"YI_Scarf2Result", lblAtki2Uretim.Text},{"YI_Scarf3", txtAtki3.Text},{"YI_Scarf3Divider", txtAtki3Bolen.Text},{"YI_Scarf3Result", lblAtki3Uretim.Text},{"YI_Scarf4", txtAtki4.Text},{"YI_Scarf4Divider", txtAtki4Bolen.Text},{"YI_Scarf4Result", lblAtki4Uretim.Text},{"WI_CombNo1",txtTarakNo1.Text},{"WI_CombNo1Multiplier",txtTarakNo1Bolen.Text},{"WI_CombNo1Result",lblTarakNo1Uretim.Text},{"WI_CombNo2",txtTarakNo2.Text},{"WI_CombNo2Multiplier",txtTarakNo2Bolen.Text},{"WI_CombNo2Result",lblTarakNo2Uretim.Text},{"WI_CombWidth",txtTarakEn.Text},{"WI_RawHeight",txtHamBoy.Text},{"WI_HeightEaves",VirgulKaldir(txtBoySacak)},{"WI_WidthEaves",VirgulKaldir(txtEnSacak)},{"WI_RawWidth",txtHamEn.Text},{"WI_ProductHeight",txtMamulBoy.Text},{"WI_ProductWidth",txtMamulEn.Text},{"D_Warp1",txtCozgu1Siklik.Text},{"D_Warp2",txtCozgu2Siklik.Text},{"D_Scarf1",txtAtki1Siklik.Text},{"D_Scarf2",txtAtki2Siklik.Text},{"D_Scarf3",txtAtki3Siklik.Text},{"D_Scarf4",txtAtki4Siklik.Text},{"NW_Warp1",txtCozgu1TelSayisi.Text},{"NW_Warp2",txtCozgu2TelSayisi.Text},{"NW_Scarf1",txtAtki1TelSayisi.Text},{"NW_Scarf2",txtAtki2TelSayisi.Text},{"NW_Scarf3",txtAtki3TelSayisi.Text},{"NW_Scarf4",txtAtki4TelSayisi.Text}
                };
                crudRepository.Insert(this.TableName2, CPI_params);
                var CPC_params = new Dictionary<string, object>
                {
                    {"CostId",this.Id},{"WC_Warp1",VirgulKaldir(txtCozgu1UH)},{"WC_Warp2",VirgulKaldir(txtCozgu2UH)},{"WC_Scarf1",VirgulKaldir(txtAtki1UH)},{"WC_Scarf2",VirgulKaldir(txtAtki2UH) },{"WC_Scarf3",VirgulKaldir(txtAtki3UH)},{"WC_Scarf4",VirgulKaldir(txtAtki4UH) },{"WC_Total",VirgulKaldir(txtGramajToplam) },{"YP_Warp1",VirgulKaldir(txtCozgu1IF) },{"YP_Warp2",VirgulKaldir(txtCozgu2IF)},{"YP_Scarf1",VirgulKaldir(txtAtki1IF) },{"YP_Scarf2",VirgulKaldir(txtAtki2IF) },{"YP_Scarf3",VirgulKaldir(txtAtki3IF) },{"YP_Scarf4",VirgulKaldir(txtAtki4IF) },{"YD_Warp1",VirgulKaldir(txtCozgu1IB) },{"YD_Warp2",VirgulKaldir(txtCozgu2IB) },{"YD_Scarf1",VirgulKaldir(txtAtki1IB) },{"YD_Scarf2",VirgulKaldir(txtAtki2IB) },{"YD_Scarf3",VirgulKaldir(txtAtki3IB)},{"YD_Scarf4",VirgulKaldir(txtAtki4IB) },{"YD_Result",VirgulKaldir(txtIpBoyamaSonuc) },{"YC_Warp1",VirgulKaldir(txtCozgu1IM)},{"YC_Warp2",VirgulKaldir(txtCozgu2IM)},{"YC_Scarf1",VirgulKaldir(txtAtki1IM)},{"YC_Scarf2",VirgulKaldir(txtAtki2IM)},{"YC_Scarf3",VirgulKaldir(txtAtki3IM)},{"YC_Scarf4",VirgulKaldir(txtAtki4IM)},{"YC_Result",VirgulKaldir(txtIpMaliyetToplam)}
                };
                crudRepository.Insert(TableName3, CPC_params);
                var CCC_params = new Dictionary<string, object> //Maliyet Hesaplama - CostCostCalculate
                {
                    {"CostId",this.Id},{"PP_Scarf",VirgulKaldir(txtAtkiMH)},{"PP_Warp",VirgulKaldir(txtCozguMH)},{"PP_PartsWashing",VirgulKaldir(txtParcaYikamaMH)},{"PP_FabricWashing",VirgulKaldir(txtKumasBoyamaMH)},{"PP_WeavingWaste",VirgulKaldir(txtDokumaFiresiMH)},{"PP_DyehouseWaster",VirgulKaldir(txtBoyahaneFiresiMH)},{"PP_GarmentCost",VirgulKaldir(txtKonfMaliyetMH)},{"PP_2QualityCost",VirgulKaldir(txt2KaliteMaliyetMH)},{"PP_Profit",VirgulKaldir(txtKarMH)},{"PP_Vat",VirgulKaldir(txtKDVMH)},{"PP_Currency",VirgulKaldir(txtKurMH)},{"PP_Parity",VirgulKaldir(txtPariteMH)},{"PP_Euro",VirgulKaldir(txtEuro)},{"WC_Weaving",VirgulKaldir(txtDokumaMH)},{"WC_Warp",VirgulKaldir(txtCozguDMMH)},{"WC_YarnCost",VirgulKaldir(txtIplikMaliyetMH)},{"PC_Total",VirgulKaldir(txtToplamUMMH)},{"PC_Wasted",VirgulKaldir(txtFireliMH)},{"RFC_ProfitableForex",VirgulKaldir(txtFireliForexHKMMH)},{"RFC_Profitable",VirgulKaldir(txtFireliTlHKMMH)},{"WDC_PartsWashing",VirgulKaldir(txtParcaYikama)},{"WDC_DyedFabric",VirgulKaldir(txtBoyanmisKumas)},{"WDC_Wasted",VirgulKaldir(txtFireliYBMMH)},{"WDC_ProfitableForex",VirgulKaldir(txtKarliYBMMH)},{"SP_DyedFabric",VirgulKaldir(txtBoyaliKumas)},{"SP_GarmentCost",VirgulKaldir(txtKonfeksiyonMaliyeti)},{"SP_2QualityCost",VirgulKaldir(txt2KaliteMaliyeti)},{"SP_ProfitableForex",VirgulKaldir(txtKarliDU)},{"SP_Profitable",VirgulKaldir(txtDUKTL)},{"SP_VatIncludeForex",VirgulKaldir(txtKDVDahilFiyat)},{"SP_VatInclude",VirgulKaldir(txtKDVDUKTL)},{"PriceDeterminedForex",VirgulKaldir(txtBelirliFiyatForex)},{"PriceDetermined",VirgulKaldir(txtbelirliFiyatTl)},{"VatIncludedPriceForex",VirgulKaldir(txtKDVliFiyatForex)},{"VatIncluded",VirgulKaldir(txtKDVliFiyatTl)}
                };
                crudRepository.Insert(TableName4,CCC_params);
                bildirim.Basarili();
            }
            else
            {
                crudRepository.Update(this.TableName1, this.Id, costParams);
                bildirim.GuncellemeBasarili();
            }
        }
        void ListeAc()
        {
            Forms.Liste.FrmMaliyetHesaplamaListesi frm = new Forms.Liste.FrmMaliyetHesaplamaListesi();
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                this.Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                dateTimePicker1.EditValue = (DateTime)frm.veriler[0]["Tarih"];
                txtFirmaKodu.Text = frm.veriler[0]["FirmaKodu"].ToString();
                lblFirmaAdi.Text = frm.veriler[0]["FirmaUnvan"].ToString();
                txtUrun.Text = frm.veriler[0]["UrunKodu"].ToString();
                txtReceteNo.Text = frm.veriler[0]["SiparisNo"].ToString(); // aslında reçete no alanında tekabül ediyor.
                txtCozgu1.Text = frm.veriler[0]["Cozgu1"].ToString();
                txtCozgu1Bolen.Text = frm.veriler[0]["Cozgu1Bolen"].ToString();
                txtCozgu2.Text = frm.veriler[0]["Cozgu2"].ToString();
                txtAtki1.Text = frm.veriler[0]["Atki1"].ToString();
                txtAtki1Bolen.Text = frm.veriler[0]["Atki1Bolen"].ToString();
                txtAtki2.Text = frm.veriler[0]["Atki2"].ToString();
                txtAtki2Bolen.Text = frm.veriler[0]["Atki2Bolen"].ToString();
                txtAtki3.Text = frm.veriler[0]["Atki3"].ToString();
                txtAtki3Bolen.Text = frm.veriler[0]["Atki3Bolen"].ToString();
                txtAtki4.Text = frm.veriler[0]["Atki4"].ToString();
                txtAtki4Bolen.Text = frm.veriler[0]["Atki4Bolen"].ToString();
                txtTarakNo1.Text = frm.veriler[0]["TarakNo1"].ToString();
                txtTarakNo1Bolen.Text = frm.veriler[0]["TarakNo1Bolen"].ToString();
                txtTarakNo2.Text = frm.veriler[0]["TarakNo2"].ToString();
                txtTarakNo2Bolen.Text = frm.veriler[0]["TarakNo2Bolen"].ToString();
                txtTarakEn.Text = frm.veriler[0]["TarakEn"].ToString();
                txtHamBoy.Text = frm.veriler[0]["HamBoy"].ToString();
                txtBoySacak.Text = frm.veriler[0]["BoySacak"].ToString();
                txtEnSacak.Text = frm.veriler[0]["EnSacak"].ToString();
                txtHamEn.Text = frm.veriler[0]["HamEn"].ToString();
                txtMamulBoy.Text = frm.veriler[0]["MamulBoy"].ToString();
                txtMamulEn.Text = frm.veriler[0]["MamulEn"].ToString();
                txtCozgu1Siklik.Text = frm.veriler[0]["Cozgu1Siklik"].ToString();
                txtCozgu2Siklik.Text = frm.veriler[0]["Cozgu2Siklik"].ToString();
                txtAtki1Siklik.Text = frm.veriler[0]["Atki1Siklik"].ToString();
                txtAtki2Siklik.Text = frm.veriler[0]["Atki2Siklik"].ToString();
                txtAtki3Siklik.Text = frm.veriler[0]["Atki3Siklik"].ToString();
                txtAtki4Siklik.Text = frm.veriler[0]["Atki4Siklik"].ToString();
                txtCozgu1TelSayisi.Text = frm.veriler[0]["Cozgu1TelSayisi"].ToString();
                txtCozgu2TelSayisi.Text = frm.veriler[0]["Cozgu2TelSayisi"].ToString();
                txtAtki1TelSayisi.Text = frm.veriler[0]["Atki1TelSayisi"].ToString();
                txtAtki2TelSayisi.Text = frm.veriler[0]["Atki2TelSayisi"].ToString();
                txtAtki3TelSayisi.Text = frm.veriler[0]["Atki3TelSayisi"].ToString();
                txtAtki4TelSayisi.Text = frm.veriler[0]["Atki4TelSayisi"].ToString();
                lblCozgu1Uretim.Text = frm.veriler[0]["Cozgu1Uretim"].ToString();
                lblCozgu2Uretim.Text = frm.veriler[0]["Cozgu2Uretim"].ToString();
                lblAtki1Uretim.Text = frm.veriler[0]["Atki1Uretim"].ToString();
                lblAtki2Uretim.Text = frm.veriler[0]["Atki2Uretim"].ToString();
                lblAtki3Uretim.Text = frm.veriler[0]["Atki3Uretim"].ToString();
                lblAtki4Uretim.Text = frm.veriler[0]["Atki4Uretim"].ToString();
                lblTarakNo1Uretim.Text = frm.veriler[0]["TarakNo1Uretim"].ToString();
                lblTarakNo2Uretim.Text = frm.veriler[0]["TarakNo2Uretim"].ToString();
                txtCozgu1IF.Text = frm.veriler[0]["Cozgu1IF"].ToString();
                txtCozgu2IF.Text = frm.veriler[0]["Cozgu2IF"].ToString();
                txtAtki1IF.Text = frm.veriler[0]["Atki1IF"].ToString();
                txtAtki2IF.Text = frm.veriler[0]["Atki2IF"].ToString();
                txtAtki3IF.Text = frm.veriler[0]["Atki3IF"].ToString();
                txtAtki4IF.Text = frm.veriler[0]["Atki4IF"].ToString();
                txtCozgu1IB.Text = frm.veriler[0]["Cozgu1IB"].ToString();
                txtCozgu2IB.Text = frm.veriler[0]["Cozgu2IB"].ToString();
                txtAtki1IB.Text = frm.veriler[0]["Atki1IB"].ToString();
                txtAtki2IB.Text = frm.veriler[0]["Atki2IB"].ToString();
                txtAtki3IB.Text = frm.veriler[0]["Atki3IB"].ToString();
                txtAtki4IB.Text = frm.veriler[0]["Atki4IB"].ToString();
                txtAtkiMH.Text = frm.veriler[0]["Atki"].ToString();
                txtCozguMH.Text = frm.veriler[0]["Cozgu"].ToString();
                txtParcaYikamaMH.Text = frm.veriler[0]["ParcaYikama"].ToString();
                txtKumasBoyamaMH.Text = frm.veriler[0]["KumasBoyama"].ToString();
                txtDokumaFiresiMH.Text = frm.veriler[0]["DokumaFiresi"].ToString();
                txtBoyahaneFiresiMH.Text = frm.veriler[0]["BoyahaneFiresi"].ToString();
                txtKonfMaliyetMH.Text = frm.veriler[0]["KonfeksiyonMaliyeti"].ToString();
                txtKarMH.Text = frm.veriler[0]["Kar"].ToString();
                txtKDVMH.Text = frm.veriler[0]["KDV"].ToString();
                txtKurMH.Text = frm.veriler[0]["Kur"].ToString();
                txtPariteMH.Text = frm.veriler[0]["Parite"].ToString();
                txtEuro.Text = frm.veriler[0]["Euro"].ToString();
                txtBelirliFiyatForex.Text = frm.veriler[0]["BelirlenenFiyatForex"].ToString();
                txtKDVliFiyatForex.Text = frm.veriler[0]["KDVliBelirlenenFiyatForex"].ToString();
            }

        }
        private void listeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListeAc();
        }
        private void btnListe_Click(object sender, EventArgs e)
        {
            ListeAc();
        }
        public void DolarKuruGetir(string Kur)
        {
            if (ayarlar.VeritabaniTuru() == "mssql") // ilgili tablo mssql için oluşturuldu ve görev zamanlayıcı ile birlikte kayıt işlemi gerçekleştirildi
            {
                var currency =crudRepository.GetAll<Currency>("Currency").OrderByDescending(c => c.TARIH).FirstOrDefault();
                if (currency != null)
                {
                    txtKurMH.Text = currency.USD_ALIS.ToString();
                    txtPariteMH.Text = currency.USD_EUR.ToString();
                }
                else { txtKurMH.Text = ""; txtPariteMH.Text = ""; }
            }
            else
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    var sorgu = $"select {Kur},USD_EUR from Kur order by TARIH desc limit 1";
                    var rate = connection.QuerySingleOrDefault(sorgu);
                    if (rate != null)
                    {
                        txtKurMH.Text = rate.USD_ALIS.ToString();
                        txtPariteMH.Text = Math.Round(rate.USD_EUR, 2).ToString();
                    }
                    else { txtKurMH.Text = ""; txtPariteMH.Text = ""; }

                }
            }
        }
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmFirmaKartiListesi frm = new FrmFirmaKartiListesi();
            frm.ShowDialog();
            txtFirmaKodu.Text = frm.FirmaKodu;
            lblFirmaAdi.Text = frm.FirmaUnvan;
            this.CompanyId = frm.Id;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ListeAc();
        }
        private void maliyetFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Id != 0)
            {
                Forms.Rapor.FrmRaporSecimEkrani frm = new Forms.Rapor.FrmRaporSecimEkrani(this.Text, this.Id);
                frm.ShowDialog();
            }
            else
            {
                bildirim.Uyari("Form alabilmek için bir kayıt seçmeniz gerekmektedir.");
            }
        }
        private void txtReceteNo_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmUrunReceteKartiListesi frm = new FrmUrunReceteKartiListesi(InventoryId);
            frm.ShowDialog();
            if (frm.ReceteNo != null)
            {
                txtReceteNo.Text = frm.ReceteNo;
                lblReceteAd.Text = $"Ham En: {frm.HamEn}, Ham Boy: {frm.HamBoy}, Ham Gr/M2: {frm.HamGr_M2}";
                RecipeId = frm.Id;
                if (frm.MamulEn != 0 && frm.MamulBoy != 0)
                {
                    lblReceteAd.Text += $" Mamül En: {frm.MamulEn}, Mamül Boy: {frm.MamulBoy}";
                }
            }
        }
        private void kayıtNumarasınıGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Id != 0)
            {
                Forms.Liste.FrmKayitNoGoster frm = new Forms.Liste.FrmKayitNoGoster(this.Id);
                frm.ShowDialog();
            }
            else
            {
                bildirim.Uyari("Kayıt numarasını görebilmek için öncelikle bir kayıt seçmelisiniz!");
            }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (this.Id == 0)
            {
                bildirim.Uyari("Silme işlemi için listeden bir kayıt seçiniz");
            }
            else
            {
                if (bildirim.SilmeOnayı())
                {
                    using (var connection = new Baglanti().GetConnection())
                    {
                        string t1 = "delete from UretimBaslik where Id = @Id";
                        string t2 = "delete from UretimBilgileri where RefNo = @Id";
                        string t3 = "delete from UretimHesaplama where RefNo = @Id";
                        string t4 = "delete from MaliyetHesaplama where RefNo = @Id";
                        connection.Execute(t1, new { Id = this.Id });
                        connection.Execute(t2, new { Id = this.Id });
                        connection.Execute(t3, new { Id = this.Id });
                        connection.Execute(t4, new { Id = this.Id });
                    }
                    bildirim.Basarili();
                    FormVerileriniTemizle();
                }
            }
        }
        void FormVerileriniTemizle()
        {
            object[] objects0 =
            {
                txtTarakNo1,txtTarakNo2,txtTarakEn,txtHamBoy,txtBoySacak,txtEnSacak,txtHamEn,txtMamulBoy,txtMamulEn,txtCozgu1Siklik,txtCozgu2Siklik,txtAtki1Siklik,txtAtki2Siklik,txtAtki3Siklik,txtAtki4Siklik,
                txtCozgu1TelSayisi,txtCozgu2TelSayisi,txtAtki1TelSayisi,txtAtki2TelSayisi,txtAtki3TelSayisi,txtAtki4TelSayisi
                ,txtCozgu1UH,txtCozgu2UH,txtAtki1UH,txtAtki2UH,txtAtki3UH,txtAtki4UH,txtCozgu1IF,txtCozgu2IF,txtAtki1IF,txtAtki2IF,txtAtki3IF,txtAtki4IF
                ,txtCozgu1IB,txtCozgu2IB,txtAtki1IB,txtAtki2IB,txtAtki3IB,txtAtki4IB,txtCozgu1IM,txtCozgu2IM,txtAtki1IM,txtAtki2IM,txtAtki3IM,txtAtki4IM,txtGramajToplam,txtIpBoyamaSonuc
                ,txtAtkiMH,txtCozguMH,txtParcaYikamaMH,txtKumasBoyamaMH,txtDokumaFiresiMH,txtBoyahaneFiresiMH,txtKonfMaliyetMH,txt2KaliteMaliyetMH,txtKarMH,txtKDVMH,
                txtDokumaMH,txtCozguDMMH,txtIplikMaliyetMH,txtToplamUMMH,txtFireliMH,txtFireliForexHKMMH,txtFireliTlHKMMH,txtParcaYikama,txtBoyanmisKumas,txtFireliYBMMH,txtKarliYBMMH,
                txtBoyaliKumas,txtKonfeksiyonMaliyeti,txt2KaliteMaliyeti,txtKarliDU,txtDUKTL,txtKDVDahilFiyat,txtKDVDUKTL,txtbelirliFiyatTl,txtBelirliFiyatForex,
                txtKDVliFiyatForex,txtKDVliFiyatTl
            };
            yardimciAraclar.Y0(objects0);
            object[] objects1 =
            {
                txtCozgu1,txtCozgu1Bolen,txtCozgu2,txtCozgu2Bolen,txtAtki1,txtAtki1Bolen,txtAtki2,txtAtki2Bolen,txtAtki3,txtAtki3Bolen,txtAtki4,txtAtki4Bolen,txtTarakNo1Bolen,txtTarakNo2Bolen


            };
            yardimciAraclar.Y1(objects1);
        }
        private void txtUrun_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmUrunKartiListesi frm = new FrmUrunKartiListesi(InventoryType);
            frm.ShowDialog();
            InventoryId = frm.Id;
            txtUrun.Text = frm.UrunKodu;
            lblUrunAdi.Text = frm.UrunAdi;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormVerileriniTemizle();
        }
        private void TextBox_EnterKeyToTab(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Bip sesini engelle
                e.Handled = true; // Varsayılan işlemi durdur

                Control ctrl = sender as Control;
                if (ctrl != null)
                {
                    Form form = ctrl.FindForm();
                    form.SelectNextControl(ctrl, true, true, true, true); // Bir sonraki kontrole geç
                }
            }
        }
        private void AttachEnterKeyHandler(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox textBox)
                {
                    textBox.KeyDown += TextBox_EnterKeyToTab;
                }
                else if (ctrl.HasChildren) // İç içe kontrolleri de kontrol et (örneğin Panel, GroupBox)
                {
                    AttachEnterKeyHandler(ctrl);
                }
            }
        }
    }
}
