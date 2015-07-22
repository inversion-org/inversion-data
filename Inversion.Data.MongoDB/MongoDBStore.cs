using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Inversion.Data
{
    public class MongoDBStore : StoreBase, IStoreHealth
    {
        protected IMongoDatabase Database;
        private MongoClient _client;

        private readonly string _connStr;
        private readonly string _dbName;

        public IMongoCollection<BsonDocument> this[string collectionName]
        {
            get
            {
                AssertIsStarted();

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
            // nothing to dispose of
            base.Stop();
        }

        public virtual bool GetHealth(out string result)
        {
            result = String.Empty;

            this.AssertIsStarted();

            IAsyncCursor<BsonDocument> databaseCursor = _client.ListDatabasesAsync().Result;

            List<BsonDocument> databases = databaseCursor.ToListAsync<BsonDocument>().Result;

            if (databases.Count > 0)
            {
                return true;
            }
            result = "No databases could be found.";
            return false;
        }
    }
}