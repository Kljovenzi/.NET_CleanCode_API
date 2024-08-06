using Filip_Furniture.Data.Repositories.Interfaces;
using Filip_Furniture.Domain.Config.Interfaces;
using Filip_Furniture.Domain.Entities;
using MongoDB.Driver;

namespace Filip_Furniture.Data.Repositories.Implementations
{
    public partial class MongoDBLogContext : IMongoDBLogContext
    {
     
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

        public IMongoCollection<MyBankLog> Logs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMongoCollection<User> Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMongoCollection<Admin> Admins { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMongoCollection<FurnitureItem> FurnitureItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMongoCollection<ShoppingCart> ShoppingCarts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMongoCollection<WishlistItem> WishlistItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMongoCollection<Order> Orders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMongoCollection<PreOrder> PreOrders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IMongoCollection<CustomerInquiry> CustomerInquiries { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
