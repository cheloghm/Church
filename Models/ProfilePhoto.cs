using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Church.Models
{
    public class ProfilePhoto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Photo")]
        public byte[] Photo { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }
    }
}
