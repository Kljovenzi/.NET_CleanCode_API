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
    public class FurnitureService : IFurnitureService
    {
        private readonly IFurnitureRepository furnitureRepository;
        public FurnitureService(IFurnitureRepository furnitureRepository)
        {
            this.furnitureRepository = furnitureRepository;
        }

        public async Task<NewResult<FurnitureItem>> AddFurnitureItemAsync(FurnitureItem item)
        {
            try
            {
                var newItem = await furnitureRepository.AddFurnitureItemAsync(item);
                if (newItem != null)
                {
                    return NewResult<FurnitureItem>.Success(newItem, "Furniture item added successfully.");
                }
                return NewResult<FurnitureItem>.Failed(null, "Failure to add item.");
            }
            catch (Exception ex)
            {
                return NewResult<FurnitureItem>.Error(null, $"Error occurred: {ex.Message}");
            }
        }

        public async Task<NewResult<IEnumerable<FurnitureItem>>> GetAllFurnitureItemsAsync()
        {
            try
            {
                var furnitureItems = await furnitureRepository.GetAllFurnitureItemsAsync();
                /// return furnitureItems ?? Enumerable.Empty<FurnitureItem>();
                if (furnitureItems == null)
                {
                    return NewResult<IEnumerable<FurnitureItem>>.Failed(null, "Kindly add items");
                }
                return NewResult<IEnumerable<FurnitureItem>>.Success(furnitureItems, "Items retrieved successfully");
            }
            catch (Exception ex)
            {

                return NewResult<IEnumerable<FurnitureItem>>.Error(null, $"Error retrieving items: {ex.Message}");
            }
        }

        public async Task<NewResult<FurnitureItem>> GetFurnitureItemByIdAsync(string furnitureItemId)
        {
            try
            {
                var furnitureItem = await furnitureRepository.GetFurnitureItemByIdAsync(furnitureItemId);
                if (furnitureItem == null)
                {
                    return NewResult<FurnitureItem>.Failed(null, "Item doesn't exist");
                }
                return NewResult<FurnitureItem>.Success(furnitureItem, "Item retrieved successfully");
            }
            catch (Exception ex)
            {

                return NewResult<FurnitureItem>.Error(null, $"Error while retrieving item: {ex.Message} ");
            }
        }

        public async Task<NewResult<IEnumerable<FurnitureItem>>> SearchFurnitureItemsAsync(FurnitureCategory category, decimal minPrice, decimal maxPrice, string keyword)
        {
            try
            {
                var searchResult = await furnitureRepository.SearchFurnitureItemsAsync(category, minPrice, maxPrice, keyword);
                if (searchResult == null)
                {
                    return NewResult<IEnumerable<FurnitureItem>>.Failed(null, "unable to provide search results");
                }
                return NewResult<IEnumerable<FurnitureItem>>.Success(searchResult, "Search carried out successfully");
            }
            catch (Exception ex)
            {

                return NewResult<IEnumerable<FurnitureItem>>.Error(null, $"An error occured while carrying out search: {ex.Message}");
            }
        }

        public async Task<NewResult<FurnitureItem>> UpdateFurnitureItemAsync(FurnitureItem item)
        {
            try
            {
                var updated = await furnitureRepository.UpdateFurnitureItemAsync(item);
                if (updated)
                {
                    return NewResult<FurnitureItem>.Success(item, "Furniture item updated successfully.");
                }
                else
                {
                    return NewResult<FurnitureItem>.Failed(null, "Failed to update furniture item.");
                }
            }
            catch (Exception ex)
            {
                return NewResult<FurnitureItem>.Failed(null, $"Error occurred: {ex.Message}");
            }
        }
    }
}
