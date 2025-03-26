using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Hesap.Utils;
using System;
using System.Data;
using System.IO;
using System.Data.SQLite;
using Dapper;
using Hesap.DataAccess;
using Hesap.Models;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Generic;
using DevExpress.XtraBars.Ribbon;


namespace Hesap
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private readonly DbConnect _connectionFactory;
        private const string DatabaseFileName = "Hesap.db";
        private string _connectionString;
        Ayarlar ayarlar = new Ayarlar();
        UpdaterHelper updaterHelper = new UpdaterHelper();
        CrudRepository crudRepository = new CrudRepository();
        Bildirim bildirim = new Bildirim();
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
        void EksikFormlariKaydet()
        {
            var formTypes = Assembly.GetExecutingAssembly().GetTypes()
                                    .Where(t => t.IsSubclassOf(typeof(Form)) && !t.IsAbstract)
                                    .ToList();

            foreach (var formType in formTypes)
            {
                string screenName = formType.Name;

                var existingScreen = crudRepository.GetAll<Authorization>("[Authorization]")
                                                   .Where(a => a.ScreenName == screenName && a.UserId == CurrentUser.UserId)
                                                   .FirstOrDefault();
                if (existingScreen == null)
                {
                    var parameters = new Dictionary<string, object>
            {
                { "ScreenName", screenName },
                { "CanAccess", true },
                { "CanSave", true },
                { "CanDelete", true },
                { "UserId", CurrentUser.UserId },
                { "CanUpdate", true },
            };

                    // Authorization tablosuna yeni kayıt ekleyin
                    crudRepository.Insert("[Authorization]", parameters);
                }
            }
        }
        void EksikRibbonItemlariKaydet()
        {
            var authorizedItems = crudRepository.GetAll<AuthVisibleItems>("AuthVisibleItems")
                                                .Where(a => a.UserId == CurrentUser.UserId)
                                                .ToList();
            foreach (var item in ribbon.Items)
            {
                if (item is BarButtonItem button)
                {
                    string buttonName = button.Name;
                    string buttonText = button.Caption;

                    if (!authorizedItems.Any(a => a.ButtonName == buttonName))
                    {
                        var parameters = new Dictionary<string, object>
                        {
                            { "ButtonName", buttonName },
                            { "ButtonText", buttonText},
                            { "IsVisible", true },
                            { "UserId", CurrentUser.UserId }
                        };
                        crudRepository.Insert("AuthVisibleItems", parameters);
                    }
                }
            }
        }
        private List<string> GetVisibleButtons()
        {
            return crudRepository.GetAll<AuthVisibleItems>("AuthVisibleItems")
                                 .Where(a => a.UserId == CurrentUser.UserId && a.IsVisible)
                                 .Select(a => a.ButtonName)
                                 .ToList();
        }
        private void UpdateButtonVisibility()
        {
            var visibleButtons = GetVisibleButtons();

            foreach (var item in ribbon.Items)
            {
                if (item is BarButtonItem button)
                {
                    button.Visibility = visibleButtons.Contains(button.Name)
                        ? DevExpress.XtraBars.BarItemVisibility.Always
                        : DevExpress.XtraBars.BarItemVisibility.Never;
                }
            }
            foreach (RibbonPage page in ribbon.Pages)
            {
                foreach (RibbonPageGroup group in page.Groups)
                {
                    bool anyVisible = group.ItemLinks
                                           .OfType<BarButtonItemLink>()
                                           .Any(link => link.Item.Visibility == DevExpress.XtraBars.BarItemVisibility.Always);

                    group.Visible = anyVisible;
                }
            }
            foreach (RibbonPage page in ribbon.Pages)
            {
                bool anyGroupVisible = page.Groups.Any(group => group.Visible);
                page.Visible = anyGroupVisible;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            SqliteDatabaseOlustur();
            EksikFormlariKaydet();
            EksikRibbonItemlariKaydet();
            UpdateButtonVisibility();
            barStVeritabani.Caption = ayarlar.VeritabaniTuru() == "mssql" ? "MSSQL" : "SQLite";
            string nameSurname;
            if (Properties.Settings.Default.KullaniciAdi.ToString().Split(' ')[1] != null || Properties.Settings.Default.KullaniciAdi.ToString().Split(' ')[2] != null)
            {
                nameSurname = Properties.Settings.Default.KullaniciAdi.ToString().Split(' ')[1] + " " + Properties.Settings.Default.KullaniciAdi.ToString().Split(' ')[2];
            }
            else
            {
                nameSurname = "";
            }
            if (nameSurname != null)
            {
                barKullanici.Caption = nameSurname;
            }
            else
            {
                barKullanici.Caption = "";
            }
        }

        void SqliteDatabaseOlustur()
        {
            string dataDirectory = "C:\\Data";
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

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.OrderYonetimi.Kartlar.FrmKategoriKarti frm = new Forms.OrderYonetimi.Kartlar.FrmKategoriKarti();
            FormAc(frm);
        }

        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.OrderYonetimi.Kartlar.FrmCinsiKarti frm = new Forms.OrderYonetimi.Kartlar.FrmCinsiKarti();
            FormAc(frm);
        }

        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.OrderYonetimi.OrderIslemleri.FrmOrderGirisi frm = new Forms.OrderYonetimi.OrderIslemleri.FrmOrderGirisi();
            FormAc(frm);
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.OrderYonetimi.Kartlar.FrmRenkKarti frm = new Forms.OrderYonetimi.Kartlar.FrmRenkKarti();
            FormAc(frm);
        }

        private void barBtnKumasKarti_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.UretimYonetimi.FrmUrunKarti frm = new Forms.UretimYonetimi.FrmUrunKarti();
            FormAc(frm);
        }

        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            Forms.Parametreler.frmUretimYonetimiParams frm = new Forms.Parametreler.frmUretimYonetimiParams();
            FormAc(frm);
        }
    }
}