using System;

namespace Nancy.FixQueryDictionaryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string format = "http://localhost:5050/hello?{0}";
            var context = new NancyContext();
            string[] arrNormal = new string[] {
                                                        string.Format(format, "name=张三&age=20"),
                                                        string.Format(format, "name=张三&age="),
                                                        string.Format(format, "name&age"),
                                                        string.Format(format, "name=&age")
                                                    };
            string[] arrEncodeKeyVal = new string[] {
                                                        string.Format(format, "name%3D%E5%BC%A0%E4%B8%89%26age%3D20"),
                                                        string.Format(format, "name%3D%E5%BC%A0%E4%B8%89%26age%3D"),
                                                        string.Format(format, "name%26age"),
                                                        string.Format(format, "name%3D%26age")
                                                    };
            string[] arrEncodeVal = new string[] {
                                                        string.Format(format, "name=%E5%BC%A0%E4%B8%89&age=20"),
                                                        string.Format(format, "name=%E5%BC%A0%E4%B8%89&age="),
                                                        string.Format(format, "name=%E5%BC%A0%E4%B8%89&age")
                                                    };
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>明文参数>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            foreach (string _url in arrNormal)
            {
                Console.WriteLine($"=============={_url}==============");
                var url = new Url(_url);
                context.Request = new Request(url);
                var dic = context.Request.Query as DynamicDictionary;
                foreach (string key in dic.Keys)
                {
                    Console.WriteLine($"{key}={dic[key]}");
                }
            }
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>编码参数键值对>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            foreach (string _url in arrEncodeKeyVal)
            {
                Console.WriteLine($"=============={_url}==============");
                var url = new Url(_url);
                context.Request = new Request(url);
                var dic = context.Request.Query as DynamicDictionary;
                foreach (string key in dic.Keys)
                {
                    Console.WriteLine($"{key}={dic[key]}");
                }
            }
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>编码参数值>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            foreach (string _url in arrEncodeVal)
            {
                Console.WriteLine($"=============={_url}==============");
                var url = new Url(_url);
                context.Request = new Request(url);
                var dic = context.Request.Query as DynamicDictionary;
                foreach (string key in dic.Keys)
                {
                    Console.WriteLine($"{key}={dic[key]}");
                }
            }
            Console.ReadKey();
        }
    }
}
