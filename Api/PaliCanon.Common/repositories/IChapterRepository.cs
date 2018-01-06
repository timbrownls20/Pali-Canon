using System.Collections.Generic;
using PaliCanon.Common.Model;

namespace PaliCanon.Common.Repository
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