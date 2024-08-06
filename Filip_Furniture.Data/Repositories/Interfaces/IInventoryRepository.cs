using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Data.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<bool> UpdateStockLevelAsync(string furnitureItemId, int newStockLevel);
        Task<IEnumerable<FurnitureItem>> GetCurrentStockLevelsAsync();
    }
}
