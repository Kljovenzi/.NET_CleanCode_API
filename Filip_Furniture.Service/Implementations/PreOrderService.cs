using Filip_Furniture.Data.Repositories.Interfaces;
using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Domain.Enum;
using Filip_Furniture.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service.Implementations
{
    public class PreOrderService : IPreOrderService
    {
        private readonly IPreOrderRepository preOrderRepository;
        public PreOrderService(IPreOrderRepository preOrderRepository)
        {
            this.preOrderRepository = preOrderRepository;
        }
        public async Task<NewResult<PreOrder>> CreatePreOrderAsync(string userId, string furnitureItemId, int quantity)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentNullException(nameof(userId));

                if (string.IsNullOrEmpty(furnitureItemId))
                    throw new ArgumentNullException(nameof(furnitureItemId));

                var preOrder = new PreOrder
                {
                    UserId = userId,
                    FurnitureItemId = furnitureItemId,
                    PreOrderDate = DateTime.UtcNow,
                    Status = PreOrderStatus.Pending,
                    Quantity = quantity
                };

                var createdPreOrder = await preOrderRepository.CreatePreOrderAsync(preOrder);

                return NewResult<PreOrder>.Success(createdPreOrder, "Pre-order created successfully.");
            }
            catch (Exception ex)
            {
                return NewResult<PreOrder>.Failed(null, $"Error occurred: {ex.Message}");
            }
        }

        public async Task<NewResult<PreOrder>> GetPreOrderByIdAsync(string preorderId)
        {
            try
            {
                var preOrder = await preOrderRepository.GetPreOrderByIdAsync(preorderId);
                if (preOrder == null)
                {
                    return NewResult<PreOrder>.Failed(null, "Pre-order not found.");
                }
                return NewResult<PreOrder>.Success(preOrder, "Pre-order retrieved successfully.");
            }
            catch (Exception ex)
            {
                return NewResult<PreOrder>.Failed(null, $"Error occurred: {ex.Message}");
            }
        }

        public async Task<NewResult<IEnumerable<PreOrder>>> GetPreOrdersByUserIdAsync(string userId)
        {
            try
            {
                var preOrders = await preOrderRepository.GetPreOrdersByUserIdAsync(userId);
                if (preOrders == null || !preOrders.Any())
                {
                    return NewResult<IEnumerable<PreOrder>>.Failed(null, "No pre-orders found.");
                }
                return NewResult<IEnumerable<PreOrder>>.Success(preOrders, "Pre-orders retrieved successfully.");
            }
            catch (Exception ex)
            {
                return NewResult<IEnumerable<PreOrder>>.Failed(null, $"Error occurred: {ex.Message}");
            }
        }
    }
}
