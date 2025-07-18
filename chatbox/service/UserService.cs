using chatbox.model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using chatbox.settings;
using BCrypt.Net;


namespace chatbox.service
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string fullName, string email, string password);
        Task<User> AuthenticateAsync(string email, string password); // Fixed typo
        Task<User> GetByEmailAsync(string email);
    }

    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        // ✅ Constructor name must match the class name
        public UserService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var settings = mongoDBSettings.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>("users"); // Fixed type
        }

        public async Task<User> RegisterAsync(string fullName, string email, string password)
        {
            if ((await _users.Find(u => u.Email == email).AnyAsync()))
                return null;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return user;

            return null;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }
    }
}
