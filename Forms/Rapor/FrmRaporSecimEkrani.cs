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
        string _formAdi;
        int _kayitNo;
        FrmRaporOlusturma raporOlusturma = new FrmRaporOlusturma();
        public FrmRaporSecimEkrani()
        {
            InitializeComponent();
        }
        public FrmRaporSecimEkrani(string FormAdi, int kayitNo)
        {
            InitializeComponent();
            this._formAdi = FormAdi;
            this._kayitNo = kayitNo;
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            raporOlusturma.DizaynAc(comboBoxEdit1.Text, false, this._kayitNo);

        }
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