using Asp.Versioning;
using Filip_Furniture.Domain.DataTransferObjects;
using Filip_Furniture.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Filip_Furniture.API.Controllers.v1
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost("api/v{version:apiVersion}/[controller]/create-order")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {

            var response = await orderService.CreateOrderAsync(request.UserId, request.items);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpGet("api/v{version:apiVersion}/[controller]/get-order-by-id")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {

            var response = await orderService.GetOrderByIdAsync(orderId);

            return response.ResponseCode switch
            {
                "00" => Ok(response),
                "99" => BadRequest(response),
                "77" => StatusCode(417, response), // DUPLICATE
                _ => StatusCode(500, response)
            };
        }

        [HttpGet("api/v{version:apiVersion}/[controller]/get-order-by-user-id")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {

            var response = await orderService.GetOrdersByUserIdAsync(userId);

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
