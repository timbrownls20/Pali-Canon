namespace PaliCanon.Contracts.Verse
{
    public interface IVerseEntity
    {
        int VerseNumber { get; set; }

        int? VerseNumberLast { get; set; }

        string Text { get; set; }
    }
}
