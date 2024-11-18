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

namespace Hesap.Forms.UretimYonetimi.Barkod
{
    public partial class FrmEtiketBasim : XtraForm
    {
        public FrmEtiketBasim()
        {
            InitializeComponent();
        }
        public int Id;
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        CRUD_Operations cRUD = new CRUD_Operations();
        Bildirim bildirim = new Bildirim();
        HesaplaVeYansit yansit = new HesaplaVeYansit();
        
        private void btnTalimatlar_Click(object sender, EventArgs e)
        {
            FrmSiparisSecimi frm = new FrmSiparisSecimi();
            frm.ShowDialog();
            if (frm.itemList.Count > 0)
            {
                foreach (var item in frm.itemList)
                {
                    gridView1.AddNewRow();
                    int newRowHandle = gridView1.FocusedRowHandle;
                    var values = item.Split(';');
                    gridView1.SetRowCellValue(newRowHandle, "ArtNo", values[0]);
                    gridView1.SetRowCellValue(newRowHandle, "OrderNo", values[1]);
                    gridView1.SetRowCellValue(newRowHandle, "UrunKodu", values[2]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker1", values[3]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker2", values[4]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker3", values[5]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker4", values[6]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker5", values[7]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker6", values[8]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker7", values[9]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker8", values[10]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker9", values[11]);
                    gridView1.SetRowCellValue(newRowHandle, "Sticker10", values[12]);
                    gridView1.SetRowCellValue(newRowHandle, "Barkod", values[13]);
                    gridView1.SetRowCellValue(newRowHandle, "EbatBeden", values[14]);
                    gridView1.SetRowCellValue(newRowHandle, "Varyant1", values[15]);
                    gridView1.SetRowCellValue(newRowHandle, "Miktar", Convert.ToInt32(values[16]));



                }
            }
        }
        private Dictionary<string, object> CreateKalemParameters(int rowIndex)
        {
            return new Dictionary<string, object>
            {
                { "RefNo", this.Id },
                { "UrunKodu", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "UrunKodu")) },
                { "ArtNo", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "ArtNo")) },
                { "Sticker1", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker1")) },
                { "Sticker2", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker2")) },
                { "Sticker3", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker3")) },
                { "Sticker4", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker4")) },
                { "Sticker5", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker5")) },
                { "Sticker6", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker6")) },
                { "Sticker7", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker7")) },
                { "Sticker8", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker8")) },
                { "Sticker9", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker9")) },
                { "Sticker10", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Sticker10")) },
                { "MusteriOrderNo", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "MusteriOrderNo")) },
                { "OrderNo", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "OrderNo")) },
                { "Barkod", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Barkod")) },
                { "Varyant1", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Varyant1")) },
                { "Varyant2", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "Varyant2")) },
                { "EbatBeden", yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(rowIndex, "EbatBeden")) },
                { "Miktar", Convert.ToInt32(gridView1.GetRowCellValue(rowIndex, "Miktar")) },
            };
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var parameters = new Dictionary<string, object>
            {
                { "Tarih", dateTarih.EditValue },
                { "Aciklama", txtAciklama.Text},
                { "BasimSayisi", txtBasimSayisi.Text},
                { "Yuzde", txtYuzde.Text},
                { "Tekli", chckTekli.Checked},
            };
            if (this.Id == 0)
            {
                this.Id = cRUD.InsertRecord("Etiket1", parameters);
                txtKayitNo.Text = this.Id.ToString();
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    var miktar = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Miktar"));
                    if (int.TryParse(gridView1.GetRowCellValue(i, "Miktar")?.ToString(), out int a))
                    {
                        if (a != 0)
                        {
                            var kalemParameters = CreateKalemParameters(i);
                            var d2Id = cRUD.InsertRecord("Etiket", kalemParameters);
                            gridView1.SetRowCellValue(i, "D2Id", d2Id);
                        }
                    }
                }
                bildirim.Basarili();
            }
            else
            {
                cRUD.UpdateRecord("Etiket1", parameters, this.Id);
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    var d2Id = Convert.ToInt32(gridView1.GetRowCellValue(i, "D2Id"));
                    var kalemParameters = CreateKalemParameters(i);
                    if (d2Id > 0)
                    {
                        cRUD.UpdateRecord("Etiket", kalemParameters, d2Id);
                    }
                    else
                    {
                        //kalemParameters["RefNo"] = this.Id; // RefNo ekle
                        var yeniId = cRUD.InsertRecord("Etiket", kalemParameters);
                        gridView1.SetRowCellValue(i, "D2Id", yeniId);
                    }
                }
                bildirim.GuncellemeBasarili();
            }
        }

        private void FrmEtiketBasim_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = new BindingList<_Etiket>();
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
        }

        private void barkodBasımıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Rapor.FrmRaporSecimEkrani frm = new Rapor.FrmRaporSecimEkrani(this.Text, this.Id);
            frm.ShowDialog();
        }

        private void btnListe_Click(object sender, EventArgs e)
        {
            FrmEtiketBasimListesi frm = new FrmEtiketBasimListesi();
            frm.ShowDialog();
            if (frm.veriler.Count > 0)
            {
                this.Id = Convert.ToInt32(frm.veriler[0]["Id"]);
                txtKayitNo.Text = this.Id.ToString();
                dateTarih.EditValue = (DateTime)frm.veriler[0]["Tarih"];
                txtAciklama.Text = frm.veriler[0]["Aciklama"].ToString();
                txtBasimSayisi.Text = frm.veriler[0]["BasimSayisi"].ToString();
                txtYuzde.Text = frm.veriler[0]["Yuzde"].ToString();
                string[] columnNames = yansit.SorgudakiKolonIsimleriniAl(frm.sql);
                yardimciAraclar.ListedenGrideYansit(gridControl1, columnNames, frm.veriler);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            cRUD.FisVeHavuzSil("Etiket1", "Etiket", this.Id);
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
                        sql = "SELECT MAX(Etiket1.Id) FROM Etiket1 INNER JOIN Etiket ON Etiket1.Id = Etiket.RefNo WHERE Etiket1.Id < @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    else if (OncemiSonrami == "Sonraki")
                    {
                        sql = "SELECT MIN(Etiket1.Id) FROM Etiket1 INNER JOIN Etiket ON Etiket1.Id = Etiket.RefNo WHERE Etiket1.Id > @Id";
                        istenenId = connection.QueryFirstOrDefault<int?>(sql, new { Id = id });
                    }
                    string fisquery = @"select
	                                    ISNULL(e1.Id,0) Id,
	                                    ISNULL(e1.Tarih,'') Tarih,
	                                    ISNULL(e1.Aciklama,'') Aciklama,
	                                    ISNULL(e1.BasimSayisi,14) BasimSayisi,
	                                    ISNULL(e1.Yuzde,5) Yuzde,
	                                    ISNULL(e1.Tekli,0) Tekli
                                    from Etiket1 e1 inner join Etiket e on e1.Id = e.RefNo WHERE e1.Id= @Id ";
                    var fis = connection.QueryFirstOrDefault(fisquery, new { Id = istenenId });
                    string kalemquery = @"select
	                                    ISNULL(e.Id,0) D2Id,
										ISNULL(e.UrunKodu,'') UrunKodu,
										ISNULL(e.ArtNo,'') ArtNo,
										ISNULL(e.Sticker1,'') Sticker1,
										ISNULL(e.Sticker2,'') Sticker2,
										ISNULL(e.Sticker3,'') Sticker3,
										ISNULL(e.Sticker4,'') Sticker4,
										ISNULL(e.Sticker5,'') Sticker5,
										ISNULL(e.Sticker6,'') Sticker6,
										ISNULL(e.Sticker7,'') Sticker7,
										ISNULL(e.Sticker8,'') Sticker8,
										ISNULL(e.Sticker9,'') Sticker9,
										ISNULL(e.Sticker10,'') Sticker10,
										ISNULL(e.MusteriOrderNo,'') MusteriOrderNo,
										ISNULL(e.OrderNo,'') OrderNo,
										ISNULL(e.Barkod,'') Barkod,
										ISNULL(e.Varyant1,'') Varyant1,
										ISNULL(e.Varyant2,'') Varyant2,
										ISNULL(e.EbatBeden,'') EbatBeden,
										ISNULL(e.Miktar,0) Miktar
                                    from Etiket1 e1 inner join Etiket e on e1.Id = e.RefNo WHERE e1.Id= @Id ";
                    var kalemler = connection.Query(kalemquery, new { Id = istenenId });
                    if (fis != null && kalemler != null)
                    {
                        gridControl1.DataSource = null;
                        this.Id = Convert.ToInt32(fis.Id);
                        dateTarih.EditValue = (DateTime)fis.Tarih;
                        txtAciklama.Text = fis.Aciklama.ToString();
                        txtBasimSayisi.Text = fis.BasimSayisi.ToString();
                        txtYuzde.Text = fis.Yuzde.ToString();
                        chckTekli.Checked = Convert.ToBoolean(fis.Yuzde);
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