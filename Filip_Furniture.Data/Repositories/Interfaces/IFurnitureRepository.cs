using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Data.Repositories.Interfaces
{
    public interface IFurnitureRepository
    {
        Task<IEnumerable<FurnitureItem>> GetAllFurnitureItemsAsync();
        Task<FurnitureItem> GetFurnitureItemByIdAsync(string furnitureItemId);
        Task<IEnumerable<FurnitureItem>> SearchFurnitureItemsAsync(FurnitureCategory? category, decimal minPrice, decimal maxPrice, string keyword);
        Task<FurnitureItem> AddFurnitureItemAsync(FurnitureItem item);
        Task<bool> UpdateFurnitureItemAsync(FurnitureItem item);
    }
}
