using MongoDB.Driver;

namespace PaliCanon.Common
{
    public class DBConnect
    {
        public const string MONGODB_CONNECTION = "mongodb://localhost:27017";

        MongoClient client;

        public DBConnect()
        {
            client = new MongoClient(MONGODB_CONNECTION);
        }

        public IMongoDatabase Connect()
        {
          
            return client.GetDatabase("PaliCanon");        
        }

        public void Drop()
        {
            client.DropDatabase("PaliCanon");  
        }
    }
}

