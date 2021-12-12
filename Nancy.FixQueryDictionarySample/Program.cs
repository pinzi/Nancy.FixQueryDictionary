using System;
using Topshelf;

namespace Nancy.FixQueryDictionarySample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
          {
              x.Service<NancySelfHost>(s =>
              {
                  s.ConstructUsing(name => new NancySelfHost());
                  s.WhenStarted(tc => tc.Start());
                  s.WhenStopped(tc => tc.Stop());
              });
              x.RunAsLocalSystem();
              x.SetDescription("Nancy SelfHost Demo");
              x.SetDisplayName("Support by TopSelf");
              x.SetServiceName("Nancy SelfHost");
          });
            Console.ReadKey();
        }
    }
}
