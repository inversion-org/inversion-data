using MongoDB.Bson;

namespace Harness.Example.Model
{
    public class MongoDBUser : User.Builder
    {
        public ObjectId Id { get; set; }

        public MongoDBUser() : base() { }
        public MongoDBUser(User user) : base(user) { }
    }
}