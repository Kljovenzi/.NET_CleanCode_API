using Filip_Furniture.Data.Repositories.Interfaces;
using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Implementations
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository wishlistRepository;
        public WishlistService(IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
        }

        public async Task<NewResult<WishlistItem>> AddItemToWishlistAsync(string userId, string furnitureItemId)
        {
            NewResult<WishlistItem> result = new NewResult<WishlistItem>();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentNullException(nameof(userId));

                if (string.IsNullOrEmpty(furnitureItemId))
                    throw new ArgumentNullException(nameof(furnitureItemId));

                var addItem = await wishlistRepository.AddItemToWishlistAsync(userId, furnitureItemId);
                if (addItem != null)
                {
                    return NewResult<WishlistItem>.Success(addItem, "Item added to wishlist successfully");
                }
                return NewResult<WishlistItem>.Failed(null, "Item added to wishlist successfully");
            }
            catch (Exception ex)
            {

                return NewResult<WishlistItem>.Error(null, $"Error while clearing cart: {ex.Message}");
            }
        }

        public async Task<NewResult<bool>> ClearUserWishlistAsync(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentNullException(nameof(userId));

                bool success = await wishlistRepository.ClearUserWishlistAsync(userId);
                if (!success)
                {

                    return NewResult<bool>.Failed(false, "Error while clearing wishlist");

                }
                return NewResult<bool>.Success(true, "Wishlist cleared successfully");
            }
            catch (Exception ex)
            {
                return NewResult<bool>.Error(false, $"Error while clearing cart: {ex.Message}");
            }
        }

        public async Task<NewResult<IEnumerable<WishlistItem>>> GetUserWishlistAsync(string userId)
        {
            try
            {
                var wishlistItems = await wishlistRepository.GetUserWishlistAsync(userId);

                if (wishlistItems != null)
                {
                    return NewResult<IEnumerable<WishlistItem>>.Success(wishlistItems, "User wishlist retrieved successfully.");
                }
                else
                {

                    return NewResult<IEnumerable<WishlistItem>>.Failed(null, "Wishlist not found.");
                }
            }
            catch (Exception ex)
            {

                return NewResult<IEnumerable<WishlistItem>>.Error(null, $"An error occured while retrieving user wishlist: {ex.Message}");
            }
        }

        public async Task<NewResult<bool>> RemoveItemFromWishlistAsync(string userId, string furnitureItemId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");

                if (string.IsNullOrEmpty(furnitureItemId))
                    throw new ArgumentNullException(nameof(furnitureItemId), "Furniture item ID cannot be null or empty.");

                bool success = await wishlistRepository.RemoveItemFromWishlistAsync(userId, furnitureItemId);

                if (success)
                {
                    return NewResult<bool>.Success(true, "Item removed from wishlist successfully.");
                }
                else
                {
                    return NewResult<bool>.Failed(false, "Failed to remove item from wishlist.");
                }
            }
            catch (Exception ex)
            {
                return NewResult<bool>.Error(false, $"Error occurred: {ex.Message}");
            }
        }

    }
}
