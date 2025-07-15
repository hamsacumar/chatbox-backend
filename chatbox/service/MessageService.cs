using chatbox.model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using chatbox.settings;

namespace chatbox.service
{
    public class MessageService
    {
        private readonly IMongoCollection<Message> _CollectionChat;
        public MessageService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var settings = mongoDBSettings.Value;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _CollectionChat = database.GetCollection<Message>("chats");
        }

        public async Task<List<Message>> GetAllAsync() =>
            await _CollectionChat.Find(_ => true).ToListAsync();

        public async Task<Message> CreateAsync(Message message)
        {
            await _CollectionChat.InsertOneAsync(message);
            return message;
        }

        
        
    }
    
}

