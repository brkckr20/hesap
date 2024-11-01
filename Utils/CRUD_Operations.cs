using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Utils
{
    public class CRUD_Operations
    {
        Bildirim bildirim = new Bildirim();
        Ayarlar ayarlar = new Ayarlar();
        private int ExecuteSql(string sql, object parameters)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                return connection.ExecuteScalar<int>(sql, parameters);
            }
        }
        private void ExecuteNonQuery(string sql, object parameters)
        {
            using (var connection = new Baglanti().GetConnection())
            {
                connection.Execute(sql, parameters);
            }
        }
        public void KartSil(int Id, string TabloAdi)
        {
            if (Id != 0)
            {
                string sql = $"DELETE FROM {TabloAdi} WHERE Id = @Id";
                using (var connection = new Baglanti().GetConnection())
                {
                    if (bildirim.SilmeOnayı())
                    {
                        connection.Execute(sql, new { Id = Id });
                        bildirim.SilmeBasarili();
                    }
                }
            }
            else
            {
                bildirim.Uyari("Kayıt silebilmek için öncelikle listeden bir kayıt seçmelisiniz!");
            }
        }
        public void FisVeHavuzSil(string tableOne,string tableTwo,int Id)
        {
            if (bildirim.SilmeOnayı())
            {
                using (var connection = new Baglanti().GetConnection())
                {
                    string d1 = $"delete from {tableOne} where Id = @Id";
                    string d2 = $"delete from {tableTwo} where RefNo = @Id";
                    connection.Execute(d1, new { Id = Id });
                    connection.Execute(d2, new { Id = Id });
                    bildirim.SilmeBasarili();
                }
            }
        }

        public int InsertRecord(string tableName, IDictionary<string, object> parameters)
        {
            if (parameters.ContainsKey("Id"))
            {
                parameters.Remove("Id"); // Id parametresini kaldır
            }
            var columns = string.Join(", ", parameters.Keys);
            var values = string.Join(", ", parameters.Keys.Select(k => "@" + k));
            var sql = $"INSERT INTO {tableName} ({columns}) OUTPUT INSERTED.Id VALUES ({values})";

            return ExecuteSql(sql, parameters);
        }
        public void UpdateRecord(string tableName, IDictionary<string, object> parameters, int id)
        {
            var setClause = string.Join(", ", parameters.Keys.Select(k => $"{k} = @{k}"));
            var sql = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";

            // Id'yi parametre olarak ekleyelim
            parameters.Add("Id", id);

            ExecuteNonQuery(sql, parameters);
        }


    }
}
