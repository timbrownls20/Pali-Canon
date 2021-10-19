using System.Collections.Generic;

namespace PaliCanon.RazorPages.ApiClient
{
    public class Book 
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Nikaya { get; set; }
        public List<Chapter> Chapters { get; set; }
    }

}