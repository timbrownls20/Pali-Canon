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
            modelbuilder.Entity<ChapterEntity>()
                .HasOne(p => p.Book)
                .WithMany(c => c.Chapters);

            modelbuilder.Entity<VerseEntity>()
                .HasOne(p => p.Chapter)
                .WithMany(c => c.Verses);
        }
    }
}

