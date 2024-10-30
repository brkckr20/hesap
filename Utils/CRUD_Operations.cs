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
       
    }
}
