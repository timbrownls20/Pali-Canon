using Microsoft.EntityFrameworkCore;
using PaliCanon.Data.Sql.Entities;

namespace PaliCanon.Data.Sql
{
    public class SqlContext: DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<ChapterEntity> Chapters { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }

        public SqlContext(DbContextOptions<SqlContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<BookEntity>(b =>
            {
                b.HasKey(k => k.Id);
                b.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            modelbuilder.Entity<AuthorEntity>(b =>
            {
                b.HasKey(k => k.Id);
                b.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            modelbuilder.Entity<ChapterEntity>(b =>
            {
                b.HasOne(p => p.Author)
                    .WithMany(p => p.Chapters);
                b.HasOne(p => p.Book)
                    .WithMany(c => c.Chapters);
                b.HasKey(k => k.Id);
                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Navigation(n => n.Verses).AutoInclude();
                b.Navigation(p => p.Author).AutoInclude();
            });

            modelbuilder.Entity<VerseEntity>(b =>
            {
                b.HasOne(p => p.Chapter)
                    .WithMany(c => c.Verses);
                b.HasKey(k => k.Id);
                b.Property(p => p.Id).ValueGeneratedOnAdd();
                
            });

        }
    }
}

