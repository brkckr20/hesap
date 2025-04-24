using Dapper;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Hesap.Context;
using Hesap.Models;
using Hesap.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Hesap.DataAccess
{
    public class CrudRepository
    {
        private readonly IDbConnection _dbConnection;
        private int UserId = CurrentUser.UserId;
        //private int UserId = Properties.Settings.Default.Id;
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
        public T GetByList<T>(string TableName, string KayitTipi, int id) //iler geri butonları için init edildi. (kartlar ve fiş başlık alanları için)
        {
            string query = this.databaseTuru == "mssql"
            ? $"SELECT TOP 1 * FROM {TableName} WHERE Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id ORDER BY Id {(KayitTipi == "Önceki" ? "DESC" : "ASC")}"
            : $"SELECT * FROM {TableName} WHERE Id {(KayitTipi == "Önceki" ? "<" : ">")} @Id ORDER BY Id {(KayitTipi == "Önceki" ? "DESC" : "ASC")} LIMIT 1";
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
        public int DeleteRows(string TableName, object id)
        {
            string query = $"DELETE FROM {TableName} WHERE ReceiptId = @Id";
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
        public string GetNumaratorWithCondition(string TableName, string FieldName, int Type)
        {
            string query;
            if (this.databaseTuru == "mssql")
            {
                query = $"SELECT top 1 {FieldName} FROM {TableName} WHERE Type = {Type} ORDER BY {FieldName} desc";
            }
            else
            {
                query = $"SELECT {FieldName} FROM {TableName} WHERE Type = {Type} ORDER BY {FieldName} desc LIMIT 1";
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
        public string GetInventoryNumerator(string TableName,string FieldName,int Type,string Prefix)
        {
            string query;
            if (this.databaseTuru == "mssql")
            {
                query = $"select top 1 {FieldName} from {TableName} where Type = {Type} order by {FieldName} desc";
            }
            else
            {
                query = $"select {FieldName} from {TableName} where Type = {Type} order by {FieldName} desc limit 1"; ;
            }
            var numarator = _dbConnection.QuerySingleOrDefault<string>(query);
            if (numarator != null)
            {
                string prefix = numarator.Substring(0, 3);
                string numberPart = numarator.Substring(3);
                int number = Convert.ToInt32(numberPart);
                int newNumber = number + 1;
                string newNumberPart = string.Format("{0:D3}", newNumber);
                string new_num = prefix + newNumberPart;
                return new_num;
            }
            else
            {
                return $"{Prefix}001";
            }
            /*
             string sorgum;
                using (var connection = new Baglanti().GetConnection())
                {
                    string mssql = "select top 1 InventoryCode from Inventory where Type = 4 order by InventoryCode desc";
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
             */
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
        public T GetByOtherCondition<T>(string TableName, string ConditionField, object id)
        {
            string query = $"SELECT * FROM {TableName} WHERE {ConditionField} = @Id";
            return _dbConnection.QueryFirstOrDefault<T>(query, new { Id = id });
        }
        public List<dynamic> GetAfterOrBeforeRecord(string query, int id)
        {
            return _dbConnection.Query(query, new { Id = id }).ToList();
        }
        public int? GetIdForAfterOrBeforeRecord(string KayitTipi, string TableName, int id, string TableName2, string TableName2Ref ,int ReceiptType)
        {
            string sql = KayitTipi == "Önceki"
                ? $"SELECT MAX(t1.Id) FROM {TableName} t1 inner join {TableName2} t2 on t1.Id=t2.{TableName2Ref} WHERE t1.Id < @Id and ReceiptType = {ReceiptType}"
                : $"SELECT MIN(t1.Id) FROM {TableName} t1 inner join {TableName2} t2 on t1.Id=t2.{TableName2Ref} WHERE t1.Id > @Id and ReceiptType = {ReceiptType}";

            return _dbConnection.QueryFirstOrDefault<int?>(sql, new { Id = id });
        }

    }
}
