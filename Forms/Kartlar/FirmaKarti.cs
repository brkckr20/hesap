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
    public partial class FrmFirmaKarti : XtraForm
    {
        Baglanti _baglanti;
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        int Id = 0;
        public FrmFirmaKarti()
        {
            InitializeComponent();
            _baglanti = new Baglanti();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            object x = new
            {
                FirmaKodu = txtFirmaKodu.Text,
                FirmaUnvan = txtFirmaUnvan.Text,
                Adres1 = txtAdres1.Text,
                Adres2 = txtAdres2.Text,
                Adres3 = txtAdres3.Text,
                Id = this.Id
            };

            using (var connection = new Baglanti().GetConnection())
            {
                if (Id == 0)
                {
                    string sqliteQuery = @"INSERT INTO FirmaKarti (FirmaKodu,FirmaUnvan,Adres1,Adres2,Adres3)
                                                VALUES(@FirmaKodu,@FirmaUnvan,@Adres1,@Adres2,@Adres3)";
                    string sqlQuery = @"INSERT INTO FirmaKarti (FirmaKodu,FirmaUnvan,Adres1,Adres2,Adres3) OUTPUT INSERTED.Id VALUES(@FirmaKodu,@FirmaUnvan,@Adres1,@Adres2,@Adres3)";
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
                    string sql = @"UPDATE FirmaKarti SET FirmaKodu = @FirmaKodu,FirmaUnvan = @FirmaUnvan,Adres1 = @Adres1,Adres2 = @Adres2,Adres3 = @Adres3
                                    WHERE Id = @Id";
                    connection.Execute(sql, x);
                    bildirim.GuncellemeBasarili();
                }

            }
        }
        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmFirmaKartiListesi frm = new Liste.FrmFirmaKartiListesi();
            frm.ShowDialog();

            txtFirmaKodu.Text = frm.FirmaKodu;
            txtFirmaUnvan.Text = frm.FirmaUnvan;
            txtAdres1.Text = frm.Adres1;
            txtAdres2.Text = frm.Adres2;
            txtAdres3.Text = frm.Adres3;
            Id = frm.Id;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        void Temizle()
        {
            Id = 0;
            txtFirmaKodu.Text = "";
            txtFirmaUnvan.Text = "";
            txtAdres1.Text = "";
            txtAdres2.Text = "";
            txtAdres3.Text = "";
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (this.Id != 0)
            {
                string sql = "DELETE FROM FirmaKarti WHERE Id = @Id";
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
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = $"select top 1 * from FirmaKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from FirmaKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtFirmaKodu.Text = veri.FirmaKodu.ToString();
                        txtFirmaUnvan.Text = veri.FirmaUnvan.ToString();
                        txtAdres1.Text = veri.Adres1.ToString();
                        txtAdres2.Text = veri.Adres2.ToString();
                        txtAdres3.Text = veri.Adres3.ToString();
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
    }
}