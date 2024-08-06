using Filip_Furniture.Data.Repositories.Interfaces;
using Filip_Furniture.Domain.Entities;
using MongoDB.Driver;

namespace Filip_Furniture.Data.Repositories.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMongoDBLogContext DbContext;
        public InventoryRepository(IMongoDBLogContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<bool> UpdateStockLevelAsync(string furnitureItemId, int newStockLevel)
        {
            var filter = Builders<FurnitureItem>.Filter.Eq(f => f.Id, furnitureItemId);
            var update = Builders<FurnitureItem>.Update.Set(f => f.StockQuantity, newStockLevel);
            var result = await DbContext.FurnitureItems.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<IEnumerable<FurnitureItem>> GetCurrentStockLevelsAsync()
        {
            return await DbContext.FurnitureItems.Find(FilterDefinition<FurnitureItem>.Empty).ToListAsync();
        }

    }
}
