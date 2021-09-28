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

            CreateMap<(ChapterEntity, VerseEntity), Quote>()
                .ForMember(s => s.Author, o => o.MapFrom(s => s.Item1.Author))
                .ForMember(s => s.BookCode, o => o.MapFrom(s => s.Item1.BookCode))
                .ForMember(s => s.Book, o => o.MapFrom(s => s.Item1.Book))
                .ForMember(s => s.ChapterNumber, o => o.MapFrom(s => s.Item1.ChapterNumber))
                .ForMember(s => s.Citation, o => o.Ignore()) //TB TODO
                .ForMember(s => s.Nikaya, o => o.MapFrom(s => s.Item1.Nikaya))
                .ForMember(s => s.ChapterTitle, o => o.MapFrom(s => s.Item1.Title))
                .ForMember(s => s.Text, o => o.MapFrom(s => s.Item2.Text))
                .ForMember(s => s.VerseNumber, o => o.MapFrom(s => s.Item2.VerseNumber))
                .ForMember(s => s.VerseNumberLast, o => o.MapFrom(s => s.Item2.VerseNumberLast))
                ;
        }
    }
}
