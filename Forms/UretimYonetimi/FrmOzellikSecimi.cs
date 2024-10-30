using Dapper;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Doc;
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
    public partial class FrmOzellikSecimi : DevExpress.XtraEditors.XtraForm
    {
        string _kullaniciSecimi, KullanimYeri = "Iplik", _ekranAdi;
        public string aciklama, id, islemTipi;
        Listele listele = new Listele();
        Bildirim bildirim = new Bildirim();
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            id = gridView1.GetFocusedRowCellValue("Id").ToString();
            aciklama = gridView1.GetFocusedRowCellValue("Aciklama").ToString();
            Close();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                string deleteSql = "delete from OzellikKodlama where Id = @Id";
                int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
                connection.Execute(deleteSql, new { Id = Id });
                Yukle();
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                string kontrol = "SELECT COUNT(1) FROM OzellikKodlama WHERE Aciklama = @Aciklama";
                bool exists = connection.ExecuteScalar<int>(kontrol, new { Aciklama = txtAciklama.Text }) > 0;
                if (!exists)
                {
                    string saveSql = @"INSERT INTO OzellikKodlama
                                     (Tip ,Aciklama ,KullanimYeri ,KullanimEkrani)
                                     VALUES (@Tip ,@Aciklama ,@KullanimYeri ,@KullanimEkrani)";
                    connection.Execute(saveSql, new
                    {
                        Tip = _kullaniciSecimi,
                        Aciklama = txtAciklama.Text,
                        KullanimYeri = KullanimYeri,
                        KullanimEkrani = _ekranAdi
                    });
                    Yukle();
                    txtAciklama.Text = "";
                }
                else
                {
                    bildirim.Uyari($"{txtAciklama.Text} daha önce kayıt edilmiş. Aynı kaydı tekrar yapmazsınız!");
                }

               
            }
        }
        public FrmOzellikSecimi(string kullaniciSecimi, string ekranAdi)
        {
            InitializeComponent();
            _kullaniciSecimi = kullaniciSecimi;
            _ekranAdi = ekranAdi;
            this.Text = "Özellik Seçimi [ " + _kullaniciSecimi.Trim() + " ]";
        }
        private void FrmOzellikSecimi_Load(object sender, EventArgs e)
        {
            Yukle();
            gridView1.Columns["Id"].Visible= false;
            //gridView1.Columns["Id"].Width = 20;
        }
        void Yukle()
        {
            string sql = $"select Id,Aciklama from OzellikKodlama where KullanimYeri = '{KullanimYeri}' and KullanimEkrani = '{_ekranAdi}' and Tip = '{_kullaniciSecimi}'";
            listele.Liste(sql, gridControl1);
        }
    }
}