using PaliCanon.Contracts.Verse;

namespace PaliCanon.Data.MongoDB.Entities
{
    public class VerseEntity: IVerseEntity 
    {
        public int VerseNumber {get; set; }

        public int? VerseNumberLast {get; set; }
        
        public string Text { get; set; }
    }

}