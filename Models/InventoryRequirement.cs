﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesap.Models
{
    public class InventoryRequirement
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
