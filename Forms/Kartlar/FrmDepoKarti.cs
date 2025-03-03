using Dapper;
using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.Kartlar
{
    public partial class FrmDepoKarti : XtraForm
    {
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        CrudRepository crudRepository = new CrudRepository();
        string TableName = "WareHouse";
        public FrmDepoKarti()
        {
            InitializeComponent();
        }
        private string kaydeden, guncelleyen;
        private DateTime? kayitTarihi, guncellemeTarihi;
        int Id = 0;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var whParams = new Dictionary<string, object>
            {
                {"Code", txtDepoKodu.Text},
                {"Name", txtDepoAdi.Text},
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

        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmDepoKartiListesi frm = new Liste.FrmDepoKartiListesi();
            frm.ShowDialog();
            txtDepoKodu.Text = frm.Kodu;
            txtDepoAdi.Text = frm.Adi;
            chckKullanimda.Checked = frm.Kullanimda;
            Id = frm.Id;
        }
        void Temizle()
        {
            Id = 0;
            txtDepoKodu.Text = "";
            txtDepoAdi.Text = "";
            chckKullanimda.Checked = true;
            this.Id = 0;
        }
        private void btnYeni_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = $"select top 1 * from {this.TableName} where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from {this.TableName} where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtDepoKodu.Text = veri.Code.ToString();
                        txtDepoAdi.Text = veri.Name.ToString();
                        //chckKullanimda.Checked = Convert.ToBoolean(veri.Kullanimda);
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
        private void btnGeri_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }

        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (this.Id != 0)
            {
                if (bildirim.SilmeOnayı())
                {
                    crudRepository.Delete(this.TableName, this.Id);
                    bildirim.SilmeBasarili();
                    Temizle();
                }
            }
            else
            {
                bildirim.Uyari("Kayıt silebilmek için öncelikle listeden bir kayıt seçmelisiniz!");
            }
        }

        private void kayıtBilgisiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Id != 0)
            {
                //Liste.FrmKayitNoGoster frm = new Liste.FrmKayitNoGoster(this.Id, this.kaydeden, this.kayitTarihi, this.guncelleyen, this.guncellemeTarihi);
                //frm.ShowDialog();
            }
            else
            {
                bildirim.Uyari("Kayıt bilgisi için öncelikle bir kayıt seçimi yapınız!");
            }
        }
    }
}