using System;
using PaliCanon.Common.Enums;
using PaliCanon.Contracts;

namespace PaliCanon.DataLoad.Provider
{
    public class ProviderFactory : IProviderFactory
    {
        private readonly IBookRepository _bookRepository;
        private readonly IChapterRepository _chapterRepository;

        public ProviderFactory(IBookRepository bookRepository, IChapterRepository chapterRepository)
        {
            _bookRepository = bookRepository;
            _chapterRepository = chapterRepository;
        }

        public IProvider Get(Book book)
        {
            IProvider provider;

            switch (book)
            {
                case Book.Dhammapada:
                    provider = new DhammapadaProvider(_bookRepository, _chapterRepository);
                    break;
                case Book.Theragatha:
                    provider = new TheragathaProvider(_chapterRepository);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(book), book, null);
            }

            return provider;
        }
    }
}
