using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaliCanon.Contracts.Book;
using PaliCanon.Contracts.Chapter;
using PaliCanon.Data.Sql;
using PaliCanon.Data.Sql.Entities;
using PaliCanon.Data.Sql.Repositories;
using Mongo = PaliCanon.Data.MongoDB;
using PaliCanon.DataLoad.Provider.Factory;
using PaliCanon.Services;
using System;
using PaliCanon.Contracts;

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
            services.AddDbContext<SqlContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
                options.UseMySql(Configuration.GetConnectionString("MySql"), new MySqlServerVersion(new Version(8, 0, 19)))
                    .EnableSensitiveDataLogging() 
                    .EnableDetailedErrors();
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mongo.MappingProfile());
                mc.AddProfile(new MappingProfile());
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

            //.. TB TODO make this cleverer to swap between data providers
            services.AddTransient(typeof(IBookRepository<BookEntity>),
                typeof(BookRepository));
            services.AddTransient(typeof(IChapterRepository<ChapterEntity, VerseEntity>),
                typeof(ChapterRepository));
            services.AddTransient(typeof(IAdminRepository),
                typeof(AdminRepository));

            services.AddTransient(typeof(IBookRepository<Mongo.Entities.BookEntity>),
                typeof(Mongo.Repositories.BookRepository));
            services.AddTransient(typeof(IChapterRepository<Mongo.Entities.ChapterEntity, Mongo.Entities.VerseEntity>),
                typeof(Mongo.Repositories.ChapterRepository));

            
            services.AddTransient(typeof(IBookService), typeof(BookService<BookEntity>));
            services.AddTransient(typeof(IChapterService), typeof(ChapterService<ChapterEntity, VerseEntity>));
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
