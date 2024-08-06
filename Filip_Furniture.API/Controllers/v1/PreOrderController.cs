using Asp.Versioning;
using Filip_Furniture.Domain.DataTransferObjects;
using Filip_Furniture.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Filip_Furniture.API.Controllers.v1
{
    public class PreOrderController : BaseController
    {
        private readonly IPreOrderService preOrderService;
        public PreOrderController(IPreOrderService preOrderService)
        {
            this.preOrderService = preOrderService;
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/create-pre-order")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> CreatePreOrder(CreatePreOrderRequest request)
        {

            var response = await preOrderService.CreatePreOrderAsync(request.UserId, request.FurnitureItemId, request.Quantity);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpGet("api/v{version:apiVersion}/[controller]/get-pre-order-by-id")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetPreOrderById(string orderId)
        {

            var response = await preOrderService.GetPreOrderByIdAsync(orderId);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpGet("api/v{version:apiVersion}/[controller]/get-pre-order-by-user-id")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetPreOrdersByUserId(string userId)
        {

            var response = await preOrderService.GetPreOrdersByUserIdAsync(userId);

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
