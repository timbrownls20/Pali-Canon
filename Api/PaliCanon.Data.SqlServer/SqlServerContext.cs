using Microsoft.EntityFrameworkCore;
using PaliCanon.Data.SqlServer.Entities;

namespace PaliCanon.Data.SqlServer
{
    public class SqlServerContext: DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<ChapterEntity> Chapters { get; set; }
        public DbSet<VerseEntity> Verses { get; set; }

        public SqlServerContext(DbContextOptions<SqlServerContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<BookEntity>(b =>
            {
                b.HasKey(k => k.Id);
                b.Property(p => p.Id).ValueGeneratedOnAdd();
            });

            modelbuilder.Entity<ChapterEntity>(b =>
            {
                b.HasOne(p => p.Book)
                    .WithMany(c => c.Chapters);
                b.HasKey(k => k.Id);
                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Navigation(n => n.Verses).AutoInclude();
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

