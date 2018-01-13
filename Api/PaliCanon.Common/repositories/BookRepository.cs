using System;

namespace PaliCanon.Common.Repository
{
    public class BookRepository: IBookRepository
    {
        
        private string[] _codes;

        public BookRepository()
        {
            _codes = new string[]{"dhp", "thag"};
        }

        public string[] Codes()
        {
            return _codes;
        }
        public string Random()
        {
            Random rnd = new Random();
           
            int randomCode = rnd.Next(0, _codes.Length);
            return _codes[randomCode];
        }
    }

}