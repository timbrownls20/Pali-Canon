namespace PaliCanon.Data.MongoDB.Entities
{
    public class VerseEntity 
    {
        public int VerseNumber {get; set; }

        public int? VerseNumberLast {get; set; }
        
        public string Text { get; set; }
    }

}