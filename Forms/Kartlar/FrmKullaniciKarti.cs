using Dapper;
using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;
namespace Hesap.Forms.Kartlar
{
    public partial class FrmKullaniciKarti : DevExpress.XtraEditors.XtraForm
    {
        public FrmKullaniciKarti()
        {
            InitializeComponent();
        }
        int Id = 0;
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        CRUD_Operations _Operations = new CRUD_Operations();
        CrudRepository crudRepository = new CrudRepository();
        private string TableName = "Users";
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var whParams = new Dictionary<string, object>
            {
                {"Code", txtKodu.Text},
                {"Name", txtAd.Text},
                {"Surname", txtSoyad.Text},
                {"Password", txtSifre.Text},
                {"IsUse", chckKullanimda.Checked},
            };
            if (this.Id == 0)
            {
                Id = crudRepository.Insert(this.TableName, whParams);
                bildirim.Basarili();
            }
            else
            {
                crudRepository.Update(this.TableName, this.Id, whParams);
                bildirim.GuncellemeBasarili();
            }
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        void Temizle()
        {
            Id = 0;
            txtKodu.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtSifre.Text = "";
            chckKullanimda.Checked = true;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(this.TableName,this.Id,this.Temizle);
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmKullaniciListesi frm = new Liste.FrmKullaniciListesi();
            frm.ShowDialog();
            txtKodu.Text = frm.Kodu;
            txtAd.Text = frm.Ad;
            txtSoyad.Text = frm.Soyad;
            txtSifre.Text = frm.Sifre;
            chckKullanimda.Checked = frm.Kullanimda;
            this.Id = frm.Id;
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }
        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = $"select top 1 * from {this.TableName} where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from KullaniciKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtKodu.Text = veri.Code.ToString();
                        txtAd.Text = veri.Name.ToString();
                        txtSoyad.Text = veri.Surname.ToString();
                        txtSifre.Text = veri.Password.ToString();
                        chckKullanimda.Checked = Convert.ToBoolean(veri.IsUse);
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