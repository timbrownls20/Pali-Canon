using System;
using PaliCanon.DataLoad;

namespace PaliCanon.Loader
{
    class Program
    {
        public static void Main(string[] args)
        {   
            //IProvider[] providers = {
            //    new TheragathaProvider(new ChapterRepository(database)),
            //    new DhammapadaProvider(new ChapterRepository(database)) 
            //};

            //foreach(var provider in providers){
            //    provider.OnNotify += ConsoleNotify;
            //    provider.Load();
            //}
        }

        public static void ConsoleNotify(object sender, NotifyEventArgs args)
        {
            if (args.IsError)
                Console.Error.WriteLine(args.Message);
            else
                Console.WriteLine(args.Message);
        }
    }
}
