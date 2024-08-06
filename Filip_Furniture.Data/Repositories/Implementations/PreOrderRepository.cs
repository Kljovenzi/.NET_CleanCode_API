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
    public class PreOrderRepository : IPreOrderRepository
    {
        private readonly IMongoDBLogContext DbContext;
        public PreOrderRepository(IMongoDBLogContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<PreOrder> CreatePreOrderAsync(PreOrder preOrder)
        {
            await DbContext.PreOrders.InsertOneAsync(preOrder);
            return preOrder;
        }

        public async Task<PreOrder> GetPreOrderByIdAsync(string preorderId)
        {
            return await DbContext.PreOrders.Find(po => po.Id == preorderId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PreOrder>> GetPreOrdersByUserIdAsync(string userId)
        {
            return await DbContext.PreOrders.Find(po => po.UserId == userId).ToListAsync();
        }
    }
}
