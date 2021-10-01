using Microsoft.Extensions.Configuration;

namespace PaliCanon.IntegrationTests.Sql.Infrastructure
{
    class TestConfig
    {
        internal IConfigurationRoot Configuration { get; set; }

        internal string Api { get; }

        internal TestConfig()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            Api = Configuration.GetValue<string>("Api");
        }
    }
}
