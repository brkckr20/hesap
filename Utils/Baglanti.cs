using DevExpress.XtraPrinting.BarCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Utils
{
    public class Baglanti : IDisposable
    {
        private SqlConnection _connection;
        private SQLiteConnection _sqliteConnection;
        private bool _disposed = false;
        public string connectionString = "Server=.;Database=Hesap;Trusted_Connection=True;";
        private readonly string _sqliteConnectionString;
        string DatabaseType;
        public Baglanti()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configFilePath = Path.Combine(baseDirectory, "Data", "config.txt");
            DatabaseType = string.Join(Environment.NewLine, File.ReadLines(configFilePath));
            string dataDirectory = Path.Combine(baseDirectory, "Data\\sqlite");
            string databasePath = Path.Combine(dataDirectory, "Hesap.db");
            if (DatabaseType == "mssql")
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
            }else
            {
                _sqliteConnectionString = $"Data Source={databasePath};Version=3;";
                _sqliteConnection = new SQLiteConnection(_sqliteConnectionString);
                _sqliteConnection.Open();
            }
        }
        public IDbConnection GetConnection()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Baglanti");
            }
            return DatabaseType == "mssql" ? (IDbConnection)_connection : _sqliteConnection;
        }


        public SqlConnection baglan
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException("Baglanti");
                }
                return _connection;
            }
        }


        public void Dispose()
        {
            if (!_disposed)
            {
                _connection?.Close();
                _connection?.Dispose();
                _sqliteConnection?.Close();
                _sqliteConnection?.Dispose();
                _disposed = true;
            }
        }
    }
}
