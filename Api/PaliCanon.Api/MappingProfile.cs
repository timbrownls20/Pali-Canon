using AutoMapper;
using PaliCanon.Data.MongoDB.Entities;
using PaliCanon.Model;

namespace PaliCanon.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Book, BookEntity>().ReverseMap();
            CreateMap<Chapter, ChapterEntity>().ReverseMap();
            CreateMap<Verse, VerseEntity>().ReverseMap();
        }
    }
}
