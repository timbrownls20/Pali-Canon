using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PaliCanon.Contracts.Book;
using PaliCanon.Data.Sql.Entities;

namespace PaliCanon.Data.Sql.Repositories
{
    public class BookRepository: IBookRepository<BookEntity>
    {
        private readonly SqlContext _context;

        public BookRepository(SqlContext context)
        {
            _context = context;
        }
        
        public BookEntity Get(string bookCode)
        {
            var book = _context.Books.Include(x => x.Chapters)
                    .FirstOrDefault(x => x.Code == bookCode);
            return book;
        }

        public List<BookEntity> List()
        {
            try
            {
                return _context.Books.Include(x => x.Chapters).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed for connection{_context.Database.GetConnectionString()}", ex);
            }
        }

        public BookEntity Random()
        {
            Random rnd = new Random();
            int randomCode = rnd.Next(0, _context.Books.Count());
            return _context.Books.ToList().ElementAt(randomCode);
        }

        public void Delete(string bookCode)
        {
            var book = _context.Books.FirstOrDefault(x => x.Code == bookCode);
            if (book != null) _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public void Insert(BookEntity book)
        {
            var bookEntity = _context.Books.FirstOrDefault(x => x.Code == book.Code);
            if (bookEntity == null)
            {
                _context.Books.Add(book);
                _context.SaveChanges(); 
            }
        }
    }
}