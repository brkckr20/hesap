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
                        var d2Id = connection.ExecuteScalar<int>(sql, new
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
                            FiyatBirimi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "FiyatBirim")),
                            DovizCinsi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "DovizCinsi")),
                            RenkId = gridView1.GetRowCellValue(i, "RenkId"),
                            Aciklama = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Aciklama")),
                            UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                            SatirTutari = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "SatirTutari")),
                            TakipNo = gridView1.GetRowCellValue(i, "TakipNo"),
                            DesenId = gridView1.GetRowCellValue(i, "DesenId"),
                            BoyaIslemId = gridView1.GetRowCellValue(i, "BoyaIslemId"),
                        });
                        gridView1.SetRowCellValue(i, "D2Id", d2Id);
                    }
                    bildirim.Basarili();
                }
            }
            else
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    var query = @"select Id from HamDepo1 where Id = @Id";
                    var sonuc = connection.QueryFirstOrDefault<int>(query, new { Id = this.Id });
                    if (sonuc != 0)
                    {
                        string updateD1 = @"UPDATE HamDepo1 SET TalimatNo = @TalimatNo,Tarih=@Tarih,FirmaId = @FirmaId,
                                            Aciklama = @Aciklama, Yetkili=@Yetkili, Vade=@Vade, OdemeSekli = @OdemeSekli WHERE Id = @Id";

                        connection.Execute(updateD1, Baslik);
                        for (int i = 0; i < gridView1.RowCount - 1; i++)
                        {
                            int d2id = Convert.ToInt32(gridView1.GetRowCellValue(i, "Id"));
                            var sqlKalem = @"select * from HamDepo2 where Id= @Id";
                            var kalem = connection.QueryFirstOrDefault(sqlKalem, new { Id = d2id });
                            if (kalem != null)
                            {
                                string updateKalem = @"UPDATE HamDepo2 SET KalemIslem = @KalemIslem,KumasId = @KumasId,GrM2=@GrM2,BrutKg = @BrutKg,NetKg = @NetKg,BrutMt=@BrutMt,NetMt=@NetMt,Adet=@Adet
                                                ,Fiyat = @Fiyat,FiyatBirimi=@FiyatBirimi,DovizCinsi = @DovizCinsi,RenkId=@RenkId,Aciklama = @Aciklama,
                                                UUID = @UUID ,SatirTutari = @SatirTutari,DesenId=@DesenId,BoyaIslemId=@BoyaIslemId WHERE Id = @Id";
                                connection.Execute(updateKalem, new
                                {
                                    Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id")),
                                    KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                                    KumasId = gridView1.GetRowCellValue(i, "KumasId"),
                                    GrM2 = gridView1.GetRowCellValue(i, "GrM2"),
                                    BrutKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "BrutKg")),
                                    NetKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "NetKg")),
                                    BrutMt = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "BrutMt")),
                                    NetMt = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "NetMt")),
                                    Adet = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "Adet")),
                                    Fiyat = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "Fiyat")),
                                    FiyatBirimi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "FiyatBirim")),
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
                            else
                            {
                                string sql = @"INSERT INTO HamDepo2 (RefNo,KalemIslem,KumasId,GrM2,BrutKg,NetKg,BrutMt,NetMt,Adet,Fiyat,FiyatBirimi,DovizCinsi,RenkId,Aciklama,UUID,SatirTutari,TakipNo,DesenId,BoyaIslemId) OUTPUT INSERTED.Id
                                     VALUES (@RefNo,@KalemIslem,@KumasId,@GrM2,@BrutKg,@NetKg,@BrutMt,@NetMt,@Adet,@Fiyat,@FiyatBirimi,@DovizCinsi,@RenkId,@Aciklama,@UUID,@SatirTutari,@TakipNo,@DesenId,@BoyaIslemId)";
                                int yeniId = connection.ExecuteScalar<int>(sql, new
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
                                    FiyatBirimi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "FiyatBirim")),
                                    DovizCinsi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "DovizCinsi")),
                                    RenkId = gridView1.GetRowCellValue(i, "RenkId"),
                                    Aciklama = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Aciklama")),
                                    UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                                    SatirTutari = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "SatirTutari")),
                                    TakipNo = gridView1.GetRowCellValue(i, "TakipNo"),
                                    DesenId = gridView1.GetRowCellValue(i, "DesenId"),
                                    BoyaIslemId = gridView1.GetRowCellValue(i, "BoyaIslemId"),
                                });
                                gridView1.SetRowCellValue(i, "D2Id", yeniId);
                            }
                        }
                        bildirim.GuncellemeBasarili();
                    }
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
        private void btnYeni_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }
        void FormTemizle()
        {
            txtTalimatNo.Text = numarator.NumaraVer("HamKSaTal");
            object[] bilgiler = { dateTarih, txtFirmaKodu, txtFirmaUnvan, rchAciklama, txtYetkili, txtVade, comboBoxEdit1 };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<_IplikDepoKalem>();
            this.Id = 0;
            this.FirmaId = 0;
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            HamDepo.FrmHamDepoListe frm = new HamDepo.FrmHamDepoListe("SaTal");
            frm.ShowDialog();
            /*
             FrmIplikDepoListe frm = new FrmIplikDepoListe("SaTal");
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                this.Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                dateTarih.EditValue = (DateTime)frm.veriler[0]["Tarih"];
                txtFirmaKodu.Text = frm.veriler[0]["FirmaKodu"].ToString();
                txtFirmaUnvan.Text = frm.veriler[0]["FirmaUnvan"].ToString();
                this.FirmaId = Convert.ToInt32(frm.veriler[0]["FirmaId"]);
                rchAciklama.Text = frm.veriler[0]["Aciklama"].ToString();
                txtTalimatNo.Text = frm.veriler[0]["TalimatNo"].ToString();
                txtYetkili.Text = frm.veriler[0]["Yetkili"].ToString();
                txtVade.Text = frm.veriler[0]["Vade"].ToString();
                comboBoxEdit1.Text = frm.veriler[0]["OdemeSekli"].ToString();
                string[] columnNames = new string[]
                {
                    "KalemIslem", "IplikId", "IplikKodu", "IplikAdi", "BrutKg", "NetKg","Fiyat","DovizCinsi","OrganikSertifikaNo","Marka","IplikRenkId",
                    "IplikRenkKodu","IplikRenkAdi","SatirAciklama","SatirTutari","TakipNo"
                };
                yardimciAraclar.ListedenGrideYansit(gridControl1, columnNames, frm.veriler);
            }
             
             */
        }

        private void repoBtnUrunKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int newRowHandle = gridView1.FocusedRowHandle;
            yansit.KumasBilgileriYansit(gridView1,newRowHandle);
        }

    }
}