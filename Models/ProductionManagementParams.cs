using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Models
{
    public class ProductionManagementParams
    {
        public int Id { get; set; }

        [Display(Name = "Kasma Payı")]
        public float KasmaPayi { get; set; }
    }
}
