using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using FastColoredTextBoxNS;
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

namespace Hesap.Forms.Liste
{
    public partial class FrmAciklamaListesi : DevExpress.XtraEditors.XtraForm
    {
        Listele listele = new Listele();
        Ayarlar ayarlar = new Ayarlar();
        string _ekranAdi;
        public string Aciklama;
        int Id;
        public FrmAciklamaListesi(string ekranAdi)
        {
            InitializeComponent();
            this._ekranAdi = ekranAdi;
        }

        private void FrmAciklamaListesi_Load(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            string sql = $"SELECT Id,Aciklama FROM IrsaliyeAciklama where EkranAdi='{_ekranAdi}'";
            listele.Liste(sql, gridControl1);
            gridView1.Columns["Id"].Visible = false;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            object x = new
            {
                Aciklama = txtAciklama.Text,
                EkranAdi = this._ekranAdi,
                Id = this.Id
            };

            using (var connection = new Baglanti().GetConnection())
            {
                //if (Id == 0)
                //{
                string sqliteQuery = @"INSERT INTO IrsaliyeAciklama (Aciklama,EkranAdi) VALUES(@Aciklama,@EkranAdi)";
                string sqlQuery = @"INSERT INTO IrsaliyeAciklama (Aciklama,EkranAdi) OUTPUT INSERTED.Id VALUES(@Aciklama,@EkranAdi)";
                string idQuery = "SELECT last_insert_rowid();";
                if (ayarlar.VeritabaniTuru() == "mssql")
                {
                    this.Id = connection.QuerySingle<int>(sqlQuery, x);
                }
                else
                {
                    connection.Execute(sqliteQuery, x);
                    this.Id = connection.QuerySingle<int>(idQuery);
                }
                //}
                Listele();
                txtAciklama.Text = "";
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            Aciklama = gridView.GetFocusedRowCellValue("Aciklama").ToString();
            this.Close();
        }
    }
}