using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Church.Models
{
    public class Visitor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Fullname")]
        public string FullName { get; set; }

        [BsonElement("GuestOf")]
        public string GuestOf { get; set; }

        [BsonElement("OtherRemarks")]
        public string OtherRemarks { get; set; }

        [BsonIgnore]
        public DateTime DateCreated { get; set; }

        [BsonElement("DateCreatedString")]
        public string DateCreatedString
        {
            get => DateCreated.ToString("yyyy-MM-dd");
            set => DateCreated = DateTime.Parse(value);
        }
    }
}
