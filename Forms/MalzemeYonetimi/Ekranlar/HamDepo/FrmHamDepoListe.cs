﻿using DevExpress.XtraEditors;
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

namespace Hesap.Forms.MalzemeYonetimi.Ekranlar.HamDepo
{
    public partial class FrmHamDepoListe : DevExpress.XtraEditors.XtraForm
    {
        string _islemCinsi;
        Listele listele = new Listele();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public string sql;
        public FrmHamDepoListe(string islemCinsi)
        {
            InitializeComponent();
            _islemCinsi = islemCinsi;
        }

        private void FrmHamDepoListe_Load(object sender, EventArgs e)
        {
            this.Text += _islemCinsi == "SaTal" ? " [Satın Alma Talimatları Listesi ]" : " [" + this._islemCinsi + "]";
            sql = $@"SELECT 
                                    ISNULL(d1.Id, 0) AS [Id],
                                    ISNULL(d1.Tarih, '') AS [Tarih],
                                    ISNULL(d1.FirmaId, 0) AS [FirmaId],
                                    ISNULL(d1.IslemCinsi, '') AS [IslemCinsi],
                                    ISNULL(d1.Aciklama, '') AS [Aciklama],
                                    ISNULL(d1.IrsaliyeNo, '') AS [IrsaliyeNo],
                                    ISNULL(d1.IrsaliyeTarihi, '') AS [IrsaliyeTarihi],
                                    ISNULL(d1.FaturaNo, '') AS [FaturaNo],
                                    ISNULL(d1.FaturaTarihi, '') AS [FaturaTarihi],
                                    ISNULL(d1.TasiyiciId, 0) AS [TasiyiciId],
                                    ISNULL(d1.TalimatNo, '') AS [TalimatNo],
                                    ISNULL(d1.Kapat, 0) AS [Kapat],
                                    ISNULL(d1.Yetkili, '') AS [Yetkili],
                                    ISNULL(d1.Vade, '') AS [Vade],
                                    ISNULL(d1.OdemeSekli, '') AS [OdemeSekli],
	                                ISNULL(d2.Id, 0) AS [D2Id],
                                    ISNULL(d2.RefNo, '') AS [RefNo],
                                    ISNULL(d2.KalemIslem, '') AS [KalemIslem],
                                    ISNULL(d2.SipNo, '') AS [SipNo],
                                    ISNULL(d2.KumasId, 0) AS [KumasId],
                                    ISNULL(d2.BordurKodu, '') AS [BordurKodu],
                                    ISNULL(d2.Bordur, '') AS [Bordur],
                                    ISNULL(d2.GrM2, 0) AS [GrM2],
                                    ISNULL(d2.HamGr, 0) AS [HamGr],
                                    ISNULL(d2.RenkId, 0) AS [RenkId],
                                    ISNULL(d2.BrutKg, 0) AS [BrutKg],
                                    ISNULL(d2.NetKg, 0) AS [NetKg],
                                    ISNULL(d2.BrutMt, 0) AS [BrutMt],
                                    ISNULL(d2.NetMt, 0) AS [NetMt],
                                    ISNULL(d2.Adet, 0) AS [Adet],
                                    ISNULL(d2.Fire, 0) AS [Fire],
                                    ISNULL(d2.CuvalSayisi, 0)  [CuvalSayisi],
                                    ISNULL(d2.TopSayisi, 0)  [TopSayisi],
                                    ISNULL(d2.Aciklama, '')  [SatirAciklama],
                                    ISNULL(d2.HataId, 0)  [HataId],
                                    ISNULL(d2.IstenenEbat, '')  [IstenenEbat],
                                    ISNULL(d2.BoyaOzellik, '')  [BoyaOzellik],
                                    ISNULL(d2.BaskiId, 0)  [BaskiId],
                                    ISNULL(d2.Barkod, '')  [Barkod],
                                    ISNULL(d2.HamKod, '')  [HamKod],
                                    ISNULL(d2.HamFasonKod, '')  [HamFasonKod],
                                    ISNULL(d2.TakipNo, '')  [TakipNo],
                                    ISNULL(d2.PartiNo, '')  [PartiNo],
                                    ISNULL(d2.BoyaKod, '')  [BoyaKod],
                                    ISNULL(d2.VaryantId, 0)  [VaryantId],
                                    ISNULL(d2.Fiyat, 0)  [Fiyat],
                                    ISNULL(d2.Organik, '')  [Organik],
                                    ISNULL(d2.DesenId, 0)  [DesenId],
                                    ISNULL(d2.BoyaIslemId, 0)  [BoyaIslemId],
                                    ISNULL(d2.DovizCinsi, '')  [DovizCinsi],
                                    ISNULL(d2.FiyatBirimi, '')  [FiyatBirim],
                                    ISNULL(d2.UUID, '')  [UUID],
                                    ISNULL(d2.SatirTutari, 0)  [SatirTutari],
									ISNULL(fk.FirmaKodu,'') [FirmaKodu],
									ISNULL(fk.FirmaUnvan,'') [FirmaUnvan],
									ISNULL(uk.UrunKodu,'') [KumasKodu],
									ISNULL(uk.UrunAdi,'') [KumasAdi],
									ISNULL(BoyahaneRenkKodu,'') [BoyahaneRenkKodu],
									ISNULL(BoyahaneRenkAdi,'') [BoyahaneRenkAdi]
                                FROM HamDepo1 d1 inner join HamDepo2 d2 on d1.Id = d2.RefNo
								left join FirmaKarti Fk on Fk.Id = d1.FirmaId
								left join UrunKarti uk on uk.Id = d2.KumasId
								left join BoyahaneRenkKartlari brk on brk.Id = d2.RenkId
                                                                where d1.IslemCinsi = '{_islemCinsi}'
								                                --where d1.IslemCinsi = 'SaTal'
                                    order by d1.Id asc
";
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1, this.Text);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1,this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void excelAktarxlsxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.ExcelOlarakAktar(gridControl1,"Kumaş Satın Alma Talimat Listesi");
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
    }
}