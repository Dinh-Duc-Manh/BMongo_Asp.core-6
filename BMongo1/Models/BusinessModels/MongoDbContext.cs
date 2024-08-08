using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using BMongo1.Models;
namespace BMongo1.Models.BusinessModels
{
    public class MongoDbContext
    {
         IConfiguration Configuration;
        public MongoDbContext(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public IMongoDatabase Connection 
        {
            get
            {
                var client = new MongoClient(Configuration.GetConnectionString("MongoConnection"));
                var database = client.GetDatabase(Configuration.GetConnectionString("database"));
                return database;
            }

        }

        public IMongoCollection<Category> Categories => Connection.GetCollection<Category>("Categories");
        public IMongoCollection<Account> Account => Connection.GetCollection<Account>("Account");
        public IMongoCollection<Cart> Cart => Connection.GetCollection<Cart>("Cart");
        public IMongoCollection<Orders> Orders => Connection.GetCollection<Orders>("Orders");
        public IMongoCollection<OrderDetail> OrderDetail => Connection.GetCollection<OrderDetail>("OrderDetail");
        public IMongoCollection<Product> Products => Connection.GetCollection<Product>("Products");

    }
}
