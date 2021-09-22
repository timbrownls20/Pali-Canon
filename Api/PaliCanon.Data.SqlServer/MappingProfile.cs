using AutoMapper;
using PaliCanon.Data.SqlServer.Entities;
using PaliCanon.Model;

namespace PaliCanon.Data.SqlServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookEntity>().ReverseMap();

            CreateMap<Chapter, ChapterEntity>()
                //.ReverseMap()
                //.ForMember(s => s.Id, o => o.Condition((src, dest) => src != null))
                .ForMember(s => s.Book, o => o.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                ;

            CreateMap<ChapterEntity, Chapter>();

            CreateMap<Verse, VerseEntity>().ReverseMap();
        }
    }
}
