﻿using Hesap.DataAccess;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.HamDepo
{
    public partial class FrmHamDepoCikis : DevExpress.XtraEditors.XtraForm
    {
        public FrmHamDepoCikis()
        {
            InitializeComponent();
        }
        int Id = 0, FirmaId = 0, TalimatId = 0, TasiyiciId = 0;
        CRUD_Operations cRUD = new CRUD_Operations();
        CrudRepository crudRepository = new CrudRepository();
        Metotlar metotlar = new Metotlar();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }
        //gridcontrol üzerindeki field isimlerinden devam edilecek - 27.06.2025 
        private void FrmHamDepoCikis_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crudRepository.SaveColumnStatus(gridView1, this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void btnAciklamaGetir_Click(object sender, EventArgs e)
        {
            //Liste.FrmAciklamaListesi frm = new Liste.FrmAciklamaListesi(0);
            //frm.ShowDialog();
            //rchAciklama.EditValue = frm.Aciklama;
        }

        private void btnStokSecimi_Click(object sender, EventArgs e)
        {
            FrmHamDepoStok frm = new FrmHamDepoStok();
            frm.ShowDialog();

            foreach (var item in frm.stokListesi)
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;
                var values = item.Split(';');
                gridView1.SetRowCellValue(newRowHandle, "KumasKodu", values[0]);
                gridView1.SetRowCellValue(newRowHandle, "KumasAdi", values[1]);
                gridView1.SetRowCellValue(newRowHandle, "GrM2", values[2]);
                gridView1.SetRowCellValue(newRowHandle, "BrutKg", values[3]);
                gridView1.SetRowCellValue(newRowHandle, "NetKg", values[4]);
                gridView1.SetRowCellValue(newRowHandle, "NetMt", values[5]);
                gridView1.SetRowCellValue(newRowHandle, "BoyahaneRenkId", values[6]);
                gridView1.SetRowCellValue(newRowHandle, "BoyahaneRenkKodu", values[7]);
                gridView1.SetRowCellValue(newRowHandle, "BoyahaneRenkAdi", values[8]);
                gridView1.SetRowCellValue(newRowHandle, "TakipNo", values[9]);
                gridView1.SetRowCellValue(newRowHandle, "KumasId", values[10]);
            }
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int newRowHandle = gridView1.FocusedRowHandle;
            yansit.MalzemeBilgileriniGrideYansit(gridView1, InventoryTypes.Kumas);
        }

        private void txtNakliyeci_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.TasiyiciBilgileriYansit(txtUnvan, txtAd, txtSoyad, txtTC, txtPlaka, txtDorse, txtNakliyeci, ref this.TasiyiciId);
        }

        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateIrsaliyeTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<ReceiptItem>();
            crudRepository.GetUserColumns(gridView1, this.Text);
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //    var parameters = new Dictionary<string, object>
            //    {
            //        { "IslemCinsi", "Çıkış" },
            //        { "Tarih", dateTarih.EditValue },
            //        { "IrsaliyeTarihi", dateIrsaliyeTarihi.EditValue },
            //        { "IrsaliyeNo", txtIrsaliyeNo.Text },
            //        { "FirmaId", this.FirmaId },
            //        { "Aciklama", rchAciklama.Text },
            //        { "TasiyiciId", txtNakliyeci.Text },
            //        { "TalimatId", TalimatId },

            //    };
            //    if (this.Id == 0)
            //    {
            //        this.Id = cRUD.InsertRecord("HamDepo1", parameters);
            //        txtKayitNo.Text = this.Id.ToString();
            //        for (int i = 0; i < gridView1.RowCount - 1; i++)
            //        {
            //            var kalemParameters = metotlar.CreateHameDepo2KalemParameters(i, this.Id, gridView1);
            //            var d2Id = cRUD.InsertRecord("HamDepo2", kalemParameters);
            //            gridView1.SetRowCellValue(i, "D2Id", d2Id);
            //        }
            //        bildirim.Basarili();
            //    }
            //    else
            //    {
            //        cRUD.UpdateRecord("HamDepo1", parameters, this.Id);
            //        for (int i = 0; i < gridView1.RowCount - 1; i++)
            //        {
            //            var d2Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id"));
            //            var kalemParameters = metotlar.CreateHameDepo2KalemParameters(i, this.Id, gridView1);
            //            if (d2Id > 0)
            //            {
            //                cRUD.UpdateRecord("HamDepo2", kalemParameters, d2Id);
            //            }
            //            else
            //            {
            //                var yeniId = cRUD.InsertRecord("HamDepo2", kalemParameters);
            //                gridView1.SetRowCellValue(i, "D2Id", yeniId);
            //            }
            //        }
            //        bildirim.GuncellemeBasarili();
            //    }
            //}
        }
    }
}