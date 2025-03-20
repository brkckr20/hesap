using Dapper;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
namespace Hesap.Forms.Kartlar
{
    public partial class FrmKullaniciKarti : DevExpress.XtraEditors.XtraForm
    {
        public FrmKullaniciKarti()
        {
            InitializeComponent();
            grdKullaniciYetkileri.Visible = false;
        }
        int Id = 0;
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        CRUD_Operations _Operations = new CRUD_Operations();
        CrudRepository crudRepository = new CrudRepository();
        private string TableName = "Users";
        KalemParametreleri parametreler = new KalemParametreleri();
        CRUD_Operations cRUD = new CRUD_Operations();
        bool kayitYetki,guncellemeYetki;
        void UpdateAccessing()
        {
            for (int i = 0; i < gridView1.RowCount - 1; i++)
            {
                var d2Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id"));
                var kalemler = parametreler.GetYetki(i, this.Id, gridView1, parametreler.Yetkiler());
                kalemler.Remove("Id");
                if (d2Id > 0)
                {
                    cRUD.UpdateRecord("[Authorization]", kalemler, d2Id);
                }
            }
            foreach (var item in (BindingList<AuthVisibleItems>)grdButtons.DataSource)
            {
                var updatedValues = new Dictionary<string, object> { { "IsVisible",item.IsVisible } };
                cRUD.UpdateRecord("AuthVisibleItems", updatedValues, item.Id); //menu grubu gizlenmeye devam edilecek
            }
        }

        void YetkileriGetir()
        {
            var userPermissions = crudRepository.GetAll<Authorization>("[Authorization]")
                                         .Where(a => a.UserId == CurrentUser.UserId && a.ScreenName == this.Name)
                                         .FirstOrDefault();
            kayitYetki = userPermissions.CanSave;
            guncellemeYetki = userPermissions.CanUpdate;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var whParams = new Dictionary<string, object>
            {
                {"Code", txtKodu.Text},
                {"Name", txtAd.Text},
                {"Surname", txtSoyad.Text},
                {"Password", txtSifre.Text},
                {"IsUse", chckKullanimda.Checked},
            };
            if (this.Id == 0)
            {
                if (kayitYetki)
                {
                    Id = crudRepository.Insert(this.TableName, whParams);
                    bildirim.Basarili();
                }
                else
                {
                    bildirim.Uyari("Yeni kayıt ekleme yetkiniz bulunmamaktadır.\nYetki açılması için lütfen Sistem Yöneticisi ile iletişime geçiniz");
                    return;
                }
                
            }
            else
            {
                if (guncellemeYetki)
                {
                crudRepository.Update(this.TableName, this.Id, whParams);
                UpdateAccessing();
                bildirim.GuncellemeBasarili();
                }
                else
                {
                    bildirim.Uyari("Güncelleme yetkiniz bulunmamaktadır.\nYetki açılması için lütfen Sistem Yöneticisi ile iletişime geçiniz");
                    return;
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
            txtAd.Text = "";
            txtSoyad.Text = "";
            txtSifre.Text = "";
            chckKullanimda.Checked = true;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(this.TableName, this.Id, this.Temizle);
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmKullaniciListesi frm = new Liste.FrmKullaniciListesi();
            frm.ShowDialog();
            if (frm.Kodu != null)
            {
                txtKodu.Text = frm.Kodu;
                txtAd.Text = frm.Ad;
                txtSoyad.Text = frm.Soyad;
                txtSifre.Text = frm.Sifre;
                chckKullanimda.Checked = frm.Kullanimda;
                this.Id = frm.Id;
                grdKullaniciYetkileri.Visible = true;
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
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = $"select top 1 * from {this.TableName} where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from KullaniciKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        txtKodu.Text = veri.Code.ToString();
                        txtAd.Text = veri.Name.ToString();
                        txtSoyad.Text = veri.Surname.ToString();
                        txtSifre.Text = veri.Password.ToString();
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
        void YetkiListesi()
        {
            var yetkiler = crudRepository.GetAll<Authorization>("[Authorization]")
                .Where(c => c.UserId == CurrentUser.UserId)
                .ToList();

            grdKullaniciYetkileri.DataSource = new BindingList<Authorization>(yetkiler);
            gridView1.Columns["Id"].Visible = false;

            var buttonlar = crudRepository.GetAll<AuthVisibleItems>("AuthVisibleItems")
                .Where(c => !c.ButtonName.Contains("barButton") && !string.IsNullOrEmpty(c.ButtonName))
                .Where(c => c.UserId == CurrentUser.UserId)
                .ToList();
            grdButtons.DataSource = new BindingList<AuthVisibleItems>(buttonlar);
            gridView2.Columns["Id"].Visible = false;
            gridView2.Columns["ButtonName"].Visible = false;

        }

        private void FrmKullaniciKarti_Load(object sender, EventArgs e)
        {
            YetkiListesi();
            YetkileriGetir();
        }
    }
}