using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Html;
using Hesap.Forms.MalzemeYonetimi.Ekranlar.HamDepo;
using Hesap.Forms.OrderYonetimi.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Forms.OrderYonetimi.OrderIslemleri
{
    public partial class FrmRenkBedenAdetleri : DevExpress.XtraEditors.XtraForm
    {
        public string SecilenRenk;
        public int ModelId,SiparisId,RenkId;
        Metotlar metotlar = new Metotlar();
        CRUD_Operations cRUD = new CRUD_Operations();
        public static Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public FrmRenkBedenAdetleri()
        {
            InitializeComponent();
        }

        private string mssqlString;
        private void FrmRenkBedenAdetleri_Load(object sender, EventArgs e)
        {
            mssqlString = ayarlar.MssqlConnStr();
            this.Text += SecilenRenk;
            Listele();
            yardimciAraclar.KolonlariGetir(gridView1,"Renk Beden Adetleri");
        }
        void Listele()
        {
            string sql = $"select Beden, Id AS [BedenId],0 [Miktar] from ModelBedenSeti where ModelId = '{ModelId}'";
            BindingList<SiparisAdet> siparisAdetListesi = new BindingList<SiparisAdet>(); // BindingList oluşturuluyor

            try
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, mssqlString))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        foreach (DataRow row in dataTable.Rows)
                        {
                            var siparisAdet = new SiparisAdet
                            {
                                BedenId = Convert.ToInt32(row["BedenId"]),
                                Beden = row["Beden"].ToString(),
                                Miktar = Convert.ToInt32(row["Miktar"]),
                            };
                            siparisAdetListesi.Add(siparisAdet);
                        }
                    }
                }

                gridControl1.DataSource = siparisAdetListesi;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1, "Renk Beden Adetleri");
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount ; i++)
            {
                var kalemParameters = metotlar.BedenRenkAdetParametreleri(i, 15, gridView1,ModelId, 5); // renk id eklenecek
                cRUD.InsertRecord("SiparisAdet", kalemParameters);
            }
        }
    }
}