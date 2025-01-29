using Dapper;
using Hesap.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.DataAccess
{
    public class UserRepository
    {
        private readonly string _connectionString = "Server=.;Database=Hesap;Trusted_Connection=True;";

        public IEnumerable<User> GetUsers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<User>("SELECT * FROM Users").ToList();
            }
        }
    }
}
