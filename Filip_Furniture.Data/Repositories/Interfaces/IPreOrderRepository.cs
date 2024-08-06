using Filip_Furniture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Data.Repositories.Interfaces
{
    public interface IPreOrderRepository
    {
        Task<PreOrder> CreatePreOrderAsync(PreOrder preOrder);
        Task<PreOrder> GetPreOrderByIdAsync(string preorderId);
        Task<IEnumerable<PreOrder>> GetPreOrdersByUserIdAsync(string userId);
    }
}
