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
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            object x = new
            {
                Kodu = txtKodu.Text,
                AdSoyad = txtAdSoyad.Text,
                Sifre = txtSifre.Text,
                DepartmanId = txtDepartman.Text,
                Id = this.Id
            };
            using (var connection = new Baglanti().GetConnection())
            {
                if (Id == 0)
                {
                    string sqliteQuery = @"INSERT INTO KullaniciKarti (Kodu,AdSoyad,Sifre,DepartmanId) VALUES((@Kodu,@AdSoyad,@Sifre,@DepartmanId)";
                    string sqlQuery = @"INSERT INTO KullaniciKarti (Kodu,AdSoyad,Sifre,DepartmanId) OUTPUT INSERTED.Id VALUES (@Kodu,@AdSoyad,@Sifre,@DepartmanId)";
                    string idQuery = "SELECT last_insert_rowid();";
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        this.Id = connection.QuerySingle<int>(sqlQuery, x);
                    }
                    else
                    {
                        connection.Execute(sqliteQuery, x);
                        this.Id = connection.QuerySingle<int>(idQuery);
                    }
                    bildirim.Basarili();
                }
                else
                {
                    string sql = @"UPDATE KullaniciKarti SET FirmaKodu = @FirmaKodu,FirmaUnvan = @FirmaUnvan,Adres1 = @Adres1,Adres2 = @Adres2,Adres3 = @Adres3
                                    WHERE Id = @Id";
                    connection.Execute(sql, x);
                    bildirim.GuncellemeBasarili();
                }

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
            txtAdSoyad.Text = "";
            txtDepartman.Text = "";
            txtSifre.Text = "";
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            _Operations.KartSil(this.Id, "KullaniciKarti");
            Temizle();
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmKullaniciListesi frm = new Liste.FrmKullaniciListesi();
            frm.ShowDialog();
            txtKodu.Text = frm.Kodu;
            txtAdSoyad.Text = frm.AdSoyad;
            txtSifre.Text = frm.Sifre;
            txtDepartman.Text = frm.Departman;
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
                    string mssql = $"select top 1 * from KullaniciKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from KullaniciKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtKodu.Text = veri.Kodu.ToString();
                        txtAdSoyad.Text = veri.AdSoyad.ToString();
                        txtSifre.Text = veri.Sifre.ToString();
                        txtDepartman.Text = veri.DepartmanId.ToString();
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