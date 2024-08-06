using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Interfaces
{
    public interface IPreOrderService
    {
        Task<NewResult<PreOrder>> CreatePreOrderAsync(string userId, string furnitureItemId, int quantity);
        Task<NewResult<PreOrder>> GetPreOrderByIdAsync(string preorderId);
        Task<NewResult<IEnumerable<PreOrder>>> GetPreOrdersByUserIdAsync(string userId);
    }
}
