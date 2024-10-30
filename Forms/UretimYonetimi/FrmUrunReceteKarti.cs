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

namespace Hesap.Forms.UretimYonetimi
{
    public partial class FrmUrunReceteKarti : DevExpress.XtraEditors.XtraForm
    {
        Numarator numarator = new Numarator();
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Bildirim bildirim = new Bildirim();
        public FrmUrunReceteKarti()
        {
            InitializeComponent();
        }
        int Id = 0;
        private void txtUrun_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmUrunKartiListesi frm = new Liste.FrmUrunKartiListesi();
            frm.ShowDialog();
            txtUrun.Text = frm.UrunKodu;
            lblUrunAdi.Text = frm.UrunAdi;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            object baslik = new
            {
                ReceteNo= txtReceteNo.Text,
                Gr_M2 = txtGrm2.Text,
                HamEn = txtHamEn.Text,
                HamBoy = txtHamBoy.Text,
                MamulEn = txtMamulEn.Text,
                MamulBoy = txtMamulBoy.Text,
                IpligiBoyali = chckIpligiBoyali.Checked,
                UrunKodu = txtUrun.Text,
                Id = this.Id
            };
            if (this.Id == 0)
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = @"INSERT INTO UrunRecete
                                       (Gr_M2,HamEn,HamBoy,MamulEn,MamulBoy,IpligiBoyali,ReceteNo,UrunKodu) OUTPUT INSERTED.Id
                                    VALUES (@Gr_M2,@HamEn,@HamBoy,@MamulEn,@MamulBoy,@IpligiBoyali,@ReceteNo,@UrunKodu)";
                    string sqlite = @"INSERT INTO UrunRecete
                                       (Gr_M2,HamEn,HamBoy,MamulEn,MamulBoy,IpligiBoyali,ReceteNo,UrunKodu)
                                    VALUES (@Gr_M2,@HamEn,@HamBoy,@MamulEn,@MamulBoy,@IpligiBoyali,@ReceteNo,@UrunKodu)";
                    string idQuery = "SELECT last_insert_rowid();";
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        this.Id = connection.QuerySingle<int>(mssql, baslik);
                    }
                    else
                    {
                        connection.Execute(sqlite, baslik);
                        this.Id = connection.QuerySingle<int>(idQuery);
                    }
                    string mssqlFis = @"INSERT INTO UrunReceteUB
                                           (RefNo,KalemIslem,IplikKodu,Miktar,Birim,BirimFiyat,Doviz,DovizFiyat)
                                     VALUES
                                           (@RefNo,@KalemIslem,@IplikKodu,@Miktar,@Birim,@BirimFiyat,@Doviz,@DovizFiyat)";
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        connection.Execute(mssqlFis, new
                        {
                            RefNo = this.Id,
                            KalemIslem = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "KalemIslem")),
                            IplikKodu = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "IplikKodu")),
                            Miktar = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "Miktar")),
                            Birim = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Birim")),
                            BirimFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "BirimFiyat")),
                            Doviz = yardimciAraclar.GetStringValue(gridView1.GetRowCellValue(i, "Doviz")),
                            DovizFiyat = yardimciAraclar.GetDoubleValue(gridView1.GetRowCellValue(i, "DovizFiyat")),
                        });
                    }
                    bildirim.Basarili();

                }
            }
        }

        private void FrmUrunReceteKarti_Load(object sender, EventArgs e)
        {
            BaslangicVerileri();
        }
        void BaslangicVerileri()
        {
            txtReceteNo.Text = numarator.NumaraVer("UrunRecete");
            gridControl1.DataSource = new BindingList<_UrunReceteUB>();

        }

        private void repoKalemIslem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            
        }

        private void repoIplikKodu_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Liste.FrmIplikKartiListesi frm = new Liste.FrmIplikKartiListesi();
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.IplikKodu) && !string.IsNullOrEmpty(frm.IplikAdi))
            {
                int newRowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(newRowHandle, "IplikKodu", frm.IplikKodu);
                gridView1.SetRowCellValue(newRowHandle, "IplikAdi", frm.IplikAdi);
            }
        }
    }
}