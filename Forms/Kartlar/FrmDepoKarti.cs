﻿using Dapper;
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

namespace Hesap.Forms.Kartlar
{
    public partial class FrmDepoKarti : DevExpress.XtraEditors.XtraForm
    {
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        CRUD_Operations cRUD = new CRUD_Operations();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public FrmDepoKarti()
        {
            InitializeComponent();
        }
        int Id = 0;
        private string kaydeden, guncelleyen;
        private DateTime? kayitTarihi, guncellemeTarihi;

        public class DepoKarti
        {
            public string Kodu { get; set; }
            public string Adi { get; set; }
            public bool Kullanimda { get; set; }
            public string KayitEden { get; set; }
            public DateTime KayitTarihi { get; set; }
            public DateTime GuncellemeTarihi { get; set; }
            public string Guncelleyen { get; set; }
            public string Makina { get; set; }
            public int Id { get; set; }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Kodu", txtDepoKodu.Text },
                { "Adi", txtDepoAdi.Text },
                { "Kullanimda", chckKullanimda.Checked},
                { "KayitEden",  Properties.Settings.Default.KullaniciAdi},
                { "KayitTarihi", DateTime.Now},
                { "GuncellemeTarihi", DateTime.Now},
                { "Guncelleyen",  Properties.Settings.Default.KullaniciAdi},
            };
            using (var connection = new Baglanti().GetConnection())
            {
                if (Id == 0)
                {
                    this.Id = cRUD.InsertRecord("DepoKarti", parameters);
                    bildirim.Basarili();
                }
                else
                {
                    parameters.Remove("KayitTarihi"); // kayit tarihini güncellememesi için
                    cRUD.UpdateRecord("DepoKarti", parameters, this.Id);
                    bildirim.GuncellemeBasarili();
                }
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
            this.kaydeden = frm.KayitEden;
            this.guncelleyen = frm.Guncelleyen;
            this.kayitTarihi = TarihleriAta(frm.KayitTarihi);
            this.guncellemeTarihi = TarihleriAta(frm.GuncellemeTarihi);
        }
        DateTime? TarihleriAta(DateTime? tarih)
        {
            if (tarih.HasValue)
            {
                return tarih.Value;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
        void Temizle()
        {
            Id = 0;
            txtDepoKodu.Text = "";
            txtDepoAdi.Text = "";
            chckKullanimda.Checked = false;
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
                    string mssql = $"select top 1 * from DepoKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from DepoKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtDepoKodu.Text = veri.Kodu.ToString();
                        txtDepoAdi.Text = veri.Adi.ToString();
                        chckKullanimda.Checked = Convert.ToBoolean(veri.Kullanimda);
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
            //var deleteQ
            if (this.Id != 0)
            {
                string sql = "DELETE FROM DepoKarti WHERE Id = @Id";
                using (var connection = new Baglanti().GetConnection())
                {
                    if (bildirim.SilmeOnayı())
                    {
                        connection.Execute(sql, new { Id = this.Id });
                        bildirim.SilmeBasarili();
                        Temizle();
                    }
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
                Liste.FrmKayitNoGoster frm = new Liste.FrmKayitNoGoster(this.Id, this.kaydeden, this.kayitTarihi, this.guncelleyen, this.guncellemeTarihi);
                frm.ShowDialog();
            }
            else
            {
                bildirim.Uyari("Kayıt bilgisi için öncelikle bir kayıt seçimi yapınız!");
            }
        }
    }
}