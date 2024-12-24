using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Forms.OrderYonetimi.Models
{
    public class RenkBedenDetaylari
    {
        public int Id { get; set; }
        public string UrunRengi { get; set; }
        public string BedenSeti { get; set; }
        public int Miktar { get; set; }
    }
}
