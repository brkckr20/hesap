using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.Context;
using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.IplikDepo
{
    public partial class FrmIplikCikis : XtraForm
    {
        HesaplaVeYansit hesaplaVeYansit = new HesaplaVeYansit();
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CRUD_Operations cRUD = new CRUD_Operations();
        CrudRepository crudRepository = new CrudRepository();
        int ReceiptType = Convert.ToInt32(ReceiptTypes.IplikDepoCikis);
        private readonly string TableName1 = "Receipt", TableName2 = "ReceiptItem";

        int TasiyiciId, Id, FirmaId,AciklamaId;
        public FrmIplikCikis()
        {
            InitializeComponent();
        }


        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateIrsaliyeTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            crudRepository.GetUserColumns(gridView1, this.Text);
        }
        private void FrmIplikCikis_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }

        private void btnAciklamaGetir_Click(object sender, EventArgs e)
        {
            Liste.FrmAciklamaListesi frm = new Liste.FrmAciklamaListesi(ReceiptType);
            frm.ShowDialog();
            rchAciklama.EditValue = frm.Aciklama;
        }

        void Kaydet()
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
                    { "ReceiptType", ReceiptType }, { "ReceiptDate", dateTarih.EditValue }, { "CompanyId", this.FirmaId },{ "Explanation", rchAciklama.Text }, { "DispatchDate", dateIrsaliyeTarihi.EditValue},{ "DispatchNo", txtIrsaliyeNo.Text },{ "CarrierId", TasiyiciId}
                };
                if (this.Id == 0)
                {
                    this.Id = crudRepository.Insert(TableName1, parameters);
                    var itemList = (BindingList<ReceiptItem>)gridControl1.DataSource;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        var item = itemList[i];
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", item.OperationType }, { "InventoryId", item.InventoryId }, { "GrossWeight", item.GrossWeight }, { "NetWeight", item.NetWeight }, { "UnitPrice", item.UnitPrice }, { "Explanation", item.Explanation }, { "UUID", item.UUID }, { "RowAmount", item.RowAmount }, { "Vat", item.Vat }, { "TrackingNumber", item.TrackingNumber }, { "MeasurementUnit", item.MeasurementUnit }, { "Brand", item.Brand }, { "ReceiptNo", item.ReceiptNo } };
                        var rec_id = crudRepository.Insert(TableName2, values);
                        gridView1.SetRowCellValue(i, "ReceiptItemId", rec_id);
                    }
                    bildirim.Basarili();
                }
                else
                {
                    crudRepository.Update(TableName1, Id, parameters);
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        var recIdObj = gridView1.GetRowCellValue(i, "ReceiptItemId");
                        int rec_id = recIdObj != null ? Convert.ToInt32(recIdObj) : 0;
                        var values = new Dictionary<string, object> { { "ReceiptId", this.Id }, { "OperationType", gridView1.GetRowCellValue(i, "OperationType") }, { "InventoryId", Convert.ToInt32(gridView1.GetRowCellValue(i, "InventoryId")) }, { "GrossWeight", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "GrossWeight").ToString()) }, { "NetWeight", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "NetWeight").ToString()) }, { "UnitPrice", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "UnitPrice").ToString()) }, { "RowAmount", yardimciAraclar.ConvertDecimal(gridView1.GetRowCellValue(i, "RowAmount").ToString()) }, { "Vat", Convert.ToInt32(gridView1.GetRowCellValue(i, "Vat")) }, { "UUID", gridView1.GetRowCellValue(i, "UUID") }, { "Explanation", gridView1.GetRowCellValue(i, "Explanation") }, { "MeasurementUnit", gridView1.GetRowCellValue(i, "MeasurementUnit") }, { "Brand", gridView1.GetRowCellValue(i, "Brand") }, { "ReceiptNo", gridView1.GetRowCellValue(i, "ReceiptNo") } };
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

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //Liste.FrmIplikKartiListesi frm = new Liste.FrmIplikKartiListesi();
            //frm.ShowDialog();
            //if (!string.IsNullOrEmpty(frm.IplikKodu))
            //{
            //    int newRowHandle = gridView1.FocusedRowHandle;
            //    gridView1.SetRowCellValue(newRowHandle, "IplikKodu", frm.IplikKodu);
            //    gridView1.SetRowCellValue(newRowHandle, "IplikAdi", frm.IplikAdi);
            //    gridView1.SetRowCellValue(newRowHandle, "IplikId", frm.Id);
            //}
        }
        private void repoBoyaRenkKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //Liste.FrmBoyahaneRenkKartlariListesi frm = new Liste.FrmBoyahaneRenkKartlariListesi();
            //frm.ShowDialog();
            //if (frm.veriler != null)
            //{
            //    int newRowHandle = gridView1.FocusedRowHandle;
            //    gridView1.SetRowCellValue(newRowHandle, "IplikRenkId", Convert.ToInt32(frm.veriler[0]["Id"]));
            //    gridView1.SetRowCellValue(newRowHandle, "IplikRenkKodu", frm.veriler[0]["BoyahaneRenkKodu"].ToString());
            //    gridView1.SetRowCellValue(newRowHandle, "IplikRenkAdi", frm.veriler[0]["BoyahaneRenkAdi"].ToString());
            //}
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

        private void btnListe_Click(object sender, EventArgs e)
        {
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
                dateIrsaliyeTarihi.EditValue = (DateTime)Convert.ToDateTime(values1[14]);
                txtIrsaliyeNo.Text = values1[15];
                rchAciklama.Text = values1[16];
                //txtTalimatNo.Text = values1[22];
                //txtYetkili.Text = values1[26];
                //txtVade.Text = values1[27];
                //comboBoxEdit1.Text = values1[28];
                //Onayli = Convert.ToBoolean(values1[29]); // sağ click ile değiştirmek için
                //chckOnayli.Checked = Convert.ToBoolean(values1[29]);
                //depo eklenecek
                TasiyiciId = Convert.ToInt32(values1[31]);
                txtTasiyiciId.Text = TasiyiciId.ToString();
                txtTasiyiciUnvan.Text = values1[32];
                txtTasiyiciAd.Text = values1[33];
                txtTasiyiciSoyad.Text = values1[34];
                txtTasiyiciPlaka.Text = values1[35];
                txtTasiyiciDorse.Text = values1[36];
                txtTasiyiciTC.Text = values1[37];
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
                    gridView1.SetRowCellValue(newRowHandle, "GrossWeight", values[23]);
                    gridView1.SetRowCellValue(newRowHandle, "NetWeight", values[24]);
                    gridView1.SetRowCellValue(newRowHandle, "MeasurementUnit", values[25]);
                    gridView1.SetRowCellValue(newRowHandle, "ReceiptNo", values[30]); // 31 tasiyici id yukarida
                }
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            //string uuid = Guid.NewGuid().ToString();
            view.SetRowCellValue(e.RowHandle, "TakipNo", 0);
            view.SetRowCellValue(e.RowHandle, "D2Id", 0);
            //view.SetRowCellValue(e.RowHandle, "UUID", uuid);
        }
        void DeleteRows()
        {
            crudRepository.DeleteRows(TableName2, this.Id);
            FormTemizle();
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            crudRepository.ConfirmAndDeleteCard(TableName1, Id, DeleteRows);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }
        void FormTemizle()
        {
            object[] bilgiler = { dateTarih, dateIrsaliyeTarihi, txtIrsaliyeNo, txtFirmaKodu, txtFirmaUnvan, rchAciklama,txtTasiyiciId,
            txtTasiyiciAd,txtTasiyiciSoyad,txtTasiyiciTC,txtTasiyiciPlaka,txtTasiyiciDorse,txtTasiyiciUnvan };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<_IplikDepoKalem>();
            this.Id = 0;
            this.FirmaId = 0;
            this.TasiyiciId = 0;
        }

        private void btnIplikDepoStok_Click(object sender, EventArgs e)
        {
            FrmIplikDepoStok frm = new FrmIplikDepoStok();
            frm.ShowDialog();
            #region açıklama
            //if (frm.stokListesi.Count > 0)
            //{
            //    var parts = frm.stokListesi[0].Split(';');
            //    this.FirmaId = Convert.ToInt32(parts[0]);
            //    txtFirmaKodu.Text = parts[1].ToString();
            //    txtFirmaUnvan.Text = parts[2].ToString();
            //}
            #endregion

            foreach (var item in frm.stokListesi)
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;
                var values = item.Split(';');
                gridView1.SetRowCellValue(newRowHandle, "InventoryCode", values[1]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryId", values[2]);
                gridView1.SetRowCellValue(newRowHandle, "InventoryName", values[3]);
                //gridView1.SetRowCellValue(newRowHandle, "Organik", values[4]);
                gridView1.SetRowCellValue(newRowHandle, "Brand", values[5]);
                //gridView1.SetRowCellValue(newRowHandle, "PartiNo", values[6]);
                //gridView1.SetRowCellValue(newRowHandle, "IplikRenkId", values[7]);
                //gridView1.SetRowCellValue(newRowHandle, "IplikRenkKodu", values[8]);
                //gridView1.SetRowCellValue(newRowHandle, "IplikRenkAdi", values[9]);
                gridView1.SetRowCellValue(newRowHandle, "TrackingNumber", values[11]);
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
                        sql = "SELECT MAX(IplikDepo1.Id) FROM IplikDepo1 INNER JOIN IplikDepo2 ON IplikDepo1.Id = IplikDepo2.RefNo WHERE IplikDepo1.Id < @Id and IplikDepo1.IslemCinsi = 'Çıkış'";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = "SELECT MIN(IplikDepo1.Id) FROM IplikDepo1 INNER JOIN IplikDepo2 ON IplikDepo1.Id = IplikDepo2.RefNo WHERE IplikDepo1.Id > @Id and IplikDepo1.IslemCinsi = 'Çıkış'";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    string fisquery = @"SELECT 
                                        ISNULL(ID.Id,0) Id,
                                        ISNULL(ID.Tarih,'') Tarih,
                                        ISNULL(ID.IrsaliyeTarihi,'') IrsaliyeTarihi,
                                        ISNULL(ID.IrsaliyeNo,'') IrsaliyeNo,
                                        ISNULL(ID.FirmaId,'') FirmaId,
                                        ISNULL(FK.FirmaKodu,'') FirmaKodu,
                                        ISNULL(FK.FirmaUnvan,'') FirmaUnvan,
                                        ISNULL(ID.Aciklama,'') Aciklama,
                                        ISNULL(ID.IslemCinsi,'') IslemCinsi ,
										ISNULL(tk.Id,'') TasiyiciId,
										ISNULL(tk.Unvan,'') TasiyiciUnvan,
										ISNULL(tk.Ad,'') TasiyiciAd,
										ISNULL(tk.Soyad,'') TasiyiciSoyad,
										ISNULL(tk.TC,'') TasiyiciTC,
										ISNULL(tk.Plaka,'') Plaka,
										ISNULL(tk.Dorse,'') Dorse
										
										FROM IplikDepo1 ID inner join FirmaKarti FK on FK.Id = ID.FirmaId
										left join TasiyiciKarti tk on ID.TasiyiciId = tk.Id
										WHERE ID.Id= @Id";
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
                        dateIrsaliyeTarihi.EditValue = (DateTime)fis.IrsaliyeTarihi;
                        txtIrsaliyeNo.Text = fis.IrsaliyeNo.ToString();
                        this.FirmaId = Convert.ToInt32(fis.FirmaId);
                        txtFirmaKodu.Text = fis.FirmaKodu.ToString();
                        txtFirmaUnvan.Text = fis.FirmaUnvan.ToString();
                        rchAciklama.Text = fis.Aciklama.ToString();
                        txtTasiyiciId.Text = fis.TasiyiciId.ToString();
                        txtTasiyiciUnvan.Text = fis.TasiyiciUnvan.ToString();
                        txtTasiyiciAd.Text = fis.TasiyiciAd.ToString();
                        txtTasiyiciSoyad.Text = fis.TasiyiciSoyad.ToString();
                        txtTasiyiciTC.Text = fis.TasiyiciTC.ToString();
                        txtTasiyiciPlaka.Text = fis.Plaka.ToString();
                        txtTasiyiciDorse.Text = fis.Dorse.ToString();
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

        private void fişFormlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Id == 0)
            {
                bildirim.Uyari("Rapor alabilmek için bir kayıt seçmelisiniz!!!");
            }
            else
            {
                Rapor.FrmRaporSecimEkrani frm = new Rapor.FrmRaporSecimEkrani(this.Text, this.Id);
                frm.ShowDialog();
            }
        }

        private void eİrsaliyeOluşturVeGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmIrsaliyeFaturaGoruntuleyici frm = new FrmIrsaliyeFaturaGoruntuleyici();
            frm.ShowDialog();
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1, this.Text);

        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            hesaplaVeYansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }

        private void txtTasiyiciId_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            hesaplaVeYansit.TasiyiciBilgileriYansit(txtTasiyiciUnvan, txtTasiyiciAd, txtTasiyiciSoyad, txtTasiyiciTC, txtTasiyiciPlaka, txtTasiyiciDorse, txtTasiyiciId, ref this.TasiyiciId);
        }
    }
}