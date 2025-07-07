using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string SizeName { get; set; }
        public int InventoryType { get; set; }
        public int InventoryId { get; set; }
    }
}
