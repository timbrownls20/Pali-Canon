using System;
using PaliCanon.Common.Enums;
using PaliCanon.Contracts;

namespace PaliCanon.DataLoad.Provider
{
    public class ProviderFactory : IProviderFactory
    {
        private readonly IChapterRepository _chapterRepository;

        public ProviderFactory(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        public IProvider Get(Book book)
        {
            IProvider provider;

            switch (book)
            {
                case Book.Dhammapada:
                    provider = new DhammapadaProvider(_chapterRepository);
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
