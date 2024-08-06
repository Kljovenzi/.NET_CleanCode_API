using Filip_Furniture.Data.Repositories.Interfaces;
using Filip_Furniture.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Data.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoDBLogContext DbContext;
        public OrderRepository(IMongoDBLogContext dblogContext)
        {
            DbContext = DbContext;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            await DbContext.Orders.InsertOneAsync(order);
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            return await DbContext.Orders.Find(o => o.Id == orderId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await DbContext.Orders.Find(o => o.UserId == userId).ToListAsync();
        }
    }
}
