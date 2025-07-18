using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace chatbox.model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("fullName")]
        public string FullName  { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email  { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        public string PasswordHash  { get; set; } = string.Empty;

        [BsonElement("role")]
        public string Role  { get; set; } = "User";
    }
}