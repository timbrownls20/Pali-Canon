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
        internal ServiceProvider GetServiceProvider()
        {
            var config = new TestConfig();
            var services = new ServiceCollection();

            services.AddDbContext<SqlContext>(options =>
            {
                options.UseMySql(config.Configuration.GetConnectionString("MySql"),
                        ServerVersion.AutoDetect(config.Configuration.GetConnectionString("MySql")))
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