using System;
using System.Diagnostics;
using Nancy.Hosting.Self;

namespace Nancy.FixQueryDictionarySample
{
    public class NancySelfHost
    {
        /// <summary>
        /// NancyHost
        /// </summary>
        private NancyHost _nancyHost;

        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            const string uriStr = "http://localhost:8080";
            _nancyHost = new NancyHost(new Uri(uriStr));
            _nancyHost.Start();
            Process.Start("explorer.exe", uriStr);
            Console.WriteLine(" 服务已启动： " + uriStr);
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            _nancyHost.Stop();
            Console.WriteLine("服务已停止");
        }
    }
}
