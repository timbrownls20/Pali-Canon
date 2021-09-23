using System.Collections.Generic;

namespace PaliCanon.Model
{
    public class Chapter 
    {
        public Chapter()
        {
            Verses = new List<Verse>();
        }
        
        public string Nikaya { get; set; }
        public string Book { get; set; }
        public string BookCode { get; set; }
        public string Title { get; set; }
        public int ChapterNumber { get; set; }
        public string Author { get; set; }
        public List<Verse> Verses { get; set; }
        public string Citation { get; set; }
    }

}