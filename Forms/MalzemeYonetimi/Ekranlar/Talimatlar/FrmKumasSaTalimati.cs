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

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.Talimatlar
{
    public partial class FrmKumasSaTalimati : DevExpress.XtraEditors.XtraForm
    {
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        Numarator numarator = new Numarator();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Bildirim bildirim = new Bildirim();
        CRUD_Operations cRUD = new CRUD_Operations();
        int FirmaId = 0, Id = 0;
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
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<_KumasDepoKalem>();
            txtTalimatNo.Text = numarator.NumaraVer("HamKSaTal");
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            object Baslik = new
            {
                IslemCinsi = "SaTal",
                TalimatNo = txtTalimatNo.Text,
                Tarih = dateTarih.EditValue,
                FirmaId = this.FirmaId,
                Aciklama = rchAciklama.Text,
                Yetkili = txtYetkili.Text,
                Vade = txtVade.Text,
                OdemeSekli = comboBoxEdit1.Text,
                Id = this.Id,
            };
            if (this.Id == 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = @"INSERT INTO HamDepo1 (IslemCinsi,TalimatNo,Tarih,FirmaId,Aciklama,Yetkili,Vade,OdemeSekli) OUTPUT INSERTED.Id
                                        VALUES (@IslemCinsi,@TalimatNo,@Tarih,@FirmaId,@Aciklama,@Yetkili,@Vade,@OdemeSekli)";
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        this.Id = connection.QuerySingle<int>(mssql, Baslik);
                    }
                    else
                    {
                        //connection.Execute(sqlite, FisBaslik);
                        //this.Id = connection.QuerySingle<int>(idQuery);
                    }
                    string sql = @"INSERT INTO HamDepo2 (RefNo,KalemIslem,KumasId,GrM2,BrutKg,NetKg,BrutMt,NetMt,Adet,Fiyat,FiyatBirimi,DovizCinsi,RenkId,Aciklama,UUID,SatirTutari,TakipNo,DesenId,BoyaIslemId) OUTPUT INSERTED.Id
                                     VALUES (@RefNo,@KalemIslem,@KumasId,@GrM2,@BrutKg,@NetKg,@BrutMt,@NetMt,@Adet,@Fiyat,@FiyatBirimi,@DovizCinsi,@RenkId,@Aciklama,@UUID,@SatirTutari,@TakipNo,@DesenId,@BoyaIslemId)";
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        connection.Execute(sql, new
                        {
                            RefNo = this.Id,
                            KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                            KumasId = gridView1.GetRowCellValue(i, "KumasId"),
                            GrM2 = gridView1.GetRowCellValue(i, "GrM2"),
                            BrutKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "BrutKg")),
                            NetKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "NetKg")),
                            BrutMt = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "BrutMt")),
                            NetMt = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "NetMt")),
                            Adet = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "Adet")),
                            Fiyat = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "Fiyat")),
                            FiyatBirimi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "FiyatBirimi")),
                            DovizCinsi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "DovizCinsi")),
                            RenkId = gridView1.GetRowCellValue(i, "RenkId"),
                            Aciklama = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Aciklama")),
                            UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                            SatirTutari = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "SatirTutari")),
                            TakipNo = gridView1.GetRowCellValue(i, "TakipNo"),
                            DesenId = gridView1.GetRowCellValue(i, "DesenId"),
                            BoyaIslemId = gridView1.GetRowCellValue(i, "BoyaIslemId"),
                        });
                    }
                    bildirim.Basarili();
                }
            }
        }

        private void txtFirmaKodu_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            yansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);

        }

        private void repoBoyaRenkKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int newRowHandle = gridView1.FocusedRowHandle;
            yansit.BoyahaneRenkBilgileriYansit(gridView1, newRowHandle);
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int newRowHandle = gridView1.FocusedRowHandle;
            yansit.KumasBilgileriYansit(gridView1,newRowHandle);
        }

    }
}