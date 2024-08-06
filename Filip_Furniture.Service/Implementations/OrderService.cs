
using Filip_Furniture.Data.Repositories.Interfaces;
using Filip_Furniture.Domain.Common.Generics;
using Filip_Furniture.Domain.Entities;
using Filip_Furniture.Domain.Enum;
using Filip_Furniture.Service.Interfaces;

namespace Lacariz.Furniture.Service.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<NewResult<Order>> CreateOrderAsync(string userId, List<OrderItem> items)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentNullException(nameof(userId));

                if (items == null || items.Count == 0)
                    throw new ArgumentNullException(nameof(items));

                var order = new Order
                {
                    UserId = userId,
                    Items = items,
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    TotalAmount = items.Sum(i => i.Quantity * i.Price)
                };

                var createdOrder = await orderRepository.CreateOrderAsync(order);
                if (createdOrder != null)
                {
                    return NewResult<Order>.Success(createdOrder, "Order created successfully.");
                }

                return NewResult<Order>.Failed(null, "Unable to create order");
            }
            catch (Exception ex)
            {
                return NewResult<Order>.Error(null, $"Error occurred: {ex.Message}");
            }
        }

        public async Task<NewResult<Order>> GetOrderByIdAsync(string orderId)
        {
            try
            {
                var order = await orderRepository.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return NewResult<Order>.Failed(null, "Order not found.");
                }
                return NewResult<Order>.Success(order, "Order retrieved successfully.");
            }
            catch (Exception ex)
            {
                return NewResult<Order>.Error(null, $"Error occurred: {ex.Message}");
            }
        }

        public async Task<NewResult<IEnumerable<Order>>> GetOrdersByUserIdAsync(string userId)
        {
            try
            {
                var orders = await orderRepository.GetOrdersByUserIdAsync(userId);
                if (orders == null || !orders.Any())
                {
                    return NewResult<IEnumerable<Order>>.Failed(null, "No orders found.");
                }
                return NewResult<IEnumerable<Order>>.Success(orders, "Orders retrieved successfully.");
            }
            catch (Exception ex)
            {
                return NewResult<IEnumerable<Order>>.Error(null, $"Error occurred: {ex.Message}");
            }
        }
    }
}
