using MongoDB.Driver;
using Church.Models;

namespace Church.Data
{
    public class DataContext
    {
        private readonly IMongoDatabase _database = null;

        public DataContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("DefaultConnection"));
            if (client != null)
            {
                _database = client.GetDatabase("ChurchDB");
                CreateIndexes();
            }
        }

        private void CreateIndexes()
        {
            var announcementIndexKeys = Builders<Announcement>.IndexKeys.Text(t => t.Title).Text(t => t.Message);
            Announcements.Indexes.CreateOne(new CreateIndexModel<Announcement>(announcementIndexKeys));

            var visitorIndexKeys = Builders<Visitor>.IndexKeys.Text(t => t.FullName).Text(t => t.GuestOf).Text(t => t.OtherRemarks);
            Visitors.Indexes.CreateOne(new CreateIndexModel<Visitor>(visitorIndexKeys));
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Role> Roles => _database.GetCollection<Role>("Roles");
        public IMongoCollection<Visitor> Visitors => _database.GetCollection<Visitor>("Visitors");
        public IMongoCollection<Request> Requests => _database.GetCollection<Request>("Requests");
        public IMongoCollection<Announcement> Announcements => _database.GetCollection<Announcement>("Announcements");
        public IMongoCollection<Notification> Notifications => _database.GetCollection<Notification>("Notifications");
        public IMongoCollection<ProfilePhoto> ProfilePhotos => _database.GetCollection<ProfilePhoto>("ProfilePhotos");
    }
}
