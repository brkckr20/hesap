using FastReport;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Hesap.Utils;
using Dapper;

namespace Hesap.Forms.Rapor
{
    public partial class FrmRaporSecimEkrani : XtraForm
    {
        //SqlConnection bg = new SqlConnection("Server=.; Database=Hesap; Integrated Security=True;");
        string _formAdi;
        int _kayitNo;
        FrmRaporOlusturma raporOlusturma = new FrmRaporOlusturma();
        public FrmRaporSecimEkrani()
        {
            InitializeComponent();
        }
        public FrmRaporSecimEkrani(string FormAdi,int kayitNo)
        {
            InitializeComponent();
            this._formAdi = FormAdi;
            this._kayitNo = kayitNo;
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            raporOlusturma.DizaynAc(comboBoxEdit1.Text, false, this._kayitNo);
            #region rapor ilk hali
            //Report report1 = new Report();
            //report1.Load(Application.StartupPath + "\\Rapor\\Müşteri Teklif Formu.frx"); 
            //report1.RegisterData(goster("select * from Siparis where Id = 3", "Siparis"));
            //report1.RegisterData(goster("select * from SiparisKalem where RefNo = 3", "SiparisKalem"));
            //if (chckDizayn.Checked)
            //{
            //    report1.Design();
            //}
            //else if (chckDizayn.Checked == false)
            //{
            //    report1.Show();
            //}
            #endregion;
        }
        //public DataSet goster(string sql, string tabloismi)
        //{
        //    DataSet ds = new DataSet();
        //    if (bg.State != ConnectionState.Closed)
        //    {
        //        bg.Close();
        //    }
        //    bg.Open();
        //    SqlDataAdapter adapter = new SqlDataAdapter(sql, bg);
        //    adapter.SelectCommand.CommandTimeout = 5000;
        //    adapter.Fill(ds, tabloismi);
        //    bg.Close();
        //    return ds;
        //}

        private void FrmRaporSecimEkrani_Load(object sender, EventArgs e)
        {
            ComboboxaYansit();
        }
        void ComboboxaYansit()
        {
            using (var connection = new Baglanti().GetConnection())
            {
                string query = "SELECT DISTINCT ReportName FROM Report WHERE FormName = @FormAdi";
                var liste = connection.Query(query, new { FormAdi = this._formAdi });
                foreach (var item in liste.ToList())
                {
                    var raporAdi = (string)item.ReportName;
                    comboBoxEdit1.Properties.Items.Add(raporAdi);
                }
            }
        }
    }
}

/*
 Report report1 = new Report();
            report1.Load(Application.StartupPath + "\\Rapor\\Müşteri Teklif Formu.frx");
            report1.RegisterData(goster("select * from Siparis where Id = 3", "Siparis"));
            report1.RegisterData(goster("select * from SiparisKalem where RefNo = 3", "SiparisKalem"));

            if (chckDizayn.Checked && chckYeni.Checked)
            {
                // Raporu tasarım modunda aç ve yeni dosya adıyla kaydet
                report1.Design();
                string yeniDosyaAdı = "YeniRapor.frx";
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Report files (*.frx)|*.frx";
                    saveFileDialog.Title = "Save a Report File";
                    saveFileDialog.FileName = yeniDosyaAdı;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Raporu yeni dosya adıyla kaydet
                        report1.Save(saveFileDialog.FileName);
                    }
                }
            }
            else if (chckDizayn.Checked)
            {
                // Sadece dizayn işaretli ise, raporu tasarım modunda aç
                report1.Design();
            }
            else if (!chckDizayn.Checked && !chckYeni.Checked)
            {
                // Hem dizayn hem de yeni işaretli değilse, boş bir rapor sayfası aç
                Report emptyReport = new Report();
                emptyReport.Load(Application.StartupPath + "\\Rapor\\BoşRapor.frx");
                emptyReport.Show();
            }
            else
            {
                // Diğer tüm durumlarda, raporu görüntüle
                report1.Show();
            }
 */