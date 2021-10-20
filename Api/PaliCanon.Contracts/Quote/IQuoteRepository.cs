using PaliCanon.Contracts.Chapter;
using PaliCanon.Contracts.Verse;
using System.Collections.Generic;

namespace PaliCanon.Contracts.Quote
{
    public interface IQuoteRepository<T, TU> where T : class, IChapterEntity
                                             where TU : class, IVerseEntity
    {
        (T chapter, TU verse) Quote(string bookCode);
        List<(T chapter, TU verse)> Quotes(int numberOfQuotes, string bookCode = null);
        List<(T chapter, TU verse)> Search(string searchTerm, int? pageSize, int? pageNumber = 1);
    }
}
