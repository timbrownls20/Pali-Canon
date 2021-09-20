using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PaliCanon.Contracts;
using PaliCanon.Data.MongoDB.Entities;
using PaliCanon.Model;

namespace PaliCanon.Data.MongoDB.Repositories
{
    public class BookRepository: IBookRepository
    {
        private List<BookEntity> _books;
        private IMapper _mapper;

        public BookRepository(IMapper mapper)
        {
            _mapper = mapper;
            _books = new List<BookEntity>{
                new BookEntity { Code = "dhp", Description="dhammapada"},
                new BookEntity { Code = "thag", Description="theragatha"}
            };
        }

        public List<Book> List()
        {
            return _mapper.Map<List<Book>>(_books);
        }
        public Book Random()
        {
            Random rnd = new Random();
           
            int randomCode = rnd.Next(0, _books.Count);
            return _mapper.Map<Book>(_books.ElementAt(randomCode));
        }
    }

}