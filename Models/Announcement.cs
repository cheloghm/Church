using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Church.Models
{
    public class Announcement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Message")]
        public string Message { get; set; }

        [BsonIgnore] // Ignore this field when saving to MongoDB
        public DateTime DateCreated { get; set; }

        [BsonElement("DateCreatedString")]
        public string DateCreatedString
        {
            get => DateCreated.ToString("yyyy-MM-dd");
            set => DateCreated = DateTime.Parse(value);
        }
    }
}
