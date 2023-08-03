using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Church.Models
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
