using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace chatbox.model
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("senderId")]
        public string senderId { get; set; } = string.Empty;

        [BsonElement("receiverId")]
        public string receiverId { get; set; } = string.Empty;

        [BsonElement("content")]
        public string Content { get; set; } = string.Empty;

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}