using System;
using System.Linq;
using System.Collections.Generic;
using PaliCanon.Common.Model;

namespace PaliCanon.Common.Repository
{
    public class BookRepository: IBookRepository
    {
        
        private List<Book> _books;

        public BookRepository()
        {
            _books = new List<Book>{
                new Book { Code = "dhp", Description="dhammapada"},
                new Book { Code = "thag", Description="theragatha"}
            };
        }

        public List<Book> List()
        {
            return _books;
        }
        public Book Random()
        {
            Random rnd = new Random();
           
            int randomCode = rnd.Next(0, _books.Count);
            return _books.ElementAt(randomCode);
        }
    }

}