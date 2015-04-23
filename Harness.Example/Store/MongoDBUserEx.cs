using System.Linq;
using MongoDB.Bson;

using Harness.Example.Model;

namespace Harness.Example.Store
{
    public static class MongoDBUserEx
    {
        public static BsonDocument ConvertUserToBson(this User user)
        {
            BsonDocument doc = new BsonDocument();
            doc["_id"] = new BsonObjectId(ObjectId.GenerateNewId());
            doc["username"] = user.Username;
            doc["password"] = user.Password;
            doc["metadata"] = new BsonDocument(user.Metadata.Metadata);
            return doc;
        }

        public static User ConvertBsonToUser(this BsonDocument doc)
        {
            User.Builder builder = new User.Builder
            {
                Username = doc["username"].AsString,
                Password = doc["password"].AsString
            };

            BsonDocument metadataDoc = doc["metadata"].AsBsonDocument;

            builder.Metadata.Metadata = metadataDoc.Names.ToDictionary(
                key => key, key => metadataDoc[key].AsString);

            return builder;
        }
    }
}