using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.DataAccess
{
    public class CrudRepository
    {
        private readonly IDbConnection _dbConnection;
        public CrudRepository()
        {
            _dbConnection = new SqlConnection("Server=.;Database=Hesap;Trusted_Connection=True;");
        }

        public T GetById<T>(string TableName, object id)
        {
            string query = $"SELECT * FROM {TableName} WHERE Id = @Id";
            return _dbConnection.QueryFirstOrDefault<T>(query, new { Id = id });
        }

        public IEnumerable<T> GetAll<T>(string TableName)
        {
            string query = $"SELECT * FROM {TableName}";
            return _dbConnection.Query<T>(query);
        }
        public int Insert(string TableName, Dictionary<string, object> parameters)
        {
            string columns = string.Join(", ", parameters.Keys);
            string values = string.Join(", ", parameters.Keys.Select(k => "@" + k));
            string query = $"INSERT INTO {TableName} ({columns}) VALUES ({values}); SELECT SCOPE_IDENTITY();";
            return _dbConnection.ExecuteScalar<int>(query, parameters);
        }
        public int Update(string TableName, object id, Dictionary<string, object> parameters)
        {
            string setClause = string.Join(", ", parameters.Keys.Select(k => $"{k} = @{k}"));
            string query = $"UPDATE {TableName} SET {setClause} WHERE Id = @Id";
            parameters["Id"] = id;
            return _dbConnection.Execute(query, parameters);
        }

        public int Delete(string TableName, object id)
        {
            string query = $"DELETE FROM {TableName} WHERE Id = @Id";
            return _dbConnection.Execute(query, new { Id = id });
        }

        public void ExecuteInTransaction(Action<IDbTransaction> action)
        {
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    action(transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
