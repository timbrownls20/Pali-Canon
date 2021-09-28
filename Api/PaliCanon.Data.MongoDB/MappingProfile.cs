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

            CreateMap<(ChapterEntity chapter, VerseEntity verse), Quote>()
                .ForMember(s => s.Author, o => o.MapFrom(s => s.chapter.Author))
                .ForMember(s => s.BookCode, o => o.MapFrom(s => s.chapter.BookCode))
                .ForMember(s => s.Book, o => o.MapFrom(s => s.chapter.Book))
                .ForMember(s => s.ChapterNumber, o => o.MapFrom(s => s.chapter.ChapterNumber))
                .ForMember(s => s.Citation, o => o.Ignore()) //TB TODO
                .ForMember(s => s.Nikaya, o => o.MapFrom(s => s.chapter.Nikaya))
                .ForMember(s => s.ChapterTitle, o => o.MapFrom(s => s.chapter.Title))
                .ForMember(s => s.Text, o => o.MapFrom(s => s.verse.Text))
                .ForMember(s => s.VerseNumber, o => o.MapFrom(s => s.verse.VerseNumber))
                .ForMember(s => s.VerseNumberLast, o => o.MapFrom(s => s.verse.VerseNumberLast))
                ;
        }
    }
}
