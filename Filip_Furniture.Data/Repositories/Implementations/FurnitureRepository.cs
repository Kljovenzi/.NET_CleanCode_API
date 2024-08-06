using Filip_Furniture.Data.Repositories.Interfaces;
using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Domain.Enum;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Data.Repositories.Implementations
{
    public class FurnitureRepository : IFurnitureRepository
    {
        private readonly IMongoDBLogContext dbContext;
        public FurnitureRepository(IMongoDBLogContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<FurnitureItem> AddFurnitureItemAsync(FurnitureItem item)
        {
            await dbContext.FurnitureItems.InsertOneAsync(item);
            return item;
        }

        public async Task<IEnumerable<FurnitureItem>> GetAllFurnitureItemsAsync()
        {
            var furnitures = await dbContext.FurnitureItems.FindAsync(_ => true);
            return await furnitures.ToListAsync();
        }

        public async Task<FurnitureItem> GetFurnitureItemByIdAsync(string furnitureItemId)
        {
            return await dbContext.FurnitureItems.Find(item => item.Id == furnitureItemId).FirstOrDefaultAsync();
        }

        //public async Task<IEnumerable<FurnitureItem>> SearchFurnitureItemsAsync(string category, decimal minPrice, decimal maxPrice, string keyword)
        //{
        //    var filterBuilder = Builders<FurnitureItem>.Filter;
        //    var filter = filterBuilder.Empty;

        //    if (!string.IsNullOrWhiteSpace(category))
        //        filter &= filterBuilder.Eq(item => item.Category, category);

        //    if (minPrice >= 0 && maxPrice > minPrice)
        //        filter &= filterBuilder.Gte(item => item.Price, minPrice) & filterBuilder.Lte(item => item.Price, maxPrice);

        //    if (!string.IsNullOrWhiteSpace(keyword))
        //        filter &= filterBuilder.Text(keyword);

        //    return await dbContext.FurnitureItems.Find(filter).ToListAsync();
        //}

        public async Task<IEnumerable<FurnitureItem>> SearchFurnitureItemsAsync(FurnitureCategory? category, decimal minPrice, decimal maxPrice, string keyword)
        {
            var filterBuilder = Builders<FurnitureItem>.Filter;
            var filter = filterBuilder.Empty;

            if (category.HasValue)
                filter &= filterBuilder.Eq(item => item.Category, category);

            if (minPrice >= 0 && maxPrice > minPrice)
                filter &= filterBuilder.Gte(item => item.Price, minPrice) & filterBuilder.Lte(item => item.Price, maxPrice);

            if (!string.IsNullOrWhiteSpace(keyword))
                filter &= filterBuilder.Text(keyword);

            return await dbContext.FurnitureItems.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateFurnitureItemAsync(FurnitureItem item)
        {
            var filter = Builders<FurnitureItem>.Filter.Eq(f => f.Id, item.Id);
            var update = Builders<FurnitureItem>.Update
                .Set(f => f.Name, item.Name)
                .Set(f => f.Description, item.Description)
                .Set(f => f.Price, item.Price)
                .Set(f => f.StockQuantity, item.StockQuantity)
                .Set(f => f.Category, item.Category);

            var result = await dbContext.FurnitureItems.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}
