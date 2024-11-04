using Dapper;
using DevExpress.XtraEditors;
using Hesap.Context;
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

namespace Hesap.Forms.TeknikDestek
{
    public partial class FrmTeknikDestek : DevExpress.XtraEditors.XtraForm
    {
        public FrmTeknikDestek()
        {
            InitializeComponent();          
        }
        CRUD_Operations cRUD = new CRUD_Operations();
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        
        public int Id = 0;
        private void FrmTeknikDestek_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            dateTalepTarihi.EditValue = DateTime.Now;
            dateTamamlanmaTarihi.EditValue = "01.01.1900";
        }
        #region satir kayitlari
        //private Dictionary<string, object> CreateKalemParameters(int rowIndex)
        //{
        //    return new Dictionary<string, object>
        //    {
        //        { "RefNo", this.Id },
        //        { "KalemIslem", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "KalemIslem")) ?? "" },
        //        { "KumasId", gridView1.GetRowCellValue(rowIndex, "KumasId") ?? 0 },
        //        { "GrM2", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "GrM2")) },
        //        { "BrutKg", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "BrutKg")) },
        //        { "NetKg", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "NetKg")) },
        //        { "BrutMt", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "BrutMt")) },
        //        { "NetMt", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "NetMt")) },
        //        { "Adet", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "Adet")) },
        //        { "Fiyat", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "Fiyat")) },
        //        { "FiyatBirimi", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "FiyatBirim")) },
        //        { "DovizCinsi", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "DovizCinsi")) },
        //        { "RenkId", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "RenkId")) },
        //        { "Aciklama", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Aciklama")) },
        //        { "UUID", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "UUID")) },
        //        { "SatirTutari", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "SatirTutari")) },
        //        { "TakipNo", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "TakipNo")) },
        //        { "DesenId", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "DesenId")) },
        //        { "BoyaIslemId", yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(rowIndex, "BoyaIslemId")) }
        //    };
        //}
        #endregion
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Tarih", dateTalepTarihi.EditValue },
                { "Departman", txtDepartman.Text },
                { "Baslik", txtBaslik.Text },
                { "Aciklama", memoAciklama.Text },
                { "Ek", txtDosyaEk.Text },
                { "Durum", cmbDurum.Text },
                { "TamamlanmaTarihi", dateTamamlanmaTarihi.Text },
            };
            if (this.Id == 0)
            {
                this.Id = cRUD.InsertRecord("Talepler", parameters);
                #region kodlar
                //for (int i = 0; i < gridView1.RowCount - 1; i++)
                //{
                //    var kalemParameters = CreateKalemParameters(i);
                //    var d2Id = cRUD.InsertRecord("HamDepo2", kalemParameters);
                //    gridView1.SetRowCellValue(i, "D2Id", d2Id);
                //}
                #endregion
                bildirim.Basarili();
            }
            else
            {
                cRUD.UpdateRecord("Talepler", parameters, this.Id);
                #region satir kodlari
                //for (int i = 0; i < gridView1.RowCount - 1; i++)
                //{
                //    var d2Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id"));
                //    var kalemParameters = CreateKalemParameters(i);
                //    if (d2Id > 0) // Eğer D2Id varsa güncelle
                //    {
                //        cRUD.UpdateRecord("HamDepo2", kalemParameters, d2Id);
                //    }
                //    else // Yeni kayıt ekle
                //    {
                //        //kalemParameters["RefNo"] = this.Id; // RefNo ekle
                //        var yeniId = cRUD.InsertRecord("HamDepo2", kalemParameters);
                //        gridView1.SetRowCellValue(i, "D2Id", yeniId); // Yeni Id'yi gridView'a set et
                //    }
                //}
                #endregion
                bildirim.GuncellemeBasarili();
            }
        }

        private void txtDepartman_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmDepartmanListesi frm = new FrmDepartmanListesi();
            frm.ShowDialog();
            if (frm.Departman != null)
            {
                txtDepartman.Text = frm.Departman;
            }
        }

        private void txtDepartman_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            FrmTalepListesi frm = new FrmTalepListesi();
            frm.ShowDialog();
            if (frm.veriler.Count>0)
            {
                this.Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                dateTalepTarihi.EditValue = (DateTime)frm.veriler[0]["Tarih"];
                txtDepartman.Text = frm.veriler[0]["Departman"].ToString();
                txtBaslik.Text = frm.veriler[0]["Baslik"].ToString();
                memoAciklama.Text = frm.veriler[0]["Aciklama"].ToString();
                txtDosyaEk.Text = frm.veriler[0]["Ek"].ToString();
                cmbDurum.EditValue = frm.veriler[0]["Durum"].ToString();

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            cRUD.KartSil(this.Id, "Talepler");
        }
        void ListeGetir(string KayitTipi)
        {
            if (this.Id != 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = $"select top 1 * from Talepler where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")}";
                    string sqlite = $"select * from FirmaKarti where Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id order by Id {(KayitTipi == "Önceki" ? "desc" : "asc")} limit 1";
                    var query = ayarlar.VeritabaniTuru() == "mssql" ? mssql : sqlite;
                    var veri = connection.QueryFirstOrDefault(query, new { Id = this.Id });
                    if (veri != null)
                    {
                        dateTalepTarihi.EditValue = veri.Tarih.ToString();
                        txtDepartman.Text = veri.Departman.ToString();
                        txtBaslik.Text = veri.Baslik.ToString();
                        memoAciklama.Text = veri.Aciklama.ToString();
                        txtDosyaEk.Text = veri.Ek.ToString();
                        cmbDurum.EditValue= veri.Durum.ToString();
                        this.Id = Convert.ToInt32(veri.Id);
                    }
                    else
                    {
                        bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
                    }
                }
            }
            else
            {
                bildirim.Uyari("Kayıt gösterebilmek için öncelikle listeden bir kayıt getirmelisiniz!");
            }

        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            ListeGetir("Önceki");
        }

        private void btnIleri_Click(object sender, EventArgs e)
        {
            ListeGetir("Sonraki");
        }
        void FormTemizle()
        {
            object[] bilgiler = { dateTalepTarihi, txtDepartman, txtBaslik, memoAciklama, txtDosyaEk};
            yardimciAraclar.KartTemizle(bilgiler);
            this.Id = 0;
            cmbDurum.SelectedIndex = 0;
            dateTamamlanmaTarihi.EditValue = "01.01.1900";
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }
    }
}