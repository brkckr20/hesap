﻿using Dapper;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using Hesap.Utils;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Hesap
{
    public partial class Form1 : XtraForm
    {
        Bildirim bildirim = new Bildirim();
        HesaplaVeYansit hesapla = new HesaplaVeYansit();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Ayarlar ayarlar = new Ayarlar();
        int Id = 0;
        public Form1()
        {
            InitializeComponent();
            InitializeTextBoxEventHandlers();
            //_connectionFactory = new DbConnect();

            //_yardimciAraclar = new YardimciAraclar("₺", txtAtkiMH, txtCozguMH);
            //_yardimciAraclar = new YardimciAraclar("$", txtParcaYikamaMH, txtKonfMaliyetMH);
            //_yardimciAraclar = new YardimciAraclar("€", txtKumasBoyamaMH);
            //_yardimciAraclar = new YardimciAraclar("%", txtDokumaFiresiMH, txtBoyahaneFiresiMH);

        }
        private void InitializeTextBoxEventHandlers()
        {
            hesapla.DogrudanYansit(lblTarakNo2Uretim, txtCozgu2Siklik);
            hesapla.DogrudanYansit(txtIpMaliyetToplam, txtIplikMaliyetMH);
            hesapla.TextboxAndTextboxToTextbox(txtCozgu1, txtCozgu1Bolen, lblCozgu1Uretim, "Bolme");
            hesapla.TextboxAndTextboxToTextbox(txtCozgu2, txtCozgu2Bolen, lblCozgu2Uretim, "Bolme");
            hesapla.TextboxAndTextboxToTextbox(txtAtki1, txtAtki1Bolen, lblAtki1Uretim, "Bolme");
            hesapla.TextboxAndTextboxToTextbox(txtAtki2, txtAtki2Bolen, lblAtki2Uretim, "Bolme");
            hesapla.TextboxAndTextboxToTextbox(txtAtki3, txtAtki3Bolen, lblAtki3Uretim, "Bolme");
            hesapla.TextboxAndTextboxToTextbox(txtAtki4, txtAtki4Bolen, lblAtki4Uretim, "Bolme");
            hesapla.TextboxAndTextboxToTextbox(txtTarakNo1, txtTarakNo1Bolen, lblTarakNo1Uretim, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtTarakNo2, txtTarakNo2Bolen, lblTarakNo2Uretim, "Carpma");
            hesapla.TextboxAndNumToTextbox(txtTarakEn, 1.05, txtHamEn);
            hesapla.TextboxAndTextboxToTextbox(txtTarakEn, lblTarakNo1Uretim, txtCozgu1TelSayisi, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtHamBoy, txtAtki1Siklik, txtAtki1TelSayisi, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtTarakNo1, txtTarakNo1Bolen, txtCozgu1Siklik, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtTarakNo2, txtTarakNo2Bolen, txtCozgu2Siklik, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtHamBoy, txtAtki2Siklik, txtAtki2TelSayisi, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtHamBoy, txtAtki3Siklik, txtAtki3TelSayisi, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtHamBoy, txtAtki4Siklik, txtAtki4TelSayisi, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(lblTarakNo2Uretim, txtTarakEn, txtCozgu2TelSayisi, "Carpma");
            hesapla.Cozgu1UHYansit(txtHamBoy, txtBoySacak, txtCozgu1TelSayisi, lblCozgu1Uretim, txtCozgu1UH);
            hesapla.Cozgu2UHYansit(txtHamBoy, txtCozgu2TelSayisi, lblCozgu2Uretim, txtCozgu2UH);
            hesapla.AtkiUHYansit(txtAtki1Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki1Uretim, txtAtki1UH);
            hesapla.AtkiUHYansit(txtAtki2Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki2Uretim, txtAtki2UH);
            hesapla.AtkiUHYansit(txtAtki3Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki3Uretim, txtAtki3UH);
            hesapla.AtkiUHYansit(txtAtki4Siklik, txtTarakEn, txtEnSacak, txtHamBoy, lblAtki4Uretim, txtAtki4UH);
            hesapla.GramajToplamYansit(txtCozgu1UH, txtCozgu2UH, txtAtki1UH, txtAtki2UH, txtAtki3UH, txtAtki4UH, txtGramajToplam);
            hesapla.TextboxAndTextboxToTextbox(txtGramajToplam, txtHamEn, txtIpBoyamaSonuc, "BolCarp");
            hesapla.TextboxAndTextboxAndTextboxToTextbox(txtCozgu1IB, txtCozgu1IF, txtCozgu1UH, txtCozgu1IM);
            hesapla.TextboxAndTextboxAndTextboxToTextbox(txtCozgu2IB, txtCozgu2IF, txtCozgu2UH, txtCozgu2IM);
            hesapla.TextboxAndTextboxAndTextboxToTextbox(txtAtki1IB, txtAtki1IF, txtAtki1UH, txtAtki1IM);
            hesapla.TextboxAndTextboxAndTextboxToTextbox(txtAtki2IB, txtAtki2IF, txtAtki2UH, txtAtki2IM);
            hesapla.TextboxAndTextboxAndTextboxToTextbox(txtAtki3IB, txtAtki3IF, txtAtki3UH, txtAtki3IM);
            hesapla.TextboxAndTextboxAndTextboxToTextbox(txtAtki4IB, txtAtki4IF, txtAtki4UH, txtAtki4IM);
            hesapla.GramajToplamYansit(txtCozgu1IM, txtCozgu2IM, txtAtki1IM, txtAtki2IM, txtAtki3IM, txtAtki4IM, txtIpMaliyetToplam);
            hesapla.EightTextbox(txtHamBoy, txtBoySacak, txtAtki1Siklik, txtAtki2Siklik, txtAtki3Siklik, txtAtki4Siklik, txtAtkiMH, txtKurMH, txtDokumaMH);
            hesapla.CozguMaliyetHesapla(txtHamBoy, txtCozguMH, txtCozguDMMH);
            hesapla.Sum3Texbox(txtDokumaMH, txtCozguDMMH, txtIplikMaliyetMH, txtToplamUMMH);
            hesapla.FireliUretimMaliyetiToplami(txtToplamUMMH, txtDokumaFiresiMH, txtToplamUMMH, txtFireliMH);
            hesapla.TextboxAndTextboxToTextbox(txtParcaYikamaMH, txtGramajToplam, txtParcaYikama, "Carpma");
            hesapla.FireliUretimMaliyetiToplami(txtFireliMH, txtKarMH, txtFireliMH, txtFireliForexHKMMH); // karli maliyette de kullanildi
            hesapla.FireliYikamaMaliyetiToplami(txtParcaYikama, txtBoyanmisKumas, txtBoyahaneFiresiMH, txtFireliYBMMH);
            hesapla.FireliUretimMaliyetiToplami(txtFireliYBMMH, txtKarMH, txtFireliYBMMH, txtKarliYBMMH);
            hesapla.DogrudanYansit(txtFireliYBMMH, txtBoyaliKumas);
            hesapla.TextboxAndTextboxToTextbox(txtBoyaliKumas, txtKonfMaliyetMH, txtKonfeksiyonMaliyeti, "Topla");
            hesapla.FireliUretimMaliyetiToplami(txtKonfeksiyonMaliyeti, txt2KaliteMaliyetMH, txtKonfeksiyonMaliyeti, txt2KaliteMaliyeti);
            hesapla.FireliUretimMaliyetiToplami(txt2KaliteMaliyeti, txtKarMH, txt2KaliteMaliyeti, txtKarliDU);
            hesapla.FireliUretimMaliyetiToplami(txtKarliDU, txtKDVMH, txtKarliDU, txtKDVDahilFiyat);
            hesapla.FireliUretimMaliyetiToplami(txtBelirliFiyatForex, txtKDVMH, txtBelirliFiyatForex, txtKDVliFiyatForex);
            hesapla.TextboxAndTextboxToTextbox(txtKumasBoyamaMH, txtPariteMH, txtEuro, "Carpma");
            hesapla.BoyanmisKumas(txtEuro, txtGramajToplam, txtFireliMH, txtBoyanmisKumas);
            hesapla.TextboxAndTextboxToTextbox(txtKurMH, txtFireliForexHKMMH, txtFireliTlHKMMH, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtKurMH, txtKarliDU, txtDUKTL, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtKurMH, txtKDVDahilFiyat, txtKDVDUKTL, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtKurMH, txtBelirliFiyatForex, txtbelirliFiyatTl, "Carpma");
            hesapla.TextboxAndTextboxToTextbox(txtKurMH, txtKDVliFiyatForex, txtKDVliFiyatTl, "Carpma");

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DolarKuruGetir("USD_ALIS");
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
            // Virgülü noktaya dönüştür
            string text = textBox.Text.Replace(",", ".");

            // Sayıyı decimal olarak parse et
            if (decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal decimalValue))
            {
                // Yuvarlama işlemi
                decimalValue = Math.Round(decimalValue, decimalPlaces);
                // Sonucu decimal olarak döndür
                return decimalValue;
            }

            return 0;
        }
        void Kaydet()
        {
            if (this.Id == 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string sqlBaslik = "INSERT INTO UretimBaslik ([FirmaKodu], [Tarih], [UrunKodu], [SiparisNo]) OUTPUT INSERTED.Id VALUES (@FirmaKodu, @Tarih, @UrunKodu, @SiparisNo)";
                    string sqlBaslikSQLite = "INSERT INTO UretimBaslik ([FirmaKodu], [Tarih], [UrunKodu], [SiparisNo]) VALUES (@FirmaKodu, @Tarih, @UrunKodu, @SiparisNo)";
                    string idQuery = "SELECT last_insert_rowid();";
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        Id = connection.QuerySingle<int>(sqlBaslik, new
                        {
                            FirmaKodu = txtFirmaKodu.Text,
                            Tarih = dateTimePicker1.EditValue,
                            UrunKodu = txtUrun.Text,
                            SiparisNo = txtReceteNo.Text
                        });
                    }
                    else
                    {
                        connection.Execute(sqlBaslikSQLite, new
                        {
                            FirmaKodu = txtFirmaKodu.Text,
                            Tarih = dateTimePicker1.EditValue,
                            UrunKodu = txtUrun.Text,
                            SiparisNo = txtReceteNo.Text
                        });
                        Id = connection.QuerySingle<int>(idQuery);
                    }
                    #region Önceki kayit - sqlite öncesi
                    //refNo =connection.QuerySingle<int>(query, new
                    //{
                    //    FirmaKodu = txtFirmaKodu.Text,
                    //    Tarih = dateTimePicker1.EditValue,
                    //    UrunKodu = txtUrun.Text,
                    //    SiparisNo = txtReceteNo.Text
                    //});
                    #endregion
                    string sqlUB = "INSERT INTO UretimBilgileri (RefNo, Cozgu1, Cozgu1Bolen, Cozgu2, Cozgu2Bolen, Atki1, Atki1Bolen, Atki2, Atki2Bolen,Atki3,Atki3Bolen,Atki4,Atki4Bolen,TarakNo1," +
                                                                "TarakNo1Bolen, TarakNo1Uretim,TarakNo2,TarakNo2Bolen,TarakNo2Uretim,TarakEn,HamBoy,BoySacak,EnSacak,HamEn,MamulBoy,MamulEn," +
                                                                "Cozgu1Siklik,Cozgu2Siklik,Atki1Siklik,Atki2Siklik,Atki3Siklik,Atki4Siklik,Cozgu1TelSayisi,Cozgu2TelSayisi," +
                                                                "Atki1TelSayisi,Atki2TelSayisi,Atki3TelSayisi,Atki4TelSayisi,Cozgu1Uretim,Cozgu2Uretim,Atki1Uretim,Atki2Uretim,Atki3Uretim,Atki4Uretim) VALUES " +
                                                             "(@RefNo, @Cozgu1, @Cozgu1Bolen, @Cozgu2, @Cozgu2Bolen, @Atki1, @Atki1Bolen, @Atki2, @Atki2Bolen, @Atki3,@Atki3Bolen, @Atki4, @Atki4Bolen,@TarakNo1," +
                                                                "@TarakNo1Bolen,@TarakNo1Uretim,@TarakNo2,@TarakNo2Bolen,@TarakNo2Uretim,@TarakEn,@HamBoy,@BoySacak,@EnSacak,@HamEn,@MamulBoy,@MamulEn," +
                                                                "@Cozgu1Siklik,@Cozgu2Siklik,@Atki1Siklik,@Atki2Siklik,@Atki3Siklik,@Atki4Siklik,@Cozgu1TelSayisi,@Cozgu2TelSayisi," +
                                                                "@Atki1TelSayisi,@Atki2TelSayisi,@Atki3TelSayisi,@Atki4TelSayisi,@Cozgu1Uretim,@Cozgu2Uretim,@Atki1Uretim,@Atki2Uretim,@Atki3Uretim,@Atki4Uretim)";
                    string sqlUH = @"INSERT INTO UretimHesaplama
                                   (RefNo,Cozgu1,Cozgu2,Atki1,Atki2,Atki3,Atki4,Cozgu1IF,Cozgu2IF,Atki1IF,Atki2IF,Atki3IF,Atki4IF,Cozgu1IB,Cozgu2IB,Atki1IB
                                    ,Atki2IB,Atki3IB,Atki4IB,Cozgu1IM,Cozgu2IM,Atki1IM,Atki2IM,Atki3IM,Atki4IM,ToplamMtul,ToplamIplikMaliyet,ToplamGRM2)
                                    VALUES
                                   (@RefNo, @Cozgu1, @Cozgu2, @Atki1, @Atki2, @Atki3, @Atki4, @Cozgu1IF, @Cozgu2IF, @Atki1IF, @Atki2IF, @Atki3IF, @Atki4IF, @Cozgu1IB, @Cozgu2IB, 
                                    @Atki1IB, @Atki2IB, @Atki3IB, @Atki4IB, @Cozgu1IM, @Cozgu2IM, @Atki1IM, @Atki2IM, @Atki3IM, @Atki4IM, @ToplamMtul, @ToplamIplikMaliyet,@ToplamGRM2)
                                    ";
                    string sqlMH = @"INSERT INTO MaliyetHesaplama
                               (RefNo,Atki,Cozgu,ParcaYikama,KumasBoyama,DokumaFiresi,BoyahaneFiresi,KonfeksiyonMaliyeti,Ikinci_K_Maliyeti,Kar,KDV,Kur,Parite,Euro,DokumaDM,CozguDM,IplikMaliyetDM,ToplamUM,FireliUM,KarliHKMForex
                            ,KarliHKM,ParcaYikamaYBM,BoyanmisKumasYBM,FireliYBM,KarliYBM,BoyaliKumasDU,KonfeksiyonMaliyetiDU,Ikinci_K_MaliyetiDU,KarliDUForex,KarliDU,KDVliDUForex,KDVliDU,BelirlenenFiyatForex,BelirleneFiyat,KDVliBelirlenenFiyatForex
                            ,KDVliBelirleneFiyat)
                            VALUES
                                       (@RefNo,@Atki,@Cozgu,@ParcaYikama,@KumasBoyama,@DokumaFiresi,@BoyahaneFiresi,@KonfeksiyonMaliyeti,@Ikinci_K_Maliyeti,@Kar,@KDV,@Kur,@Parite,@Euro,@DokumaDM,@CozguDM,@IplikMaliyetDM,@ToplamUM
                            ,@FireliUM,@KarliHKMForex,@KarliHKM,@ParcaYikamaYBM,@BoyanmisKumasYBM,@FireliYBM,@KarliYBM,@BoyaliKumasDU,@KonfeksiyonMaliyetiDU,@Ikinci_K_MaliyetiDU,@KarliDUForex,@KarliDU,@KDVliDUForex,@KDVliDU,@BelirlenenFiyatForex
                            ,@BelirleneFiyat,@KDVliBelirlenenFiyatForex,@KDVliBelirleneFiyat)";
                    connection.Execute(sqlUB, new
                    {
                        RefNo = Id,
                        Cozgu1 = txtCozgu1.Text,
                        Cozgu1Bolen = txtCozgu1Bolen.Text,
                        Cozgu2 = txtCozgu2.Text,
                        Cozgu2Bolen = txtCozgu2Bolen.Text,
                        Atki1 = txtAtki1.Text,
                        Atki1Bolen = txtAtki1Bolen.Text,
                        Atki2 = txtAtki2.Text,
                        Atki2Bolen = txtAtki2Bolen.Text,
                        Atki3 = txtAtki3.Text,
                        Atki3Bolen = txtAtki3Bolen.Text,
                        Atki4 = txtAtki4.Text,
                        Atki4Bolen = txtAtki4Bolen.Text,
                        TarakNo1 = txtTarakNo1.Text,
                        TarakNo1Bolen = txtTarakNo1Bolen.Text,
                        TarakNo1Uretim = lblTarakNo1Uretim.Text,
                        TarakNo2 = txtTarakNo2.Text,
                        TarakNo2Bolen = txtTarakNo2Bolen.Text,
                        TarakNo2Uretim = lblTarakNo2Uretim.Text,
                        TarakEn = txtTarakEn.Text,
                        HamBoy = txtHamBoy.Text,
                        BoySacak = VirgulKaldir(txtBoySacak), //KAYITTA PROBLEM OLURSA VİRGUL REPLACE EDİLECEK
                        EnSacak = VirgulKaldir(txtEnSacak),  //KAYITTA PROBLEM OLURSA VİRGUL REPLACE EDİLECEK
                        HamEn = txtHamEn.Text,
                        MamulBoy = txtMamulBoy.Text,
                        MamulEn = txtMamulEn.Text,
                        Cozgu1Siklik = txtCozgu1Siklik.Text,
                        Cozgu2Siklik = txtCozgu2Siklik.Text,
                        Atki1Siklik = txtAtki1Siklik.Text,
                        Atki2Siklik = txtAtki2Siklik.Text,
                        Atki3Siklik = txtAtki3Siklik.Text,
                        Atki4Siklik = txtAtki4Siklik.Text,
                        Cozgu1TelSayisi = txtCozgu1TelSayisi.Text,
                        Cozgu2TelSayisi = txtCozgu2TelSayisi.Text,
                        Atki1TelSayisi = txtAtki1TelSayisi.Text,
                        Atki2TelSayisi = txtAtki2TelSayisi.Text,
                        Atki3TelSayisi = txtAtki3TelSayisi.Text,
                        Atki4TelSayisi = txtAtki4TelSayisi.Text,
                        Cozgu1Uretim = lblCozgu1Uretim.Text,
                        Cozgu2Uretim = lblCozgu2Uretim.Text,
                        Atki1Uretim = lblAtki1Uretim.Text,
                        Atki2Uretim = lblAtki2Uretim.Text,
                        Atki3Uretim = lblAtki3Uretim.Text,
                        Atki4Uretim = lblAtki4Uretim.Text,
                    });
                    connection.Execute(sqlUH, new
                    {
                        RefNo = Id,
                        Cozgu1 = VirgulKaldir(txtCozgu1UH),
                        Cozgu2 = VirgulKaldir(txtCozgu2UH),
                        Atki1 = VirgulKaldir(txtAtki1UH),
                        Atki2 = VirgulKaldir(txtAtki2UH),
                        Atki3 = VirgulKaldir(txtAtki3UH),
                        Atki4 = VirgulKaldir(txtAtki4UH),
                        Cozgu1IF = VirgulKaldir(txtCozgu1IF),
                        Cozgu2IF = VirgulKaldir(txtCozgu2IF),
                        Atki1IF = VirgulKaldir(txtAtki1IF),
                        Atki2IF = VirgulKaldir(txtAtki2IF),
                        Atki3IF = VirgulKaldir(txtAtki3IF),
                        Atki4IF = VirgulKaldir(txtAtki4IF),
                        Cozgu1IB = VirgulKaldir(txtCozgu1IB),
                        Cozgu2IB = VirgulKaldir(txtCozgu2IB),
                        Atki1IB = VirgulKaldir(txtAtki1IB),
                        Atki2IB = VirgulKaldir(txtAtki2IB),
                        Atki3IB = VirgulKaldir(txtAtki3IB),
                        Atki4IB = VirgulKaldir(txtAtki4IB),
                        Cozgu1IM = VirgulKaldir(txtCozgu1IM),
                        Cozgu2IM = VirgulKaldir(txtCozgu2IM),
                        Atki1IM = VirgulKaldir(txtAtki1IM),
                        Atki2IM = VirgulKaldir(txtAtki2IM),
                        Atki3IM = VirgulKaldir(txtAtki3IM),
                        Atki4IM = VirgulKaldir(txtAtki4IM),
                        ToplamMtul = VirgulKaldir(txtGramajToplam),
                        ToplamIplikMaliyet = VirgulKaldir(txtIpMaliyetToplam),
                        ToplamGRM2 = VirgulKaldir(txtIpBoyamaSonuc)
                    });
                    connection.Execute(sqlMH, new
                    {
                        RefNo = Id,
                        Atki = VirgulKaldir(txtAtkiMH),
                        Cozgu = VirgulKaldir(txtCozguMH),
                        ParcaYikama = VirgulKaldir(txtParcaYikamaMH),
                        KumasBoyama = VirgulKaldir(txtKumasBoyamaMH),
                        DokumaFiresi = VirgulKaldir(txtDokumaFiresiMH),
                        BoyahaneFiresi = VirgulKaldir(txtBoyahaneFiresiMH),
                        KonfeksiyonMaliyeti = VirgulKaldir(txtKonfMaliyetMH),
                        Ikinci_K_Maliyeti = VirgulKaldir(txt2KaliteMaliyetMH),
                        Kar = VirgulKaldir(txtKarMH),
                        KDV = VirgulKaldir(txtKDVMH),
                        Kur = VirgulKaldir(txtKurMH),
                        Parite = VirgulKaldir(txtPariteMH),
                        Euro = VirgulKaldir(txtEuro),
                        DokumaDM = VirgulKaldir(txtDokumaMH),
                        CozguDM = VirgulKaldir(txtCozguDMMH),
                        IplikMaliyetDM = VirgulKaldir(txtIplikMaliyetMH),
                        ToplamUM = VirgulKaldir(txtToplamUMMH),
                        FireliUM = VirgulKaldir(txtFireliMH),
                        KarliHKMForex = VirgulKaldir(txtFireliForexHKMMH),
                        KarliHKM = VirgulKaldir(txtFireliTlHKMMH),
                        ParcaYikamaYBM = VirgulKaldir(txtParcaYikama),
                        BoyanmisKumasYBM = VirgulKaldir(txtBoyanmisKumas),
                        FireliYBM = VirgulKaldir(txtFireliYBMMH),
                        KarliYBM = VirgulKaldir(txtKarliYBMMH),
                        BoyaliKumasDU = VirgulKaldir(txtBoyaliKumas),
                        KonfeksiyonMaliyetiDU = VirgulKaldir(txtKonfeksiyonMaliyeti),
                        Ikinci_K_MaliyetiDU = VirgulKaldir(txt2KaliteMaliyeti),
                        KarliDUForex = VirgulKaldir(txtKarliDU),
                        KarliDU = VirgulKaldir(txtDUKTL),
                        KDVliDUForex = VirgulKaldir(txtKDVDahilFiyat),
                        KDVliDU = VirgulKaldir(txtKDVDUKTL),
                        BelirlenenFiyatForex = VirgulKaldir(txtBelirliFiyatForex),
                        BelirleneFiyat = VirgulKaldir(txtbelirliFiyatTl),
                        KDVliBelirlenenFiyatForex = VirgulKaldir(txtKDVliFiyatForex),
                        KDVliBelirleneFiyat = VirgulKaldir(txtKDVliFiyatTl),
                    });
                    bildirim.Basarili();
                }
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
            if (ayarlar.VeritabaniTuru() == "mssql")
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    var sorgu = $"select top 1 {Kur},USD_EUR from Kur order by TARIH desc";
                    var rate = connection.QuerySingleOrDefault(sorgu);
                    if (rate != null)
                    {
                        txtKurMH.Text = rate.USD_ALIS.ToString();
                        txtPariteMH.Text = Math.Round(rate.USD_EUR, 2).ToString();
                    }
                    else { txtKurMH.Text = ""; txtPariteMH.Text = ""; }
                }
            }
            else
            {
                using(var connection = new Baglanti().GetConnection())
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
                //try
                //{
                //    XmlDocument xmlVerisi = new XmlDocument();
                //    xmlVerisi.Load("http://www.tcmb.gov.tr/kurlar/today.xml");
                //    decimal dolar = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "USD")).InnerText.Replace('.', ','));
                //    decimal EUR = Convert.ToDecimal(xmlVerisi.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "EUR")).InnerText.Replace('.', ','));
                //    txtKurMH.Text = dolar.ToString();
                //    txtPariteMH.Text = Math.Round((EUR / dolar), 2).ToString();
                //}
                //catch (Exception)
                //{
                //    throw;
                //}
            }


        }
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Forms.Liste.FrmFirmaKartiListesi frm = new Forms.Liste.FrmFirmaKartiListesi();
            frm.ShowDialog();
            txtFirmaKodu.Text = frm.FirmaKodu;
            lblFirmaAdi.Text = frm.FirmaUnvan;
        }
        private void buttonEdit1_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Forms.Liste.FrmUrunKartiListesi frm = new Forms.Liste.FrmUrunKartiListesi();
            frm.ShowDialog();
            txtUrun.Text = frm.UrunKodu;
            lblUrunAdi.Text = frm.UrunAdi;
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
            Forms.Liste.FrmUrunReceteKartiListesi frm = new Forms.Liste.FrmUrunReceteKartiListesi(txtUrun.Text);
            frm.ShowDialog();
            if (frm.ReceteNo != null)
            {
                txtReceteNo.Text = frm.ReceteNo;
                lblReceteAd.Text = $"Ham En: {frm.HamEn}, Ham Boy: {frm.HamBoy}, Gr/M2: {frm.Gr_M2}";
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormVerileriniTemizle();
        }
    }
}
