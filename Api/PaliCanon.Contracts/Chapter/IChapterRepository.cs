namespace PaliCanon.Contracts.Chapter
{
    public interface IChapterRepository<T>: IRepository<T> where T : class, IChapterEntity
    {
        T Get(string bookCode, int chapter, int? verse);
        T Next(string bookCode, int verse);
        T First(string bookCode);
        T Last(string bookCode);

    }
}