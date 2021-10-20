using System.Collections.Generic;
using AutoMapper;
using PaliCanon.Contracts.Book;
using PaliCanon.Model;

namespace PaliCanon.Services
{
    public class BookService<T>: IBookService where T : class, IBookEntity
    {
        private readonly IBookRepository<T> _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository<T> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public void Insert(Book record)
        {
            _bookRepository.Insert(_mapper.Map<T>(record));
        }

        public Book Get(string bookCode)
        {
            return _mapper.Map<Book>(_bookRepository.Get(bookCode));
        }

        public List<Book> List()
        {
            return _mapper.Map<List<Book>>(_bookRepository.List());
        }
    }
}
