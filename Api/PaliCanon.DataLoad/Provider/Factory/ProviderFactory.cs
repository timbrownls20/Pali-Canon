using System;
using PaliCanon.Common.Enums;
using PaliCanon.Contracts.Book;
using PaliCanon.Contracts.Chapter;
using PaliCanon.DataLoad.Provider.Buddharakkhita;
using PaliCanon.DataLoad.Provider.Thanissaro;

namespace PaliCanon.DataLoad.Provider.Factory
{
    public class ProviderFactory : IProviderFactory
    {
        private readonly IBookService _bookService;
        private readonly IChapterService _chapterService;

        public ProviderFactory(IBookService bookService, IChapterService chapterService)
        {
            _bookService = bookService;
            _chapterService = chapterService;
        }

        public IProvider Get(Book book)
        {
            IProvider provider;

            switch (book)
            {
                case Book.dhp:
                    provider = new DhammapadaProvider(_bookService, _chapterService);
                    break;
                case Book.ther:
                    provider = new TheragathaProvider(_chapterService);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(book), book, null);
            }

            return provider;
        }
    }
}
