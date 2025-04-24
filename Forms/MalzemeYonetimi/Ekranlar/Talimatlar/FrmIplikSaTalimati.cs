using Dapper;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.Context;
using Hesap.DataAccess;
using Hesap.Forms.MalzemeYonetimi.Ekranlar.IplikDepo;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.Talimatlar
{
    public partial class FrmIplikSaTalimati : DevExpress.XtraEditors.XtraForm
    {
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        Numarator numarator = new Numarator();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Bildirim bildirim = new Bildirim();
        CRUD_Operations cRUD = new CRUD_Operations();
        CrudRepository crudRepository = new CrudRepository();
        int FirmaId = 0, Id = 0;
        private const string TableName1 = "Receipt", TableName2 = "ReceiptItem";
        public FrmIplikSaTalimati()
        {
            InitializeComponent();
            SayiFormati();

        }
        void SayiFormati()
        {
            gridView1.CustomColumnDisplayText += (sender, e) => yansit.SayiyaNoktaKoy(sender, e, "SatirTutari");
            gridView1.CustomColumnDisplayText += (sender, e) => yansit.SayiyaNoktaKoy(sender, e, "NetKg");
            gridView1.CustomColumnDisplayText += (sender, e) => yansit.SayiyaNoktaKoy(sender, e, "BrutKg");
        }
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }
        private void FrmIplikSaTalimati_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            txtTalimatNo.Text = crudRepository.GetNumaratorWithCondition(TableName1, "ReceiptNo", Convert.ToInt32(ReceiptTypes.IplikSatinAlmaTalimati));
        }
        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmIplikKartiListesi frm = new Liste.FrmIplikKartiListesi();
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.IplikKodu))
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "IplikKodu", frm.IplikKodu);
                gridView1.SetRowCellValue(newRowHandle, "IplikAdi", frm.IplikAdi);
                gridView1.SetRowCellValue(newRowHandle, "IplikId", frm.Id);
            }
        }
        private void repoBtnMarka_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //Liste.FrmMarkaSecimListesi frm = new Liste.FrmMarkaSecimListesi("IplikDepo2");
            //frm.ShowDialog();
            //if (frm._marka != null)
            //{
            //    int newRowHandle = gridView1.FocusedRowHandle;
            //    gridView1.SetRowCellValue(newRowHandle, "Marka", frm._marka);
            //}
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.FirmaId == 0)
                {
                    bildirim.Uyari("Firma seçilmeden kayıt işlemi gerçekleştirilemez!!");
                    return;
                }
                var parameters = new Dictionary<string, object>
                {
                    { "ReceiptType", ReceiptTypes.IplikSatinAlmaTalimati }, { "ReceiptDate", dateTarih.EditValue }, { "CompanyId", this.FirmaId },{ "Explanation", rchAciklama.Text }, { "ReceiptNo", txtTalimatNo.Text },{ "Authorized", txtYetkili.Text },{"Maturity",txtVade.Text},{"PaymentType",comboBoxEdit1.Text}
                };
                if (this.Id == 0)
                {
                    this.Id = crudRepository.Insert(TableName1, parameters);
                    var itemList = (BindingList<ReceiptItem>)gridControl1.DataSource;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        var item = itemList[i];
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "Piece", item.Piece }, { "UnitPrice", item.UnitPrice }, { "Explanation", item.Explanation }, { "UUID", item.UUID }, { "RowAmount", item.RowAmount }, { "Vat", item.Vat }, { "TrackingNumber", item.TrackingNumber } };
                        var rec_id = crudRepository.Insert(TableName2, values);
                        gridView1.SetRowCellValue(i, "ReceiptItemId", rec_id);
                    }
                    bildirim.Basarili();
                }
                //else
                //{
                //    crudRepository.Update(TableName1, Id, parameters);
                //    for (int i = 0; i < gridView1.RowCount - 1; i++)
                //    {
                //        var recIdObj = gridView1.GetRowCellValue(i, "ReceiptItemId");
                //        int rec_id = recIdObj != null ? Convert.ToInt32(recIdObj) : 0;
                //        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", gridView1.GetRowCellValue(i, "OperationType") }, { "InventoryId", Convert.ToInt32(gridView1.GetRowCellValue(i, "InventoryId")) }, { "Piece", ConvertDecimal(gridView1.GetRowCellValue(i, "Piece").ToString()) }, { "UnitPrice", ConvertDecimal(gridView1.GetRowCellValue(i, "UnitPrice").ToString()) }, { "RowAmount", ConvertDecimal(gridView1.GetRowCellValue(i, "RowAmount").ToString()) }, { "Vat", Convert.ToInt32(gridView1.GetRowCellValue(i, "Vat")) }, { "UUID", gridView1.GetRowCellValue(i, "UUID") }, { "Explanation", gridView1.GetRowCellValue(i, "Explanation") } };
                //        if (rec_id != 0)
                //        {
                //            crudRepository.Update(TableName2, rec_id, values);
                //        }
                //        else
                //        {
                //            var new_rec_id = crudRepository.Insert(TableName2, values);
                //            gridView1.SetRowCellValue(i, "ReceiptItemId", new_rec_id);
                //        }
                //    }
                //    bildirim.GuncellemeBasarili();
                //}
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata : " + ex.Message);
            }
            #region eski_kodlar
            //object Baslik = new
            //{
            //    IslemCinsi = "SaTal",
            //    TalimatNo = txtTalimatNo.Text,
            //    Tarih = dateTarih.EditValue,
            //    FirmaId = this.FirmaId,
            //    Aciklama = rchAciklama.Text,
            //    Yetkili = txtYetkili.Text,
            //    Vade = txtVade.Text,
            //    OdemeSekli = comboBoxEdit1.Text,
            //    Id = this.Id,
            //}; 
            //if (this.Id == 0)
            //{
            //    using (var connection = new Baglanti().GetConnection())
            //    {
            //        string mssql = @"INSERT INTO IplikDepo1 (IslemCinsi,TalimatNo,Tarih,FirmaId,Aciklama,Yetkili,Vade,OdemeSekli) OUTPUT INSERTED.Id
            //                            VALUES (@IslemCinsi,@TalimatNo,@Tarih,@FirmaId,@Aciklama,@Yetkili,@Vade,@OdemeSekli)";
            //        if (ayarlar.VeritabaniTuru() == "mssql")
            //        {
            //            this.Id = connection.QuerySingle<int>(mssql, Baslik);
            //        }
            //        else
            //        {
            //            //connection.Execute(sqlite, FisBaslik);
            //            //this.Id = connection.QuerySingle<int>(idQuery);
            //        }
            //        string sql = @"INSERT INTO IplikDepo2 (RefNo,KalemIslem,IplikId,NetKg,BrutKg,Fiyat,DovizCinsi,OrganikSertifikaNo,Marka,IplikRenkId
            //                    ,Aciklama,UUID,SatirTutari) OUTPUT INSERTED.Id
            //                         VALUES (@RefNo,@KalemIslem,@IplikId,@NetKg,@BrutKg,@Fiyat,@DovizCinsi,@OrganikSertifikaNo,@Marka,@IplikRenkId
            //                    ,@Aciklama,@UUID,@SatirTutari)";
            //        for (int i = 0; i < gridView1.RowCount - 1; i++)
            //        {
            //            connection.Execute(sql, new
            //            {
            //                RefNo = this.Id,
            //                KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
            //                IplikId = gridView1.GetRowCellValue(i, "IplikId"),
            //                NetKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "NetKg")),
            //                BrutKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "BrutKg")),
            //                Fiyat = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "Fiyat")),
            //                DovizCinsi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "DovizCinsi")),
            //                OrganikSertifikaNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "OrganikSertifikaNo")),
            //                Marka = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Marka")),
            //                IplikRenkId = gridView1.GetRowCellValue(i, "IplikRenkId"),
            //                Aciklama = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Aciklama")),
            //                UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
            //                SatirTutari = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "SatirTutari")),
            //            });
            //        }
            //        bildirim.Basarili();
            //    }
            //}
            //else
            //{
            //    using (var connection = new Baglanti().GetConnection())
            //    {
            //        var query = @"select Id from IplikDepo1 where Id = @Id";
            //        var sonuc = connection.QueryFirstOrDefault<int>(query, new { Id = this.Id });
            //        if (sonuc != 0)
            //        {
            //            string updateD1 = @"UPDATE IplikDepo1 SET Tarih = @Tarih,FirmaId = @FirmaId,
            //                                Aciklama = @Aciklama, Yetkili=@Yetkili, Vade=@Vade, OdemeSekli = @OdemeSekli WHERE Id = @Id";

            //            connection.Execute(updateD1, Baslik);
            //            for (int i = 0; i < gridView1.RowCount - 1; i++)
            //            {
            //                int takipNo = Convert.ToInt32(gridView1.GetRowCellValue(i, "TakipNo"));
            //                var sqlKalem = @"select * from IplikDepo2 where Id= @Id";
            //                var kalem = connection.QueryFirstOrDefault(sqlKalem, new { Id = takipNo });
            //                if (kalem != null)
            //                {
            //                    string updateKalem = @"UPDATE IplikDepo2 SET KalemIslem = @KalemIslem,IplikId = @IplikId,NetKg = @NetKg,BrutKg = @BrutKg,Fiyat = @Fiyat,DovizCinsi = @DovizCinsi,OrganikSertifikaNo = @OrganikSertifikaNo
            //                                            ,Marka = @Marka,IplikRenkId = @IplikRenkId,Aciklama = @Aciklama,UUID = @UUID ,SatirTutari = @SatirTutari WHERE Id = @Id";
            //                    connection.Execute(updateKalem, new
            //                    {
            //                        Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "TakipNo")),
            //                        KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
            //                        IplikId = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "IplikId")),
            //                        NetKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "NetKg")),
            //                        BrutKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "BrutKg")),
            //                        Fiyat = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "Fiyat")),
            //                        DovizCinsi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "DovizCinsi")),
            //                        OrganikSertifikaNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "OrganikSertifikaNo")),
            //                        Marka = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Marka")),
            //                        IplikRenkId = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "IplikRenkId")),
            //                        Aciklama = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "SatirAciklama")),
            //                        UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
            //                        SatirTutari = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "SatirTutari")),
            //                    });
            //                }
            //                else
            //                {
            //                    string sql = @"INSERT INTO IplikDepo2 (RefNo,KalemIslem,IplikId,NetKg,BrutKg,Fiyat,DovizCinsi,OrganikSertifikaNo,Marka,IplikRenkId,Aciklama,UUID,SatirTutari) OUTPUT INSERTED.Id
            //                         VALUES (@RefNo,@KalemIslem,@IplikId,@NetKg,@BrutKg,@Fiyat,@DovizCinsi,@OrganikSertifikaNo,@Marka,@IplikRenkId,@Aciklama,@UUID,@SatirTutari)";
            //                    int yeniId = connection.ExecuteScalar<int>(sql, new
            //                    {
            //                        RefNo = this.Id,
            //                        KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
            //                        IplikId = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "IplikId")),
            //                        NetKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "NetKg")),
            //                        BrutKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "BrutKg")),
            //                        Fiyat = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "Fiyat")),
            //                        DovizCinsi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "DovizCinsi")),
            //                        OrganikSertifikaNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "OrganikSertifikaNo")),
            //                        Marka = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Marka")),
            //                        IplikRenkId = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "IplikRenkId")),
            //                        Aciklama = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "SatirAciklama")),
            //                        UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
            //                        SatirTutari = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "SatirTutari")),
            //                    });
            //                    gridView1.SetRowCellValue(i, "TakipNo", yeniId);
            //                }
            //            }
            //            bildirim.GuncellemeBasarili();
            //        }
            //    }
            //}
            #endregion
        }
        private void satınAlmaTalimatFormuToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void btnListe_Click(object sender, EventArgs e)
        {
            //FrmIplikDepoListe frm = new FrmIplikDepoListe("SaTal");
            //frm.ShowDialog();
            //if (frm.veriler.Count > 0)
            //{
            //    this.Id = Convert.ToInt32(frm.veriler[0]["Id"]);
            //    dateTarih.EditValue = (DateTime)frm.veriler[0]["Tarih"];
            //    txtFirmaKodu.Text = frm.veriler[0]["FirmaKodu"].ToString();
            //    txtFirmaUnvan.Text = frm.veriler[0]["FirmaUnvan"].ToString();
            //    this.FirmaId = Convert.ToInt32(frm.veriler[0]["FirmaId"]);
            //    rchAciklama.Text = frm.veriler[0]["Aciklama"].ToString();
            //    txtTalimatNo.Text = frm.veriler[0]["TalimatNo"].ToString();
            //    txtYetkili.Text = frm.veriler[0]["Yetkili"].ToString();
            //    txtVade.Text = frm.veriler[0]["Vade"].ToString();
            //    comboBoxEdit1.Text = frm.veriler[0]["OdemeSekli"].ToString();
            //    string[] columnNames = new string[]
            //    {
            //        "KalemIslem", "IplikId", "IplikKodu", "IplikAdi", "BrutKg", "NetKg","Fiyat","DovizCinsi","OrganikSertifikaNo","Marka","IplikRenkId",
            //        "IplikRenkKodu","IplikRenkAdi","SatirAciklama","SatirTutari","TakipNo"
            //    };
            //    yardimciAraclar.ListedenGrideYansit(gridControl1, columnNames, frm.veriler);
            //}
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            cRUD.FisVeHavuzSil("IplikDepo1", "IplikDepo2", this.Id);
            FormTemizle();
        }
        void FormTemizle()
        {
            object[] bilgiler = { txtTalimatNo, dateTarih, txtFirmaKodu, txtFirmaUnvan, rchAciklama, txtYetkili, txtVade, comboBoxEdit1 };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<_IplikDepoKalem>();
            this.Id = 0;
            this.FirmaId = 0;
        }
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, "TrackingNumber", 0);
            string uuid = Guid.NewGuid().ToString();
            view.SetRowCellValue(e.RowHandle, "UUID", uuid);
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
                        sql = "SELECT MAX(IplikDepo1.Id) FROM IplikDepo1 INNER JOIN IplikDepo2 ON IplikDepo1.Id = IplikDepo2.RefNo WHERE IplikDepo1.Id < @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = "SELECT MIN(IplikDepo1.Id) FROM IplikDepo1 INNER JOIN IplikDepo2 ON IplikDepo1.Id = IplikDepo2.RefNo WHERE IplikDepo1.Id > @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    string fisquery = @"SELECT 
                                        ISNULL(ID.Id,0) Id,
                                        ISNULL(ID.Tarih,'') Tarih,
                                        ISNULL(ID.TalimatNo,'') TalimatNo,
                                        ISNULL(ID.FirmaId,'') FirmaId,
                                        ISNULL(FK.FirmaKodu,'') FirmaKodu,
                                        ISNULL(FK.FirmaUnvan,'') FirmaUnvan,
                                        ISNULL(ID.Aciklama,'') Aciklama,
                                        ISNULL(ID.IslemCinsi,'') IslemCinsi,
                                        ISNULL(ID.Yetkili,'') Yetkili,
                                        ISNULL(ID.Vade,'') Vade,
                                        ISNULL(ID.OdemeSekli,'') OdemeSekli
                                        FROM IplikDepo1 ID inner join FirmaKarti FK on FK.Id = ID.FirmaId WHERE ID.Id= @Id and ID.IslemCinsi = 'SaTal'";
                    var fis = connection.QueryFirstOrDefault(fisquery, new { Id = istenenId });
                    string kalemquery = @"select
	                                    ISNULL(D2.Id,0) TakipNo,ISNULL(D2.RefNo,0) RefNo,ISNULL(D2.KalemIslem,'') KalemIslem,
	                                    ISNULL(D2.IplikId,0) IplikId,ISNULL(IK.IplikKodu,'') IplikKodu,ISNULL(IK.IplikAdi,'') IplikAdi,
	                                    ISNULL(D2.BrutKg,0) BrutKg,ISNULL(D2.NetKg,0) NetKg,ISNULL(D2.Fiyat,0) Fiyat,
	                                    ISNULL(D2.DovizCinsi,'') DovizCinsi,ISNULL(D2.DovizFiyat,0) DovizFiyat,
	                                    ISNULL(D2.OrganikSertifikaNo,'')OrganikSertifikaNo,ISNULL(D2.Marka,'') Marka,
	                                    ISNULL(D2.KullanimYeri,'') KullanimYeri,ISNULL(D2.IplikRenkId,0) IplikRenkId,
	                                    ISNULL(BRK.BoyahaneRenkKodu,'') IplikRenkKodu,ISNULL(BRK.BoyahaneRenkAdi,'') IplikRenkAdi,
	                                    ISNULL(D2.PartiNo,'') PartiNo,ISNULL(D2.Aciklama,'') SatirAciklama,ISNULL(D2.Barkod,'') Barkod,
	                                    ISNULL(D2.TalimatNo,'') TalimatNo,ISNULL(D2.UUID,'') UUID,ISNULL(D2.SatirTutari,0) SatirTutari
                                    from IplikDepo2 D2 left join IplikKarti IK on IK.Id = D2.IplikId
                                    left join BoyahaneRenkKartlari BRK on D2.IplikRenkId = BRK.Id
                                    WHERE D2.RefNo = @Id";
                    var kalemler = connection.Query(kalemquery, new { Id = istenenId });
                    if (fis != null && kalemler != null)
                    {
                        gridControl1.DataSource = null;
                        this.Id = Convert.ToInt32(fis.Id);
                        dateTarih.EditValue = (DateTime)fis.Tarih;
                        txtTalimatNo.Text = fis.TalimatNo.ToString();
                        this.FirmaId = Convert.ToInt32(fis.FirmaId);
                        txtFirmaKodu.Text = fis.FirmaKodu.ToString();
                        txtFirmaUnvan.Text = fis.FirmaUnvan.ToString();
                        rchAciklama.Text = fis.Aciklama.ToString();
                        txtYetkili.Text = fis.Yetkili.ToString();
                        txtVade.Text = fis.Vade.ToString();
                        comboBoxEdit1.Text = fis.OdemeSekli.ToString();
                        gridControl1.DataSource = kalemler.ToList();
                    }
                    else
                    {
                        bildirim.Uyari("Gösterilecek başka kayıt bulunamadı!!");
                    }
                }
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata : " + ex.Message);
            }
        }
        private void btnGeri_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Önceki");
        }
        private void btnIleri_Click(object sender, EventArgs e)
        {
            KayitlariGetir("Sonraki");
        }
        private void btnYeni_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }

        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }

        private void repoBoyaRenkKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmBoyahaneRenkKartlariListesi frm = new Liste.FrmBoyahaneRenkKartlariListesi();
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkId", Convert.ToInt32(frm.veriler[0]["Id"]));
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkKodu", frm.veriler[0]["BoyahaneRenkKodu"].ToString());
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkAdi", frm.veriler[0]["BoyahaneRenkAdi"].ToString());
            }
        }
    }
}