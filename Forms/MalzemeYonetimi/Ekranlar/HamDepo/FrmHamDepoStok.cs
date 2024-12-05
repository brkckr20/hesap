using DevExpress.XtraEditors;
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
    public partial class FrmHamDepoStok : DevExpress.XtraEditors.XtraForm
    {
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        public List<string> stokListesi = new List<string>();
        Listele listele = new Listele();
        public FrmHamDepoStok()
        {
            InitializeComponent();
        }

        private void FrmHamDepoStok_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT d1.[Id] ,[Tarih] ,[FirmaId] ,[IslemCinsi] ,d1.[Aciklama] ,[IrsaliyeNo] ,[IrsaliyeTarihi] ,[FaturaNo] ,[FaturaTarihi] ,[TasiyiciId] ,[TalimatNo] ,[Kapat] ,[Yetkili] ,[Vade] ,[OdemeSekli] ,[TalimatId] ,d2.[Id] D2Id,[RefNo] ,[KalemIslem] ,[SipNo] ,[BordurKodu] ,[Bordur] ,[GrM2] ,[HamGr] ,[RenkId] ,SUM([BrutKg]) [BrutKg]
            ,COALESCE(SUM([NetKg]) - (select sum(NetKg) from HamDepo1 x inner join HamDepo2 y on x.Id = y.RefNo where y.TakipNo = d2.Id and x.IslemCinsi = 'Çıkış'), SUM([NetKg])) AS [KalanNetKg]
            ,sum([BrutMt]) [BrutMt] ,sum([NetMt]) [NetMt] ,sum([Adet]) [Adet] ,[Fire] ,[CuvalSayisi] ,sum([TopSayisi]) [TopSayisi] ,d2.[Aciklama] [SatirAciklama] ,[HataId] ,[IstenenEbat] ,[BoyaOzellik] ,[BaskiId] ,[Barkod] ,[HamKod] ,[HamFasonKod] ,d2.[Id] [TakipNo] ,[PartiNo] ,[BoyaKod] ,[VaryantId] ,[Fiyat] ,[Organik] ,[DesenId] ,[BoyaIslemId] ,[DovizCinsi] ,[FiyatBirimi] ,[UUID] ,[SatirTutari],uk.[Id] [KumasId],[UrunKodu] [KumasKodu],[UrunAdi] [KumasAdi] FROM HamDepo1 d1 inner join HamDepo2 d2 on d1.Id = d2.RefNo 
			left join UrunKarti uk on uk.Id = d2.KumasId
				where d1.IslemCinsi = 'Giriş'
            group by
            d1.[Id] ,[Tarih] ,[FirmaId] ,[IslemCinsi] ,d1.[Aciklama] ,[IrsaliyeNo] ,[IrsaliyeTarihi] ,[FaturaNo] ,[FaturaTarihi] ,[TasiyiciId] ,[TalimatNo] ,[Kapat] ,[Yetkili] ,[Vade] ,[OdemeSekli] ,[TalimatId] ,d2.[Id],[RefNo] ,[KalemIslem] ,[SipNo] ,[BordurKodu] ,[Bordur] ,[GrM2] ,[HamGr] ,[RenkId] ,[Fire] ,[CuvalSayisi] ,d2.[Aciklama],[HataId] ,[IstenenEbat] ,[BoyaOzellik] ,[BaskiId] ,[Barkod] ,[HamKod] ,[HamFasonKod] ,[TakipNo] ,[PartiNo] ,[BoyaKod] ,[VaryantId] ,[Fiyat] ,[Organik] ,[DesenId] ,[BoyaIslemId] ,[DovizCinsi] ,[FiyatBirimi] ,[UUID] ,[SatirTutari],uk.[Id],[UrunKodu],[UrunAdi] 
            having  COALESCE(SUM([NetKg]) - (select sum(NetKg) from HamDepo1 x inner join HamDepo2 y on x.Id = y.RefNo where y.TakipNo = d2.Id and x.IslemCinsi = 'Çıkış'), SUM([NetKg])) > 0 ";
            listele.Liste(sql, gridControl1);
            yardimciAraclar.KolonlariGetir(gridView1, this.Text);
        }

        private void dizaynKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonDurumunuKaydet(gridView1, this.Text);
        }

        private void sütunSeçimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimciAraclar.KolonSecici(gridControl1);
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                string KumasKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KumasKodu"));
                string KumasAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "KumasAdi"));
                int GrM2 = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "GrM2"));
                decimal BrutKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "BrutKg"));
                decimal NetKg = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "KalanNetKg"));
                decimal NetMt = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "NetMt"));
                int BoyahaneRenkId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "BoyahaneRenkId"));
                string BoyahaneRenkKodu = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "BoyahaneRenkKodu"));
                string BoyahaneRenkAdi = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "BoyahaneRenkAdi"));
                string TakipNo = Convert.ToString(gridView1.GetRowCellValue(rowHandle, "TakipNo"));
                int KumasId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "KumasId"));


                stokListesi.Add($"{KumasKodu};{KumasAdi};{GrM2};{BrutKg};{NetKg};{NetMt};{BoyahaneRenkId};{BoyahaneRenkKodu};{BoyahaneRenkAdi};" +
                    $"{TakipNo};{KumasId}");
            }
            Close();
        }
    }
}