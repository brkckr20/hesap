using Dapper;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.Models;
using Hesap.Utils;
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
        private int UserId = Properties.Settings.Default.Id;
        Bildirim bildirim = new Bildirim();
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

        public T GetMaxRecord<T>(string TableName,string ColumnName)
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
