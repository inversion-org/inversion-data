using MongoDB.Bson;
using MongoDB.Driver;

namespace Inversion.Data
{
    public class MongoDBStore : Store
    {
        private MongoClient _client;
        protected IMongoDatabase Database;
        private readonly string _connStr;
        private readonly string _dbName;

        public IMongoCollection<BsonDocument> this[string collectionName]
        {
            get
            {
                if (!this.HasStarted)
                {
                    throw new StoreProcessException("The store must be started to use it.");
                }

                return this.Database.GetCollection<BsonDocument>(collectionName);
            }
        }

        public MongoDBStore(string connStr, string dbName)
        {
            _connStr = connStr;
            _dbName = dbName;
        }

        public override void Start()
        {
            base.Start();

            _client = new MongoClient(_connStr);
            this.Database = _client.GetDatabase(_dbName);
        }

        public sealed override void Dispose()
        {
            // nothing to do
        }
    }
}