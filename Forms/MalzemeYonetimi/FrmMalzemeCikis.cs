using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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

namespace Hesap.Forms.MalzemeYonetimi
{
    public partial class FrmMalzemeCikis : DevExpress.XtraEditors.XtraForm
    {
        public FrmMalzemeCikis()
        {
            InitializeComponent();
        }
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        int Id = 0;
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmFirmaKartiListesi frm = new Liste.FrmFirmaKartiListesi();
            frm.ShowDialog();
            txtFirmaKodu.Text = frm.FirmaKodu;
            txtFirmaUnvan.Text = frm.FirmaUnvan;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }
        void FormTemizle()
        {
            object[] bilgiler = { dateTarih, dateSevkTarihi, txtFirmaKodu, txtFirmaUnvan, txtDepoKodu, txtIrsaliyeNo, rchAciklama, txtYetkili };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<_MalzemeKalem>();
            this.Id = 0;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            object FisBaslik = new
            {
                IslemCinsi = "Çıkış",
                Tarih = dateTarih.EditValue,
                SevkTarihi = dateSevkTarihi.EditValue,
                FirmaKodu = txtFirmaKodu.Text,
                FirmaUnvani = txtFirmaUnvan.Text,
                Aciklama = rchAciklama.Text,
                DepoId = txtDepoKodu.Text,
                IrsaliyeNo = txtIrsaliyeNo.Text,
                KayitEden = "",
                KayitTarihi = DateTime.Now,
                Duzenleyen = "",
                DuzenlemeTarihi = DateTime.Now,
                Yetkili = txtYetkili.Text,
                Id = Id,
            };
            if (this.Id == 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = @"INSERT INTO MalzemeDepo1
                                       (IslemCinsi,Tarih,FirmaKodu,FirmaUnvani,Aciklama,DepoId,IrsaliyeNo
                            ,KayitEden,KayitTarihi,Duzenleyen,DuzenlemeTarihi,SevkTarihi,Yetkili) OUTPUT INSERTED.Id
                                 VALUES (@IslemCinsi,@Tarih,@FirmaKodu,@FirmaUnvani,@Aciklama,@DepoId,@IrsaliyeNo
                            ,@KayitEden,@KayitTarihi,@Duzenleyen,@DuzenlemeTarihi,@SevkTarihi,@Yetkili)";
                    string sqlite = @"INSERT INTO MalzemeDepo1
                                       (IslemCinsi,Tarih,FirmaKodu,FirmaUnvani,Aciklama,DepoId,FaturaNo,FaturaTarihi,IrsaliyeNo
                            ,KayitEden,KayitTarihi,Duzenleyen,DuzenlemeTarihi,SevkTarihi,Yetkili)
                                 VALUES (@IslemCinsi,@Tarih,@FirmaKodu,@FirmaUnvani,@Aciklama,@DepoId,@FaturaNo,@FaturaTarihi,@IrsaliyeNo
                            ,@KayitEden,@KayitTarihi,@Duzenleyen,@DuzenlemeTarihi,@SevkTarihi,@Yetkili)";
                    string idQuery = "SELECT last_insert_rowid();";
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        this.Id = connection.QuerySingle<int>(mssql, FisBaslik);
                    }
                    else
                    {
                        connection.Execute(sqlite, FisBaslik);
                        this.Id = connection.QuerySingle<int>(idQuery);
                    }
                    string sql = @"INSERT INTO MalzemeDepo2
                                   (RefNo,KalemIslem,MalzemeKodu,MalzemeAdi,Miktar,Birim,UUID,TeslimAlan) OUTPUT INSERTED.Id
                             VALUES (@RefNo,@KalemIslem,@MalzemeKodu,@MalzemeAdi,@Miktar,@Birim,@UUID,@TeslimAlan)";
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        int newId = connection.ExecuteScalar<int>(sql, new
                        {
                            RefNo = this.Id,
                            KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                            MalzemeKodu = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeKodu")),
                            MalzemeAdi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeAdi")),
                            Miktar = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "Miktar")),
                            Birim = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Birim")),
                            UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                            TeslimAlan = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "TeslimAlan")),
                        });
                        gridView1.SetRowCellValue(i, "KayitNo", newId); // "KayitNo" sütununun adı veritabanınızdaki adla uyuşmalıdır

                    }
                    bildirim.Basarili();
                }
            }
            else
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    var query = @"select Id from MalzemeDepo1 where Id = @Id";
                    var sonuc = connection.QueryFirstOrDefault<int>(query, new { Id = this.Id });
                    if (sonuc != 0)
                    {
                        string updateQueryD1 = @"UPDATE dbo.MalzemeDepo1
                            SET IslemCinsi = @IslemCinsi,Tarih = @Tarih,FirmaKodu = @FirmaKodu,FirmaUnvani = @FirmaUnvani,Aciklama = @Aciklama,DepoId = @DepoId
                        ,SevkTarihi = @SevkTarihi,IrsaliyeNo = @IrsaliyeNo,KayitEden = @KayitEden,KayitTarihi = @KayitTarihi
                        ,Duzenleyen = @Duzenleyen,DuzenlemeTarihi = @DuzenlemeTarihi, Yetkili=@Yetkili WHERE Id = @Id";
                        connection.Execute(updateQueryD1, FisBaslik);
                        for (int i = 0; i < gridView1.RowCount - 1; i++)
                        {
                            int KayitNo = Convert.ToInt32(gridView1.GetRowCellValue(i, "KayitNo"));
                            var sqlKalem = @"select * from MalzemeDepo2 where Id = @KayitNo";
                            var kalem = connection.QueryFirstOrDefault(sqlKalem, new { KayitNo = KayitNo });
                            if (kalem != null)
                            {
                                string updateKalem = @"UPDATE MalzemeDepo2
                                                SET KalemIslem = @KalemIslem, MalzemeKodu = @MalzemeKodu,MalzemeAdi= @MalzemeAdi,Miktar= @Miktar,Birim = @Birim,TeslimAlan = @TeslimAlan
                                               WHERE Id = @KayitNo";
                                connection.Execute(updateKalem, new
                                {
                                    KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                                    MalzemeKodu = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeKodu")),
                                    MalzemeAdi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeAdi")),
                                    Miktar = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "Miktar")),
                                    Birim = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Birim")),
                                    UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                                    TeslimAlan = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "TeslimAlan")),
                                    KayitNo = KayitNo
                                });
                            }
                            else
                            {
                                string sql = @"INSERT INTO MalzemeDepo2
                                   (RefNo,KalemIslem,MalzemeKodu,MalzemeAdi,Miktar,Birim,UUID)
                                VALUES (@RefNo,@KalemIslem,@MalzemeKodu,@MalzemeAdi,@Miktar,@Birim,@UUID)";
                                int newId = connection.ExecuteScalar<int>(sql, new
                                {
                                    RefNo = this.Id,
                                    KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                                    MalzemeKodu = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeKodu")),
                                    MalzemeAdi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeAdi")),
                                    Miktar = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "Miktar")),
                                    Birim = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Birim")),
                                    UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                                    TeslimAlan = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "TeslimAlan")),
                                });
                                gridView1.SetRowCellValue(i, "KayitNo", newId);
                            }
                        }
                        bildirim.GuncellemeBasarili();
                    }
                }
            }
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmMalzemeKartiListesi frm = new Liste.FrmMalzemeKartiListesi("");
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.Kodu) && !string.IsNullOrEmpty(frm.Adi))
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "MalzemeKodu", frm.Kodu);
                gridView1.SetRowCellValue(newRowHandle, "MalzemeAdi", frm.Adi);
            }
        }

        private void FrmMalzemeCikis_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateSevkTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<_MalzemeKalem>();
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
        }

        private void btnIslemBekleyenler_Click(object sender, EventArgs e)
        {
            FrmMalzemeDepoStok frm = new FrmMalzemeDepoStok();
            frm.ShowDialog();
            foreach (var item in frm.malzemeBilgileri)
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;
                var values = item.Split(';');
                gridView1.SetRowCellValue(newRowHandle, "MalzemeKodu", values[0]);
                gridView1.SetRowCellValue(newRowHandle, "MalzemeAdi", values[1]);
                gridView1.SetRowCellValue(newRowHandle, "UUID", values[2]);
                gridView1.SetRowCellValue(newRowHandle, "Birim", values[3]);
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            Liste.FrmMalzemeCikisListesi frm = new Liste.FrmMalzemeCikisListesi();
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                dateTarih.EditValue = (DateTime)frm.veriler[0]["Tarih"];
                txtFirmaKodu.Text = frm.veriler[0]["FirmaKodu"].ToString();
                txtFirmaUnvan.Text = frm.veriler[0]["FirmaUnvani"].ToString();
                txtDepoKodu.Text = frm.veriler[0]["DepoId"].ToString();
                txtIrsaliyeNo.Text = frm.veriler[0]["IrsaliyeNo"].ToString();
                rchAciklama.Text = frm.veriler[0]["Aciklama"].ToString();
                txtYetkili.Text = frm.veriler[0]["Yetkili"].ToString();
                Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                string[] columnNames = new string[]
                {
                    "KalemIslem", "MalzemeKodu", "MalzemeAdi", "Miktar", "Birim", "UUID","KayitNo","TeslimAlan"
                };
                yardimciAraclar.ListedenGrideYansit(gridControl1, columnNames, frm.veriler);
            }

        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            GridView gridView = sender as GridView;

            if (e.KeyCode == Keys.Delete)
            {
                if (bildirim.SilmeOnayı())
                {
                    int selectedRowHandle = gridView.FocusedRowHandle;
                    int kayitNo = Convert.ToInt32(gridView.GetRowCellValue(selectedRowHandle, "KayitNo"));
                    using (var connection = new Baglanti().GetConnection())
                    {
                        string sql = "delete from MalzemeDepo2 where Id = @Id";
                        connection.Execute(sql, new { Id = kayitNo });
                        gridView.DeleteRow(selectedRowHandle);
                        bildirim.SilmeBasarili();
                    }
                }
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (bildirim.SilmeOnayı())
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string d1 = "delete from MalzemeDepo1 where Id = @Id";
                    string d2 = "delete from MalzemeDepo2 where RefNo = @Id";
                    connection.Execute(d1, new { Id = this.Id });
                    connection.Execute(d2, new { Id = this.Id });
                    bildirim.SilmeBasarili();
                    FormTemizle();
                }
            }
        }
        public void KayitlariGetir(string OncemiSonrami)
        {
            int id = this.Id;
            int? istenenId = null;

            try
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string sql;

                    if (OncemiSonrami == "Önceki")
                    {
                        sql = "SELECT MAX(MalzemeDepo1.Id) FROM MalzemeDepo1 INNER JOIN MalzemeDepo2 ON MalzemeDepo1.Id = MalzemeDepo2.RefNo WHERE MalzemeDepo1.Id < @Id and MalzemeDepo1.IslemCinsi='Çıkış'";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = "SELECT MIN(MalzemeDepo1.Id) FROM MalzemeDepo1 INNER JOIN MalzemeDepo2 ON MalzemeDepo1.Id = MalzemeDepo2.RefNo WHERE MalzemeDepo1.Id > @Id and MalzemeDepo1.IslemCinsi='Çıkış'";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    string fisquery = "SELECT * FROM MalzemeDepo1 WHERE Id = @Id";
                    string kalemquery = "SELECT *,Id [KayitNo] FROM MalzemeDepo2  WHERE RefNo = @Id";
                    var fis = connection.QueryFirstOrDefault(fisquery, new { Id = istenenId });
                    var kalemler = connection.Query(kalemquery, new { Id = istenenId });
                    if (fis != null && kalemler != null)
                    {
                        gridControl1.DataSource = null;
                        dateTarih.EditValue = (DateTime)fis.Tarih;
                        txtFirmaKodu.Text = fis.FirmaKodu?.ToString();
                        txtFirmaUnvan.Text = fis.FirmaUnvani?.ToString();
                        txtDepoKodu.Text = fis.DepoId?.ToString();
                        txtIrsaliyeNo.Text = fis.IrsaliyeNo?.ToString();
                        rchAciklama.Text = fis.Aciklama?.ToString();
                        txtYetkili.Text = fis.Yetkili?.ToString();
                        this.Id = Convert.ToInt32(fis.Id);
                        gridControl1.DataSource = kalemler.ToList();

                    }
                    else
                    {
                        bildirim.Uyari("Gösterilecek herhangi bir kayıt bulunamadı!");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Önceki");
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Sonraki");
        }

        private void siparişFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Id == 0)
            {
                bildirim.Uyari("Rapor alabilmek için bir kayıt seçmelisiniz!");
            }
            else
            {
                Rapor.FrmRaporSecimEkrani frm = new Rapor.FrmRaporSecimEkrani(this.Text, this.Id);
                frm.ShowDialog();
            }
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, "KayitNo", 0);
        }
    }
}