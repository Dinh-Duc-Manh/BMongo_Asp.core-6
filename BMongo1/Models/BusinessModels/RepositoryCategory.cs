using MongoDB.Driver;

namespace BMongo1.Models.BusinessModels
{
    public class RepositoryCategory : IRepositoryCategory
    {
        MongoDbContext db;
        public RepositoryCategory(MongoDbContext db)
        {
            this.db = db;
        }

        public bool Delete(int key)
        {
            try
            {
                db.Categories.DeleteOne(x => x._id == key);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Category> GetAll()
        {
            var catDb = db.Categories;
            var categories = catDb.Find(FilterDefinition<Category>.Empty).ToList();
            return categories;
        }

        public Category GetById(int key)
        {
            return db.Categories.Find(x => x._id == key).FirstOrDefault();
        }

        public List<Category> GetForeignKey()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Category entity)
        {
            var sort = Builders<Category>.Sort.Descending(p => p._id);
            var c = db.Categories.Find(FilterDefinition<Category>.Empty).Sort(sort).FirstOrDefault();
            int rows = (int)db.Categories.CountDocuments(FilterDefinition<Category>.Empty);
            if (rows > 0)
            {
                entity._id = (int)c._id + 1;
            }
            else
            {
                entity._id = 1;
            }
            db.Categories.InsertOne(entity);
            return true;
        }

        public List<Category> Paging(int page, int pageSize, out long totalrows)
        {
            int skip = pageSize * (page - 1);
            long rows = db.Categories.CountDocuments(FilterDefinition<Category>.Empty);
            totalrows = rows % pageSize == 0 ? rows / pageSize : (rows / pageSize) + 1;
            return db.Categories.Find(FilterDefinition<Category>.Empty).Skip(skip).Limit(pageSize).ToList();
        }

        public List<Category> SearchPaging(string name, int page, int pageSize, out long totalrows)
        {
            int skip = pageSize * (page - 1);
            long rows = db.Categories.CountDocuments(FilterDefinition<Category>.Empty);
            totalrows = rows % pageSize == 0 ? rows / pageSize : (rows / pageSize) + 1;
            return db.Categories.Find(x => x.CategoryName.ToLower().Contains(name.ToLower())).Skip(skip).Limit(pageSize).ToList();
        }

        public bool Update(Category entity)
        {
            var p = Builders<Category>.Update.Set("CategoryName", entity.CategoryName);
            db.Categories.UpdateOne(x => x._id == entity._id, p);
            return true;
        }
    }
}

