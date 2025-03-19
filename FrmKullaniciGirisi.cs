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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

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
        CrudRepository crudRepository = new CrudRepository();

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
                Properties.Settings.Default.Id = KullaniciId;
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
            string selectedKodu = cmbKodu.Properties.Items[cmbKodu.SelectedIndex].ToString().Split(' ')[0];
            var user = crudRepository.GetAll<User>("Users")
                .Where(u => u.Code == selectedKodu)
                .Select(u => new
                    {
                        u.Id,u.Code,u.Password,u.Name,u.Surname
                    }
                ).FirstOrDefault();
            if (user.Password != null && user.Password == txtSifre.Text)
            {
                CurrentUser.UserId = user.Id;
                CurrentUser.Username = user.Name + " " + user.Surname;
                giris = true;
                BilgileriKaydet();
                this.Close();
            }
            else
            {
                bildirim.Uyari("Geçersiz şifre!\nLütfen şifrenizi kontrol ederek tekrar deneyin");
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
            var users = crudRepository.GetAll<User>("Users").ToList();
            
            foreach (var item in users)
            {
                string itemText = $"{item.Code} {item.Name + " " +  item.Surname}";
                cmbKodu.Properties.Items.Add(itemText);
            }
        }
    }
}