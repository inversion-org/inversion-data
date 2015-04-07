using MongoDB.Bson;
using MongoDB.Driver;

namespace Inversion.Data
{
    public class MongoDBStore : Store
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        public IMongoCollection<BsonDocument> this[string collectionName]
        {
            get { return this.Database.GetCollection<BsonDocument>(collectionName); }
        }

        public MongoClient Client
        {
            get { return _client; }
        }

        public IMongoDatabase Database
        {
            get { return _db; }
        }

        public MongoDBStore(string connStr, string dbName)
        {
            _client = new MongoClient(connStr);
            _db = _client.GetDatabase(dbName);
        }

        public sealed override void Dispose()
        {
            // nothing to do
        }
    }
}