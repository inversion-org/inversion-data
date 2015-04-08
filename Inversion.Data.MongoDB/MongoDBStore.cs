using MongoDB.Bson;
using MongoDB.Driver;

namespace Inversion.Data
{
    public class MongoDBStore : Store
    {
        private readonly MongoClient _client;
        protected readonly IMongoDatabase Database;

        public IMongoCollection<BsonDocument> this[string collectionName]
        {
            get { return this.Database.GetCollection<BsonDocument>(collectionName); }
        }

        public MongoDBStore(string connStr, string dbName)
        {
            _client = new MongoClient(connStr);
            this.Database = _client.GetDatabase(dbName);
        }

        public sealed override void Dispose()
        {
            // nothing to do
        }
    }
}