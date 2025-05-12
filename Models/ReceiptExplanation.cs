using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Models
{
    public class ReceiptExplanation
    {
        public int Id { get; set; }
        public string Explanation { get; set; }
        public int ReceiptType { get; set; }        
    }
}
