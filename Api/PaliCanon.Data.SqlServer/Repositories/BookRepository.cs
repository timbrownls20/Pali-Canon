using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PaliCanon.Contracts;
using PaliCanon.Data.SqlServer.Entities;
using PaliCanon.Model;

namespace PaliCanon.Data.SqlServer.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly IMapper _mapper;
        private readonly SqlServerContext _context;

        public BookRepository(IMapper mapper, SqlServerContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<Book> List()
        {
            return _mapper.Map<List<Book>>(_context.Books);
        }
        public Book Random()
        {
            Random rnd = new Random();
            int randomCode = rnd.Next(0, _context.Books.Count());
            return _mapper.Map<Book>(_context.Books.ToList().ElementAt(randomCode));
        }

        public void Insert(Book book)
        {
            
            var bookEntity = _context.Books.FirstOrDefault(x => x.Code == book.Code);
            if (bookEntity == null)
            {
                _context.Books.Add(_mapper.Map<BookEntity>(book));
                _context.SaveChanges(); //.. TB TODO implement unit of work
            }
        }
    }
}