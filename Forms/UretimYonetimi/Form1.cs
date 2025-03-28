using Dapper;
using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Forms.Liste;
using Hesap.Helpers;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
        DateConvertHelper dch = new DateConvertHelper();
        int Id = 0, CompanyId = 0, InventoryId = 0, RecipeId = 0, InventoryType = Convert.ToInt32(InventoryTypes.Kumas), UretimBilgileriId = 0, UretimHesaplamaId = 0, MaliyetHesaplamaId = 0;
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
            lblFisNo.Text = crudRepository.GetNumaratorWithCondition("Cost","OrderNo");
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
            try
            {
                byte[] resimData = yardimciAraclar.GetPictureData(pictureBox1);
                var costParams = new Dictionary<string, object>
                {
                    {"CompanyId",CompanyId},
                    {"Date", dch.SaveFormattedDate(dateTimePicker1)},
                    {"InventoryId", InventoryId},
                    {"RecipeId", RecipeId},
                    {"OrderNo", lblFisNo.Text},
                };
                if (resimData != null && resimData.Length > 0)
                {
                    costParams.Add("ProductImage", resimData);
                }
                var CPI_params = new Dictionary<string, object>
                { // ilgili alanlara ihtiyaç olması durumunda VirgulKaldır metodu eklenebilir
                     {"YI_Warp1", VirgulKaldir(txtCozgu1)},{"YI_Warp1Divider", VirgulKaldir(txtCozgu1Bolen)},{"YI_Warp1Result", VirgulKaldir(lblCozgu1Uretim)},{"YI_Warp2", VirgulKaldir(txtCozgu2)},{"YI_Warp2Divider", VirgulKaldir(txtCozgu2Bolen)},{"YI_Warp2Result", VirgulKaldir(lblCozgu2Uretim)},{"YI_Scarf1", VirgulKaldir(txtAtki1)},{"YI_Scarf1Divider", VirgulKaldir(txtAtki1Bolen)},{"YI_Scarf1Result", VirgulKaldir(lblAtki1Uretim)},{"YI_Scarf2", VirgulKaldir(txtAtki2)},{"YI_Scarf2Divider", VirgulKaldir(txtAtki2Bolen)},{"YI_Scarf2Result", VirgulKaldir(lblAtki2Uretim)},{"YI_Scarf3", VirgulKaldir(txtAtki3)},{"YI_Scarf3Divider", VirgulKaldir(txtAtki3Bolen)},{"YI_Scarf3Result", VirgulKaldir(lblAtki3Uretim)},{"YI_Scarf4", VirgulKaldir(txtAtki4)},{"YI_Scarf4Divider", VirgulKaldir(txtAtki4Bolen)},{"YI_Scarf4Result", VirgulKaldir(lblAtki4Uretim)},{"WI_CombNo1",VirgulKaldir(txtTarakNo1)},{"WI_CombNo1Multiplier",VirgulKaldir(txtTarakNo1Bolen)},{"WI_CombNo1Result",VirgulKaldir(lblTarakNo1Uretim)},{"WI_CombNo2",VirgulKaldir(txtTarakNo2)},{"WI_CombNo2Multiplier",VirgulKaldir(txtTarakNo2Bolen)},{"WI_CombNo2Result",VirgulKaldir(lblTarakNo2Uretim)},{"WI_CombWidth",VirgulKaldir(txtTarakEn)},{"WI_RawHeight",VirgulKaldir(txtHamBoy)},{"WI_HeightEaves",VirgulKaldir(txtBoySacak)},{"WI_WidthEaves",VirgulKaldir(txtEnSacak)},{"WI_RawWidth",VirgulKaldir(txtHamEn)},{"WI_ProductHeight",VirgulKaldir(txtMamulBoy)},{"WI_ProductWidth",VirgulKaldir(txtMamulEn)},{"D_Warp1",VirgulKaldir(txtCozgu1Siklik)},{"D_Warp2",VirgulKaldir(txtCozgu2Siklik)},{"D_Scarf1",VirgulKaldir(txtAtki1Siklik)},{"D_Scarf2",VirgulKaldir(txtAtki2Siklik)},{"D_Scarf3",VirgulKaldir(txtAtki3Siklik)},{"D_Scarf4",VirgulKaldir(txtAtki4Siklik)},{"NW_Warp1",VirgulKaldir(txtCozgu1TelSayisi)},{"NW_Warp2",VirgulKaldir(txtCozgu2TelSayisi)},{"NW_Scarf1",VirgulKaldir(txtAtki1TelSayisi)},{"NW_Scarf2",VirgulKaldir(txtAtki2TelSayisi)},{"NW_Scarf3",VirgulKaldir(txtAtki3TelSayisi)},{"NW_Scarf4",VirgulKaldir(txtAtki4TelSayisi)}
                };
                var CPC_params = new Dictionary<string, object>
                {
                     {"WC_Warp1",VirgulKaldir(txtCozgu1UH)},{"WC_Warp2",VirgulKaldir(txtCozgu2UH)},{"WC_Scarf1",VirgulKaldir(txtAtki1UH)},{"WC_Scarf2",VirgulKaldir(txtAtki2UH) },{"WC_Scarf3",VirgulKaldir(txtAtki3UH)},{"WC_Scarf4",VirgulKaldir(txtAtki4UH) },{"WC_Total",VirgulKaldir(txtGramajToplam) },{"YP_Warp1",VirgulKaldir(txtCozgu1IF) },{"YP_Warp2",VirgulKaldir(txtCozgu2IF)},{"YP_Scarf1",VirgulKaldir(txtAtki1IF) },{"YP_Scarf2",VirgulKaldir(txtAtki2IF) },{"YP_Scarf3",VirgulKaldir(txtAtki3IF) },{"YP_Scarf4",VirgulKaldir(txtAtki4IF) },{"YD_Warp1",VirgulKaldir(txtCozgu1IB) },{"YD_Warp2",VirgulKaldir(txtCozgu2IB) },{"YD_Scarf1",VirgulKaldir(txtAtki1IB) },{"YD_Scarf2",VirgulKaldir(txtAtki2IB) },{"YD_Scarf3",VirgulKaldir(txtAtki3IB)},{"YD_Scarf4",VirgulKaldir(txtAtki4IB) },{"YD_Result",VirgulKaldir(txtIpBoyamaSonuc) },{"YC_Warp1",VirgulKaldir(txtCozgu1IM)},{"YC_Warp2",VirgulKaldir(txtCozgu2IM)},{"YC_Scarf1",VirgulKaldir(txtAtki1IM)},{"YC_Scarf2",VirgulKaldir(txtAtki2IM)},{"YC_Scarf3",VirgulKaldir(txtAtki3IM)},{"YC_Scarf4",VirgulKaldir(txtAtki4IM)},{"YC_Result",VirgulKaldir(txtIpMaliyetToplam)}
                };
                var CCC_params = new Dictionary<string, object> //Maliyet Hesaplama - CostCostCalculate
                {
                    {"PP_Scarf",VirgulKaldir(txtAtkiMH)},{"PP_Warp",VirgulKaldir(txtCozguMH)},{"PP_PartsWashing",VirgulKaldir(txtParcaYikamaMH)},{"PP_FabricWashing",VirgulKaldir(txtKumasBoyamaMH)},{"PP_WeavingWaste",VirgulKaldir(txtDokumaFiresiMH)},{"PP_DyehouseWaster",VirgulKaldir(txtBoyahaneFiresiMH)},{"PP_GarmentCost",VirgulKaldir(txtKonfMaliyetMH)},{"PP_2QualityCost",VirgulKaldir(txt2KaliteMaliyetMH)},{"PP_Profit",VirgulKaldir(txtKarMH)},{"PP_Vat",VirgulKaldir(txtKDVMH)},{"PP_Currency",VirgulKaldir(txtKurMH)},{"PP_Parity",VirgulKaldir(txtPariteMH)},{"PP_Euro",VirgulKaldir(txtEuro)},{"WC_Weaving",VirgulKaldir(txtDokumaMH)},{"WC_Warp",VirgulKaldir(txtCozguDMMH)},{"WC_YarnCost",VirgulKaldir(txtIplikMaliyetMH)},{"PC_Total",VirgulKaldir(txtToplamUMMH)},{"PC_Wasted",VirgulKaldir(txtFireliMH)},{"RFC_ProfitableForex",VirgulKaldir(txtFireliForexHKMMH)},{"RFC_Profitable",VirgulKaldir(txtFireliTlHKMMH)},{"WDC_PartsWashing",VirgulKaldir(txtParcaYikama)},{"WDC_DyedFabric",VirgulKaldir(txtBoyanmisKumas)},{"WDC_Wasted",VirgulKaldir(txtFireliYBMMH)},{"WDC_ProfitableForex",VirgulKaldir(txtKarliYBMMH)},{"SP_DyedFabric",VirgulKaldir(txtBoyaliKumas)},{"SP_GarmentCost",VirgulKaldir(txtKonfeksiyonMaliyeti)},{"SP_2QualityCost",VirgulKaldir(txt2KaliteMaliyeti)},{"SP_ProfitableForex",VirgulKaldir(txtKarliDU)},{"SP_Profitable",VirgulKaldir(txtDUKTL)},{"SP_VatIncludeForex",VirgulKaldir(txtKDVDahilFiyat)},{"SP_VatInclude",VirgulKaldir(txtKDVDUKTL)},{"PriceDeterminedForex",VirgulKaldir(txtBelirliFiyatForex)},{"PriceDetermined",VirgulKaldir(txtbelirliFiyatTl)},{"VatIncludedPriceForex",VirgulKaldir(txtKDVliFiyatForex)},{"VatIncluded",VirgulKaldir(txtKDVliFiyatTl)},{"WDC_DyedFabricTL", Convert.ToDecimal(VirgulKaldir(txtKurMH)) * Convert.ToDecimal(VirgulKaldir(txtBoyanmisKumas))}
                };
                if (this.Id == 0)
                {
                    costParams.Add("InsertedBy",Properties.Settings.Default.Id);
                    costParams.Add("InsertedDate",DateTime.Now);
                    Id = crudRepository.Insert(this.TableName1, costParams);
                    CPI_params.Add("CostId", this.Id);
                    CPC_params.Add("CostId", this.Id);
                    CCC_params.Add("CostId", this.Id);
                    UretimBilgileriId = crudRepository.Insert(TableName2, CPI_params);
                    UretimHesaplamaId = crudRepository.Insert(TableName3, CPC_params);
                    MaliyetHesaplamaId = crudRepository.Insert(TableName4, CCC_params);
                    bildirim.Basarili();
                }
                else
                {
                    costParams.Add("UpdatedBy", Properties.Settings.Default.Id);
                    costParams.Add("UpdatedDate", DateTime.Now);
                    crudRepository.Update(this.TableName1, this.Id, costParams);
                    crudRepository.Update(TableName2, this.UretimBilgileriId, CPI_params);
                    crudRepository.Update(TableName3, this.UretimHesaplamaId, CPC_params);
                    crudRepository.Update(TableName4, this.MaliyetHesaplamaId, CCC_params);
                    bildirim.GuncellemeBasarili();
                }
            }
            catch (Exception ex)
            {
                bildirim.Uyari($"Hata : {ex.Message}");
            }
        }
        void ListeAc()
        {
            FrmMaliyetHesaplamaListesi frm = new FrmMaliyetHesaplamaListesi();
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                this.Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                dch.ConvertDateForDbType(dateTimePicker1, frm.veriler[0]["Tarih"].ToString());
                this.CompanyId = Convert.ToInt32(frm.veriler[0]["Firma Id"]);
                txtFirmaKodu.Text = frm.veriler[0]["Firma Kodu"].ToString();
                lblFirmaAdi.Text = frm.veriler[0]["Firma Adı"].ToString();
                this.InventoryId = Convert.ToInt32(frm.veriler[0]["Malzeme Id"]);
                txtUrun.Text = frm.veriler[0]["Malzeme Kodu"].ToString();
                lblUrunAdi.Text = frm.veriler[0]["Malzeme Adı"].ToString();
                this.RecipeId = Convert.ToInt32(frm.veriler[0]["Reçete Id"]);
                txtReceteNo.Text = frm.veriler[0]["Reçete No"].ToString();
                lblReceteAd.Text = frm.veriler[0]["Reçete Açıklama"].ToString();
                lblFisNo.Text = frm.veriler[0]["Fiş No"].ToString(); // aslında reçete no alanında tekabül ediyor.
                this.UretimBilgileriId = Convert.ToInt32(frm.veriler[0]["Uretim Bilgileri Id"]);
                this.UretimHesaplamaId = Convert.ToInt32(frm.veriler[0]["Uretim Hesaplama Id"]);
                this.MaliyetHesaplamaId = Convert.ToInt32(frm.veriler[0]["Maliyet Hesaplama Id"]);
                txtCozgu1.Text = Convert.ToDecimal(frm.veriler[0]["Çözgü 1 İp.Bil."]).ToString("0");
                txtCozgu1Bolen.Text = Convert.ToDecimal(frm.veriler[0]["Çözgü 1 Bölen İp.Bil."]).ToString("0");
                txtCozgu2.Text = Convert.ToDecimal(frm.veriler[0]["Çözgü 2 İp.Bil."]).ToString("0");
                txtCozgu2Bolen.Text = Convert.ToDecimal(frm.veriler[0]["Çözgü 2 Bölen İp.Bil."]).ToString("0");
                txtAtki1.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 1 İp.Bil."]).ToString("0");
                txtAtki1Bolen.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 1 Bölen İp.Bil."]).ToString("0");
                txtAtki2.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 2 İp.Bil."]).ToString("0");
                txtAtki2Bolen.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 2 Bölen İp.Bil."]).ToString("0");
                txtAtki3.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 3 İp.Bil."]).ToString("0");
                txtAtki3Bolen.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 3 Bölen İp.Bil."]).ToString("0");
                txtAtki4.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 4 İp.Bil."]).ToString("0");
                txtAtki4Bolen.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 4 Bölen İp.Bil."]).ToString("0");
                txtTarakNo1.Text = Convert.ToDecimal(frm.veriler[0]["Tar.No 1"]).ToString("0");
                txtTarakNo1Bolen.Text = Convert.ToDecimal(frm.veriler[0]["Tar.No 1 Çarpan"]).ToString("0");
                txtTarakNo2.Text = Convert.ToDecimal(frm.veriler[0]["Tar.No 2"]).ToString("0");
                txtTarakNo2Bolen.Text = Convert.ToDecimal(frm.veriler[0]["Tar.No 2 Çarpan"]).ToString("0");
                txtTarakEn.Text = Convert.ToDecimal(frm.veriler[0]["Tar.En"]).ToString("0");
                txtHamBoy.Text = Convert.ToDecimal(frm.veriler[0]["Ham Boy"]).ToString("0");
                txtBoySacak.Text = Convert.ToDecimal(frm.veriler[0]["Boy Saçak"]).ToString("0.#");
                txtEnSacak.Text = Convert.ToDecimal(frm.veriler[0]["En Saçak"]).ToString("0.#");
                txtHamEn.Text = Convert.ToDecimal(frm.veriler[0]["Ham En"]).ToString("0");
                txtMamulBoy.Text = Convert.ToDecimal(frm.veriler[0]["Mamul Boy"]).ToString("0");
                txtMamulEn.Text = Convert.ToDecimal(frm.veriler[0]["Mamul En"]).ToString("0");
                txtAtki1Siklik.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 1 Sıklık"]).ToString("0");
                txtAtki2Siklik.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 2 Sıklık"]).ToString("0");
                txtAtki3Siklik.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 3 Sıklık"]).ToString("0");
                txtAtki4Siklik.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 4 Sıklık"]).ToString("0");
                txtCozgu1IF.Text = Convert.ToDecimal(frm.veriler[0]["Çözgü 1 İp.Fiy"]).ToString("0.###");
                txtCozgu2IF.Text = Convert.ToDecimal(frm.veriler[0]["Çözgü 2 İp.Fiy"]).ToString("0.###");
                txtAtki1IF.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 1 İp.Fiy"]).ToString("0.###");
                txtAtki2IF.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 2 İp.Fiy"]).ToString("0.###");
                txtAtki3IF.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 3 İp.Fiy"]).ToString("0.###");
                txtAtki4IF.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 4 İp.Fiy"]).ToString("0.###");
                txtCozgu1IB.Text = Convert.ToDecimal(frm.veriler[0]["Çözgü 1 İpBoya"]).ToString("0.###");
                txtCozgu2IB.Text = Convert.ToDecimal(frm.veriler[0]["Çözgü 2 İpBoya"]).ToString("0.###");
                txtAtki1IB.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 1 İpBoya"]).ToString("0.###");
                txtAtki2IB.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 2 İpBoya"]).ToString("0.###");
                txtAtki3IB.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 3 İpBoya"]).ToString("0.###");
                txtAtki4IB.Text = Convert.ToDecimal(frm.veriler[0]["Atkı 4 İpBoya"]).ToString("0.###");
                txtAtkiMH.Text = Convert.ToDecimal(frm.veriler[0]["Atkı"]).ToString("0.###");
                txtCozguMH.Text = Convert.ToDecimal(frm.veriler[0]["Çözgü"]).ToString("0.###");
                txtParcaYikamaMH.Text = Convert.ToDecimal(frm.veriler[0]["Parça Yıkama"]).ToString("0.###");
                txtKumasBoyamaMH.Text = Convert.ToDecimal(frm.veriler[0]["Kumaş Boyama"]).ToString("0.###");
                txtDokumaFiresiMH.Text = Convert.ToDecimal(frm.veriler[0]["Dokuma Firesi"]).ToString("0.###");
                txtBoyahaneFiresiMH.Text = Convert.ToDecimal(frm.veriler[0]["Boyahane Boyama"]).ToString("0.###");
                txtKonfMaliyetMH.Text = Convert.ToDecimal(frm.veriler[0]["Konf.Maliyeti"]).ToString("0.###");
                txt2KaliteMaliyetMH.Text = Convert.ToDecimal(frm.veriler[0]["2.Kalite Maliyet"]).ToString("0.###");
                txtKarMH.Text = Convert.ToDecimal(frm.veriler[0]["Kâr"]).ToString("0.###");
                txtKDVMH.Text = Convert.ToDecimal(frm.veriler[0]["KDV"]).ToString("0.###");
                txtKurMH.Text = Convert.ToDecimal(frm.veriler[0]["Kur"]).ToString("0.###");
                txtPariteMH.Text = Convert.ToDecimal(frm.veriler[0]["Parite"]).ToString("0.###");
                txtEuro.Text = Convert.ToDecimal(frm.veriler[0]["Euro"]).ToString("0.###");
                txtBelirliFiyatForex.Text = Convert.ToDecimal(frm.veriler[0]["Belirlenen Fiyat Döviz"]).ToString("0.###");
                txtKDVliFiyatForex.Text = Convert.ToDecimal(frm.veriler[0]["KDVli Belirlenen Fiyat Döviz"]).ToString("0.###");
                if (frm.veriler[0]["Ürün Resmi"] != DBNull.Value && frm.veriler[0]["Ürün Resmi"] is byte[])
                {
                    byte[] imageData = (byte[])frm.veriler[0]["Ürün Resmi"];
                    if (imageData.Length > 0)
                    {
                        using (MemoryStream memoryStream = new MemoryStream(imageData))
                        {
                            pictureBox1.Image = Image.FromStream(memoryStream);
                        }

                        string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                        File.WriteAllBytes(tempFilePath, imageData);
                        pictureBox1.Tag = tempFilePath;
                    }
                }
                else
                {
                    pictureBox1.Image = null;
                    pictureBox1.Tag = null;
                }

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
                var currency = crudRepository.GetAll<Currency>("Currency").OrderByDescending(c => c.TARIH).FirstOrDefault();
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
                    var sorgu = $"select {Kur},USD_EUR from Currency order by TARIH desc limit 1";
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
                bildirim.Uyari("Form alabilmek için bir kayıt seçmeniz veya formu kaydetmeniz gerekmektedir.");
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
                if (frm.UrunResmi != null)
                {
                    using (MemoryStream memoryStream = new MemoryStream(frm.UrunResmi))
                    {
                        pictureBox1.Image = Image.FromStream(memoryStream);
                    }
                    string tempFilePath = Path.GetTempFileName() + ".png";
                    File.WriteAllBytes(tempFilePath, frm.UrunResmi);
                    pictureBox1.Tag = tempFilePath;

                }
                if (frm.MamulEn != 0 && frm.MamulBoy != 0)
                {
                    lblReceteAd.Text += $" Mamül En: {frm.MamulEn}, Mamül Boy: {frm.MamulBoy}";
                }
            }
        }

        private void alertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bildirim.Uyari("deneme amaçlı oluşturuldu");
        }

        private void btnUrunResmiSec_Click(object sender, EventArgs e)
        {
            yardimciAraclar.SelectImage(pictureBox1);
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            yardimciAraclar.OpenPicture(pictureBox1);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            yardimciAraclar.DeleteTempFile(pictureBox1);
        }
        private void kayıtNumarasınıGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Id != 0)
            {
                FrmKayitNoGoster frm = new FrmKayitNoGoster(this.Id,this.TableName1);
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
                    crudRepository.Delete(this.TableName1,this.Id);
                    crudRepository.Delete(this.TableName2,this.UretimBilgileriId);
                    crudRepository.Delete(this.TableName3,this.UretimHesaplamaId);
                    crudRepository.Delete(this.TableName4,this.MaliyetHesaplamaId);
                    bildirim.SilmeBasarili();
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
            dateTimePicker1.EditValue = DateTime.Now;
            txtFirmaKodu.Text = "";
            lblFirmaAdi.Text = "";
            txtUrun.Text = "";
            lblUrunAdi.Text = "";
            txtReceteNo.Text = "";
            lblReceteAd.Text = "";
            lblFisNo.Text = numarator.NumaraVer("Maliyet");
            DolarKuruGetir("USD_ALIS");
            pictureBox1.Image = null;
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
