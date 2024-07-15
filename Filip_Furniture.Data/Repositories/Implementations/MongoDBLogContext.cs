using Filip_Furniture.Data.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filip_Furniture.Data.Repositories.Implementations
{
    public partial class MongoDBLogContext : IMongoDBLogContext
    {
        IMongoCollection<MyBankLog> IMongoDBLogContext.Logs { get; set; }
        IMongoCollection<User> IMongoDBLogContext.Users { get ; set; }
        IMongoCollection<Admin> IMongoDBLogContext.Admins { get ; set ; }
        IMongoCollection<FurnitureItem> IMongoDBLogContext.FurnitureItems { get ; set ; }
        IMongoCollection<ShoppingCart> IMongoDBLogContext.ShoppingCarts { get ; set => throw new NotImplementedException(); }
        IMongoCollection<WishlistItem> IMongoDBLogContext.WishlistItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IMongoCollection<Order> IMongoDBLogContext.Orders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IMongoCollection<PreOrder> IMongoDBLogContext.PreOrders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IMongoCollection<CustomerInquiry> IMongoDBLogContext.CustomerInquiries { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public MongoDBLogContext(IMongoDbConfig config, IMongoClient mongoClient)
        {
            var client = new MongoClient(config.ConnectionString);
            var database = client.GetDatabase(config.DatabaseName);

            Logs = database.GetCollection<MyBankLog>("MyBankLog");
            Users = database.GetCollection<User>("User");
            Admins = database.GetCollection<Admin>("Admin");
            FurnitureItems = database.GetCollection<FurnitureItem>("FurnitureItem");
            ShoppingCarts = database.GetCollection<ShoppingCart>("ShoppingCartItem");
            WishlistItems = database.GetCollection<WishlistItem>("WishlistItem");
            Orders = database.GetCollection<Order>("Order");
            PreOrders = database.GetCollection<PreOrder>("PreOrder");
            CustomerInquiries = database.GetCollection<CustomerInquiry>("CustomerInquiry");

        }

    }
}
