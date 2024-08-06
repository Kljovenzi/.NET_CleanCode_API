using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Data.Repositories.Interfaces
{
    public interface IWishlistRepository
    {
        Task<WishlistItem> AddItemToWishlistAsync(string userId, string furnitureItemId);
        Task<bool> RemoveItemFromWishlistAsync(string userId, string furnitureItemId);
        Task<IEnumerable<WishlistItem>> GetUserWishlistAsync(string userId);
        Task<bool> ClearUserWishlistAsync(string userId);
    }
}
