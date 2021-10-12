using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaliCanon.Data.Sql;
using System;

namespace PaliCanon.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

#if DEBUG
            MigrateDatabase(host);
#endif            

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void MigrateDatabase(IHost host)
        {
            SqlContext dbContext = null;

            try
            {
                IServiceScope scope = host.Services.CreateScope();
                dbContext = scope.ServiceProvider.GetRequiredService<SqlContext>();
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed for connection{dbContext?.Database.GetConnectionString() ?? " no connction string specified"}", ex);
            }
        }
    }

}
