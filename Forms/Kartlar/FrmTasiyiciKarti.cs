using Dapper;
using DevExpress.XtraEditors;
using FastColoredTextBoxNS;
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
    public partial class FrmTasiyiciKarti : DevExpress.XtraEditors.XtraForm
    {
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        CRUD_Operations cRUD = new CRUD_Operations();
        public FrmTasiyiciKarti()
        {
            InitializeComponent();
        }
        int Id = 0;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            object Kart = new
            {
                Unvan = txtUnvan.Text,
                Ad = txtAd.Text,
                Soyad = txtSoyad.Text,
                TC = txtTC.Text,
                Plaka = txtPlaka.Text,
                Dorse = txtDorse.Text,
                Id = this.Id
            }; using (var connection = new Baglanti().GetConnection())
            {
                if (Id == 0)
                {
                    string sqliteQuery = @"INSERT INTO TasiyiciKarti (Unvan,Ad,Soyad,TC,Plaka,Dorse)
                                                VALUES(@Unvan,@Ad,@Soyad,@TC,@Plaka,@Dorse)";
                    string sqlQuery = @"INSERT INTO TasiyiciKarti (Unvan,Ad,Soyad,TC,Plaka,Dorse) OUTPUT INSERTED.Id VALUES(@Unvan,@Ad,@Soyad,@TC,@Plaka,@Dorse)";
                    string idQuery = "SELECT last_insert_rowid();";
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        this.Id = connection.QuerySingle<int>(sqlQuery, Kart);
                    }
                    else
                    {
                        connection.Execute(sqliteQuery, Kart);
                        this.Id = connection.QuerySingle<int>(idQuery);
                    }
                    bildirim.Basarili();
                }
                else
                {
                    string sql = @"UPDATE TasiyiciKarti SET Unvan = @Unvan,Ad = @Ad,Soyad = @Soyad,TC = @TC,Plaka = @Plaka,Dorse = @Dorse
                                    WHERE Id = @Id";
                    connection.Execute(sql, Kart);
                    bildirim.GuncellemeBasarili();
                }

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        void Temizle()
        {
            Id = 0;
            txtUnvan.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtTC.Text = "";
            txtPlaka.Text = "";
            txtDorse.Text = "";
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmTasiyiciKartiListesi frm = new Liste.FrmTasiyiciKartiListesi();
            frm.ShowDialog();
            txtUnvan.Text = frm.Unvan;
            txtAd.Text = frm.Ad;
            txtSoyad.Text = frm.Soyad;
            txtTC.Text = frm.TC;
            txtPlaka.Text = frm.Plaka;
            txtDorse.Text = frm.Dorse;
            Id = frm.Id;

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            cRUD.KartSil(this.Id,"TasiyiciKarti");
            Temizle();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = $"select top 1 * from TasiyiciKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from TasiyiciKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtUnvan.Text = veri.Unvan.ToString();
                        txtAd.Text = veri.Ad.ToString();
                        txtSoyad.Text = veri.Soyad.ToString();
                        txtTC.Text = veri.TC.ToString();
                        txtPlaka.Text = veri.Plaka.ToString();
                        txtDorse.Text = veri.Dorse.ToString();
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

        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }

        private void FrmTasiyiciKarti_Load(object sender, EventArgs e)
        {

        }
    }
}