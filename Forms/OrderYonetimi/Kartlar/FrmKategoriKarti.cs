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

namespace Hesap.Forms.OrderYonetimi.Kartlar
{
    public partial class FrmKategoriKarti : DevExpress.XtraEditors.XtraForm
    {
        public FrmKategoriKarti()
        {
            InitializeComponent();
        }
        int Id = 0;
        CRUD_Operations cRUD = new CRUD_Operations();
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        //Tip = 0 kategoriye denk gelmektedir.
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Adi", txtAd.Text },
                { "OrjAdi", txtOrjAd.Text },
                { "Kullanimda", chckKullanimda.Checked},
                { "Tip","0"},
                { "EkranAdi",this.Text},
            };
            using (var connection = new Baglanti().GetConnection())
            {
                if (this.Id == 0)
                {
                    this.Id = cRUD.InsertRecord("OzellikKarti", parameters);
                    bildirim.Basarili();
                }
                else
                {
                    cRUD.UpdateRecord("OzellikKarti", parameters, this.Id);
                    bildirim.GuncellemeBasarili();
                }
            }
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmListe frm = new Liste.FrmListe(this.Text);
            frm.ShowDialog();
            txtAd.Text = frm.Adi;
            txtOrjAd.Text = frm.OrjAdi;
            chckKullanimda.Checked= frm.Kullanimda;
            Id = frm.Id;
        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = $"select top 1 * from OzellikKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from OzellikKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtAd.Text = veri.Adi.ToString();
                        txtOrjAd.Text = veri.OrjAdi.ToString();
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
            cRUD.KartSil(this.Id,"OzellikKarti");
        }
        void Temizle()
        {
            object[] kart = { txtAd,txtOrjAd,chckKullanimda};
            yardimciAraclar.KartTemizle(kart);
            this.Id = 0;
        }
    }
}