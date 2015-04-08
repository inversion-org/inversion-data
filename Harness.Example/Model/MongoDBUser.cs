using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
