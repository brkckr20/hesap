using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;


namespace Hesap.Forms.Kartlar
{
    public partial class FrmFirmaKarti : XtraForm
    {
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CrudRepository crudRepository = new CrudRepository();
        int Id = 0;

        private string TableName = "Company";
        public FrmFirmaKarti()
        {
            InitializeComponent();
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
            if (this.Id == 0)
            {
                if (!string.IsNullOrEmpty(txtFirmaKodu.Text))
                {
                    this.Id = crudRepository.Insert(this.TableName, parameters);
                    bildirim.Basarili();
                }
                else
                {
                    bildirim.Uyari("Firma kodu girmeden kayıt yapılamaz!");
                }
                
            }
            else
            {
                crudRepository.Update(this.TableName, this.Id, parameters);
                bildirim.GuncellemeBasarili();
            }
        }
        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmFirmaKartiListesi frm = new Liste.FrmFirmaKartiListesi();
            frm.ShowDialog();
            if (frm.FirmaKodu != null)
            {
                txtFirmaKodu.Text = frm.FirmaKodu;
                txtFirmaUnvan.Text = frm.FirmaUnvan;
                txtAdres1.Text = frm.Adres1;
                txtAdres2.Text = frm.Adres2;
                txtAdres3.Text = frm.Adres3;
                Id = frm.Id;
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        void Temizle()
        {
            object[] kart = { txtFirmaKodu, txtFirmaUnvan, txtAdres1, txtAdres2, txtAdres3 };
            yardimciAraclar.KartTemizle(kart);
            this.Id = 0;
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (this.Id != 0)
            {
                crudRepository.ConfirmAndDeleteCard(this.TableName, this.Id, Temizle);
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
                var list = crudRepository.GetByList<Company>(this.TableName, KayitTipi, this.Id);
                if (list != null)
                {
                    this.Id = list.Id;
                    txtFirmaKodu.Text = list.CompanyCode;
                    txtFirmaUnvan.Text = list.CompanyName;
                    txtAdres1.Text = list.AddressLine1;
                    txtAdres2.Text = list.AddressLine2;
                    txtAdres3.Text = list.AddressLine3;
                }
                else
                {
                    bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
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