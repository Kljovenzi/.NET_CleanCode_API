using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Interfaces
{
    public interface IWishlistService
    {
        Task<NewResult<WishlistItem>> AddItemToWishlistAsync(string userId, string furnitureItemId);
        Task<NewResult<bool>> RemoveItemFromWishlistAsync(string userId, string furnitureItemId);
        Task<NewResult<IEnumerable<WishlistItem>>> GetUserWishlistAsync(string userId);
        Task<NewResult<bool>> ClearUserWishlistAsync(string userId);
    }
}
