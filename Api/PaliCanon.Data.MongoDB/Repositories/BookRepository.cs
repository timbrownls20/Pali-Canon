using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MongoDB.Driver;
using PaliCanon.Contracts;
using PaliCanon.Data.MongoDB.Entities;
using PaliCanon.Model;
using AppConfig = Microsoft.Extensions.Configuration;

namespace PaliCanon.Data.MongoDB.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly IMapper _mapper;
        readonly IMongoDatabase _database;

        public BookRepository(IMapper mapper, AppConfig.IConfiguration config)
        {
            var mongodb = new MongoDbContext(config);
            mongodb.Drop();
            _database = mongodb.Connect();

            _mapper = mapper;
        }

        public List<Book> List()
        {
            return _mapper.Map<List<Book>>(_database.GetCollection<BookEntity>(nameof(BookEntity)));
        }
        public Book Random()
        {
            var collection = _database.GetCollection<BookEntity>(nameof(BookEntity));
            return _mapper.Map<Book>(collection.AsQueryable().FirstOrDefault()); //. TB TODO not random - correct
        }

        public void Insert(Book record)
        {
            var collection = _database.GetCollection<ChapterEntity>(nameof(ChapterEntity));
            collection.InsertOne(_mapper.Map<ChapterEntity>(record));
        }
    }
}