using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
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
    public partial class FrmHamDepoListeSecimli : DevExpress.XtraEditors.XtraForm
    {
        public string sql,_islemCinsi;
        Listele listele = new Listele();
        public FrmHamDepoListeSecimli(string islemCinsi)
        {
            InitializeComponent();
            this._islemCinsi = islemCinsi;
        }
        private void FrmHamDepoListeSecimli_Load(object sender, EventArgs e)
        {
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
                                    order by d1.Id asc";
            listele.Liste(sql, gridControl1);
        }

        public List<string> islemListesi = new List<string>();

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();

            foreach (int rowHandle in selectedRows)
            {
                string TalimatNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "TalimatNo"));
                int FirmaId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "FirmaId"));
                string FirmaKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaKodu"));
                string FirmaUnvan = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "FirmaUnvan"));
                int TakipNo = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "TakipNo"));
                int KumasId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "KumasId"));
                string KumasKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KumasKodu"));
                string KumasAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KumasAdi"));
                decimal BrutKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "BrutKg"));
                decimal NetKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "NetKg"));
                decimal Fiyat = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Fiyat"));
                string DovizCinsi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "DovizCinsi"));
                int GrM2 = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "GrM2"));
                int Id = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Id"));
                int D2Id = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "D2Id"));

                islemListesi.Add($"{TalimatNo};{FirmaId};{FirmaKodu};{FirmaUnvan};{TakipNo};{KumasId};{KumasKodu};{KumasAdi};{BrutKg};{Fiyat};{DovizCinsi};{NetKg};{GrM2};{Id};{D2Id}");
            }
            Close();
        }
    }
}