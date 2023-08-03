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
        public string Fullname { get; set; }

        [BsonElement("GuestOf")]
        public string GuestOf { get; set; }

        [BsonElement("OtherRemarks")]
        public string OtherRemarks { get; set; }

        [BsonElement("DateEntered")]
        public DateTime DateEntered { get; set; }

        [BsonElement("OtherGuests")]
        public List<string> OtherGuests { get; set; }
    }
}
