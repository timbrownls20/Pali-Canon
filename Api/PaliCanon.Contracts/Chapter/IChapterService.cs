using System.Collections.Generic;

namespace PaliCanon.Contracts.Chapter
{
    public interface IChapterService: IService<Model.Chapter>
    {
        Model.Chapter Get(string bookCode, int chapter, int? verse);
        Model.Quote Quote(string bookCode = null, int? maxLength = null);
        List<Model.Quote> Quotes(int numberOfQuotes, int? maxLength = null);
        List<Model.Quote> Search(string searchTerm, int? pageSize, int? pageNumber = 1);
        Model.Chapter Next(string bookCode, int verse);
        Model.Chapter First(string bookCode);
        Model.Chapter Last(string bookCode);
    }
}