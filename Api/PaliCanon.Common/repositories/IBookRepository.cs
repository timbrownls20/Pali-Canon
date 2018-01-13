using System.Collections.Generic;
using PaliCanon.Common.Model;

namespace PaliCanon.Common.Repository
{
    public interface IBookRepository
    {
        string[] Codes();
        string Random();
        

    }
}