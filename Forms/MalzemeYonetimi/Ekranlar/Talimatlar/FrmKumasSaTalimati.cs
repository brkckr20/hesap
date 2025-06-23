using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.Talimatlar
{
    public partial class FrmKumasSaTalimati : XtraForm
    {
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        Numarator numarator = new Numarator();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Bildirim bildirim = new Bildirim();
        CRUD_Operations cRUD = new CRUD_Operations();
        int FirmaId = 0, Id = 0;
        KalemParametreleri parametreler = new KalemParametreleri();
        CrudRepository crudRepository = new CrudRepository();
        private const string TableName1 = "Receipt", TableName2 = "ReceiptItem";
        public FrmKumasSaTalimati()
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
        private void FrmKumasSaTalimati_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
            yardimciAraclar.KolonlariGetir(gridView1, this.Text);
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            txtTalimatNo.Text = numarator.NumaraVer("Fiş", Convert.ToInt32(ReceiptTypes.KumasSatinAlmaTalimati));
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
                    { "ReceiptType", ReceiptTypes.KumasSatinAlmaTalimati }, { "ReceiptDate", dateTarih.EditValue }, { "CompanyId", this.FirmaId },{ "Explanation", rchAciklama.Text }, { "ReceiptNo", txtTalimatNo.Text },{ "Authorized", txtYetkili.Text },{"Maturity",txtVade.Text},{"PaymentType",comboBoxEdit1.Text},/*{"Approved",Onayli},*/{"SavedUser",CurrentUser.UserId},{"SavedDate",DateTime.Now}, {"IsFinished",0}
                };
                if (this.Id == 0)
                {
                    this.Id = crudRepository.Insert(TableName1, parameters);
                    var itemList = (BindingList<ReceiptItem>)gridControl1.DataSource;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        var item = itemList[i];
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "GrossWeight", item.GrossWeight }, { "NetWeight", item.NetWeight }, { "UnitPrice", item.UnitPrice }, { "Explanation", item.Explanation }, { "UUID", item.UUID }, { "RowAmount", item.RowAmount }, { "Vat", item.Vat }, { "TrackingNumber", item.TrackingNumber }, { "MeasurementUnit", item.MeasurementUnit }, { "ColorId", item.ColorId }, { "Wastage", item.Wastage }, { "Quantity", item.Quantity } };
                        var rec_id = crudRepository.Insert(TableName2, values);
                        gridView1.SetRowCellValue(i, "ReceiptItemId", rec_id);
                    }
                    bildirim.Basarili();
                }
                else
                {
                    parameters.Add("UpdatedDate", DateTime.Now);
                    parameters.Add("UpdatedUser", CurrentUser.UserId);
                    crudRepository.Update(TableName1, Id, parameters);
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        var recIdObj = gridView1.GetRowCellValue(i, "ReceiptItemId");
                        int rec_id = recIdObj != null ? Convert.ToInt32(recIdObj) : 0;
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", gridView1.GetRowCellValue(i, "OperationType") }, { "InventoryId", Convert.ToInt32(gridView1.GetRowCellValue(i, "InventoryId")) }, { "GrossWeight", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "GrossWeight").ToString()) }, { "NetWeight", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "NetWeight").ToString()) }, { "UnitPrice", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "UnitPrice").ToString()) }, { "RowAmount", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "RowAmount").ToString()) }, { "Vat", Convert.ToInt32(gridView1.GetRowCellValue(i, "Vat")) }, { "UUID", gridView1.GetRowCellValue(i, "UUID") }, { "Explanation", gridView1.GetRowCellValue(i, "Explanation") }, { "MeasurementUnit", gridView1.GetRowCellValue(i, "MeasurementUnit") }, { "ColorId", Convert.ToInt32(gridView1.GetRowCellValue(i, "ColorId")) }, { "Wastage", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "Wastage").ToString()) }, { "Quantity", Convert.ToInt32(gridView1.GetRowCellValue(i, "Quantity")) } };
                        if (rec_id != 0)
                        {
                            crudRepository.Update(TableName2, rec_id, values);
                        }
                        else
                        {
                            var new_rec_id = crudRepository.Insert(TableName2, values);
                            gridView1.SetRowCellValue(i, "ReceiptItemId", new_rec_id);
                        }
                    }
                    bildirim.GuncellemeBasarili();
                }
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Hata : " + ex.Message);
            }
        }

        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }
        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1, this.Text);
        }
        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);

        }
        private void repoBoyaRenkKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.BoyahaneRenkBilgileriYansit(gridView1, "Kumaş");
        }
        private void btnYeni_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }
        void FormTemizle()
        {
            txtTalimatNo.Text = numarator.NumaraVer("Fiş", Convert.ToInt32(ReceiptTypes.KumasSatinAlmaTalimati));
            object[] bilgiler = { dateTarih, txtFirmaKodu, txtFirmaUnvan, rchAciklama, txtYetkili, txtVade, comboBoxEdit1 };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            this.Id = 0;
            this.FirmaId = 0;
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            LstIslemListesi frm = new LstIslemListesi(Convert.ToInt32(ReceiptTypes.KumasSatinAlmaTalimati));
            frm.ShowDialog();
            DateTime tarih = DateTime.MinValue;
            if (frm.liste.Count > 0)
            {
                yardimciAraclar.ClearGridViewRows(gridView1);
                var firstItem = frm.liste[0];
                var values1 = firstItem.Split(';');
                this.Id = Convert.ToInt32(values1[6]);
                tarih = Convert.ToDateTime(values1[8]);
                dateTarih.EditValue = (DateTime)tarih;
                this.FirmaId = Convert.ToInt32(values1[9]);
                txtFirmaKodu.Text = values1[10];
                txtFirmaUnvan.Text = values1[11];
                //dateFaturaTarihi.EditValue = (DateTime)Convert.ToDateTime(values1[12]);
                //txtFaturaNo.Text = values1[13];
                //dateIrsaliyeTarihi.EditValue = (DateTime)Convert.ToDateTime(values1[14]);
                //txtIrsaliyeNo.Text = values1[15];
                rchAciklama.Text = values1[16];
                txtTalimatNo.Text = values1[22];
                txtYetkili.Text = values1[26];
                txtVade.Text = values1[27];
                comboBoxEdit1.Text = values1[28];
                //Onayli = Convert.ToBoolean(values1[29]); // sağ click ile değiştirmek için
               // chckOnayli.Checked = Convert.ToBoolean(values1[29]);
                //depo eklenecek
                foreach (var item in frm.liste)
                {
                    gridView1.AddNewRow();
                    int newRowHandle = gridView1.FocusedRowHandle;
                    var values = item.Split(';');
                    gridView1.SetRowCellValue(newRowHandle, "InventoryCode", values[0]);
                    gridView1.SetRowCellValue(newRowHandle, "InventoryName", values[1]);
                    gridView1.SetRowCellValue(newRowHandle, "Piece", values[2]);
                    gridView1.SetRowCellValue(newRowHandle, "OperationType", values[3]);
                    gridView1.SetRowCellValue(newRowHandle, "UUID", values[4]);
                    gridView1.SetRowCellValue(newRowHandle, "InventoryId", values[5]);
                    gridView1.SetRowCellValue(newRowHandle, "UnitPrice", values[17]);
                    gridView1.SetRowCellValue(newRowHandle, "Vat", values[18]);
                    gridView1.SetRowCellValue(newRowHandle, "ReceiptItemId", values[19]);
                    gridView1.SetRowCellValue(newRowHandle, "Explanation", values[20]);
                    gridView1.SetRowCellValue(newRowHandle, "TrackingNumber", values[21]); // 22 receiptNo yukarıda
                    gridView1.SetRowCellValue(newRowHandle, "BrutWeight", values[23]);
                    gridView1.SetRowCellValue(newRowHandle, "NetWeight", values[24]);
                    gridView1.SetRowCellValue(newRowHandle, "MeasurementUnit", values[25]);
                    gridView1.SetRowCellValue(newRowHandle, "ColorId", values[38]);
                    gridView1.SetRowCellValue(newRowHandle, "ColorCode", values[39]);
                    gridView1.SetRowCellValue(newRowHandle, "ColorName", values[40]);
                }
            }
        
        /*
         FrmIplikDepoListe frm = new FrmIplikDepoListe(ReceiptType);
        frm.ShowDialog();
        DateTime tarih = DateTime.MinValue;
        if (frm.liste.Count > 0)
        {
            yardimciAraclar.ClearGridViewRows(gridView1);
            var firstItem = frm.liste[0];
            var values1 = firstItem.Split(';');
            this.Id = Convert.ToInt32(values1[6]);
            tarih = Convert.ToDateTime(values1[8]);
            dateTarih.EditValue = (DateTime)tarih;
            this.FirmaId = Convert.ToInt32(values1[9]);
            txtFirmaKodu.Text = values1[10];
            txtFirmaUnvan.Text = values1[11];
            //dateFaturaTarihi.EditValue = (DateTime)Convert.ToDateTime(values1[12]);
            //txtFaturaNo.Text = values1[13];
            //dateIrsaliyeTarihi.EditValue = (DateTime)Convert.ToDateTime(values1[14]);
            //txtIrsaliyeNo.Text = values1[15];
            rchAciklama.Text = values1[16];
            txtTalimatNo.Text = values1[22];
            txtYetkili.Text = values1[26];
            txtVade.Text = values1[27];
            comboBoxEdit1.Text = values1[28];
            Onayli = Convert.ToBoolean(values1[29]); // sağ click ile değiştirmek için
            chckOnayli.Checked = Convert.ToBoolean(values1[29]);
            //depo eklenecek
            foreach (var item in frm.liste)
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;
                var values = item.Split(';');
                gridView1.SetRowCellValue(newRowHandle, "InventoryCode", values[0]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryName", values[1]);
                gridView1.SetRowCellValue(newRowHandle, "Piece", values[2]);
                gridView1.SetRowCellValue(newRowHandle, "OperationType", values[3]);
                gridView1.SetRowCellValue(newRowHandle, "UUID", values[4]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryId", values[5]);
                gridView1.SetRowCellValue(newRowHandle, "UnitPrice", values[17]);
                gridView1.SetRowCellValue(newRowHandle, "Vat", values[18]);
                gridView1.SetRowCellValue(newRowHandle, "ReceiptItemId", values[19]);
                gridView1.SetRowCellValue(newRowHandle, "Explanation", values[20]);
                gridView1.SetRowCellValue(newRowHandle, "TrackingNumber", values[21]); // 22 receiptNo yukarıda
                gridView1.SetRowCellValue(newRowHandle, "BrutWeight", values[23]);
                gridView1.SetRowCellValue(newRowHandle, "NetWeight", values[24]);
                gridView1.SetRowCellValue(newRowHandle, "MeasurementUnit", values[25]);
                gridView1.SetRowCellValue(newRowHandle, "ColorId", values[38]);
                gridView1.SetRowCellValue(newRowHandle, "ColorCode", values[39]);
                gridView1.SetRowCellValue(newRowHandle, "ColorName", values[40]);
            }
        }
    }
         */


        //HamDepo.FrmHamDepoListe frm = new HamDepo.FrmHamDepoListe("SaTal");
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
        //    string[] columnNames = yansit.SorgudakiKolonIsimleriniAl(frm.sql);
        //    yardimciAraclar.ListedenGrideYansit(gridControl1, columnNames, frm.veriler);
        //}
    }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            view.SetRowCellValue(e.RowHandle, "ReceiptItemId", 0);

        }

        private void talimatFormuToolStripMenuItem_Click(object sender, EventArgs e)
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
                        sql = "SELECT MAX(HamDepo1.Id) FROM HamDepo1 INNER JOIN HamDepo2 ON HamDepo1.Id = HamDepo2.RefNo WHERE HamDepo1.Id < @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = "SELECT MIN(HamDepo1.Id) FROM HamDepo1 INNER JOIN HamDepo2 ON HamDepo1.Id = HamDepo2.RefNo WHERE HamDepo1.Id > @Id";
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
                                        FROM HamDepo1 ID inner join FirmaKarti FK on FK.Id = ID.FirmaId WHERE ID.Id=  @Id and ID.IslemCinsi = 'SaTal'";
                    var fis = connection.QueryFirstOrDefault(fisquery, new { Id = istenenId });
                    string kalemquery = @"select
	                                    ISNULL(D2.Id,0) TakipNo,ISNULL(D2.RefNo,0) RefNo,ISNULL(D2.KalemIslem,'') KalemIslem,
	                                    ISNULL(D2.KumasId,0) KumasId,ISNULL(IK.UrunKodu,'') KumasKodu,ISNULL(IK.UrunAdi,'') KumasAdi,
	                                    ISNULL(D2.BrutKg,0) BrutKg,ISNULL(D2.NetKg,0) NetKg,ISNULL(D2.Fiyat,0) Fiyat,
	                                    ISNULL(D2.DovizCinsi,'') DovizCinsi,ISNULL(D2.RenkId,0) RenkId,
	                                    ISNULL(BRK.BoyahaneRenkKodu,'') BoyahaneRenkKodu,ISNULL(BRK.BoyahaneRenkAdi,'') BoyahaneRenkAdi,
	                                    ISNULL(D2.PartiNo,'') PartiNo,ISNULL(D2.Aciklama,'') SatirAciklama,ISNULL(D2.Barkod,'') Barkod,
	                                    ISNULL(D2.UUID,'') UUID,ISNULL(D2.SatirTutari,0) SatirTutari
                                    from HamDepo2 D2 left join UrunKarti IK on IK.Id = D2.KumasId
                                    left join BoyahaneRenkKartlari BRK on D2.RenkId = BRK.Id
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

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            GridView gridView = sender as GridView;
            if (e.KeyCode == Keys.Delete)
            {
                cRUD.SatirSil(gridView, "HamDepo2");
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        void DeleteRows()
        {
            crudRepository.DeleteRows(TableName2, this.Id);
            FormTemizle();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(TableName1, Id, DeleteRows);
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.MalzemeBilgileriniGrideYansit(gridView1,InventoryTypes.Kumas);
        }

    }
}