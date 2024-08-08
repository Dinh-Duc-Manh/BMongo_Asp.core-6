using BMongo1.Models.ModelsViews;
using MongoDB.Bson;
using MongoDB.Driver;
namespace BMongo1.Models.BusinessModels
{
    public class RepositoryOrderDetail : IRepositoryOrderDetail
    {
        MongoDbContext db;
        public RepositoryOrderDetail(MongoDbContext db)
        {
            this.db = db;
        }
        public bool Delete(int id)
        {
            try
            {
                db.OrderDetail.DeleteOne(x => x._id == id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public OrderDetail GetAccId(int id)
        {
            return db.OrderDetail.Find(x => x._id == id).FirstOrDefault();
        }

        public List<OrderDetail> GetAll()
        {
            return db.OrderDetail.Find(FilterDefinition<OrderDetail>.Empty).ToList();
        }

        public OrderDetail GetById(int id)
        {
            return db.OrderDetail.Find(x => x._id == id).FirstOrDefault();
        }

        public List<OrderDetailViewModel> GetForeignKey(int OdId)
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
                            {"from" ,"Orders"},
                            {"localField" ,"OrdersId"},
                            {"foreignField" ,"_id"},
                            {"as" ,"Orders"}
                        }
                    }
                }
            };
            var OrderDetail = db.OrderDetail.Aggregate<BsonDocument>(lookup).ToList();
            var data = new List<OrderDetailViewModel>();
            foreach (var e in OrderDetail)
            {
                if (e["OrdersId"].ToInt32() == OdId)
                {
                    var p = new OrderDetailViewModel();
                    p._id = e["_id"].ToInt32();
                    p.Quantity = e["Quantity"].ToInt32();
                    p.TotalPrice = e["TotalPrice"].ToInt32();
                    p.ProductId = e["ProductId"].ToString();
                    p.ProductName = e["Products"].AsBsonArray[0]["ProductName"].ToString();
                    p.ProductImage = e["Products"].AsBsonArray[0]["ProductImage"].ToString();
                    p.Price = e["Products"].AsBsonArray[0]["Price"].ToInt32();

                    p.OrdersId = e["OrdersId"].ToInt32();
                    p.ReceiverName = e["Orders"].AsBsonArray[0]["ReceiverName"].ToString();
                    p.ReceiverPhone = e["Orders"].AsBsonArray[0]["ReceiverPhone"].ToString();
                    p.ReceiverAddress = e["Orders"].AsBsonArray[0]["ReceiverAddress"].ToString();
                    p.Note = e["Orders"].AsBsonArray[0]["Note"].ToString();
                    p.OrderDate = e["Orders"].AsBsonArray[0]["OrderDate"].ToString();
                    p.OrderStatus = e["Orders"].AsBsonArray[0]["OrderStatus"].ToString();
                    p.AccountId = e["Orders"].AsBsonArray[0]["AccountId"].ToInt32();
                    data.Add(p);
                }
                else
                {
                    continue;
                }

            }
            return data;
        }

        public bool Insert(OrderDetail entity)
        {
            var sort = Builders<OrderDetail>.Sort.Descending(p => p._id);
            var c = db.OrderDetail.Find(FilterDefinition<OrderDetail>.Empty).Sort(sort).FirstOrDefault();
            int rows = (int)db.OrderDetail.CountDocuments(FilterDefinition<OrderDetail>.Empty);
            if (rows > 0)
            {
                entity._id = (int)c._id + 1;
            }
            else
            {
                entity._id = 1;
            }
            db.OrderDetail.InsertOne(entity);
            return true;
        }

        public bool Update(OrderDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
