using PaliCanon.Contracts.Verse;
using System.Collections.Generic;

namespace PaliCanon.Contracts.Chapter
{
    public interface IChapterRepository<T, TU>: IRepository<T> where T : class, IChapterEntity 
                                                                where TU : class, IVerseEntity
    {
        T Get(string bookCode, int chapter, int? verse);
        (T chapter, TU verse) Quote(string bookCode);
        List<(T chapter, TU verse)> Quotes(int numberOfQuotes);
        T Next(string bookCode, int verse);
        T First(string bookCode);
        T Last(string bookCode);

    }
}