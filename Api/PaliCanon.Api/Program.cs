using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaliCanon.Data.Sql;

namespace PaliCanon.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            MigrateDatabase(host);    
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void MigrateDatabase(IHost host)
        {
            try
            {
                IServiceScope scope = host.Services.CreateScope();
                SqlContext dbContext = scope.ServiceProvider.GetRequiredService<SqlContext>();
                dbContext.Database.Migrate();
            }
            catch //(Exception ex)
            {
                //throw new Exception($"Failed for connection{dbContext?.Database.GetConnectionString() ?? " no connction string specified"}", ex);
            }
        }
    }

}
