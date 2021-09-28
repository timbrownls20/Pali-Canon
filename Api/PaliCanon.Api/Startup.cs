using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaliCanon.Contracts;
using PaliCanon.Contracts.Book;
using PaliCanon.Contracts.Chapter;
using Mongo = PaliCanon.Data.MongoDB;
using SqlServer = PaliCanon.Data.SqlServer;
using PaliCanon.DataLoad.Provider.Factory;
using PaliCanon.Services;

namespace PaliCanon.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //.. Entity Framework
            services.AddDbContext<SqlServer.SqlServerContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mongo.MappingProfile());
                mc.AddProfile(new SqlServer.MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddCors();
            services.AddControllers();

            ConfigureDependencyInjection(services);
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<IProviderFactory, ProviderFactory>();

            services.AddTransient(typeof(IBookRepository<SqlServer.Entities.BookEntity>),
                typeof(SqlServer.Repositories.BookRepository));
            services.AddTransient(typeof(IChapterRepository<SqlServer.Entities.ChapterEntity, SqlServer.Entities.VerseEntity>),
                typeof(SqlServer.Repositories.ChapterRepository));

            services.AddTransient(typeof(IBookRepository<Mongo.Entities.BookEntity>),
                typeof(Mongo.Repositories.BookRepository));
            services.AddTransient(typeof(IChapterRepository<Mongo.Entities.ChapterEntity, Mongo.Entities.VerseEntity>),
                typeof(Mongo.Repositories.ChapterRepository));

            //.. TB TODO make this cleverer to swap between data providers
            services.AddTransient(typeof(IBookService), typeof(BookService<SqlServer.Entities.BookEntity>));
            services.AddTransient(typeof(IChapterService), typeof(ChapterService<SqlServer.Entities.ChapterEntity, SqlServer.Entities.VerseEntity>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder => builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
