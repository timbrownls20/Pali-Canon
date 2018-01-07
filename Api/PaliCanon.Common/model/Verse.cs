using MongoDB.Bson;

namespace PaliCanon.Common.Model
{
    public class Verse 
    {
        public int VerseNumber {get; set; }

        public int? VerseNumberLast {get; set; }
        
        public string Text { get; set; }
    }

}