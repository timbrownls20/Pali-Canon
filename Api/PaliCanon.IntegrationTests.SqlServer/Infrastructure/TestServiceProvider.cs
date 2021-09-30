using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaliCanon.Contracts.Book;
using PaliCanon.Data.Sql;
using PaliCanon.Data.Sql.Entities;
using PaliCanon.Data.Sql.Repositories;

namespace PaliCanon.IntegrationTests.Sql.Infrastructure
{
    internal class TestServiceProvider
    {
        internal IConfigurationRoot Configuration { get; set; }

        internal ServiceProvider GetServiceProvider()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddDbContext<SqlContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MySql"),
                        ServerVersion.AutoDetect(Configuration.GetConnectionString("MySql")))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IBookRepository<BookEntity>, BookRepository>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }

}