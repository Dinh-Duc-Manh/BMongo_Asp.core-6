using BMongo1.Models.ModelsViews;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MongoDB.Bson;
using MongoDB.Driver;
namespace BMongo1.Models.BusinessModels
{
    public class RepositoryOrder : IRepositoryOrder
    {
        MongoDbContext db;
        public RepositoryOrder(MongoDbContext db)
        {
            this.db = db;
        }
        public bool Delete(int id)
        {
            try
            {
                db.Orders.DeleteOne(x => x._id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Orders GetAccId(int id)
        {
            return db.Orders.Find(x => x._id == id).FirstOrDefault();
        }

        public List<Orders> GetAll()
        {
            return db.Orders.Find(FilterDefinition<Orders>.Empty).ToList();
        }

        public Orders GetById(int id)
        {
            return db.Orders.Find(x => x._id == id).FirstOrDefault();
        }

        public List<OrdersViewModel> GetForeignKey(int AccId)
        {
            BsonDocument[] lookup = new BsonDocument[]
            {
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
            var Orders = db.Orders.Aggregate<BsonDocument>(lookup).ToList();
            var data = new List<OrdersViewModel>();
            foreach (var e in Orders)
            {
                if (e["AccountId"].ToInt32() == AccId)
                {
                    var p = new OrdersViewModel();
                    p._id = e["_id"].ToInt32();
                    p.ReceiverName = e["ReceiverName"].ToString();
                    p.ReceiverPhone = e["ReceiverPhone"].ToString();
                    p.ReceiverAddress = e["ReceiverAddress"].ToString();
                    p.Note = e["Note"].ToString();
                    p.OrderStatus = e["status"].ToString();
                    p.AccountId = e["AccountId"].ToInt32();
                    p.Phone = e["Account"].AsBsonArray[0]["Phone"].ToString();
                    p.FullName = e["Account"].AsBsonArray[0]["FullName"].ToString();
                    p.Address = e["Account"].AsBsonArray[0]["Address"].ToString();
                    data.Add(p);
                }
                else
                {
                    continue;
                }

            }
            return data;
        }

        public bool Insert(Orders entity)
        {
            var sort = Builders<Orders>.Sort.Descending(p => p._id);
            var c = db.Orders.Find(FilterDefinition<Orders>.Empty).Sort(sort).FirstOrDefault();
            int rows = (int)db.Orders.CountDocuments(FilterDefinition<Orders>.Empty);
            if (rows > 0)
            {
                entity._id = (int)c._id + 1;
            }
            else
            {
                entity._id = 1;
            }
            db.Orders.InsertOne(entity);
            return true;
        }
        public int GetId()
        {
            var sort = Builders<Orders>.Sort.Descending(p => p._id);
            var c = db.Orders.Find(FilterDefinition<Orders>.Empty).Sort(sort).FirstOrDefault();
            return c._id;
        }
        public bool Update(Orders entity)
        {
            var p = Builders<Orders>.Update.Set("ReceiverName", entity.ReceiverName)
                .Set("ReceiverPhone", entity.ReceiverPhone)
                .Set("ReceiverAddress", entity.ReceiverAddress)
                .Set("OrderDate", entity.OrderDate)
                .Set("OrderStatus", entity.OrderStatus)
                .Set("AccountId", entity.AccountId);
            db.Orders.UpdateOne(x => x._id == entity._id, p);
            return true;
        }
    }
}
