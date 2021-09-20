using AutoMapper;
using PaliCanon.Data.MongoDB.Entities;
using PaliCanon.Model;

namespace PaliCanon.Data.MongoDB
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookEntity>().ReverseMap();
            CreateMap<Chapter, ChapterEntity>().ReverseMap();
            CreateMap<Verse, VerseEntity>().ReverseMap();
        }
    }
}
