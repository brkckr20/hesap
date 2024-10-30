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

namespace Hesap.Forms.MalzemeYonetimi
{
    public partial class FrmMalzemeKarti : XtraForm
    {
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        public FrmMalzemeKarti()
        {
            InitializeComponent();
        }
        int Id = 0;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var fields = new
            {
                Kodu = txtKodu.Text,
                Adi = txtAdi.Text,
                GrupKodu = txtGrupKodu.Text,
                Kullanimda = chckKullanimda.Checked,
                Tip = cmbTip.SelectedIndex.ToString(),
                Id = Id
            };
            if (this.Id == 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = @"INSERT INTO MalzemeKarti (Kodu,Adi,GrupKodu,Kullanimda,Tip) OUTPUT INSERTED.Id VALUES (@Kodu,@Adi,@GrupKodu,@Kullanimda,@Tip)";
                    string sqlite = @"INSERT INTO MalzemeKarti (Kodu,Adi,GrupKodu,Kullanimda,Tip) VALUES (@Kodu,@Adi,@GrupKodu,@Kullanimda,@Tip)";
                    string idQuery = "SELECT last_insert_rowid();";

                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        Id = connection.QuerySingle<int>(mssql, fields);
                    }
                    else
                    {
                        connection.Execute(sqlite, fields);
                        Id = connection.QuerySingle<int>(idQuery);
                    }
                    bildirim.Basarili();
                }
            }
            else
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string update = "UPDATE MalzemeKarti SET Kodu = @Kodu,Adi = @Adi,GrupKodu = @GrupKodu,Kullanimda = @Kullanimda, Tip = @Tip WHERE Id = @Id";
                    connection.Execute(update, fields);
                }
                bildirim.GuncellemeBasarili();
            }
        }

        private void grupKodlarınıGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlGrupKodlari.Visible = !pnlGrupKodlari.Visible;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        void Temizle()
        {
            Id = 0;
            txtKodu.Text = "";
            txtAdi.Text = "";
            txtGrupKodu.Text = "";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmMalzemeKartiListesi frm = new Liste.FrmMalzemeKartiListesi();
            frm.ShowDialog();
            Id = frm.Id;
            txtKodu.Text = frm.Kodu;
            txtAdi.Text = frm.Adi;
            txtGrupKodu.Text = frm.GrupKodu;
            chckKullanimda.Checked = frm.Kullanimda;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (Id != 0)
            {
                if (bildirim.SilmeOnayı())
                {
                    string sql = "delete from MalzemeKarti where Id = @Id";
                    using (var connection = new Baglanti().GetConnection())
                    {
                        connection.Execute(sql, new { Id = Id });
                    }
                    bildirim.SilmeBasarili();
                    Temizle();
                }

            }
            else
            {
                bildirim.Uyari("Silme işlemini gerçekleştirebilmek için bir kayıt seçiniz!!");
            }
        }
        void ListeGetir(string KayitTipi)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                string mssql = $"select top 1 * from MalzemeKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                string sqlite = $"select * from MalzemeKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                if (veri != null)
                {
                    // Veri nesnesini beklenen türe dönüştürüyoruz
                    var urun = veri;
                    txtKodu.Text = urun.Kodu.ToString();
                    txtAdi.Text = urun.Adi.ToString();
                    txtGrupKodu.Text = urun.GrupKodu.ToString();
                    chckKullanimda.Checked = Convert.ToBoolean(urun.Kullanimda);
                    this.Id = Convert.ToInt32(urun.Id);
                }
                else
                {
                    bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");

        }
    }
}