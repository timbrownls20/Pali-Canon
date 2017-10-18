using System;
using PaliCanon.Loader.Provider;
using PaliCanon.Common;
using PaliCanon.Common.Repository;

namespace PaliCanon.Loader
{
    class Program
    {

        public static void Main(string[] args)
        {   
            
            var mongo = new DBConnect();
            mongo.Drop();
            var database = mongo.Connect();
            IProvider provider = new DhammapadaProvider(new ChapterRepository(database));
            provider.OnNotify += ConsoleNotify;

            provider.Load();
        }

        public static void ConsoleNotify(object sender, NotifyEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}
