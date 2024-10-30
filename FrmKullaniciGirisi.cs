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

namespace Hesap
{
    public partial class FrmKullaniciGirisi : DevExpress.XtraEditors.XtraForm
    {
        public FrmKullaniciGirisi()
        {
            InitializeComponent();
            BilgileriGetir();
        }
        Bildirim bildirim = new Bildirim();
        bool giris;
        int KullaniciId;

        void BilgileriGetir()
        {
            if (Properties.Settings.Default.BeniHatirla)
            {
                cmbKodu.SelectedText = Properties.Settings.Default.KullaniciAdi;
                txtSifre.Text = Properties.Settings.Default.Sifre;
                chckBeniHatirla.Checked = true;
                KullaniciId = Properties.Settings.Default.Id;
            }
            else
            {
                cmbKodu.SelectedIndex = -1;
                txtSifre.Text = "";
                chckBeniHatirla.Checked = false;
                KullaniciId = 0;
            }
        }

        void BilgileriKaydet()
        {
            if (chckBeniHatirla.Checked)
            {
                Properties.Settings.Default.Sifre = txtSifre.Text;
                Properties.Settings.Default.KullaniciAdi = cmbKodu.SelectedItem.ToString();
                Properties.Settings.Default.BeniHatirla = true;
                Properties.Settings.Default.Id= KullaniciId;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Sifre = null;
                Properties.Settings.Default.KullaniciAdi = null;
                Properties.Settings.Default.BeniHatirla = false;
                Properties.Settings.Default.Id = 0;
                Properties.Settings.Default.Save();
            }
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                var selectedKodu = cmbKodu.Properties.Items[cmbKodu.SelectedIndex].ToString().Split(' ')[0];
                var query = "SELECT Sifre FROM KullaniciKarti WHERE Kodu = @Kodu";
                var idQuery = "SELECT Id FROM KullaniciKarti WHERE Kodu = @Kodu";
                var kullaniciSifre = connection.QuerySingleOrDefault<string>(query, new { Kodu = selectedKodu });
                KullaniciId = connection.QuerySingleOrDefault<int>(idQuery, new { Kodu = selectedKodu });
                if (kullaniciSifre != null && kullaniciSifre == txtSifre.Text)
                {
                    giris = true;
                    BilgileriKaydet();
                    this.Close();
                }
                else
                {
                    bildirim.Uyari("Geçersiz şifre!\nLütfen şifrenizi kontrol ederek tekrar deneyin");
                }
            }
        }

        private void FrmKullaniciGirisi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!giris)
            {
                Application.Exit();
            }
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            if (!giris)
            {
                Application.Exit();
            }
        }

        private void FrmKullaniciGirisi_Load(object sender, EventArgs e)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                var query = "SELECT Kodu, AdSoyad, Sifre FROM KullaniciKarti";
                var kullaniciKartlari = connection.Query(query).ToList();
                foreach (var kullaniciKarti in kullaniciKartlari)
                {
                    string itemText = $"{kullaniciKarti.Kodu} {kullaniciKarti.AdSoyad}";
                    cmbKodu.Properties.Items.Add(itemText);
                }

            }
        }
    }
}