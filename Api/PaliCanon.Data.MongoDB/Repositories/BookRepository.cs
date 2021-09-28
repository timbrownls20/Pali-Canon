using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MongoDB.Driver;
using PaliCanon.Contracts;
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

        public List<BookEntity> List()
        {
            return _database.GetCollection<BookEntity>(nameof(BookEntity)).AsQueryable().ToList();
        }
        public BookEntity Random()
        {
            var collection = _database.GetCollection<BookEntity>(nameof(BookEntity));
            return collection.AsQueryable().FirstOrDefault(); //. TB TODO not random - correct
        }

        public void Insert(BookEntity record)
        {
            var collection = _database.GetCollection<BookEntity>(nameof(BookEntity));
            collection.InsertOne(record);
        }
    }
}