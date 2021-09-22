using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace PaliCanon.Data.MongoDB
{
    public class MongoDbContext
    {
        readonly MongoClient _client;

        public MongoDbContext(IConfiguration config)
        {
            string connectionString =  config.GetValue<string>("ConnectionStrings:MongoDB");
            _client = new MongoClient(connectionString);
        }

        public IMongoDatabase Connect()
        {
            return _client.GetDatabase("PaliCanon");        
        }

        public void Drop()
        {
            _client.DropDatabase("PaliCanon");  
        }
    }
}

