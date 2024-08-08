using MongoDB.Driver;
using BMongo1.Models.BusinessModels;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace BMongo1.Models.BusinessModels
{
    public class RepositoryAccount : IRepositoryAccount
    {

        MongoDbContext db;
        public RepositoryAccount(MongoDbContext db)
        {
            this.db = db;
        }
        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAll()
        {
            return db.Account.Find(FilterDefinition<Account>.Empty).ToList();
        }

        public Account GetById(int key)
        {
            return db.Account.Find(x => x._id == key).FirstOrDefault();
        }

        public Account GetEmail(string email)
        {
            return db.Account.Find(x => x.Email == email).FirstOrDefault();
        }

        public List<Account> GetForeignKey()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Account entity)
        {
            var sort = Builders<Account>.Sort.Descending(p => p._id);
            var c = db.Account.Find(FilterDefinition<Account>.Empty).Sort(sort).FirstOrDefault();
            int rows = (int)db.Account.CountDocuments(FilterDefinition<Account>.Empty);
            if (rows > 0)
            {
                entity._id = (int)c._id + 1;
            }
            else
            {
                entity._id = 1;
            }
            entity.AccountStatus = entity.AccountStatus ?? "Disable";
            entity.AccountType = entity.AccountType ?? "user";
            db.Account.InsertOne(entity);
            return true;
        }

        public Account Login(string email, string password)
        {
            return db.Account.Find(x => x.Email == email && x.Password == password).FirstOrDefault();
        }

        public bool Update(Account entity)
        {
            var p = Builders<Account>.Update.Set("FullName", entity.FullName)
                .Set("Avatar", entity.Avatar)
               .Set("Address", entity.Address)
               .Set("Email", entity.Email)
               .Set("Phone", entity.Phone)
               .Set("Address", entity.Address);
            db.Account.UpdateOne(x => x._id == entity._id, p);
            return true;
        }
    }
}
