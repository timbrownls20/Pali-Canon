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
            
            IProvider[] providers = new IProvider[]{
                new TheragathaProvider(new ChapterRepository(database)),
                new DhammapadaProvider(new ChapterRepository(database)) 
            };

            foreach(var provider in providers){
                provider.OnNotify += ConsoleNotify;
                provider.Load();
            }
        }

        public static void ConsoleNotify(object sender, NotifyEventArgs args)
        {
            if(args.IsError)
                Console.Error.WriteLine(args.Message);
            else
                Console.WriteLine(args.Message);
        }
    }
}
