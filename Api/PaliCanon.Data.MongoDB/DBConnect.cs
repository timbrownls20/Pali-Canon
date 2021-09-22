using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace PaliCanon.Data.MongoDB
{
    public class DbConnect
    {
        readonly MongoClient _client;

        public DbConnect(IConfiguration config)
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

