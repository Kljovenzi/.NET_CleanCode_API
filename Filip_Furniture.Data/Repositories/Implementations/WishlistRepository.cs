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
    public class WishlistRepository : IWishlistRepository
    {
        private readonly IMongoDBLogContext dbContext;

        public WishlistRepository(IMongoDBLogContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<WishlistItem> AddItemToWishlistAsync(string userId, string furnitureItemId)
        {
            var filter = Builders<WishlistItem>.Filter.And(
                Builders<WishlistItem>.Filter.Eq(w => w.UserId, userId),
                Builders<WishlistItem>.Filter.Eq(w => w.FurnitureItemId, furnitureItemId)
            );

            var existingWishlistItem = await dbContext.WishlistItems.Find(filter).FirstOrDefaultAsync();

            if (existingWishlistItem == null)
            {
                var wishlistItem = new WishlistItem
                {
                    UserId = userId,
                    FurnitureItemId = furnitureItemId
                };


                await dbContext.WishlistItems.InsertOneAsync(wishlistItem);

                return wishlistItem;
            }
            else
            {
                return existingWishlistItem;
            }
        }


        public async Task<bool> ClearUserWishlistAsync(string userId)
        {
            try
            {
                var filter = Builders<WishlistItem>.Filter.Eq(w => w.UserId, userId);
                var result = await dbContext.WishlistItems.DeleteManyAsync(filter);
                return result.IsAcknowledged && result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error occurred while clearing user wishlist: {ex.Message}");
                return false;
            }
        }


        public async Task<IEnumerable<WishlistItem>> GetUserWishlistAsync(string userId)
        {
            try
            {
                var filter = Builders<WishlistItem>.Filter.Eq(w => w.UserId, userId);
                var wishlistItems = await dbContext.WishlistItems.Find(filter).ToListAsync();
                return wishlistItems;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error occurred while retrieving user wishlist: {ex.Message}");
                throw;
            }
        }



        public async Task<bool> RemoveItemFromWishlistAsync(string userId, string furnitureItemId)
        {
            try
            {
                var filter = Builders<WishlistItem>.Filter.And(
         Builders<WishlistItem>.Filter.Eq(w => w.UserId, userId),
         Builders<WishlistItem>.Filter.Eq(w => w.FurnitureItemId, furnitureItemId)
     );

                var result = await dbContext.WishlistItems.DeleteOneAsync(filter);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error occurred while removing item from wishlist: {ex.Message}");
                throw;
            }
        }

    }
}
