﻿using AutoMapper;
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

            CreateMap<VerseEntity, Quote>()
                .ForMember(s => s.Author, o => o.MapFrom(s => s.Chapter.Author))
                .ForMember(s => s.BookCode, o => o.MapFrom(s => s.Chapter.Book.Code))
                .ForMember(s => s.Book, o => o.MapFrom(s => s.Chapter.Book.Description))
                .ForMember(s => s.ChapterNumber, o => o.MapFrom(s => s.Chapter.ChapterNumber))
                .ForMember(s => s.Citation, o => o.MapFrom(s => s.Chapter.Citation))
                .ForMember(s => s.Nikaya, o => o.MapFrom(s => s.Chapter.Book.Nikaya))
                .ForMember(s => s.ChapterTitle, o => o.MapFrom(s => s.Chapter.Title))
                ;
        }
    }
}
