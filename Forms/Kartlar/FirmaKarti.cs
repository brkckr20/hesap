using Dapper;
using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Models;
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
        CRUD_Operations cRUD = new CRUD_Operations();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CrudRepository crudRepository = new CrudRepository();
        int Id = 0;
        private string TableName = "Company";
        public FrmFirmaKarti()
        {
            InitializeComponent();
            _baglanti = new Baglanti();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "CompanyCode", txtFirmaKodu.Text },
                { "CompanyName", txtFirmaUnvan.Text },
                { "AddressLine1", txtAdres1.Text},
                { "AddressLine2", txtAdres2.Text},
                { "AddressLine3", txtAdres3.Text},
            };
            using (var connection = new Baglanti().GetConnection())
            {
                if (this.Id == 0)
                {
                    this.Id = cRUD.InsertRecord(TableName, parameters);
                    bildirim.Basarili(); 
                }
                else
                {
                    cRUD.UpdateRecord(TableName, parameters, this.Id);
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        void Temizle()
        {
            object[] kart = { txtFirmaKodu,txtFirmaUnvan,txtAdres1,txtAdres2,txtAdres3 };
            yardimciAraclar.KartTemizle(kart);
            this.Id = 0;
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (this.Id != 0)
            {
                crudRepository.Delete(this.TableName, this.Id);
                Temizle();
            }
            else
            {
                bildirim.Uyari("Silme işlemini gerçekleştirebilmek için bir kayıt seçiniz");
            }
        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = $"select top 1 * from {TableName} where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from {TableName} where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtFirmaKodu.Text = veri.CompanyCode.ToString();
                        txtFirmaUnvan.Text = veri.CompanyName.ToString();
                        txtAdres1.Text = veri.AddressLine1.ToString();
                        txtAdres2.Text = veri.AddressLine2.ToString();
                        txtAdres3.Text = veri.AddressLine3.ToString();
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

        private void FrmFirmaKarti_Load(object sender, EventArgs e)
        {

        }

        private void sonNumarayıAktarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtFirmaKodu.Text = crudRepository.GetMaxRecord<string>(this.TableName, "CompanyCode");
        }
    }
}