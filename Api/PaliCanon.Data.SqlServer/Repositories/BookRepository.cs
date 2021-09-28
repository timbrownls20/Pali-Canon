using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PaliCanon.Contracts;
using PaliCanon.Contracts.Book;
using PaliCanon.Data.SqlServer.Entities;
using PaliCanon.Model;

namespace PaliCanon.Data.SqlServer.Repositories
{
    public class BookRepository: IBookRepository<BookEntity>
    {
        private readonly IMapper _mapper;
        private readonly SqlServerContext _context;

        public BookRepository(IMapper mapper, SqlServerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<BookEntity> List()
        {
            return _context.Books.ToList();
        }
        public BookEntity Random()
        {
            Random rnd = new Random();
            int randomCode = rnd.Next(0, _context.Books.Count());
            return _context.Books.ToList().ElementAt(randomCode);
        }

        public void Insert(BookEntity book)
        {
            var bookEntity = _context.Books.FirstOrDefault(x => x.Code == book.Code);
            if (bookEntity == null)
            {
                _context.Books.Add(book);
                _context.SaveChanges(); //.. TB TODO implement unit of work
            }
        }
    }
}