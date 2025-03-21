using Hesap.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Utils
{
    public class KayitGetir
    {
        CrudRepository crudRepository = new CrudRepository();
        Bildirim bildirim = new Bildirim();

        //public T TipeGoreGetir<T>(string TableName, int RecId,string KayitTipi) where T : class, new()
        //{
        //    if (RecId == 0)
        //    {
        //        bildirim.Uyari("Kayıt gösterebilmek için öncelikle listeden bir kayıt getirmelisiniz!");
        //        return null;
        //    }

        //}
    }
}
