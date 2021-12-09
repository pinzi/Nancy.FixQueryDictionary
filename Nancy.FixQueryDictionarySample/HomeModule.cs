using Nancy.ModelBinding;

namespace Nancy.FixQueryDictionarySample
{
    /// <summary>
    /// 演示webapi
    /// </summary>
    public class HomeModule : NancyModule
    {
        /// <summary>
        /// 默认初始化
        /// </summary>
        public HomeModule()
        {
            Before += BeforeRequest;
            //hello world
            Get["/"] = _ => "Hello World!";
            //GET请求示例
            Get["/get"] = GetSamp;
            //POST请求示例
            Post["/post"] = PostSample;
        }

        /// <summary>
        /// 前置拦截器
        /// </summary>
        /// <param name="ctx">NancyContext上下文对象</param>
        /// <returns></returns>
        private Response BeforeRequest(NancyContext ctx)
        {
            //ctx.FixQueryDictionary();
            //TODO:

            return ctx.Response;
        }

        /// <summary>
        /// GET请求示例
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetSamp(dynamic _)
        {
            var req = this.Bind<SampleInDto>();
            return Response.AsJson(req);
        }
        /// <summary>
        /// POST请求示例
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response PostSample(dynamic _)
        {
            var req = this.Bind<SampleInDto>();
            return Response.AsJson(req);
        }
    }
}
