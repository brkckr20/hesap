using Dapper;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.OrderYonetimi.Kartlar
{
    public partial class FrmCinsiKarti : DevExpress.XtraEditors.XtraForm
    {
        public FrmCinsiKarti()
        {
            InitializeComponent();
        }
        int Id = 0;
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CrudRepository crudRepository = new CrudRepository();
        int _type = Convert.ToInt32(LookupTypes.Cinsi);
        // Tip 1 cinsi ye denk gelmektedir.
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Name", txtAd.Text },
                { "OriginalName", txtOrjAd.Text },
                //{ "Kullanimda", chckKullanimda.Checked},
                { "Type",_type},
                //{ "EkranAdi",this.Text},
            };
            if (this.Id == 0)
            {
                crudRepository.Insert("Lookup", parameters);
                bildirim.Basarili();
            }
            else
            {
                crudRepository.Update("Lookup", this.Id, parameters);
                bildirim.GuncellemeBasarili();
            }
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            Liste.FrmListe frm = new Liste.FrmListe(this.Text);
            frm.ShowDialog();
            txtAd.Text = frm.Adi;
            txtOrjAd.Text = frm.OrjAdi;
            chckKullanimda.Checked = frm.Kullanimda;
            Id = frm.Id;

        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                var list = crudRepository.GetByList<Lookup>("Lookup", KayitTipi, this.Id);
                if (list != null)
                {
                    this.Id = list.Id;
                    txtAd.Text = list.Name;
                    txtOrjAd.Text = list.OriginalName;
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

        private void btnSil_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard("Lookup", Id, Temizle);
        }
        void Temizle()
        {
            object[] kart = { txtAd, txtOrjAd, chckKullanimda };
            yardimciAraclar.KartTemizle(kart);
            this.Id = 0;
        }
    }
}