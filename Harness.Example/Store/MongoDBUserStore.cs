using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Inversion.Data;
using Harness.Example.Model;

namespace Harness.Example.Store
{
    public class MongoDBUserStore : MongoDBStore, IUserStore
    {
        private readonly string _collectionName;

        private IMongoCollection<MongoDBUser> _collection;

        public MongoDBUserStore(string connStr, string dbName, string collectionName) : base(connStr, dbName)
        {
            _collectionName = collectionName;
        }

        public override void Start()
        {
            base.Start();
            _collection = this.Database.GetCollection<MongoDBUser>(_collectionName);
        }

        public User Get(string username)
        {
            return _collection.Find(x => x.Username == username).FirstAsync().Result;
        }

        public IEnumerable<User> GetAll()
        {
            return _collection.FindAsync(x => true).Result.ToListAsync().Result.Select(x => x.ToModel());
        }

        public void Put(User user)
        {
            MongoDBUser result = _collection.Find(x => x.Username == user.Username).SingleOrDefaultAsync().Result;

            if(result == null)
            {
                // user with this username does not exist
                _collection.InsertOneAsync(new MongoDBUser(user)).Wait();
            }
            else
            {
                // replace the document
                MongoDBUser replacement = new MongoDBUser(user);
                replacement.Id = result.Id;

                _collection.ReplaceOneAsync(x => x.Id == result.Id, replacement).Wait();
            }
        }

        public void Put(IEnumerable<User> users)
        {
            _collection.InsertManyAsync(users.Select(x => new MongoDBUser(x))).Wait();
        }

        public void Delete(User user)
        {
            _collection.DeleteOneAsync(x => x.Username == user.Username).Wait();
        }
    }
}