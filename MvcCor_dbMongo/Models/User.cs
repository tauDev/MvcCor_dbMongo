using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCor_dbMongo.Models
{
    public class User
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public string Uname { get; set; }
        [BsonElement]
        public string Password { get; set; }
    }
}
