using PaliCanon.Model;

namespace PaliCanon.Contracts.Chapter
{
    public interface IChapterService: IService<Model.Chapter>
    {
        Model.Chapter Get(string bookCode, int chapter, int? verse);
        Quote Quote(string bookCode);
        Model.Chapter Next(string bookCode, int verse);
        Model.Chapter First(string bookCode);
        Model.Chapter Last(string bookCode);

    }
}