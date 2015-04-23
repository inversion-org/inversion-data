using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using MongoDB.Bson;
using MongoDB.Driver;

using Inversion.Data;

using Harness.Example.Model;

namespace Harness.Example.Store
{
    public class MongoDBUserStore : MongoDBStore, IUserStore
    {
        private readonly string _collectionName;

        private IMongoCollection<BsonDocument> _collection;

        public MongoDBUserStore(string connStr, string dbName, string collectionName) : base(connStr, dbName)
        {
            _collectionName = collectionName;
        }

        public override void Start()
        {
            base.Start();
            _collection = this.Database.GetCollection<BsonDocument>(_collectionName);
        }

        public User Get(string username)
        {
            AssertIsStarted();
            return _collection
                .Find(x => x["username"] == username)
                .FirstAsync().Result.ConvertBsonToUser();
        }

        public IEnumerable<User> GetAll()
        {
            AssertIsStarted();

            return
                _collection.FindAsync(new BsonDocument())
                    .Result.ToListAsync()
                    .Result.Select(doc => doc.ConvertBsonToUser());
        }

        public void Put(User user)
        {
            AssertIsStarted();
            BsonDocument result = _collection.Find(x => x["username"] == user.Username).FirstAsync().Result;

            if(result == null)
            {
                // user with this username does not exist
                Task.Run(async () =>
                    await _collection.InsertOneAsync(user.ConvertUserToBson())).Wait();
            }
            else
            {
                // replace the document
                BsonDocument replacement = user.ConvertUserToBson();
                replacement["_id"] = result["_id"];

                Task.Run(async () =>
                    await _collection.ReplaceOneAsync(x => x["_id"] == replacement["_id"], replacement))
                    .Wait();
            }
        }

        public void Put(IEnumerable<User> users)
        {
            AssertIsStarted();
            Task.Run(async () =>
                await _collection.InsertManyAsync(users.Select(x => x.ConvertUserToBson())))
                .Wait();
        }

        public void Delete(User user)
        {
            AssertIsStarted();
            Task.Run(async () =>
                await _collection.DeleteOneAsync(x => x["username"] == user.Username))
                .Wait();
        }
    }
}