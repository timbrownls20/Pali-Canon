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

            CreateMap<(ChapterEntity chapter, VerseEntity verse), Quote>()
                .ForMember(s => s.Author, o => o.MapFrom(s => s.chapter.Author.Name))
                .ForMember(s => s.BookCode, o => o.MapFrom(s => s.chapter.Book.Code))
                .ForMember(s => s.Book, o => o.MapFrom(s => s.chapter.Book.Description))
                .ForMember(s => s.ChapterNumber, o => o.MapFrom(s => s.chapter.ChapterNumber))
                .ForMember(s => s.Citation, o => o.MapFrom(s => s.chapter.Citation))
                .ForMember(s => s.Nikaya, o => o.MapFrom(s => s.chapter.Book.Nikaya))
                .ForMember(s => s.ChapterTitle, o => o.MapFrom(s => s.chapter.Title))
                .ForMember(s => s.Text, o => o.MapFrom(s => s.verse.Text))
                .ForMember(s => s.VerseNumber, o => o.MapFrom(s => s.verse.VerseNumber))
                .ForMember(s => s.VerseNumberLast, o => o.MapFrom(s => s.verse.VerseNumberLast))
                ;
        }
    }
}
