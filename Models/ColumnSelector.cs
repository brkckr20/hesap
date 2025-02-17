using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Models
{
    public class ColumnSelector
    {
        public int Id { get; set; }

        [Display(Name = "Kolon Adı")]
        public string ColumnName { get; set; }

        [Display(Name = "Genişlik")]
        public int Width { get; set; }

        [Display(Name = "Gizli")]
        public bool Hidden{ get; set; }

        [Display(Name = "Konum")]
        public string Location { get; set; }

    }
}
