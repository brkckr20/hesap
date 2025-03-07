using Dapper;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using FastReport.Utils;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hesap.DataAccess
{
    public class CrudRepository
    {
        private readonly IDbConnection _dbConnection;
        private int UserId = Properties.Settings.Default.Id;
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        string databaseTuru;
        public CrudRepository()
        {
            if (ayarlar.VeritabaniTuru() == "mssql")
            {
                _dbConnection = new SqlConnection(ayarlar.MssqlConnStr());
                databaseTuru = "mssql";
            }
            else
            {
                _dbConnection = new SQLiteConnection(ayarlar.SqliteConnStr());
                databaseTuru = "sqlite";
            }
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
            string query;
            string columns = string.Join(", ", parameters.Keys);
            string values = string.Join(", ", parameters.Keys.Select(k => "@" + k));
            if (this.databaseTuru == "mssql")
            {
                query = $"INSERT INTO {TableName} ({columns}) VALUES ({values}); SELECT SCOPE_IDENTITY();";
            }
            else
            {
                query = $"INSERT INTO {TableName} ({columns}) VALUES ({values}); SELECT last_insert_rowid();";
            }

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

        public T GetMaxRecord<T>(string TableName, string ColumnName)
        {
            string query = $"SELECT MAX({ColumnName}) FROM {TableName}";
            return _dbConnection.ExecuteScalar<T>(query);
        }

        public void ConfirmAndDeleteCard(string TableName, int id, Action successCallback)
        {
            if (id != 0)
            {
                if (bildirim.SilmeOnayı())
                {
                    this.Delete(TableName, id);
                    bildirim.SilmeBasarili();
                    successCallback?.Invoke();
                }
            }
            else
            {
                bildirim.Uyari("Kayıt silebilmek için öncelikle listeden bir kayıt seçmelisiniz!");
            }
        }

        public int GetCountByPrefix(string TableName, string ColumnName, string Prefix)
        {
            string checkQuery = $"SELECT COUNT(*) FROM {TableName} WHERE {ColumnName} LIKE @Prefix";
            return _dbConnection.ExecuteScalar<int>(checkQuery, new { Prefix = Prefix + "%" });
        }
        public int IfExistRecord(string tableName, string columnName, string columnValue)
        {
            string checkQuery = $"SELECT COUNT(1) FROM {tableName} WHERE {columnName} = @Value";
            return _dbConnection.ExecuteScalar<int>(checkQuery, new { Value = columnValue });
        }
        public string GetByCode(string ColumnName, string TableName, string ConditionName)
        {
            string query = $"SELECT {ColumnName} FROM {TableName} WHERE CombinedCode = @CombinedCode";
            return _dbConnection.ExecuteScalar<string>(query, new { CombinedCode = ConditionName });
        }
        public string GetNumaratorNotCondition(string FieldName,string TableName)
        {
            string query;
            if (this.databaseTuru == "mssql")
            {
                query = $"SELECT top 1 {FieldName} FROM {TableName} ORDER BY OrderNo desc";
            }
            else
            {
                query = $"SELECT {FieldName} FROM {TableName} ORDER BY OrderNo desc LIMIT 1";
            }
            var numarator = _dbConnection.QuerySingleOrDefault<string>(query);
            if (numarator != null)
            {
                return string.Format("{0:D8}", Convert.ToInt32(numarator) + 1);
            }
            else
            {
                return "00000001";
            }
        }
        // kullanıcının kolonlarını listeleme işlemidir
        public void GetUserColumns(GridView gridView, string ScreenName)
        {
            string query = $"Select ColumnName, Width,Hidden,Location from ColumnSelector where UserId = {UserId} and ScreenName = '{ScreenName}' order by Location";
            var liste = _dbConnection.Query<ColumnSelector>(query).ToList();
            foreach (GridColumn item in gridView.Columns)
            {
                var columnInfo = liste.FirstOrDefault(k => k.ColumnName == item.FieldName);
                if (columnInfo != null)
                {
                    item.Visible = columnInfo.Hidden;
                    item.Width = columnInfo.Width;
                }
                else
                {
                    item.Visible = true;
                }
            }
        }

        // kullanıcının kolonlarını kaydetme işlemidir
        public void SaveColumnStatus(GridView gridView, string ScreenName)
        {
            foreach (GridColumn col in gridView.Columns)
            {
                string columnCheckQuery = $"select count (*) from ColumnSelector where UserId = {this.UserId} and ScreenName = '{ScreenName}' and ColumnName = '{col.FieldName}'";
                var isExist = _dbConnection.ExecuteScalar<int>(columnCheckQuery);
                if (isExist > 0)
                {
                    string updateQuery = $@"update ColumnSelector set Width = {col.Width}, Hidden = {(col.Visible ? 1 : 0)}, 
                                            Location = {col.VisibleIndex} where UserId = {this.UserId} and ScreenName = '{ScreenName}' and ColumnName = '{col.FieldName}'";
                    _dbConnection.Execute(updateQuery);
                }
                else
                {
                    string insertQuery = $@"INSERT INTO ColumnSelector (UserId, ScreenName, ColumnName, Width, Hidden, Location)
                                                                VALUES ({this.UserId}, '{ScreenName}', '{col.FieldName}', {col.Width}, {(col.Visible ? 1 : 0)}, {col.VisibleIndex})";
                    _dbConnection.Execute(insertQuery);
                }
            }
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
