using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using PaliCanon.Contracts.Book;
using PaliCanon.Data.MongoDB.Entities;
using AppConfig = Microsoft.Extensions.Configuration;

namespace PaliCanon.Data.MongoDB.Repositories
{
    public class BookRepository: IBookRepository<BookEntity>
    {
        readonly IMongoDatabase _database;

        public BookRepository(AppConfig.IConfiguration config)
        {
            var mongodb = new MongoDbContext(config);
            mongodb.Drop();
            _database = mongodb.Connect();
        }

        public BookEntity Get(string bookCode)
        {
            var collection = _database.GetCollection<BookEntity>(nameof(BookEntity));
            var query = collection.AsQueryable().Where(x => x.Code == bookCode);
            return query.FirstOrDefault();
        }

        public List<BookEntity> List()
        {
            return _database.GetCollection<BookEntity>(nameof(BookEntity)).AsQueryable().ToList();
        }
        public BookEntity Random()
        {
            var collection = _database.GetCollection<BookEntity>(nameof(BookEntity));
            return collection.AsQueryable().FirstOrDefault(); //. TB TODO not random - correct
        }

        public void Delete(string bookCode)
        {
            IMongoCollection<BookEntity> collection = _database.GetCollection<BookEntity>(nameof(BookEntity));
            FilterDefinition<BookEntity> deleteFilter = Builders<BookEntity>.Filter.Eq("Code", bookCode);
            collection.DeleteOne(deleteFilter);
        }

        public void Insert(BookEntity record)
        {
            var collection = _database.GetCollection<BookEntity>(nameof(BookEntity));
            collection.InsertOne(record);
        }
    }
}