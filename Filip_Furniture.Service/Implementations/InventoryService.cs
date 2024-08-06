﻿using Filip_Furniture.Data.Repositories.Interfaces;
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
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository inventoryRepository;
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        public async Task<NewResult<IEnumerable<FurnitureItem>>> GetCurrentStockLevelsAsync()
        {
            try
            {
                var stockLevels = await inventoryRepository.GetCurrentStockLevelsAsync();

                if (stockLevels != null && stockLevels.Any())
                {
                    return NewResult<IEnumerable<FurnitureItem>>.Success(stockLevels, "Current stock levels retrieved successfully.");
                }
                else
                {
                    return NewResult<IEnumerable<FurnitureItem>>.Failed(null, "No stock levels found.");
                }
            }
            catch (Exception ex)
            {
                return NewResult<IEnumerable<FurnitureItem>>.Failed(null, $"Error occurred: {ex.Message}");
            }
        }

        public async Task<NewResult<bool>> UpdateStockLevelAsync(string furnitureItemId, int newStockLevel)
        {
            try
            {
                if (string.IsNullOrEmpty(furnitureItemId))
                    throw new ArgumentNullException(nameof(furnitureItemId), "Furniture item ID cannot be null or empty.");

                var result = await inventoryRepository.UpdateStockLevelAsync(furnitureItemId, newStockLevel);

                if (result)
                {
                    return NewResult<bool>.Success(true, "Stock level updated successfully.");
                }
                else
                {
                    return NewResult<bool>.Failed(false, "Failed to update stock level.");
                }
            }
            catch (Exception ex)
            {
                return NewResult<bool>.Failed(false, $"Error occurred: {ex.Message}");
            }
        }


    }
}
