using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraReports;
using FastReport;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.Forms.Rapor.Etiket
{
    public partial class FrmEtiketTasarimi : DevExpress.XtraEditors.XtraForm
    {
        public FrmEtiketTasarimi()
        {
            InitializeComponent();
        }
        Ayarlar ayarlar = new Ayarlar();
        YardimciAraclar yardimciAraclar = new YardimciAraclar();
        Bildirim bildirim = new Bildirim();
        int Id = 0; int kayitSayi;
        void RaporDosyasiOlustur(string yeniRaporAdi)
        {
            string kaynakDosyaYolu = Path.Combine(Application.StartupPath, "Rapor", "blank.frx");
            string yeniDosyaYolu = Path.Combine(Application.StartupPath, "Rapor", yeniRaporAdi + ".frx");

            try
            {
                if (!File.Exists(kaynakDosyaYolu))
                {
                    throw new FileNotFoundException("Kaynak dosya mevcut değil", kaynakDosyaYolu);
                }
                string klasorYolu = Path.GetDirectoryName(yeniDosyaYolu);
                if (!Directory.Exists(klasorYolu))
                {
                    Directory.CreateDirectory(klasorYolu);
                }
                File.Copy(kaynakDosyaYolu, yeniDosyaYolu, true);
            }
            catch (Exception ex)
            {
                bildirim.Uyari("Dosya oluşturulurken veya yüklenirken bir hata oluştu: " + ex.Message);
            }
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {

            object baslik = new
            {
                FormAdi = "Etiket Tasarımı",
                RaporAdi = txtRaporAdi.Text.Trim(),
                Sorgu1 = @"WITH Numbers AS (
                        SELECT TOP (100000) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
                        FROM master.dbo.spt_values
                    ),
                    KesimSayisi AS (
                        SELECT 
                            t.*,
                            CEILING(CAST(t.miktar AS FLOAT) * 1.05 / 7) * 7 AS YuvarlanmisKesimSayisi  -- Kesim sayısını yukarı yuvarla
                        FROM Etiket t
                        WHERE t.RefNo = 5 AND t.OrderNo = 'EIH24-0044-32'
                    )
                    SELECT 
                        k.Sticker1,
                        k.Sticker2,
                        k.Sticker3,
                        k.Sticker4,
                        k.Sticker5,
                        k.Sticker6,
                        k.Sticker7,
                        k.Sticker8,
                        k.Sticker9,
                        k.Sticker10,
                        k.OrderNo,
	                    k.Barkod
                    FROM KesimSayisi k
                    JOIN Numbers n ON n.n <= k.YuvarlanmisKesimSayisi",
                FormGrubu = "Etiket",
                Id = this.Id,
                KayitNo = this.Id
            };
            using (var connection = new Baglanti().GetConnection())
            {
                if (this.Id == 0)
                {
                    string mssql = @"
                                    INSERT INTO Rapor
                                           (FormAdi, RaporAdi, Sorgu1, FormGrubu)
                                    OUTPUT INSERTED.KayitNo
                                    VALUES (@FormAdi, @RaporAdi, @Sorgu1, @FormGrubu)";

                    string sqlite = @"INSERT INTO Rapor
                               (FormAdi,RaporAdi,Sorgu1,Sorgu2,Sorgu3,Sorgu4,Sorgu5,Sorgu6,Sorgu7,Sorgu8,Sorgu9,FormGrubu)
                         VALUES (@FormAdi,@RaporAdi,@Sorgu1,@Sorgu2,@Sorgu3,@Sorgu4,@Sorgu5,@Sorgu6,@Sorgu7,@Sorgu8,@Sorgu9,@FormGrubu)";
                    string idQuery = "SELECT last_insert_rowid();";
                    string kayitSayisi = "select COUNT(*) from Rapor";
                    kayitSayi = connection.QuerySingle<int>(kayitSayisi);
                    if (ayarlar.VeritabaniTuru() == "mssql")
                    {
                        this.Id = connection.QuerySingle<int>(mssql, baslik);
                    }
                    else
                    {
                        connection.Execute(sqlite, baslik);
                        this.Id = connection.QuerySingle<int>(idQuery);
                    }
                    RaporDosyasiOlustur(txtRaporAdi.Text.Trim());
                    bildirim.Basarili();
                }
                else
                {
                    //string update = @"UPDATE Rapor
                    //   SET [Id] = @Id,[FormAdi] = @FormAdi,[RaporAdi] = @RaporAdi,[Sorgu1] = @Sorgu1,[Sorgu2] = @Sorgu2,[Sorgu3] = @Sorgu3
                    //,[Sorgu4] = @Sorgu4,[Sorgu5] = @Sorgu5,[Sorgu6] = @Sorgu6,[Sorgu7] = @Sorgu7,[Sorgu8] = @Sorgu8,[Sorgu9] = @Sorgu9,[FormGrubu] = @FormGrubu
                    // WHERE KayitNo = @KayitNo";
                    //connection.Execute(update, baslik);
                    //bildirim.GuncellemeBasarili();
                }
            }

        }
        public void VeriKaydetVeRaporuKaydet(string sorgu, Report report, int kayitNumarasi)
        {
            if (!string.IsNullOrEmpty(sorgu))
            {
                string tabloAdı = yardimciAraclar.TabloAdiniAl(sorgu);
                if (tabloAdı != null)
                {
                    DataSet dsSorgu = goster(sorgu, tabloAdı, kayitNumarasi);
                    if (dsSorgu.Tables.Count > 0)
                    {
                        report.RegisterData(dsSorgu);
                    }
                }
            }
        }
        public DataSet goster(string sql, string tabloismi, int kayitNumarasi)
        {
            string DbTuru = ayarlar.VeritabaniTuru();
            string mssqlconn = ayarlar.MssqlConnStr();
            string sqliteconn = ayarlar.SqliteConnStr();
            DataSet ds = new DataSet();
            if (DbTuru == "mssql")
            {
                using (SqlConnection bg = new SqlConnection(mssqlconn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        SqlCommand command = new SqlCommand(sql, bg);
                        {
                            command.Parameters.AddWithValue("@Id", kayitNumarasi);
                        }
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 5000;
                        adapter.Fill(ds, tabloismi);
                    }
                }
            }
            else
            {
                using (SQLiteConnection bg = new SQLiteConnection(sqliteconn))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
                    {
                        SQLiteCommand command = new SQLiteCommand(sql, bg);
                        {
                            command.Parameters.AddWithValue("@Id", kayitNumarasi);
                        }
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 5000;
                        adapter.Fill(ds, tabloismi);
                    }
                }
            }
            return ds;
        }

        private void btnDizayn_Click(object sender, EventArgs e)
        {
            if (this.Id != 0)
            {
                DizaynAc(txtRaporAdi.Text, true, 4);
            }
            else
            {
                string yeniRaporAdi = txtRaporAdi.Text.Trim();
                if (string.IsNullOrEmpty(yeniRaporAdi))
                {
                    bildirim.Uyari("Lütfen geçerli bir rapor adı girin.");
                    return;
                }
            }
        }
        Report report1 = new Report();
        public void DizaynAc(string raporName, bool isDesing, int kayitNumarasi)
        {
            string dosyaYolu = Path.Combine(Application.StartupPath, "Rapor", raporName + ".frx");
            report1.Load(dosyaYolu);
            string Rapor1, Rapor2, Rapor3, Rapor4;
            using (var connection = new Baglanti().GetConnection())
            {
                string sql = $"select Sorgu1,Sorgu2,Sorgu3,Sorgu4 from Rapor where RaporAdi = @RaporName";
                var parameters = new { RaporName = raporName };
                var result = connection.QuerySingleOrDefault(sql, parameters);
                Rapor1 = result.Sorgu1;
                Rapor2 = result.Sorgu2;
                Rapor3 = result.Sorgu3;
                Rapor4 = result.Sorgu4;
            }
            VeriKaydetVeRaporuKaydet(Rapor1, report1, kayitNumarasi);
            VeriKaydetVeRaporuKaydet(Rapor2, report1, kayitNumarasi);
            VeriKaydetVeRaporuKaydet(Rapor3, report1, kayitNumarasi);
            VeriKaydetVeRaporuKaydet(Rapor4, report1, kayitNumarasi);
            if (isDesing)
            {
                report1.Design();
            }
            else
            {
                report1.Show();
            }
        }
    }
}