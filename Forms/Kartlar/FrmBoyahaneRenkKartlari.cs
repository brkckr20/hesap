using Dapper;
using DevExpress.XtraEditors;
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
using static DevExpress.Xpo.Helpers.CommandChannelHelper;

namespace Hesap.Forms
{
    public partial class FrmBoyahaneRenkKartlari : DevExpress.XtraEditors.XtraForm
    {
        public FrmBoyahaneRenkKartlari()
        {
            InitializeComponent();
        }
        Bildirim bildirim = new Bildirim();
        HesaplaVeYansit hesaplaVeYansit = new HesaplaVeYansit();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CRUD_Operations cRUD = new CRUD_Operations();
        Ayarlar ayarlar = new Ayarlar();
        int Tur = 0, Id = 0, FirmaId = 0, RenkId = 0;
        private void FrmBoyahaneRenkKartlari_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        void Temizle()
        {
            object[] kart = { txtRenkKodu,txtRenkAdi,btnCari,txtCariAdi,btnVaryant,dateTarih,dateTalepTarihi,dateOkeyTarihi,
                txtPantoneNo,txtFiyat,chckKullanimda,radioIplik,radioKumas,cmbDoviz };
            yardimciAraclar.KartTemizle(kart);
            this.Id = 0;
        }
        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmBoyahaneRenkKartlariListesi frm = new Liste.FrmBoyahaneRenkKartlariListesi();
            frm.ShowDialog();
            if (frm.veriler != null && frm.veriler.Count > 0)
            {
                this.Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                string renkTuru = frm.veriler[0]["RenkTuru"].ToString();
                radioKumas.Checked = renkTuru == "Kumaş";
                radioIplik.Checked = renkTuru == "İplik";
                if (renkTuru == "Kumaş")
                {
                    this.Tur = 1;
                }
                else if (renkTuru == "İplik")
                {
                    this.Tur = 2;
                }
                txtRenkKodu.Text = frm.veriler[0]["BoyahaneRenkKodu"].ToString();
                txtRenkAdi.Text = frm.veriler[0]["BoyahaneRenkAdi"].ToString();
                this.FirmaId = Convert.ToInt32(frm.veriler[0]["CariId"]);
                btnCari.Text = frm.veriler[0]["FirmaKodu"].ToString();
                txtCariAdi.Text = frm.veriler[0]["FirmaUnvan"].ToString();
                txtPantoneNo.Text = frm.veriler[0]["PantoneNo"].ToString();
                txtFiyat.Text = frm.veriler[0]["Fiyat"].ToString();
                cmbDoviz.Text = frm.veriler[0]["DovizCinsi"].ToString();
                chckKullanimda.Checked = Convert.ToBoolean(frm.veriler[0]["Kullanimda"]);
            }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            cRUD.KartSil(this.Id, "BoyahaneRenkKartlari");
            Temizle();
        }
        private void btnCari_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            hesaplaVeYansit.FirmaKoduVeAdiYansit(btnCari, txtCariAdi, ref this.FirmaId);
        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }
        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }
        private void radioIplik_CheckedChanged(object sender, EventArgs e)
        {
            this.Tur = 2;
        }
        private void radioKumas_CheckedChanged(object sender, EventArgs e)
        {
            this.Tur = 1;
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateTalepTarihi.EditValue = DateTime.Now;
            dateOkeyTarihi.EditValue = DateTime.Now;
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (!radioKumas.Checked && !radioIplik.Checked)
            {
                bildirim.Uyari("Kayıt yapabilmek için renk türü seçmelisiniz!");
                return;
            }
            object x = new
            {
                RenkTuru = this.Tur,
                BoyahaneRenkKodu = txtRenkKodu.Text,
                BoyahaneRenkAdi = txtRenkAdi.Text,
                CariId = this.FirmaId,
                RenkId = this.RenkId,
                Tarih = (DateTime)dateTarih.EditValue,
                TalepTarihi = (DateTime)dateTalepTarihi.EditValue,
                OkeyTarihi = (DateTime)dateOkeyTarihi.EditValue,
                PantoneNo = txtPantoneNo.Text,
                Fiyat = Convert.ToDecimal(txtFiyat.Text),
                DovizCinsi = cmbDoviz.Text,
                Kullanimda = chckKullanimda.Checked,
                Id = this.Id
            };
            using (var connection = new Baglanti().GetConnection())
            {
                if (this.Id == 0)
                {
                    if (radioIplik.Checked)
                    {
                        this.Tur = 2;
                    }
                    if (radioKumas.Checked)
                    {
                        this.Tur = 1;
                    }

                    string mssql = @"INSERT INTO BoyahaneRenkKartlari (RenkTuru,BoyahaneRenkKodu,BoyahaneRenkAdi,CariId,RenkId,Tarih,TalepTarihi,OkeyTarihi,PantoneNo,Fiyat,DovizCinsi,Kullanimda) OUTPUT INSERTED.Id
                                        VALUES (@RenkTuru,@BoyahaneRenkKodu,@BoyahaneRenkAdi,@CariId,@RenkId,@Tarih,@TalepTarihi,@OkeyTarihi,@PantoneNo,@Fiyat,@DovizCinsi,@Kullanimda)";
                    this.Id = connection.QuerySingle<int>(mssql, x);
                    bildirim.Basarili();
                }
                else
                {
                    string sql = @"UPDATE BoyahaneRenkKartlari
                               SET RenkTuru = @RenkTuru,BoyahaneRenkKodu = @BoyahaneRenkKodu,BoyahaneRenkAdi = @BoyahaneRenkAdi,CariId = @CariId,RenkId = @RenkId
                            ,Tarih = @Tarih,TalepTarihi = @TalepTarihi,OkeyTarihi = @OkeyTarihi,PantoneNo = @PantoneNo,Fiyat = @Fiyat,DovizCinsi = @DovizCinsi
                            ,Kullanimda = @Kullanimda
                             WHERE Id = @Id";
                    connection.Execute(sql, x);
                    bildirim.GuncellemeBasarili();
                }
            }
        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = $@"
                                    SELECT BRK.Id
		                            ,RenkTuru
		                            ,BoyahaneRenkKodu
		                            ,BoyahaneRenkAdi
		                            ,Tarih
		                            ,TalepTarihi
		                            ,OkeyTarihi
		                            ,ISNULL(FK.Id,0) [CariId]
		                            ,ISNULL(FK.FirmaKodu,'') [FirmaKodu]
		                            ,ISNULL(FK.FirmaUnvan,'') [FirmaUnvan]
		                            ,ISNULL(PantoneNo,'') [PantoneNo]
		                            ,ISNULL(Fiyat,0) [Fiyat]
		                            ,ISNULL(DovizCinsi,'') [DovizCinsi]
		                            ,ISNULL(Kullanimda,0) [Kullanimda]
                                    FROM BoyahaneRenkKartlari BRK
                                    LEFT JOIN FirmaKarti FK ON BRK.CariId = FK.Id
                                    WHERE BRK.Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id
                                    ORDER BY BRK.Id {(KayitTipi == "Önceki" ? "DESC" : "ASC")}
                                    OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
                    string sqlite = $"select * from FirmaKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtRenkKodu.Text = veri.BoyahaneRenkKodu.ToString();
                        txtRenkAdi.Text = veri.BoyahaneRenkAdi.ToString();
                        int renkTuru = veri.RenkTuru;
                        radioKumas.Checked = renkTuru == 1;
                        radioIplik.Checked = renkTuru == 2;
                        if (renkTuru == 1)
                        {
                            this.Tur = 1;
                        }
                        else if (renkTuru == 2)
                        {
                            this.Tur = 2;
                        }
                        this.FirmaId = veri.CariId;
                        btnCari.Text = veri.FirmaKodu.ToString();
                        txtCariAdi.Text = veri.FirmaUnvan.ToString();
                        dateTarih.EditValue = veri.Tarih;
                        dateTalepTarihi.EditValue = veri.TalepTarihi;
                        dateOkeyTarihi.EditValue = veri.OkeyTarihi;
                        //  varyant kodu id ve adı getirilicek
                        txtPantoneNo.Text = veri.PantoneNo.ToString();
                        txtFiyat.Text = veri.Fiyat.ToString();
                        cmbDoviz.Text = veri.DovizCinsi.ToString();
                        this.Id = Convert.ToInt32(veri.Id);
                    }
                    else
                    {
                        bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
                    }
                }
            }
            else
            {
                bildirim.Uyari("Kayıt gösterebilmek için öncelikle listeden bir kayıt getirmelisiniz!");
            }

        }

    }
}