using Dapper;
using DevExpress.XtraEditors;
using Hesap.DataAccess;
using Hesap.Utils;
using System;
using System.Collections.Generic;

namespace Hesap.Forms.MalzemeYonetimi
{
    public partial class FrmMalzemeKarti : XtraForm
    {
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        CRUD_Operations cRUD = new CRUD_Operations();
        CrudRepository crudRepository = new CrudRepository();
        private readonly string TableName = "Inventory";
        int Type = Convert.ToInt32(InventoryTypes.Malzeme);
        public FrmMalzemeKarti()
        {
            InitializeComponent();
        }
        int Id = 0;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "InventoryCode", txtKodu.Text },
                { "InventoryName", txtAdi.Text },
                { "Unit", ""},
                { "Type",Type},
                { "SubType", ""},
                { "IsUse", chckKullanimda.Checked},
                { "IsStock", chckStokMu.Checked},
                { "IsPrefix", false},
            };
            using (var connection = new Baglanti().GetConnection())
            {
                if (this.Id == 0)
                {
                    this.Id = crudRepository.Insert(TableName, parameters);
                    bildirim.Basarili();
                }
                else
                {
                    crudRepository.Update(TableName, this.Id, parameters);
                    bildirim.GuncellemeBasarili();
                }
            }
        }

        private void grupKodlarınıGösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  pnlGrupKodlari.Visible = !pnlGrupKodlari.Visible;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        void Temizle()
        {
            Id = 0;
            txtKodu.Text = "";
            txtAdi.Text = "";
            chckKullanimda.Checked = true;
            chckStokMu.Checked = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmMalzemeKartiListesi frm = new Liste.FrmMalzemeKartiListesi(Convert.ToInt32(InventoryTypes.Malzeme));
            frm.ShowDialog();
            Id = frm.Id;
            txtKodu.Text = frm.Kodu;
            txtAdi.Text = frm.Adi;
            chckKullanimda.Checked = frm.Kullanimda;
            chckStokMu.Checked = frm.Stok;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(TableName, this.Id, Temizle);
        }
        void ListeGetir(string KayitTipi)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                string mssql = $"select top 1 * from MalzemeKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                string sqlite = $"select * from MalzemeKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                if (veri != null)
                {
                    // Veri nesnesini beklenen türe dönüştürüyoruz
                    var urun = veri;
                    txtKodu.Text = urun.Kodu.ToString();
                    txtAdi.Text = urun.Adi.ToString();
                    chckKullanimda.Checked = Convert.ToBoolean(urun.Kullanimda);
                    this.Id = Convert.ToInt32(urun.Id);
                }
                else
                {
                    bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");

        }
    }
}