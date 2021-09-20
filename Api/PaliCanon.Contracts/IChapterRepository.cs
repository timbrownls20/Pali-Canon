using System.Collections.Generic;
using PaliCanon.Model;

namespace PaliCanon.Contracts
{
    public interface IChapterRepository: IRepository<Chapter>
    {
        List<Chapter> Get(string bookCode, int? chapter, int? verse);
        Chapter Quote(string bookCode);
        Chapter Next(string bookCode, int chapter, int verse);
        Chapter First(string bookCode);
        Chapter Last(string bookCode);

    }
}