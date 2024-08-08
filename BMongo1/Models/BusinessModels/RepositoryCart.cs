using BMongo1.Models.ModelsViews;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MongoDB.Bson;
using MongoDB.Driver;
namespace BMongo1.Models.BusinessModels
{
    public class RepositoryCart : IRepositoryCart
    {
        MongoDbContext db;
        public RepositoryCart(MongoDbContext db)
        {
            this.db = db;
        }

        public List<Cart> All()
        {
            return db.Cart.Find(FilterDefinition<Cart>.Empty).ToList();
        }

        public bool Delete(int id)
        {

            try
            {
                db.Cart.DeleteOne(x => x._id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAll(int AccId)
        {
            try
            {
                db.Cart.DeleteMany(x => x.AccountId == AccId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public Cart GetAccId(int AccId)
        {
            return db.Cart.Find(x => x.AccountId == AccId).FirstOrDefault();
            
        }

        public Cart GetById(int id)
        {
            return db.Cart.Find(x => x._id == id).FirstOrDefault();
        }

        public List<CartViewModel> GetForeignKey(int AccId)
        {
            BsonDocument[] lookup = new BsonDocument[2]
             {
                new BsonDocument
                {
                    {
                        "$lookup", new BsonDocument
                        {
                            {"from" ,"Products"},
                            {"localField" ,"ProductId"},
                            {"foreignField" ,"_id"},
                            {"as" ,"Products"}
                        }
                    }
                },
                new BsonDocument
                {
                    {
                        "$lookup", new BsonDocument
                        {
                            {"from" ,"Account"},
                            {"localField" ,"AccountId"},
                            {"foreignField" ,"_id"},
                            {"as" ,"Account"}
                        }
                    }
                }
             };
            var cart = db.Cart.Aggregate<BsonDocument>(lookup).ToList();
            var data = new List<CartViewModel>();
            foreach (var e in cart)
            {
                if (e["AccountId"].ToInt32() == AccId)
                {
                    var p = new CartViewModel();
                    p._id = e["_id"].ToInt32();
                    p.Quantity = e["Quantity"].ToInt32();
                    p.TotalPrice = e["TotalPrice"].ToInt32();
                    p.ProductId = e["ProductId"].ToString();
                    p.ProductName = e["Products"].AsBsonArray[0]["ProductName"].ToString();
                    p.ProductImage = e["Products"].AsBsonArray[0]["ProductImage"].ToString();
                    p.Price = e["Products"].AsBsonArray[0]["Price"].ToInt32();
                    p.AccountId = e["AccountId"].ToInt32();
                    data.Add(p);
                }
                else
                {
                    continue;
                }

            }
            return data;
        }

        public bool Insert(Cart entity)
        {
            var sort = Builders<Cart>.Sort.Descending(p => p._id);
            var c = db.Cart.Find(FilterDefinition<Cart>.Empty).Sort(sort).FirstOrDefault();
            int rows = (int)db.Cart.CountDocuments(FilterDefinition<Cart>.Empty);
            if (rows > 0)
            {
                entity._id = (int)c._id + 1;
            }
            else
            {
                entity._id = 1;
            }
            db.Cart.InsertOne(entity);
            return true;
        }

        public bool Update(Cart entity)
        {
            var p = Builders<Cart>.Update.Set("Quantity", entity.Quantity)
            .Set("TotalPrice", entity.TotalPrice)
            .Set("ProductId", entity.ProductId)
            .Set("AccountId", entity.AccountId);
            db.Cart.UpdateOne(x => x._id == entity._id, p);
            return true;
        }
    }
}
