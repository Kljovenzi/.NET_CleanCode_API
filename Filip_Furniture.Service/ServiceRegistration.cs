using Filip_Furniture.Service.External_Services.Implementations;
using Filip_Furniture.Service.External_Services.Interfaces;
using Filip_Furniture.Service.Helpers.Implementations;
using Filip_Furniture.Service.Helpers.Interfaces;
using Filip_Furniture.Service.Implementations;
using Filip_Furniture.Service.Interfaces;
using Lacariz.Furniture.Service.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Service
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ISampleService, SampleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IFurnitureService, FurnitureService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPreOrderService, PreOrderService>();
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
            services.AddScoped<ICustomerSupportService, CustomerSupportService>();
            services.AddScoped<IPaystackService, PaystackService>();
            services.AddScoped<IFlutterwaveService, FlutterwaveService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddAutoMapper(typeof(ServiceRegistration));



            return services;

        }

    }
}
