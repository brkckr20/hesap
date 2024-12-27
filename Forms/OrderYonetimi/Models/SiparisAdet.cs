using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Forms.OrderYonetimi.Models
{
    public class SiparisAdet
    {
        public int Id{ get; set; }
        public int SiparisId { get; set; }
        public int ModelId { get; set; }
        public int BedenId { get; set; }
        public int RenkId { get; set; }
        public decimal Miktar { get; set; }
        public string Beden { get; set; }
    }
}
