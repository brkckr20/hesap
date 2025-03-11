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
using Dapper;
using Hesap.DataAccess;
using static DevExpress.XtraPrinting.Export.Pdf.PdfImageCache;
using Hesap.Helpers;
using System.Drawing.Drawing2D;

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmUrunKarti : DevExpress.XtraEditors.XtraForm
    {
        public FrmUrunKarti()
        {
            InitializeComponent();
        }

        int Id = 0, InventoryType = Convert.ToInt32(InventoryTypes.Kumas);
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        CrudRepository crudRepository = new CrudRepository();
        string TableName = "Inventory", KumasAdiOzellik = "";

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string CombinedCode = txtUrunKodu.Text.Substring(0, 3) + btnCinsiId.Text;
            string InventoryName = KumasAdiOzellik + " " + lblCinsiAciklama.Text;
            if (crudRepository.IfExistRecord(TableName, "CombinedCode", CombinedCode) > 0)
            {
                string code = crudRepository.GetByCode("InventoryCode", this.TableName, CombinedCode);
                bildirim.Uyari($"Seçtiğiniz özelliklere ait bir kayıt bulunmaktadır.\nLütfen {code} numaralı kaydı kontrol ediniz!!");
                return;
            }

            var InvParams = new Dictionary<string, object>
            {
                { "InventoryCode", txtUrunKodu.Text }, { "InventoryName", InventoryName },
                {"Type" , InventoryTypes.Kumas}, { "IsUse", chckPasif.Checked }, {"Unit",""},{"CombinedCode",CombinedCode},{"SubType" , KumasAdiOzellik},{"IsPrefix" , false}
            };
            if (this.Id == 0)
            {
                Id = crudRepository.Insert(TableName, InvParams);
                txtUrunAdi.Text = InventoryName;
                bildirim.Basarili();
            }
            else
            {
                crudRepository.Update(this.TableName, this.Id, InvParams);
                bildirim.GuncellemeBasarili();
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmUrunKartiListesi frm = new Liste.FrmUrunKartiListesi(InventoryType);
            frm.ShowDialog();
            txtUrunKodu.Text = frm.UrunKodu;
            txtUrunAdi.Text = frm.UrunAdi;
            chckPasif.Checked = frm.Pasif;
            this.Id = frm.Id;
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(this.TableName, this.Id, null);
        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }
        void ListeGetir(string KayitTipi)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                string mssql = $"select top 1 * from UrunKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                string sqlite = $"select * from UrunKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                if (veri != null)
                {
                    var urun = veri;
                    txtUrunKodu.Text = urun.UrunKodu.ToString();
                    txtUrunAdi.Text = urun.UrunAdi.ToString();
                    chckPasif.Checked = Convert.ToBoolean(urun.Pasif);
                    this.Id = Convert.ToInt32(urun.Id);
                }
                else
                {
                    bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
                }
            }
        }
        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtUrunKodu.Text = "";
            txtUrunAdi.Text = "";
            chckPasif.Checked = false;
            btnCinsiId.Text = "";
            lblCinsiAciklama.Text = "";
            this.Id = 0;
        }
        private void FrmUrunKarti_Load(object sender, EventArgs e)
        {

        }
        private void buttonEdit1_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmUrunTipiSecimi frm = new FrmUrunTipiSecimi();
            frm.ShowDialog();
            txtUrunKodu.Text = frm.OnEk + frm.yeniNumaraStr;
            KumasAdiOzellik = frm.KumasAdiOzellik;
        }
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OzellikSecikEkrani(lblCinsi.Text);
        }
        void OzellikSecikEkrani(string labelName)
        {
            FrmOzellikSecimi frm = new FrmOzellikSecimi(SemiColonHelper.RemoveSemiColon(labelName), this.Name);
            frm.ShowDialog();
            if (frm.id == null)
            {
                return;
            }
            switch (SemiColonHelper.RemoveSemiColon(labelName).Trim())
            {
                case "Cinsi":
                    btnCinsiId.Text = frm.id;
                    lblCinsiAciklama.Text = frm.aciklama;
                    break;
                default:
                    break;
            }
        }
    }
}