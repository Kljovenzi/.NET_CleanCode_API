using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Interfaces
{
    public interface IInventoryService
    {
        Task<NewResult<bool>> UpdateStockLevelAsync(string furnitureItemId, int newStockLevel);
        Task<NewResult<IEnumerable<FurnitureItem>>> GetCurrentStockLevelsAsync();
    }
}
