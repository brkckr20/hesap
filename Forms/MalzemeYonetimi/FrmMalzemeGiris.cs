using Dapper;
using DevExpress.LookAndFeel;
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
    public partial class FrmMalzemeGiris : DevExpress.XtraEditors.XtraForm
    {
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        CRUD_Operations cRUD = new CRUD_Operations();
        KalemParametreleri parametreler = new KalemParametreleri();
        private string TableName1 = "Receipt", TableName2 = "ReceiptItem";
        public FrmMalzemeGiris()
        {
            InitializeComponent();
        }
        int Id = 0;
        int FirmaId=0;
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu,txtFirmaUnvan, ref this.FirmaId);
        }
        private void FrmMalzemeGiris_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateFaturaTarihi.EditValue = DateTime.Now;
            dateIrsaliyeTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<_ReceiptItem>();
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "ReceiptType", ReceiptTypes.SatinAlmaTalimati },
                { "ReceiptDate", dateTarih.EditValue },
                { "CompanyId", this.FirmaId },
                { "Explanation", rchAciklama.Text },
                { "WareHouseId", txtDepoKodu.Text },
                { "InvoiceNo", txtFaturaNo.Text },
                { "InvoiceDate", dateFaturaTarihi.EditValue },
                { "DispatchNo", txtIrsaliyeNo.Text },
                { "DispatchDate", dateIrsaliyeTarihi.EditValue},
            };

            if (this.Id == 0)
            {
                this.Id = cRUD.InsertRecord(TableName1, parameters);

                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var columnNames = new List<string>
                                        {
                                            "KalemIslem", "KumasId", "GrM2", "BrutKg", "NetKg", "BrutMt", "NetMt", "Adet", "Fiyat",
                                            "FiyatBirimi", "DovizCinsi", "RenkId", "Aciklama", "UUID", "SatirTutari", "TakipNo",
                                            "DesenId", "BoyaIslemId"
                                        };
                    var kalemler = parametreler.GetGridViewData(i,this.Id,gridView1,columnNames);
                    var d2Id = cRUD.InsertRecord(TableName2,kalemler);
                    gridView1.SetRowCellValue(i, "D2Id", d2Id); 
                    //var kalemParameters = parametreler.KumasDepoParams(i, this.Id, gridView1);
                    //var d2Id = cRUD.InsertRecord("HamDepo2", kalemParameters);
                    //gridView1.SetRowCellValue(i, "D2Id", d2Id);
                }
                bildirim.Basarili();
            }
            else
            {
                cRUD.UpdateRecord(TableName1, parameters, this.Id);
                //for (int i = 0; i < gridView1.RowCount - 1; i++)
                //{
                //    var d2Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id"));
                //    var kalemParameters = parametreler.KumasDepoParams(i, this.Id, gridView1);
                //    if (d2Id > 0)
                //    {
                //        cRUD.UpdateRecord("HamDepo2", kalemParameters, d2Id);
                //    }
                //    else
                //    {
                //        var yeniId = cRUD.InsertRecord("HamDepo2", kalemParameters);
                //        gridView1.SetRowCellValue(i, "D2Id", yeniId);
                //    }
                //}
                bildirim.GuncellemeBasarili();
                #region eskikod
                //object FisBaslik = new
                //{
                //    IslemCinsi = "Giriş",
                //    Tarih = dateTarih.EditValue,
                //    FirmaKodu = txtFirmaKodu.Text,
                //    FirmaUnvani = txtFirmaUnvan.Text,
                //    Aciklama = rchAciklama.Text,
                //    DepoId = txtDepoKodu.Text,
                //    FaturaNo = txtFaturaNo.Text,
                //    FaturaTarihi = dateFaturaTarihi.EditValue,
                //    IrsaliyeNo = txtIrsaliyeNo.Text,
                //    KayitEden = "",
                //    KayitTarihi = DateTime.Now,
                //    Duzenleyen = "",
                //    DuzenlemeTarihi = DateTime.Now,
                //    Id = Id,
                //};

                //if (this.Id == 0)
                //{
                //    using (var connection = new Baglanti().GetConnection())
                //    {
                //        string mssql = @"INSERT INTO MalzemeDepo1
                //                           (IslemCinsi,Tarih,FirmaKodu,FirmaUnvani,Aciklama,DepoId,FaturaNo,FaturaTarihi,IrsaliyeNo
                //                ,KayitEden,KayitTarihi,Duzenleyen,DuzenlemeTarihi) OUTPUT INSERTED.Id
                //                     VALUES (@IslemCinsi,@Tarih,@FirmaKodu,@FirmaUnvani,@Aciklama,@DepoId,@FaturaNo,@FaturaTarihi,@IrsaliyeNo
                //                ,@KayitEden,@KayitTarihi,@Duzenleyen,@DuzenlemeTarihi)";
                //        string sqlite = @"INSERT INTO MalzemeDepo1
                //                           (IslemCinsi,Tarih,FirmaKodu,FirmaUnvani,Aciklama,DepoId,FaturaNo,FaturaTarihi,IrsaliyeNo
                //                ,KayitEden,KayitTarihi,Duzenleyen,DuzenlemeTarihi)
                //                     VALUES (@IslemCinsi,@Tarih,@FirmaKodu,@FirmaUnvani,@Aciklama,@DepoId,@FaturaNo,@FaturaTarihi,@IrsaliyeNo
                //                ,@KayitEden,@KayitTarihi,@Duzenleyen,@DuzenlemeTarihi)";
                //        string idQuery = "SELECT last_insert_rowid();";
                //        if (ayarlar.VeritabaniTuru() == "mssql")
                //        {
                //            this.Id = connection.QuerySingle<int>(mssql, FisBaslik);
                //        }
                //        else
                //        {
                //            connection.Execute(sqlite, FisBaslik);
                //            this.Id = connection.QuerySingle<int>(idQuery);
                //        }
                //        string sql = @"INSERT INTO MalzemeDepo2
                //                       (RefNo,KalemIslem,MalzemeKodu,MalzemeAdi,Miktar,Birim,UUID,TakipNo,BirimFiyat,SatirTutari)
                //                 VALUES (@RefNo,@KalemIslem,@MalzemeKodu,@MalzemeAdi,@Miktar,@Birim,@UUID,@TakipNo,@BirimFiyat,@SatirTutari)";
                //        for (int i = 0; i < gridView1.RowCount - 1; i++)
                //        {
                //            connection.Execute(sql, new
                //            {
                //                RefNo = this.Id,
                //                KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                //                MalzemeKodu = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeKodu")),
                //                MalzemeAdi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeAdi")),
                //                Miktar = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "Miktar")),
                //                Birim = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Birim")),
                //                UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                //                TakipNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "TakipNo")),
                //                BirimFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "BirimFiyat")),
                //                SatirTutari = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "SatirTutari")),
                //            });
                //        }
                //        bildirim.Basarili();
                //    }
                //}
                //else
                //{
                //    using (var connection = new Baglanti().GetConnection())
                //    {
                //        var query = @"select Id from MalzemeDepo1 where Id = @Id";
                //        var sonuc = connection.QueryFirstOrDefault<int>(query, new { Id = this.Id });
                //        if (sonuc != 0)
                //        {
                //            string updateQueryD1 = @"UPDATE dbo.MalzemeDepo1
                //                SET IslemCinsi = @IslemCinsi,Tarih = @Tarih,FirmaKodu = @FirmaKodu,FirmaUnvani = @FirmaUnvani,Aciklama = @Aciklama,DepoId = @DepoId
                //            ,FaturaNo = @FaturaNo,FaturaTarihi = @FaturaTarihi,IrsaliyeNo = @IrsaliyeNo,KayitEden = @KayitEden,KayitTarihi = @KayitTarihi
                //            ,Duzenleyen = @Duzenleyen,DuzenlemeTarihi = @DuzenlemeTarihi WHERE Id = @Id";
                //            connection.Execute(updateQueryD1, FisBaslik);
                //            for (int i = 0; i < gridView1.RowCount - 1; i++)
                //            {
                //                int uuid = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id"));
                //                var sqlKalem = @"select * from MalzemeDepo2 where Id= @Id";
                //                var kalem = connection.QueryFirstOrDefault(sqlKalem, new { Id = uuid });
                //                if (kalem != null)
                //                {
                //                    string updateKalem = @"UPDATE MalzemeDepo2
                //                                    SET KalemIslem = @KalemIslem, MalzemeKodu = @MalzemeKodu,MalzemeAdi= @MalzemeAdi,Miktar= @Miktar,Birim = @Birim,TakipNo=@TakipNo,BirimFiyat = @BirimFiyat,SatirTutari=@SatirTutari
                //                                   WHERE UUID = @UUID";
                //                    connection.Execute(updateKalem, new
                //                    {
                //                        KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                //                        MalzemeKodu = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeKodu")),
                //                        MalzemeAdi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeAdi")),
                //                        Miktar = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "Miktar")),
                //                        Birim = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Birim")),
                //                        UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                //                        TakipNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "TakipNo")),
                //                        BirimFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "BirimFiyat")),
                //                        SatirTutari = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "SatirTutari")),
                //                    });
                //                }
                //                else
                //                {
                //                    string sql = @"INSERT INTO MalzemeDepo2
                //                       (RefNo,KalemIslem,MalzemeKodu,MalzemeAdi,Miktar,Birim,UUID,BirimFiyat,SatirTutari)
                //                    VALUES (@RefNo,@KalemIslem,@MalzemeKodu,@MalzemeAdi,@Miktar,@Birim,@UUID,@BirimFiyat,@SatirTutari)";
                //                    connection.Execute(sql, new
                //                    {
                //                        RefNo = this.Id,
                //                        KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                //                        MalzemeKodu = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeKodu")),
                //                        MalzemeAdi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "MalzemeAdi")),
                //                        Miktar = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "Miktar")),
                //                        Birim = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Birim")),
                //                        UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                //                        TakipNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "TakipNo")),
                //                        BirimFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "BirimFiyat")),
                //                        SatirTutari = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "SatirTutari")),
                //                    });
                //                }
                //            }
                //            bildirim.GuncellemeBasarili();
                //        }
                //    }
                //}
                #endregion
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
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            string uuid = Guid.NewGuid().ToString();
            view.SetRowCellValue(e.RowHandle, "UUID", uuid);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste.FrmMalzemeGirisListesi frm = new Liste.FrmMalzemeGirisListesi();
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                dateTarih.EditValue = (DateTime)frm.veriler[0]["Tarih"];
                dateFaturaTarihi.EditValue = (DateTime)frm.veriler[0]["FaturaTarihi"];
                txtFirmaKodu.Text = frm.veriler[0]["FirmaKodu"].ToString();
                txtFirmaUnvan.Text = frm.veriler[0]["FirmaUnvani"].ToString();
                txtDepoKodu.Text = frm.veriler[0]["DepoId"].ToString();
                txtFaturaNo.Text = frm.veriler[0]["FaturaNo"].ToString();
                txtIrsaliyeNo.Text = frm.veriler[0]["IrsaliyeNo"].ToString();
                rchAciklama.Text = frm.veriler[0]["Aciklama"].ToString();
                Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                string[] columnNames = new string[]
                {
                    "KalemIslem", "MalzemeKodu", "MalzemeAdi", "Miktar", "Birim", "UUID","TakipNo","BirimFiyat","D2Id"
                };
                yardimciAraclar.ListedenGrideYansit(gridControl1, columnNames, frm.veriler);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }
        void FormTemizle()
        {
            object[] bilgiler = { dateTarih, dateFaturaTarihi, txtFirmaKodu, txtFirmaUnvan, txtDepoKodu, txtFaturaNo, txtIrsaliyeNo, rchAciklama };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<_MalzemeKalem>();
            this.Id = 0;
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
                        sql = "SELECT MAX(MalzemeDepo1.Id) FROM MalzemeDepo1 INNER JOIN MalzemeDepo2 ON MalzemeDepo1.Id = MalzemeDepo2.RefNo WHERE MalzemeDepo1.Id < @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = "SELECT MIN(MalzemeDepo1.Id) FROM MalzemeDepo1 INNER JOIN MalzemeDepo2 ON MalzemeDepo1.Id = MalzemeDepo2.RefNo WHERE MalzemeDepo1.Id > @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    string fisquery = "SELECT * FROM MalzemeDepo1  WHERE Id = @Id";
                    string kalemquery = "SELECT * FROM MalzemeDepo2  WHERE RefNo = @Id";
                    var fis = connection.QueryFirstOrDefault(fisquery, new { Id = istenenId });
                    var kalemler = connection.Query(kalemquery, new { Id = istenenId });
                    if (fis != null && kalemler != null)
                    {
                        gridControl1.DataSource = null;
                        dateTarih.EditValue = (DateTime)fis.Tarih;
                        dateFaturaTarihi.EditValue = (DateTime)fis.FaturaTarihi;
                        txtFirmaKodu.Text = fis.FirmaKodu.ToString();
                        txtFirmaUnvan.Text = fis.FirmaUnvani.ToString();
                        txtDepoKodu.Text = fis.DepoId.ToString();
                        txtFaturaNo.Text = fis.FaturaNo.ToString();
                        txtIrsaliyeNo.Text = fis.IrsaliyeNo.ToString();
                        rchAciklama.Text = fis.Aciklama.ToString();
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

        private void siparişFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnIslemBekleyenler_Click(object sender, EventArgs e)
        {
            FrmIslemBekleyenler frm = new FrmIslemBekleyenler();
            frm.ShowDialog();
            foreach (var item in frm.malzemeBilgileri)
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;
                var values = item.Split(';');
                gridView1.SetRowCellValue(newRowHandle, "KalemIslem", values[0]);
                gridView1.SetRowCellValue(newRowHandle, "MalzemeKodu", values[1]);
                gridView1.SetRowCellValue(newRowHandle, "MalzemeAdi", values[2]);
                gridView1.SetRowCellValue(newRowHandle, "Birim", values[3]);
                gridView1.SetRowCellValue(newRowHandle, "TakipNo", values[4]);
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
    }
}