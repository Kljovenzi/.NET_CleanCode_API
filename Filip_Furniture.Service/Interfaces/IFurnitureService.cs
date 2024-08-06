using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Interfaces
{
    public interface IFurnitureService
    {
        Task<NewResult<IEnumerable<FurnitureItem>>> GetAllFurnitureItemsAsync();
        Task<NewResult<FurnitureItem>> GetFurnitureItemByIdAsync(string furnitureItemId);
        Task<NewResult<IEnumerable<FurnitureItem>>> SearchFurnitureItemsAsync(FurnitureCategory category, decimal minPrice, decimal maxPrice, string keyword);
        Task<NewResult<FurnitureItem>> AddFurnitureItemAsync(FurnitureItem item);
        Task<NewResult<FurnitureItem>> UpdateFurnitureItemAsync(FurnitureItem item);
    }
}
