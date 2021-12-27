# Nancy.FixQueryDictionary
#### 简介
一个用来修复Nancy Http请求参数绑定时空参数绑定出错的nuget包
#### 使用方法
1.安装Nancy.FixQueryDictionary
```
Install-Package Nancy.FixQueryDictionary
```
2.引入命名空间
```C#
using Nancy.ModelBinding;
```
3.在前置拦截器中调用修复方法
```C#
/// <summary>
/// 前置拦截器
/// </summary>
/// <param name="ctx">NancyContext上下文对象</param>
/// <returns></returns>
private Response BeforeRequest(NancyContext ctx)
{
   ctx.FixQueryDictionary();
   //TODO:

   return ctx.Response;
}
```
