using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Hesap.Utils
{
    public class DbConnect : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public DbConnect()
        {
            _connectionString = "Server=.;Database=Hesap;Trusted_Connection=True;";
            _connection = new SqlConnection(_connectionString);
        }

        public IDbConnection GetConnection()
        {
            try
            {
                // Bağlantı kapalıysa aç
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }

                return _connection;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operation error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
