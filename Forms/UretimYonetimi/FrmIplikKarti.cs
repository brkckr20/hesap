using Dapper;
using DevExpress.LookAndFeel;
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

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmIplikKarti : DevExpress.XtraEditors.XtraForm
    {
        Numarator numarator = new Numarator();
        Bildirim bildirim = new Bildirim();
        private int Id = 0;
        Ayarlar ayarlar = new Ayarlar();
        public FrmIplikKarti()
        {
            InitializeComponent();
        }
        private void txtUrun_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OzellikSecikEkrani(lblIplikNo.Text);
        }

        void OzellikSecikEkrani(string labelName)
        {
            FrmOzellikSecimi frm = new FrmOzellikSecimi(NoktaliVirgulKaldir(labelName), this.Name);
            frm.ShowDialog();
            switch (NoktaliVirgulKaldir(labelName).Trim())
            {
                case "İplik No":
                    txtIplikNo.Text = frm.id;
                    lblIplikNoAciklama.Text = frm.aciklama;
                    break;
                case "İplik Cinsi":
                    txtIplikCinsi.Text = frm.id;
                    lblIplikCinsiAciklama.Text = frm.aciklama;
                    break;
                default:
                    break;
            }
        }
        string NoktaliVirgulKaldir(string metin)
        {
            string veri = metin.Split(':')[0];
            return veri;
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OzellikSecikEkrani(lblIplikCinsi.Text);
        }
        private void lblIplikNoAciklama_TextChanged(object sender, EventArgs e)
        {
            string iplikNo = lblIplikNoAciklama.Text;
            string[] parts = iplikNo.Split('/');
            if (parts.Length == 2)
            {
                int birinci = Convert.ToInt32(parts[0].Trim());
                int ikinci = Convert.ToInt32(parts[1].Trim());
                txtNumara.Text = (birinci / ikinci).ToString();
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            int kayitId;
            
            using (var connection = new Baglanti().GetConnection())
            {
                string _birlesikKod = txtIplikNo.Text + txtIplikCinsi.Text + (checkEdit1.Checked ? "1" : "0");
                string _iplikAdi = lblIplikNoAciklama.Text + " " + lblIplikCinsiAciklama.Text + " " + (checkEdit1.Checked ? "Organik" : "");
                string kontrol = "SELECT COUNT(1) FROM IplikKarti WHERE BirlesikKod = @BirlesikKod";
                bool exists = connection.ExecuteScalar<int>(kontrol, new { BirlesikKod = _birlesikKod }) > 0;
                object veriler = new
                {
                    IplikKodu = txtIplikKodu.Text,
                    IplikAdi = _iplikAdi,
                    IplikCinsi = txtIplikCinsi.Text,
                    IplikNo = txtIplikNo.Text,
                    Numara = txtNumara.Text,
                    Organik = checkEdit1.Checked,
                    BirlesikKod = _birlesikKod
                };
                if (!exists)
                {
                    string insertQuerymssql = @"INSERT INTO IplikKarti 
                                           (IplikKodu,IplikAdi,IplikNo,IplikCinsi,Numara,Organik,BirlesikKod) OUTPUT INSERTED.Id
                                     VALUES (@IplikKodu,@IplikAdi,@IplikNo,@IplikCinsi,@Numara,@Organik,@BirlesikKod)";
                    string insertQuerySqlite = @"INSERT INTO IplikKarti 
                                           (IplikKodu,IplikAdi,IplikNo,IplikCinsi,Numara,Organik,BirlesikKod)
                                     VALUES (@IplikKodu,@IplikAdi,@IplikNo,@IplikCinsi,@Numara,@Organik,@BirlesikKod)";
                    string idQuery = "SELECT last_insert_rowid();";
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        kayitId = connection.QuerySingle<int>(insertQuerymssql, veriler);
                    }
                    else
                    {
                        connection.Execute(insertQuerySqlite, veriler);
                        kayitId = connection.QuerySingle<int>(idQuery);
                    }
                    lblIplikAdi.Text = _iplikAdi;
                    this.Id = kayitId;
                    bildirim.Basarili();
                }
                else
                {
                    MessageBox.Show("kayit var");
                }
            }
        }

        private void FrmIplikKarti_Load(object sender, EventArgs e)
        {
            txtIplikKodu.Text = numarator.NumaraVer("İplik");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmIplikKartiListesi frm = new Liste.FrmIplikKartiListesi();
            frm.ShowDialog();
            if (frm.IplikKodu != null)
            {
                this.Id = frm.Id;
                txtIplikKodu.Text = frm.IplikKodu;
                lblIplikAdi.Text = frm.IplikAdi;
                txtIplikNo.Text = frm.IplikNo;
                lblIplikNoAciklama.Text = frm.IplikNoAciklama;
                txtIplikCinsi.Text = frm.IplikCinsi;
                lblIplikCinsiAciklama.Text = frm.IplikCinsiAciklama;
                checkEdit1.Checked = frm.Organik;
            }
        }
    }
}