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
              x.SetDisplayName("support by TopSelf");
              x.SetServiceName("Nancy SelfHost");
          });

            //var url = new Url("http://127.0.0.1:9090");
            //var hostConfig = new HostConfiguration()
            //{
            //    UrlReservations = new UrlReservations { CreateAutomatically = true }
            //};
            //using (var host = new NancyHost(hostConfig, url))
            //{
            //    host.Start();
            //    Console.WriteLine("Your application is running on " + url);
            //}
            //Process.Start("explorer.exe", "http://localhost:9090");
            Console.ReadKey();
        }
    }
}
