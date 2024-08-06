using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Domain.DataTransferObjects
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; }
        public List<OrderItem> items { get; set; }
    }
}
