using Asp.Versioning;
using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Domain.Enum;
using Filip_Furniture.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Filip_Furniture.API.Controllers.v1
{
    public class ProductController : BaseController
    {
        private readonly IFurnitureService furnitureService;
        //private readonly IShoppingCartService shoppingCartService;
        public ProductController(IFurnitureService furnitureService/*, IShoppingCartService shoppingCartService*/)
        {
            this.furnitureService = furnitureService;
            //this.shoppingCartService = shoppingCartService;
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/admin/add-products")]
        [Authorize(Policy = "AdminOnly")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> AddFurnitureItems([FromBody] FurnitureItem item)
        {

            var response = await furnitureService.AddFurnitureItemAsync(item);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpGet("api/v{version:apiVersion}/[controller]/get-all-products")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetAllItems()
        {

            var response = await furnitureService.GetAllFurnitureItemsAsync();

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpGet("api/v{version:apiVersion}/[controller]/get-product-by-id")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetItemById(string productId)
        {

            var response = await furnitureService.GetFurnitureItemByIdAsync(productId);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/admin/update-product")]
        [Authorize(Policy = "AdminOnly")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> UpdateFurnitureItems([FromBody] FurnitureItem item)
        {

            var response = await furnitureService.UpdateFurnitureItemAsync(item);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/search-product")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> SearchProduct(FurnitureCategory category, decimal minPrice, decimal maxPrice, string keyword)
        {

            var response = await furnitureService.SearchFurnitureItemsAsync(category, minPrice, maxPrice, keyword);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        //[HttpPost("api/v{version:apiVersion}/[controller]/add-item-to-cart")]
        //[ApiVersion("1.0")]
        //public async Task<IActionResult> AddItemToCart(string userId, ShoppingCartItem item)
        //{

        //    var response = await shoppingCartService.AddItemToCartAsync(userId, item);

        //    return response.ResponseCode switch
        //    {
        //        "00" => Ok(response),
        //        "99" => BadRequest(response),
        //        "77" => StatusCode(417, response), // DUPLICATE
        //        _ => StatusCode(500, response)
        //    };
        //}

        //[HttpDelete("api/v{version:apiVersion}/[controller]/clear-cart-items")]
        //[ApiVersion("1.0")]
        //public async Task<IActionResult> ClearCartAsync(string userId)
        //{

        //    var response = await shoppingCartService.ClearCartAsync(userId);

        //    return response.ResponseCode switch
        //    {
        //        "00" => Ok(response),
        //        "99" => BadRequest(response),
        //        "77" => StatusCode(417, response), // DUPLICATE
        //        _ => StatusCode(500, response)
        //    };
        //}

        //[HttpDelete("api/v{version:apiVersion}/[controller]/delete-cart-item")]
        //[ApiVersion("1.0")]
        //public async Task<IActionResult> DeleteItem(string userId, string productId)
        //{

        //    var response = await shoppingCartService.DeleteItemAsync(userId, productId);

        //    return response.ResponseCode switch
        //    {
        //        "00" => Ok(response),
        //        "99" => BadRequest(response),
        //        "77" => StatusCode(417, response), // DUPLICATE
        //        _ => StatusCode(500, response)
        //    };
        //}

        //    [HttpGet("api/v{version:apiVersion}/[controller]/get-cart-item")]
        //    [ApiVersion("1.0")]
        //    public async Task<IActionResult> GetCartItem(string userId, string productId)
        //    {

        //        var response = await shoppingCartService.GetCartItemAsync(userId, productId);

        //        return response.ResponseCode switch
        //        {
        //            "00" => Ok(response),
        //            "99" => BadRequest(response),
        //            "77" => StatusCode(417, response), // DUPLICATE
        //            _ => StatusCode(500, response)
        //        };
        //    }

        //    [HttpGet("api/v{version:apiVersion}/[controller]/get-all-cart-items")]
        //    [ApiVersion("1.0")]
        //    public async Task<IActionResult> GetCartItems(string userId)
        //    {

        //        var response = await shoppingCartService.GetCartItemsAsync(userId);

        //        return response.ResponseCode switch
        //        {
        //            "00" => Ok(response),
        //            "99" => BadRequest(response),
        //            "77" => StatusCode(417, response), // DUPLICATE
        //            _ => StatusCode(500, response)
        //        };
        //    }
        //}
    }
}
