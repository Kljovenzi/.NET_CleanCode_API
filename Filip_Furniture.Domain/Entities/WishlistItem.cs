﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Domain.Entities
{
    public class WishlistItem
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FurnitureItemId { get; set; }
    }
}
