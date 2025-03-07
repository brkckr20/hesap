using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DevExpress.Pdf.Native.BouncyCastle.Utilities;

namespace Hesap.Utils
{
    public class Numarator
    {
        string sorgu;
        Ayarlar ayarlar = new Ayarlar();
        string _dbTuru;
        public Numarator()
        {
            _dbTuru = ayarlar.VeritabaniTuru();
        }

        public string NumaraVer(string fisMiSipMi, int ReceiptType = 0)
        {
            if (fisMiSipMi == "Fiş")
            {
                string sorgum;
                using (var connection = new Baglanti().GetConnection())
                {
                    string sql = $"SELECT top 1 ReceiptNo FROM Receipt where ReceiptType = {ReceiptType} ORDER BY ReceiptNo desc";
                    string sqlite = "SELECT FisNo FROM Siparis ORDER BY FisNo desc LIMIT 1";
                    sorgum = ayarlar.DbTuruneGoreSorgu(sql, sqlite);
                    var fisNo = connection.QuerySingleOrDefault<string>(sorgum);
                    if (fisNo != null)
                    {
                        return string.Format("{0:D8}", Convert.ToInt32(fisNo) + 1);
                    }
                    else
                    {
                        return "00000001";
                    }

                }
            }
            else if (fisMiSipMi == "Sipariş")
            {
                string sorgum;
                using (var connection = new Baglanti().GetConnection())
                {
                    string sql = "SELECT top 1 SiparisNo FROM Siparis ORDER BY FisNo desc";
                    string sqlite = "SELECT FisNo FROM Siparis ORDER BY FisNo desc LIMIT 1";
                    sorgum = ayarlar.DbTuruneGoreSorgu(sql, sqlite);
                    var sipNo = connection.QuerySingleOrDefault<string>(sorgum);
                    if (sipNo != null)
                    {
                        string prefix = sipNo.Substring(0, 3);
                        string numberPart = sipNo.Substring(3);
                        int number = Convert.ToInt32(numberPart);
                        int newNumber = number + 1;
                        string newNumberPart = string.Format("{0:D3}", newNumber);
                        string newSiparisNo = prefix + newNumberPart;
                        return newSiparisNo;
                    }
                    else
                    {
                        return "SIP001";
                    }
                }
            }
            else if (fisMiSipMi == "İplik")
            {
                string sorgum;
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = "select top 1 IplikKodu from IplikKarti order by IplikKodu desc";
                    string sqlite = "select IplikKodu from IplikKarti order by IplikKodu desc limit 1";
                    sorgum = ayarlar.DbTuruneGoreSorgu(mssql, sqlite);

                    var sipNo = connection.QuerySingleOrDefault<string>(sorgum);
                    if (sipNo != null)
                    {
                        string prefix = sipNo.Substring(0, 3);
                        string numberPart = sipNo.Substring(3);
                        int number = Convert.ToInt32(numberPart);
                        int newNumber = number + 1;
                        string newNumberPart = string.Format("{0:D3}", newNumber);
                        string newSiparisNo = prefix + newNumberPart;
                        return newSiparisNo;
                    }
                    else
                    {
                        return "IPL001";
                    }
                }
            }
            else if (fisMiSipMi == "UrunRecete") // düzenlenme tarihi : 19.02.2025
            {
                string sorgum;
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = "select top 1 ReceiptNo from InventoryReceipt order by ReceiptNo desc"; // değiştirilen satır
                    string sqlite = "select ReceteNo from UrunRecete order by ReceteNo desc limit 1";
                    sorgum = ayarlar.DbTuruneGoreSorgu(mssql, sqlite);

                    var fisNo = connection.QuerySingleOrDefault<string>(sorgum);
                    if (fisNo != null)
                    {
                        return string.Format("{0:D8}", Convert.ToInt32(fisNo) + 1);
                    }
                    else
                    {
                        return "00000001";
                    }
                }
            }
            //else if (fisMiSipMi == "IpSaTal")
            //{
            //    string sorgum;
            //    using (var connection = new Baglanti().GetConnection())
            //    {
            //        string sql = "SELECT top 1 TalimatNo FROM IplikDepo1 ORDER BY TalimatNo desc";
            //        string sqlite = "SELECT TalimatNo FROM IplikDepo1 ORDER BY TalimatNo desc LIMIT 1";
            //        sorgum = ayarlar.DbTuruneGoreSorgu(sql, sqlite);
            //        var fisNo = connection.QuerySingleOrDefault<string>(sorgum);
            //        if (fisNo != null)
            //        {
            //            return string.Format("{0:D8}", Convert.ToInt32(fisNo) + 1);
            //        }
            //        else
            //        {
            //            return "00000001";
            //        }

            //    }
            //}
            //else if (fisMiSipMi == "HamKSaTal")
            //{
            //    string sorgum;
            //    using (var connection = new Baglanti().GetConnection())
            //    {
            //        string sql = "SELECT top 1 TalimatNo FROM HamDepo1 ORDER BY TalimatNo desc";
            //        string sqlite = "SELECT TalimatNo FROM HamDepo1 ORDER BY TalimatNo desc LIMIT 1";
            //        sorgum = ayarlar.DbTuruneGoreSorgu(sql, sqlite);
            //        var fisNo = connection.QuerySingleOrDefault<string>(sorgum);
            //        if (fisNo != null)
            //        {
            //            return string.Format("{0:D8}", Convert.ToInt32(fisNo) + 1);
            //        }
            //        else
            //        {
            //            return "00000001";
            //        }

            //    }
            //}
            else if (fisMiSipMi == "Maliyet")
            {
                string sorgum;
                using (var connection = new Baglanti().GetConnection())
                {
                    string sql = "SELECT top 1 OrderNo FROM Cost ORDER BY OrderNo desc"; // 19.02.2025 tarihinde table adı değştirildi
                    string sqlite = "SELECT OrderNo FROM Cost ORDER BY OrderNo desc LIMIT 1";
                    sorgum = ayarlar.DbTuruneGoreSorgu(sql, sqlite);
                    var fisNo = connection.QuerySingleOrDefault<string>(sorgum);
                    if (fisNo != null)
                    {
                        return string.Format("{0:D8}", Convert.ToInt32(fisNo) + 1);
                    }
                    else
                    {
                        return "00000001";
                    }

                }
            }
            return "";
        }

        public string GetNumaratorNotCondition(string TableName, string FieldName)
        {
            string sorgum;
            using (var connection = new Baglanti().GetConnection())
            {
                string sql = $"SELECT top 1 {FieldName} FROM {TableName} ORDER BY OrderNo desc"; // 19.02.2025 tarihinde table adı değştirildi
                string sqlite = $"SELECT {FieldName} FROM {TableName} ORDER BY OrderNo desc LIMIT 1";
                sorgum = ayarlar.DbTuruneGoreSorgu(sql, sqlite);
                var fisNo = connection.QuerySingleOrDefault<string>(sorgum);
                if (fisNo != null)
                {
                    return string.Format("{0:D8}", Convert.ToInt32(fisNo) + 1);
                }
                else
                {
                    return "00000001";
                }

            }
            //return "";
        }
    }
}
