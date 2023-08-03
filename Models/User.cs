using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;

namespace Church.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("FistName")]
        public string FirstName { get; set; }
        [BsonElement("MiddleName")]
        public string MiddleName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("DOB")]
        public DateTime DOB { get; set; }

        [BsonElement("PasswordHash")]
        public byte[] PasswordHash { get; set; }

        [BsonElement("PasswordSalt")]
        public byte[] PasswordSalt { get; set; }

        [BsonElement("Role")]
        public string RoleId { get; set; }

        public string PasswordRecoveryToken { get; set; }

    }
}
