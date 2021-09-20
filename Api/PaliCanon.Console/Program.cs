using System;
using Microsoft.Extensions.DependencyInjection;
using PaliCanon.Contracts;
using PaliCanon.DataLoad;

namespace PaliCanon.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            var chapterRepository = serviceProvider.GetService<IChapterRepository>();

            IProvider[] providers = {
                //new TheragathaProvider(chapterRepository),
                new DhammapadaProvider(chapterRepository)
            };

            foreach (var provider in providers)
            {
                provider.OnNotify += ConsoleNotify;
                provider.Load();
            }
        }

        public static void ConsoleNotify(object sender, NotifyEventArgs args)
        {
            if (args.IsError)
                System.Console.Error.WriteLine(args.Message);
            else
                System.Console.WriteLine(args.Message);
        }
    }
}
