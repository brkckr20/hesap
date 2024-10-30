using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class FrmIplikDepoListe : DevExpress.XtraEditors.XtraForm
    {
        string _islemCinsi;
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public FrmIplikDepoListe(string islemCinsi)
        {
            InitializeComponent();
            _islemCinsi = islemCinsi;
        }

        private void FrmIplikDepoListe_Load(object sender, EventArgs e)
        {
            string sql = "";
            this.Text += " [" + this._islemCinsi +"]";
            if (this._islemCinsi == "Giriş" || this._islemCinsi == "Çıkış")
            {
                sql = $@"select 
                                ISNULL(d1.Id,'') [Id],
                                ISNULL(d2.Id,'') D2Id,
                                ISNULL(d2.TakipNo,'') TakipNo,
                                ISNULL(d1.Tarih,'') [Tarih],
                                ISNULL(IrsaliyeTarihi,'') IrsaliyeTarihi,
                                ISNULL(IrsaliyeNo,'') IrsaliyeNo,
                                isnull(d2.KalemIslem,'') KalemIslem,
                                isnull(FK.Id,0) FirmaId,
                                ISNULL(FK.FirmaKodu,'') FirmaKodu,
                                ISNULL(FK.FirmaUnvan,'') FirmaUnvan,
                                ISNULL(KalemIslem,'') FirmaUnvan,
                                ISNULL(d1.Aciklama,'') Aciklama,
                                isnull(IK.Id,0) IplikId,
                                isnull(IK.IplikKodu,0) IplikKodu,
                                isnull(IK.IplikAdi,0) IplikAdi,
                                ISNULL(d2.Fiyat,0) Fiyat,
                                ISNULL(d2.DovizCinsi,0) DovizCinsi,
                                ISNULL(d2.DovizFiyat,0) DovizFiyat,
                                ISNULL(NetKg,0) NetKg,
                                ISNULL(BrutKg,0) BrutKg,
                                ISNULL(Marka,'') Marka,
                                ISNULL(d2.KullanimYeri,'') KullanimYeri,
                                ISNULL(BRK.Id,0) IplikRenkId,
                                ISNULL(BRK.BoyahaneRenkKodu,'') IplikRenkKodu,
                                ISNULL(BRK.BoyahaneRenkAdi,'') IplikRenkAdi,
                                ISNULL(PartiNo,'') PartiNo,
                                ISNULL(d2.Aciklama,'') SatirAciklama,
								ISNULL(tk.Id,'') TasiyiciId,
								ISNULL(tk.Unvan,'') TasiyiciUnvan,
								ISNULL(tk.Ad,'') TasiyiciAd,
								ISNULL(tk.Soyad,'') TasiyiciSoyad,
								ISNULL(tk.TC,'') TasiyiciTC,
								ISNULL(tk.Plaka,'') Plaka,
								ISNULL(tk.Dorse,'') Dorse,
                                ISNULL(d2.SatirTutari,0) SatirTutari
                                from IplikDepo1 d1 inner join
                                IplikDepo2 d2 on d1.Id = d2.RefNo
                                left join FirmaKarti FK on FK.Id = d1.FirmaId
                                left join BoyahaneRenkKartlari BRK on BRK.Id = d2.IplikRenkId
                                left join IplikKarti IK on IK.Id = d2.IplikId
								left join TasiyiciKarti tk on tk.Id = d1.TasiyiciId
                                where d1.IslemCinsi = '{_islemCinsi}'
								--where d1.IslemCinsi = 'Çıkış'
                                order by d1.Id asc
";
            }
            else if (this._islemCinsi == "SaTal")
            {
                sql = $@"select 
                                ISNULL(d1.Id,'') [Id],
                                ISNULL(d2.Id,'') TakipNo,
								ISNULL(d1.TalimatNo,'') [TalimatNo],
                                ISNULL(d1.Tarih,'') [Tarih],
                                isnull(FK.Id,0) FirmaId,
                                ISNULL(FK.FirmaKodu,'') FirmaKodu,
                                ISNULL(FK.FirmaUnvan,'') FirmaUnvan,
                                ISNULL(KalemIslem,'') FirmaUnvan,
                                ISNULL(d1.Aciklama,'') Aciklama,	
                                ISNULL(d1.Yetkili,'') Yetkili,							
                                ISNULL(d1.Vade,'') Vade,							
                                ISNULL(d1.OdemeSekli,'') OdemeSekli,							
                                isnull(d2.KalemIslem,'') KalemIslem,
                                isnull(IK.Id,0) IplikId,
                                isnull(IK.IplikKodu,0) IplikKodu,
                                isnull(IK.IplikAdi,0) IplikAdi,
                                ISNULL(d2.Fiyat,0) Fiyat,
                                ISNULL(d2.DovizCinsi,0) DovizCinsi,
                                ISNULL(d2.DovizFiyat,0) DovizFiyat,
                                ISNULL(NetKg,0) NetKg,
                                ISNULL(BrutKg,0) BrutKg,
                                ISNULL(Marka,'') Marka,
                                ISNULL(BRK.Id,0) IplikRenkId,
                                ISNULL(BRK.BoyahaneRenkKodu,'') IplikRenkKodu,
                                ISNULL(BRK.BoyahaneRenkAdi,'') IplikRenkAdi,
                                ISNULL(PartiNo,'') PartiNo,
                                ISNULL(d2.Aciklama,'') SatirAciklama,
                                ISNULL(d2.SatirTutari,0) SatirTutari
                                from IplikDepo1 d1 inner join
                                IplikDepo2 d2 on d1.Id = d2.RefNo
                                left join FirmaKarti FK on FK.Id = d1.FirmaId
                                left join BoyahaneRenkKartlari BRK on BRK.Id = d2.IplikRenkId
                                left join IplikKarti IK on IK.Id = d2.IplikId
                                where d1.IslemCinsi = '{_islemCinsi}'
                                                                order by d1.Id asc
";
            }
            
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1,this.Text);
        }
        public List<Dictionary<string, object>> veriler = new List<Dictionary<string, object>>();

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridView gridView = sender as GridView;
            if (gridView == null)
                return;
            int secilenId = Convert.ToInt32(gridView.GetFocusedRowCellValue("Id"));
            veriler.Clear();
            for (int i = 0; i < gridView.DataRowCount; i++)
            {
                int id = Convert.ToInt32(gridView.GetRowCellValue(i, "Id"));

                if (id == secilenId)
                {
                    var rowData = new Dictionary<string, object>();
                    foreach (GridColumn column in gridView.Columns)
                    {
                        var columnName = column.FieldName;
                        var cellValue = gridView.GetRowCellValue(i, columnName);
                        rowData[columnName] = cellValue;
                    }
                    veriler.Add(rowData);
                }
            }
            Close();
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