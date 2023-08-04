using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Church.Models
{
    public class Request
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("NameOfRequestor")]
        public string NameOfRequestor { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Remarks")]
        public string Remarks { get; set; }

        [BsonElement("DateEntered")]
        public DateTime DateEntered { get; set; }
    }
}
