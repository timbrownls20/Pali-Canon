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
                .ForMember(d => d.Book, o => o.Ignore())
                .ForMember(d => d.Author, o => o.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null))
                ;
                
            CreateMap<ChapterEntity, Chapter>()
                .ForMember(s => s.Book, o => o.MapFrom(s => s.Book.Description))
                .ForMember(s => s.Nikaya, o => o.MapFrom(s => s.Book.Nikaya))
                .ForMember(s => s.Author, o => o.MapFrom(s => s.Author.Name))
                ;

            CreateMap<Verse, VerseEntity>().ReverseMap();

            CreateMap<(ChapterEntity, VerseEntity), Quote>()
                .ForMember(s => s.Author, o => o.MapFrom(s => s.Item1.Author.Name))
                .ForMember(s => s.BookCode, o => o.MapFrom(s => s.Item1.Book.Code))
                .ForMember(s => s.Book, o => o.MapFrom(s => s.Item1.Book.Description))
                .ForMember(s => s.ChapterNumber, o => o.MapFrom(s => s.Item1.ChapterNumber))
                .ForMember(s => s.Citation, o => o.MapFrom(s => s.Item1.Citation))
                .ForMember(s => s.Nikaya, o => o.MapFrom(s => s.Item1.Book.Nikaya))
                .ForMember(s => s.ChapterTitle, o => o.MapFrom(s => s.Item1.Title))
                .ForMember(s => s.Text, o => o.MapFrom(s => s.Item2.Text))
                .ForMember(s => s.VerseNumber, o => o.MapFrom(s => s.Item2.VerseNumber))
                .ForMember(s => s.VerseNumberLast, o => o.MapFrom(s => s.Item2.VerseNumberLast))
                ;
        }
    }
}
