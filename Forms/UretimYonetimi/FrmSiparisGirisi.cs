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

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmSiparisGirisi : DevExpress.XtraEditors.XtraForm
    {
        Bildirim bildirim = new Bildirim();
        Numarator numarator = new Numarator();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Ayarlar ayarlar = new Ayarlar();
        public FrmSiparisGirisi()
        {
            InitializeComponent();
        }
        int Id = 0;
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmFirmaKartiListesi frm = new Liste.FrmFirmaKartiListesi();
            frm.ShowDialog();
            txtFirmaKodu.Text = frm.FirmaKodu;
            lblFirmaAdi.Text = frm.FirmaUnvan;
        }
        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            object baslik = new
            {
                SiparisNo = txtSiparisNo.Text,
                Tarih = Convert.ToDateTime(dateTarih.EditValue),
                Termin = Convert.ToDateTime(dateTermin.EditValue),
                FirmaKodu = txtFirmaKodu.Text,
                FirmaUnvan = lblFirmaAdi.Text,
                Yetkili = txtYetkili.Text,
                DovizBirim = cmbDovizBirim.Text,
                Doviz = Convert.ToDecimal(txtDoviz.Text),
                Vade = txtVade.Text,
                FisNo = txtFisNo.Text,
                Aciklama = rchAciklama.Text,
                Onayli = false,
                Bitti = false,
                MusteriSipNo = txtMusteriSipNo.Text,
                Id = this.Id,
            };
            int refNo;
            if (this.Id == 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string sqlBaslik = @"INSERT INTO Siparis
                                   (SiparisNo,Tarih,Termin,FirmaKodu,FirmaUnvan,Yetkili,DovizBirim,Doviz,Vade,FisNo,Aciklama,Onayli,Bitti,MusteriSipNo) OUTPUT INSERTED.Id
                             VALUES (@SiparisNo,@Tarih,@Termin,@FirmaKodu,@FirmaUnvan,@Yetkili,@DovizBirim,@Doviz,@Vade,@FisNo,@Aciklama,@Onayli,@Bitti,@MusteriSipNo)";
                    string sqlBaslikSqlite = @"INSERT INTO Siparis
                                   (SiparisNo,Tarih,Termin,FirmaKodu,FirmaUnvan,Yetkili,DovizBirim,Doviz,Vade,FisNo,Aciklama,Onayli,Bitti,MusteriSipNo)
                             VALUES (@SiparisNo,@Tarih,@Termin,@FirmaKodu,@FirmaUnvan,@Yetkili,@DovizBirim,@Doviz,@Vade,@FisNo,@Aciklama,@Onayli,@Bitti,@MusteriSipNo)";
                    string idQuery = "SELECT last_insert_rowid();";
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        refNo = connection.QuerySingle<int>(sqlBaslik, baslik);
                    }
                    else
                    {
                        connection.Execute(sqlBaslikSqlite, baslik);
                        refNo = connection.QuerySingle<int>(idQuery);
                    }
                    string sqlFis = @"INSERT INTO SiparisKalem
                                       (RefNo ,UrunKodu ,UrunAdi  ,Varyant ,Birim ,PesinFiyat ,VadeFiyat ,VadeSuresi ,UUID,Miktar)
                                 VALUES
                                       (@RefNo ,@UrunKodu ,@UrunAdi  ,@Varyant ,@Birim ,@PesinFiyat ,@VadeFiyat ,@VadeSuresi ,@UUID,@Miktar)";
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        connection.Execute(sqlFis, new
                        {
                            RefNo = refNo,
                            UrunKodu = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UrunKodu")),
                            UrunAdi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UrunAdi")),
                            Varyant = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Varyant")),
                            Birim = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Birim")),
                            PesinFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "PesinFiyat")),
                            VadeFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "VadeFiyat")),
                            VadeSuresi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "VadeSuresi")),
                            UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                            Miktar = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "Miktar")),
                        });
                    }
                    bildirim.Basarili();
                }
            }
            else
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    var query = @"select Id from Siparis where Id = @Id";
                    var sonuc = connection.QueryFirstOrDefault<int>(query, new { Id = this.Id });
                    if (sonuc != 0)
                    {
                        string updateQueryD1 = @"UPDATE Siparis SET SiparisNo = @SiparisNo,Tarih = @Tarih,Termin = @Termin,FirmaKodu = @FirmaKodu,FirmaUnvan = @FirmaUnvan
                                            ,Yetkili = @Yetkili,DovizBirim = @DovizBirim,Doviz = @Doviz,Vade = @Vade,FisNo = @FisNo,Aciklama = @Aciklama
                                            ,MusteriSipNo = @MusteriSipNo WHERE Id = @Id";
                        connection.Execute(updateQueryD1, baslik);
                        for (int i = 0; i < gridView1.RowCount - 1; i++)
                        {
                            string uuid = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID"));
                            var sqlKalem = @"select * from SiparisKalem where UUID = @UUID";
                            var kalem = connection.QueryFirstOrDefault(sqlKalem, new { UUID = uuid });
                            if (kalem != null)
                            {
                                string updateKalem = @"UPDATE SiparisKalem
                                                SET UrunKodu = @UrunKodu,UrunAdi = @UrunAdi,Varyant = @Varyant,Birim = @Birim
                                               ,PesinFiyat = @PesinFiyat,VadeFiyat = @VadeFiyat,VadeSuresi = @VadeSuresi,Miktar = @Miktar
                                               WHERE UUID = @UUID";
                                connection.Execute(updateKalem, new
                                {
                                    UrunKodu = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UrunKodu")),
                                    UrunAdi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UrunAdi")),
                                    Varyant = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Varyant")),
                                    Birim = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Birim")),
                                    PesinFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "PesinFiyat")),
                                    VadeFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "VadeFiyat")),
                                    VadeSuresi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "VadeSuresi")),
                                    UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                                    Miktar = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "Miktar")),
                                });
                            }
                            else
                            {
                                string sqlFis = @"INSERT INTO SiparisKalem
                                       (RefNo ,UrunKodu ,UrunAdi  ,Varyant ,Birim ,PesinFiyat ,VadeFiyat ,VadeSuresi ,UUID,Miktar)
                                 VALUES
                                       (@RefNo ,@UrunKodu ,@UrunAdi  ,@Varyant ,@Birim ,@PesinFiyat ,@VadeFiyat ,@VadeSuresi ,@UUID,@Miktar)";
                                connection.Execute(sqlFis, new
                                {
                                    RefNo = this.Id,
                                    UrunKodu = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UrunKodu")),
                                    UrunAdi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UrunAdi")),
                                    Varyant = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Varyant")),
                                    Birim = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Birim")),
                                    PesinFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "PesinFiyat")),
                                    VadeFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "VadeFiyat")),
                                    VadeSuresi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "VadeSuresi")),
                                    UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                                    Miktar = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "Miktar")),
                                });
                            }
                        }
                    }
                    bildirim.GuncellemeBasarili();
                }
            }
        }
        private void FrmSiparisGirisi_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            txtFisNo.Text = numarator.NumaraVer("Fiş");
            txtSiparisNo.Text = numarator.NumaraVer("Sipariş");
            dateTarih.EditValue = DateTime.Now;
            dateTermin.EditValue = DateTime.Now;
            KurBilgisiYansit();
            gridControl1.DataSource = new BindingList<SiparisKalem>();
        }
        void KurBilgisiYansit()
        {
            if (cmbDovizBirim.SelectedIndex == 0)
            {
                txtDoviz.Text = "1";
            }
        }
        private void cmbDovizBirim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDovizBirim.SelectedIndex == 1)
            {
                txtDoviz.Text = yardimciAraclar.KurBilgisiniYansit("EUR_ALIS").ToString();
            }
            else if (cmbDovizBirim.SelectedIndex == 2)
            {
                txtDoviz.Text = yardimciAraclar.KurBilgisiniYansit("USD_ALIS").ToString();
            }
            else
            {
                txtDoviz.Text = "1";
            }
        }
        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmUrunKartiListesi frm = new Liste.FrmUrunKartiListesi();
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.UrunKodu) && !string.IsNullOrEmpty(frm.UrunAdi))
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;

                gridView1.SetRowCellValue(newRowHandle, "UrunKodu", frm.UrunKodu);
                gridView1.SetRowCellValue(newRowHandle, "UrunAdi", frm.UrunAdi);

            }
        }
        private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            string uuid = Guid.NewGuid().ToString();
            view.SetRowCellValue(e.RowHandle, "UUID", uuid);
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
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmSiparisGirisiListesi frm = new Liste.FrmSiparisGirisiListesi();
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                txtSiparisNo.Text = frm.veriler[0]["SiparisNo"].ToString();
                dateTarih.EditValue = frm.veriler[0]["Tarih"].ToString();
                dateTermin.EditValue = frm.veriler[0]["Termin"].ToString();
                txtFirmaKodu.Text = frm.veriler[0]["FirmaKodu"].ToString();
                lblFirmaAdi.Text = frm.veriler[0]["FirmaUnvan"].ToString();
                txtYetkili.Text = frm.veriler[0]["Yetkili"].ToString();
                cmbDovizBirim.Text = frm.veriler[0]["DovizBirim"].ToString();
                txtDoviz.Text = frm.veriler[0]["Doviz"].ToString();
                txtVade.Text = frm.veriler[0]["Vade"].ToString();
                txtFisNo.Text = frm.veriler[0]["FisNo"].ToString();
                rchAciklama.Text = frm.veriler[0]["Aciklama"].ToString();
                txtMusteriSipNo.Text = frm.veriler[0]["MusteriSipNo"].ToString();
                Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                string[] columnNames = new string[]
                {
                    "UrunKodu", "UrunAdi", "Varyant", "Miktar", "Birim", "PesinFiyat", "VadeFiyat", "VadeSuresi", "UUID"
                };
                yardimciAraclar.ListedenGrideYansit(gridControl1, columnNames, frm.veriler);
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtFisNo.Text = numarator.NumaraVer("Fiş");
            txtSiparisNo.Text = numarator.NumaraVer("Sipariş");
            chckBitti.Checked = false;
            chckOnayli.Checked = false;
            this.Id = 0;
            object[] bilgiler = { dateTarih, dateTermin, txtFirmaKodu, lblFirmaAdi, txtYetkili, cmbDovizBirim, txtDoviz, txtVade, rchAciklama, txtMusteriSipNo };
            yardimciAraclar.FisVeHavuzuTemizle(bilgiler, gridControl1);
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Önceki");
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
                        sql = "SELECT MAX(Siparis.Id) FROM Siparis INNER JOIN SiparisKalem ON Siparis.Id = SiparisKalem.RefNo WHERE Siparis.Id < @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = "SELECT MAX(Siparis.Id) FROM Siparis INNER JOIN SiparisKalem ON Siparis.Id = SiparisKalem.RefNo WHERE Siparis.Id > @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    string fisquery = "SELECT * FROM Siparis  WHERE Id = @Id";
                    string kalemquery = "SELECT * FROM SiparisKalem  WHERE RefNo = @Id";
                    var fis = connection.QueryFirstOrDefault(fisquery, new { Id = istenenId });
                    var kalemler = connection.Query(kalemquery, new { Id = istenenId });
                    if (fis != null && kalemler != null)
                    {
                        gridControl1.DataSource = null;
                        txtSiparisNo.Text = fis.SiparisNo.ToString();
                        dateTarih.EditValue = fis.Tarih.ToString();
                        dateTermin.EditValue = fis.Termin.ToString();
                        txtFirmaKodu.Text = fis.FirmaKodu.ToString();
                        lblFirmaAdi.Text = fis.FirmaUnvan.ToString();
                        txtYetkili.Text = fis.Yetkili.ToString();
                        cmbDovizBirim.Text = fis.DovizBirim.ToString();
                        txtDoviz.Text = fis.Doviz.ToString();
                        txtVade.Text = fis.Vade.ToString();
                        txtFisNo.Text = fis.FisNo.ToString();
                        rchAciklama.Text = fis.Aciklama.ToString();
                        txtMusteriSipNo.Text = fis.MusteriSipNo.ToString();
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
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Sonraki");
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }

        private void repoBtnReceteNo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int newRowHandle = gridView1.FocusedRowHandle;
            string kod = gridView1.GetFocusedRowCellValue("UrunKodu").ToString();
            Liste.FrmUrunReceteKartiListesi frm = new Liste.FrmUrunReceteKartiListesi(kod);
            frm.ShowDialog();
            if (frm.ReceteNo != null)
            {
                gridView1.SetRowCellValue(newRowHandle, "MalzemeKodu", frm.ReceteNo);
            }
        }
    }
}