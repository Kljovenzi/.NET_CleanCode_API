﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Domain.DataTransferObjects
{
    public class StockLevelRequest
    {
        public string FurnitureItemId { get; set; }
        public int NewStockLevel { get; set; }
    }
}
