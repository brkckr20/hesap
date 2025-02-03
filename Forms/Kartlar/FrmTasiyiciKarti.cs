using Dapper;
using DevExpress.XtraEditors;
using FastColoredTextBoxNS;
using Hesap.DataAccess;
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
        CrudRepository crudRepository = new CrudRepository();
        private string TableName = "Transporter";
        public FrmTasiyiciKarti()
        {
            InitializeComponent();
        }
        int Id = 0;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var whParams = new Dictionary<string, object>
            {
                {"Title", txtUnvan.Text},
                {"Name", txtAd.Text},
                {"Surname", txtSoyad.Text},
                {"TCKN", txtTC.Text},
                {"NumberPlate", txtPlaka.Text},
                {"TrailerNumber", txtDorse.Text},
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
            chckKullanimda.Checked = true;
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
            crudRepository.ConfirmAndDeleteCard(this.TableName, this.Id, Temizle);
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
                    string mssql = $"select top 1 * from {this.TableName} where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from TasiyiciKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtUnvan.Text = veri.Title.ToString();
                        txtAd.Text = veri.Name.ToString();
                        txtSoyad.Text = veri.Surname.ToString();
                        txtTC.Text = veri.TCKN.ToString();
                        txtPlaka.Text = veri.NumberPlate.ToString();
                        txtDorse.Text = veri.TrailerNumber.ToString();
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

        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }

        private void FrmTasiyiciKarti_Load(object sender, EventArgs e)
        {

        }
    }
}