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
using Dapper;

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmUrunKarti : DevExpress.XtraEditors.XtraForm
    {
        public FrmUrunKarti()
        {
            InitializeComponent();
        }

        int Id = 0;
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            object baslik = new
            {
                UrunKodu = txtUrunKodu.Text,
                UrunAdi = txtUrunAdi.EditValue,
                Pasif = chckPasif.Checked,
                Id = this.Id,
            };
            if (Id == 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string sql = "INSERT INTO UrunKarti (UrunKodu ,UrunAdi ,Pasif) OUTPUT INSERTED.Id  VALUES (@UrunKodu ,@UrunAdi ,@Pasif)";
                    string sqlite = "INSERT INTO UrunKarti (UrunKodu ,UrunAdi ,Pasif) VALUES (@UrunKodu ,@UrunAdi ,@Pasif)";
                    string idQuery = "SELECT last_insert_rowid();";
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        this.Id = connection.QuerySingle<int>(sql, baslik);
                    }
                    else
                    {
                        connection.Execute(sqlite, baslik);
                        this.Id = connection.QuerySingle<int>(idQuery);
                    }

                    bildirim.Basarili();
                }
            }
            else
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string sql = "UPDATE UrunKarti SET [UrunKodu] = @UrunKodu ,[UrunAdi] = @UrunAdi ,[Pasif] = @Pasif WHERE Id = @Id";
                    connection.Execute(sql, baslik);
                    bildirim.GuncellemeBasarili();
                }
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmUrunKartiListesi frm = new Liste.FrmUrunKartiListesi();
            frm.ShowDialog();
            txtUrunKodu.Text = frm.UrunKodu;
            txtUrunAdi.Text = frm.UrunAdi;
            chckPasif.Checked = frm.Pasif;
            this.Id = frm.Id;
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (bildirim.SilmeOnayı())
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string sql = "delete from UrunKarti where Id = @Id";
                    connection.Execute(sql, new { Id = this.Id });
                    bildirim.SilmeBasarili();
                    txtUrunKodu.Text = "";
                    txtUrunAdi.Text = "";
                    chckPasif.Checked = false;
                    this.Id = 0;
                }
            }

        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }
        void ListeGetir(string KayitTipi)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                string mssql = $"select top 1 * from UrunKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                string sqlite = $"select * from UrunKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                if (veri != null)
                {
                    // Veri nesnesini beklenen türe dönüştürüyoruz
                    var urun = veri;
                    txtUrunKodu.Text = urun.UrunKodu.ToString();
                    txtUrunAdi.Text = urun.UrunAdi.ToString();
                    chckPasif.Checked = Convert.ToBoolean(urun.Pasif);
                    this.Id = Convert.ToInt32(urun.Id);
                }
                else
                {
                    bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
                }
            }
        }
        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtUrunKodu.Text = "";
            txtUrunAdi.Text = "";
            chckPasif.Checked = false;
            this.Id = 0;
        }

        private void FrmUrunKarti_Load(object sender, EventArgs e)
        {

        }

        private void buttonEdit1_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmUrunTipiSecimi frm = new FrmUrunTipiSecimi();
            frm.ShowDialog();
            txtUrunKodu.Text = frm.OnEk + frm.yeniNumaraStr;
        }
    }
}