using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Interfaces
{
    public interface IOrderService
    {
        Task<NewResult<Order>> CreateOrderAsync(string userId, List<OrderItem> items);
        Task<NewResult<Order>> GetOrderByIdAsync(string orderId);
        Task<NewResult<IEnumerable<Order>>> GetOrdersByUserIdAsync(string userId);
    }
}
