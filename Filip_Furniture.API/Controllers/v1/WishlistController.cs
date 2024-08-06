using Asp.Versioning;
using Filip_Furniture.Domain.DataTransferObjects;
using Filip_Furniture.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Filip_Furniture.API.Controllers.v1
{
    public class WishlistController : BaseController
    {
        private readonly IWishlistService wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            this.wishlistService = wishlistService;
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/add-item-to-wishlist")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> AddItemToWishlist(WishlistRequest request)
        {

            var response = await wishlistService.AddItemToWishlistAsync(request.UserId, request.FurnitureItemId);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpDelete("api/v{version:apiVersion}/[controller]/remove-item-from-Wishlist")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> RemoveItemFromWishlist(WishlistRequest request)
        {

            var response = await wishlistService.RemoveItemFromWishlistAsync(request.UserId, request.FurnitureItemId);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpGet("api/v{version:apiVersion}/[controller]/get-user-wishlist")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetUserWishlist(string userId)
        {

            var response = await wishlistService.GetUserWishlistAsync(userId);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }


        [HttpDelete("api/v{version:apiVersion}/[controller]/clear-user-wishlist")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> ClearUserWishlist(string userId)
        {

            var response = await wishlistService.ClearUserWishlistAsync(userId);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }
    }
}
