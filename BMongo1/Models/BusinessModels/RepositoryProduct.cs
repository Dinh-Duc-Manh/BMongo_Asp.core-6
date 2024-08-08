using BMongo1.Models.ModelsViews;
using MongoDB.Bson;
using MongoDB.Driver;
using BMongo1.Models.BusinessModels;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace BMongo1.Models.BusinessModels
{
    public class RepositoryProduct : IRepositoryProduct
    {
        MongoDbContext db;
        public RepositoryProduct(MongoDbContext db)
        {
            this.db = db;
        }

        public bool Delete(string key)
        {
            try
            {
                db.Products.DeleteOne(x => x._id == key);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAll()
        {
            return db.Products.Find(FilterDefinition<Product>.Empty).ToList();
        }

        public Product GetById(string key)
        {
            return db.Products.Find(x => x._id == key).FirstOrDefault();
        }


        public bool Insert(Product entity)
        {
            entity.Description = entity.Description ?? "";
            entity.ProductImage = entity.ProductImage ?? "";
            db.Products.InsertOne(entity);
            return true;
        }

        public List<Product> Paging(int page, int pageSize, out long totalrows)
        {
            int skip = pageSize * (page - 1);
            long rows = db.Products.CountDocuments(FilterDefinition<Product>.Empty);
            totalrows = rows % pageSize == 0 ? rows / pageSize : (rows / pageSize) + 1;
            return db.Products.Find(FilterDefinition<Product>.Empty).Skip(skip).Limit(pageSize).ToList();
        }

        public List<Product> SearchPaging(string name, int page, int pageSize, out long totalrows)
        {
            int skip = pageSize * (page - 1);
            long rows = db.Products.CountDocuments(FilterDefinition<Product>.Empty);
            totalrows = rows % pageSize == 0 ? rows / pageSize : (rows / pageSize) + 1;
            return db.Products.Find(x => x.ProductName.ToLower().Contains(name.ToLower())).Skip(skip).Limit(pageSize).ToList();
        }

        public bool Update(Product entity)
        {
            var p = Builders<Product>.Update.Set("ProductName", entity.ProductName)
                .Set("Price", entity.Price)
                .Set("Description", entity.Description)
                .Set("ProductImage", entity.ProductImage)
                .Set("ProductStatus", entity.ProductStatus)
                .Set("CategoryId", entity.CategoryId);
            db.Products.UpdateOne(x => x._id == entity._id, p);
            return true;
        }
        public List<ProductsViewModel> GetForeignKey()
        {
            BsonDocument[] lookup = new BsonDocument[]
            {
                new BsonDocument
                {
                    {
                        "$lookup", new BsonDocument
                        {
                            {"from" ,"Categories"},
                            {"localField" ,"CategoryId"},
                            {"foreignField" ,"_id"},
                            {"as" ,"Categories"}
                        }
                    }
                }
            };
            var products = db.Products.Aggregate<BsonDocument>(lookup).ToList();
            var data = new List<ProductsViewModel>();
            foreach (var e in products)
            {
                var p = new ProductsViewModel();
                p._id = e["_id"].ToString();
                p.ProductName = e["ProductName"].ToString();
                p.Price = e["Price"].ToInt32();
                p.Description = e["Description"].ToString();
                p.ProductImage = e["ProductImage"].ToString();
                p.ProductStatus = e["ProductStatus"].ToString();
                p.CategoryId = e["CategoryId"].ToInt32();
                p.CategoryName = e["Categories"].AsBsonArray[0]["CategoryName"].ToString();
                data.Add(p);

            }
            return data;
        }

        public Task<List<Product>> GetByCatId(int CatId)
        {
            return db.Products.Find(x => x.CategoryId == CatId).ToListAsync();
        }
    }
}
