using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaliCanon.Contracts;
using Mongo = PaliCanon.Data.MongoDB;
using SqlServer = PaliCanon.Data.SqlServer;
using PaliCanon.DataLoad.Provider;

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

            services.AddTransient<IProviderFactory, ProviderFactory>();

            services.AddTransient<IBookRepository, SqlServer.Repositories.BookRepository>();
            services.AddTransient<IChapterRepository, SqlServer.Repositories.ChapterRepository>();

            //.. TB TODO make this cleverer to swap between data providers
            //services.AddTransient<IBookRepository, Mongo.Repositories.BookRepository>();
            //services.AddTransient<IChapterRepository, Mongo.Repositories.ChapterRepository>();

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
