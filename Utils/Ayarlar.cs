using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Utils
{
    public class Ayarlar
    {
        string baseDirectory;
        public Ayarlar()
        {
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }
        public string VeritabaniTuru()
        {
            string configFilePath = Path.Combine(baseDirectory, "Data", "config.txt");
            string content = string.Join(Environment.NewLine, File.ReadLines(configFilePath));
            return content;
        }

        public string MssqlConnStr()
        {
            string mssqlStringPath = Path.Combine(baseDirectory, "Data", "mssql.txt");
            string mssqlString = string.Join(Environment.NewLine, File.ReadLines(mssqlStringPath));
            return mssqlString;
        }
        public string SqliteConnStr()
        {
            string sqliteStringPath = Path.Combine(baseDirectory, "Data", "sqlite.txt");
            string sqliteString = string.Join(Environment.NewLine, File.ReadLines(sqliteStringPath));
            return sqliteString;
        }

        public string DbTuruneGoreSorgu(string mssqlSorgusu,string sqliteSorgusu)
        {
            if (VeritabaniTuru() == "mssql")
            {
                return mssqlSorgusu;
            }
            return sqliteSorgusu;
        }
    }
}
