using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaliCanon.Contracts;
using PaliCanon.Data.MongoDB;
using PaliCanon.Data.MongoDB.Repositories;

namespace PaliCanon.Console
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

                Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IChapterRepository, ChapterRepository>();
        }
    }
}
