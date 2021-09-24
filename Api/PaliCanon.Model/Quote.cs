namespace PaliCanon.Model
{
    public class Quote
    {
        public string Nikaya { get; set; }
        public string Book { get; set; }
        public string BookCode { get; set; }
        public string ChapterTitle { get; set; }
        public int ChapterNumber { get; set; }
        public string Author { get; set; }
        public string Citation { get; set; }
        public int VerseNumber { get; set; }
        public int? VerseNumberLast { get; set; }
        public string Text { get; set; }
    }
}
