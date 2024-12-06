using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Dapper;


namespace Hesap
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly DbConnect _connectionFactory;
        private const string DatabaseFileName = "Hesap.db";
        private string _connectionString;
        Ayarlar ayarlar = new Ayarlar();

        public Main()
        {
            InitializeComponent();
            _connectionFactory = new DbConnect();
            FrmKullaniciGirisi frm = new FrmKullaniciGirisi();
            frm.ShowDialog();
        }

        void FormAc(XtraForm form)
        {
            XtraForm newForm = (XtraForm)Activator.CreateInstance(form.GetType());
            newForm.MdiParent = this;
            newForm.Show();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            SqliteDatabaseOlustur();
            barStVeritabani.Caption = ayarlar.VeritabaniTuru() == "mssql" ? "MSSQL" : "SQLite";
            barKullanici.Caption = Properties.Settings.Default.KullaniciAdi.ToString().Split(' ')[1] + " " + Properties.Settings.Default.KullaniciAdi.ToString().Split(' ')[2];
        }

        void SqliteDatabaseOlustur()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dataDirectory = Path.Combine(baseDirectory, "Data\\sqlite");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }
            string databasePath = Path.Combine(dataDirectory, DatabaseFileName);

            _connectionString = $"Data Source={databasePath};Version=3;";
            if (!File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
                InitializeDatabase();
            }
            InitializeDatabase();
        }
        private void InitializeDatabase()
        {
            using (IDbConnection dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string dataDirectory = Path.Combine(baseDirectory, "Data\\sqlite\\CreateTable.sql");
                string createTableQuery = File.ReadAllText(dataDirectory);
                dbConnection.Execute(createTableQuery);
            }
        }

        private void barBtnUM_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form1 frm = new Form1();
            FormAc(frm);
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.Kartlar.FrmFirmaKarti frm = new Forms.Kartlar.FrmFirmaKarti();
            FormAc(frm);
        }

        private void barBtnSiparisGirisi_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.UretimYonetimi.FrmSiparisGirisi frm = new Forms.UretimYonetimi.FrmSiparisGirisi();
            FormAc(frm);
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.UretimYonetimi.FrmUrunKarti frm = new Forms.UretimYonetimi.FrmUrunKarti();
            FormAc(frm);
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.UretimYonetimi.FrmUrunReceteKarti frm = new Forms.UretimYonetimi.FrmUrunReceteKarti();
            FormAc(frm);
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void barBtnRapor_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.Rapor.FrmRaporOlusturma frm = new Forms.Rapor.FrmRaporOlusturma();
            FormAc(frm);
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.MalzemeYonetimi.FrmMalzemeKarti frm = new Forms.MalzemeYonetimi.FrmMalzemeKarti();
            FormAc(frm);
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.MalzemeYonetimi.FrmMalzemeGiris frm = new Forms.MalzemeYonetimi.FrmMalzemeGiris();
            FormAc(frm);
        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.MalzemeYonetimi.FrmMalzemeCikis frm = new Forms.MalzemeYonetimi.FrmMalzemeCikis();
            FormAc(frm);
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.Kartlar.FrmDepoKarti frm = new Forms.Kartlar.FrmDepoKarti();
            FormAc(frm);
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.Kartlar.FrmKullaniciKarti frm = new Forms.Kartlar.FrmKullaniciKarti();
            FormAc(frm);
        }

        private void barStVeritabani_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.UretimYonetimi.FrmIplikKarti frm = new Forms.UretimYonetimi.FrmIplikKarti();
            FormAc(frm);
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.MalzemeYonetimi.Ekranlar.IplikDepo.FrmIplikGiris frm = new Forms.MalzemeYonetimi.Ekranlar.IplikDepo.FrmIplikGiris();
            FormAc(frm);
        }

        private void barBtnBoyahaneRenkKarti_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.FrmBoyahaneRenkKartlari frm = new Forms.FrmBoyahaneRenkKartlari();
            FormAc(frm);
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.MalzemeYonetimi.Ekranlar.Talimatlar.FrmIplikSaTalimati frm = new Forms.MalzemeYonetimi.Ekranlar.Talimatlar.FrmIplikSaTalimati(); 
            FormAc(frm);
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.MalzemeYonetimi.Ekranlar.IplikDepo.FrmIplikCikis frm = new Forms.MalzemeYonetimi.Ekranlar.IplikDepo.FrmIplikCikis();
            FormAc(frm);
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.Kartlar.FrmTasiyiciKarti frm = new Forms.Kartlar.FrmTasiyiciKarti();
            FormAc(frm);
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.MalzemeYonetimi.Ekranlar.Talimatlar.FrmKumasSaTalimati frm = new Forms.MalzemeYonetimi.Ekranlar.Talimatlar.FrmKumasSaTalimati();
            FormAc(frm);
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.TeknikDestek.FrmTeknikDestek frm = new Forms.TeknikDestek.FrmTeknikDestek();
            FormAc(frm);
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.MalzemeYonetimi.Ekranlar.HamDepo.FrmHamDepoGiris frm = new Forms.MalzemeYonetimi.Ekranlar.HamDepo.FrmHamDepoGiris();
            FormAc(frm);
        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.UretimYonetimi.Barkod.FrmEtiketBasim frm = new Forms.UretimYonetimi.Barkod.FrmEtiketBasim();
            FormAc(frm);
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.Rapor.Etiket.FrmEtiketTasarimi frm = new Forms.Rapor.Etiket.FrmEtiketTasarimi();
            FormAc(frm);
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.MalzemeYonetimi.Ekranlar.HamDepo.FrmHamDepoCikis frm = new Forms.MalzemeYonetimi.Ekranlar.HamDepo.FrmHamDepoCikis();
            FormAc(frm);
        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.OrderYonetimi.FrmModelKarti frm = new Forms.OrderYonetimi.FrmModelKarti();
            FormAc(frm);
        }
    }
}