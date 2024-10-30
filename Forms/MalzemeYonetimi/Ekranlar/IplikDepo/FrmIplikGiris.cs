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

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.IplikDepo
{
    public partial class FrmIplikGiris : DevExpress.XtraEditors.XtraForm
    {
        public FrmIplikGiris()
        {
            InitializeComponent();
        }
        HesaplaVeYansit hesaplaVeYansit = new HesaplaVeYansit();
        Ayarlar ayarlar = new Ayarlar();
        Bildirim bildirim = new Bildirim();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public int Id, FirmaId;
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            hesaplaVeYansit.FirmaKoduVeAdiYansit(txtFirmaKodu, txtFirmaUnvan, ref this.FirmaId);
        }
        private void FrmIplikGiris_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            dateTarih.EditValue = DateTime.Now;
            dateIrsaliyeTarihi.EditValue = DateTime.Now;
            gridControl1.DataSource = new BindingList<_IplikDepoKalem>();
            gridView1.Columns["IplikRenkId"].Visible = false;
        }
        private void buttonEdit1_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //hesaplaVeYansit.DepoKoduVeAdiYansit(btnDepoKodu,lblDepoAdi);
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
        private void repoBoyaRenkKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmBoyahaneRenkKartlariListesi frm = new Liste.FrmBoyahaneRenkKartlariListesi();
            frm.ShowDialog();
            if (frm.veriler != null)
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkId", Convert.ToInt32(frm.veriler[0]["Id"]));
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkKodu", frm.veriler[0]["BoyahaneRenkKodu"].ToString());
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkAdi", frm.veriler[0]["BoyahaneRenkAdi"].ToString());
            }
        }
        private void repoBtnMarka_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmMarkaSecimListesi frm = new Liste.FrmMarkaSecimListesi("IplikDepo2");
            frm.ShowDialog();
            if (frm._marka != null)
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "Marka", frm._marka);
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FrmIplikDepoListe frm = new FrmIplikDepoListe("Giriş");
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                this.Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                dateTarih.EditValue = (DateTime)frm.veriler[0]["Tarih"];
                dateIrsaliyeTarihi.EditValue = (DateTime)frm.veriler[0]["IrsaliyeTarihi"];
                txtIrsaliyeNo.Text = frm.veriler[0]["IrsaliyeNo"].ToString();
                txtFirmaKodu.Text = frm.veriler[0]["FirmaKodu"].ToString();
                txtFirmaUnvan.Text = frm.veriler[0]["FirmaUnvan"].ToString();
                this.FirmaId = Convert.ToInt32(frm.veriler[0]["FirmaId"]);
                rchAciklama.Text = frm.veriler[0]["Aciklama"].ToString();
                string[] columnNames = new string[]
                {
                    "KalemIslem", "IplikId", "IplikKodu", "IplikAdi", "BrutKg", "NetKg","Fiyat","DovizFiyat","DovizCinsi","OrganikSertifikaNo","Marka","KullanimYeri","IplikRenkId",
                    "IplikRenkKodu","IplikRenkAdi","PartiNo","SatirAciklama","Barkod","TalimatNo","UUID","TakipNo","SatirTutari","D2Id",
                };
                yardimciAraclar.ListedenGrideYansit(gridControl1, columnNames, frm.veriler);
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
                        sql = "SELECT MAX(IplikDepo1.Id) FROM IplikDepo1 INNER JOIN IplikDepo2 ON IplikDepo1.Id = IplikDepo2.RefNo WHERE IplikDepo1.Id < @Id and IplikDepo1.IslemCinsi = 'Giriş'";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = "SELECT MIN(IplikDepo1.Id) FROM IplikDepo1 INNER JOIN IplikDepo2 ON IplikDepo1.Id = IplikDepo2.RefNo WHERE IplikDepo1.Id > @Id and IplikDepo1.IslemCinsi = 'Giriş'";
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
                                        ISNULL(ID.IslemCinsi,'') IslemCinsi FROM IplikDepo1 ID inner join FirmaKarti FK on FK.Id = ID.FirmaId WHERE ID.Id= @Id";
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

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = sender as GridView;
            string uuid = Guid.NewGuid().ToString();
            view.SetRowCellValue(e.RowHandle, "TakipNo", 0);
            view.SetRowCellValue(e.RowHandle, "UUID", uuid);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (bildirim.SilmeOnayı())
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string d1 = "delete from IplikDepo1 where Id = @Id";
                    string d2 = "delete from IplikDepo2 where RefNo = @Id";
                    connection.Execute(d1, new { Id = this.Id });
                    connection.Execute(d2, new { Id = this.Id });
                    bildirim.SilmeBasarili();
                    FormTemizle();
                }
            }
        }
        void FormTemizle()
        {
            object[] bilgiler = { dateTarih, dateIrsaliyeTarihi, txtIrsaliyeNo, txtFirmaKodu, txtFirmaUnvan, rchAciklama };
            yardimciAraclar.KartTemizle(bilgiler);
            gridControl1.DataSource = new BindingList<_IplikDepoKalem>();
            this.Id = 0;
            this.FirmaId = 0;
        }

        private void btnTalimatlar_Click(object sender, EventArgs e)
        {
            FrmTalimatlar frm = new FrmTalimatlar();
            frm.ShowDialog();
            if (frm.satinAlmaListesi.Count > 0)
            {
                var parts = frm.satinAlmaListesi[0].Split(';');
                this.FirmaId = Convert.ToInt32(parts[1]);
                txtFirmaKodu.Text = parts[2].ToString();
                txtFirmaUnvan.Text = parts[3].ToString();
            }
            foreach (var item in frm.satinAlmaListesi)
            {
                gridView1.AddNewRow();
                int newRowHandle = gridView1.FocusedRowHandle;
                var values = item.Split(';');
                gridView1.SetRowCellValue(newRowHandle, "KalemIslem", "Satın Alma");
                gridView1.SetRowCellValue(newRowHandle, "TakipNo", values[4]);
                gridView1.SetRowCellValue(newRowHandle, "IplikId", values[5]);
                gridView1.SetRowCellValue(newRowHandle, "IplikKodu", values[6]);
                gridView1.SetRowCellValue(newRowHandle, "IplikAdi", values[7]);
                gridView1.SetRowCellValue(newRowHandle, "BrutKg", values[8]);
                gridView1.SetRowCellValue(newRowHandle, "Fiyat", values[9]);
                gridView1.SetRowCellValue(newRowHandle, "DovizCinsi", values[10]);
                gridView1.SetRowCellValue(newRowHandle, "OrganikSertifikaNo", values[11]);
                gridView1.SetRowCellValue(newRowHandle, "Marka", values[12]);
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkId", values[13]);
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkKodu", values[14]);
                gridView1.SetRowCellValue(newRowHandle, "IplikRenkAdi", values[15]);
                gridView1.SetRowCellValue(newRowHandle, "TalimatNo", values[0]);
                gridView1.SetRowCellValue(newRowHandle, "NetKg", values[16]);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormTemizle();
        }

        private void btnFasonaGidenler_Click(object sender, EventArgs e)
        {
            FrmFasonaGönderilenler frm = new FrmFasonaGönderilenler(this.FirmaId);
            frm.ShowDialog();
            if (frm.listem != null)
            {
                foreach (var item in frm.listem)
                {
                    gridView1.AddNewRow();
                    int newRowHandle = gridView1.FocusedRowHandle;
                    var values = item.Split(';');
                    gridView1.SetRowCellValue(newRowHandle, "KalemIslem", values[16]);
                    gridView1.SetRowCellValue(newRowHandle, "TakipNo", values[3]);
                    gridView1.SetRowCellValue(newRowHandle, "IplikId", values[4]);
                    gridView1.SetRowCellValue(newRowHandle, "IplikKodu", values[5]);
                    gridView1.SetRowCellValue(newRowHandle, "IplikAdi", values[6]);
                    gridView1.SetRowCellValue(newRowHandle, "BrutKg", values[7]);
                    gridView1.SetRowCellValue(newRowHandle, "Fiyat", values[8]);
                    gridView1.SetRowCellValue(newRowHandle, "DovizCinsi", values[9]);
                    gridView1.SetRowCellValue(newRowHandle, "OrganikSertifikaNo", values[10]);
                    gridView1.SetRowCellValue(newRowHandle, "Marka", values[11]);
                    gridView1.SetRowCellValue(newRowHandle, "IplikRenkId", values[12]);
                    gridView1.SetRowCellValue(newRowHandle, "IplikRenkKodu", values[13]);
                    gridView1.SetRowCellValue(newRowHandle, "IplikRenkAdi", values[14]);
                    //gridView1.SetRowCellValue(newRowHandle, "TalimatNo", values[0]);
                    gridView1.SetRowCellValue(newRowHandle, "NetKg", values[15]);
                }
            }
            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            object Baslik = new
            {
                IslemCinsi = "Giriş",
                Tarih = dateTarih.EditValue,
                IrsaliyeTarihi = dateIrsaliyeTarihi.EditValue,
                IrsaliyeNo = txtIrsaliyeNo.Text,
                FirmaId = this.FirmaId,
                Aciklama = rchAciklama.Text,
                Id = this.Id,
            };
            if (this.Id == 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = @"INSERT INTO IplikDepo1 (Tarih,IrsaliyeTarihi,IrsaliyeNo,FirmaId,Aciklama,IslemCinsi) OUTPUT INSERTED.Id
                                        VALUES (@Tarih,@IrsaliyeTarihi,@IrsaliyeNo,@FirmaId,@Aciklama,@IslemCinsi)";
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        this.Id = connection.QuerySingle<int>(mssql, Baslik);
                    }
                    else
                    {
                        //connection.Execute(sqlite, FisBaslik);
                        //this.Id = connection.QuerySingle<int>(idQuery);
                    }
                    string sql = @"INSERT INTO IplikDepo2 (RefNo,KalemIslem,IplikId,NetKg,BrutKg,Fiyat,DovizCinsi,OrganikSertifikaNo,Marka,KullanimYeri,IplikRenkId,PartiNo
                                ,Aciklama,Barkod,TalimatNo,UUID,SatirTutari,TakipNo) OUTPUT INSERTED.Id
                                     VALUES (@RefNo,@KalemIslem,@IplikId,@NetKg,@BrutKg,@Fiyat,@DovizCinsi,@OrganikSertifikaNo,@Marka,@KullanimYeri,@IplikRenkId,@PartiNo
                                ,@Aciklama,@Barkod,@TalimatNo,@UUID,@SatirTutari,@TakipNo)";
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        int yeniId = connection.ExecuteScalar<int>(sql, new
                        {
                            RefNo = this.Id,
                            KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                            IplikId = gridView1.GetRowCellValue(i, "IplikId"),
                            NetKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "NetKg")),
                            BrutKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "BrutKg")),
                            Fiyat = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "Fiyat")),
                            DovizCinsi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "DovizCinsi")),
                            OrganikSertifikaNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "OrganikSertifikaNo")),
                            Marka = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Marka")),
                            KullanimYeri = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KullanimYeri")),
                            IplikRenkId = gridView1.GetRowCellValue(i, "IplikRenkId"),
                            PartiNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "PartiNo")),
                            Aciklama = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Aciklama")),
                            Barkod = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Barkod")),
                            TalimatNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "TalimatNo")),
                            UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                            SatirTutari = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "SatirTutari")),
                            TakipNo = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "TakipNo")),
                        });
                        gridView1.SetRowCellValue(i, "D2Id", yeniId);
                    }
                    bildirim.Basarili();
                }
            }
            else
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    var query = @"select Id from IplikDepo1 where Id = @Id";
                    var sonuc = connection.QueryFirstOrDefault<int>(query, new { Id = this.Id });
                    if (sonuc != 0)
                    {
                        string updateD1 = @"UPDATE IplikDepo1 SET Tarih = @Tarih,IrsaliyeTarihi = @IrsaliyeTarihi,IrsaliyeNo = @IrsaliyeNo,FirmaId = @FirmaId,
                                            Aciklama = @Aciklama,IslemCinsi = @IslemCinsi WHERE Id = @Id";

                        connection.Execute(updateD1, Baslik);
                        for (int i = 0; i < gridView1.RowCount - 1; i++)
                        {
                            int takipNo = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id"));
                            var sqlKalem = @"select * from IplikDepo2 where Id= @Id";
                            var kalem = connection.QueryFirstOrDefault(sqlKalem, new { Id = takipNo });
                            if (kalem != null)
                            {
                                string updateKalem = @"UPDATE IplikDepo2 SET KalemIslem = @KalemIslem,IplikId = @IplikId,NetKg = @NetKg,BrutKg = @BrutKg,Fiyat = @Fiyat,DovizCinsi = @DovizCinsi,OrganikSertifikaNo = @OrganikSertifikaNo
                                                        ,Marka = @Marka,KullanimYeri = @KullanimYeri,IplikRenkId = @IplikRenkId,PartiNo = @PartiNo,Aciklama = @Aciklama,Barkod = @Barkod,TalimatNo = @TalimatNo,UUID = @UUID,DovizFiyat = @DovizFiyat
                                                        ,SatirTutari = @SatirTutari,TakipNo=@TakipNo WHERE Id = @Id";
                                connection.Execute(updateKalem, new
                                {
                                    Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id")),
                                    KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                                    IplikId = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "IplikId")),
                                    NetKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "NetKg")),
                                    BrutKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "BrutKg")),
                                    Fiyat = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "Fiyat")),
                                    DovizCinsi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "DovizCinsi")),
                                    OrganikSertifikaNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "OrganikSertifikaNo")),
                                    Marka = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Marka")),
                                    KullanimYeri = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KullanimYeri")),
                                    IplikRenkId = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "IplikRenkId")),
                                    PartiNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "PartiNo")),
                                    Aciklama = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "SatirAciklama")),
                                    Barkod = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Barkod")),
                                    TalimatNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "TalimatNo")),
                                    UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                                    DovizFiyat = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "DovizFiyat")),
                                    SatirTutari = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "SatirTutari")),
                                    TakipNo = Convert.ToInt32(gridView1.GetRowCellValue(i, "TakipNo")),
                                });
                            }
                            else
                            {
                                string sql = @"INSERT INTO IplikDepo2 (RefNo,KalemIslem,IplikId,NetKg,BrutKg,Fiyat,DovizCinsi,OrganikSertifikaNo,Marka,KullanimYeri,IplikRenkId,PartiNo
                                ,Aciklama,Barkod,TalimatNo,UUID,SatirTutari) OUTPUT INSERTED.Id
                                     VALUES (@RefNo,@KalemIslem,@IplikId,@NetKg,@BrutKg,@Fiyat,@DovizCinsi,@OrganikSertifikaNo,@Marka,@KullanimYeri,@IplikRenkId,@PartiNo
                                ,@Aciklama,@Barkod,@TalimatNo,@UUID,@SatirTutari)";
                                int yeniId = connection.ExecuteScalar<int>(sql, new
                                {
                                    RefNo = this.Id,
                                    KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                                    IplikId = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "IplikId")),
                                    NetKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "NetKg")),
                                    BrutKg = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "BrutKg")),
                                    Fiyat = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "Fiyat")),
                                    DovizCinsi = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "DovizCinsi")),
                                    OrganikSertifikaNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "OrganikSertifikaNo")),
                                    Marka = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Marka")),
                                    KullanimYeri = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KullanimYeri")),
                                    IplikRenkId = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "IplikRenkId")),
                                    PartiNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "PartiNo")),
                                    Aciklama = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "SatirAciklama")),
                                    Barkod = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Barkod")),
                                    TalimatNo = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "TalimatNo")),
                                    UUID = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "UUID")),
                                    DovizFiyat = yardimciAraclar.GetDecimalValue(gridView1.GetRowCellValue(i, "DovizFiyat")),
                                    SatirTutari = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "SatirTutari")),
                                    TakipNo = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "TakipNo")),
                                });
                                gridView1.SetRowCellValue(i, "D2Id", yeniId);
                            }
                        }
                        bildirim.GuncellemeBasarili();
                    }
                }
            }
        }
    }
}