# Nancy.FixQueryDictionary
#### Build status

[![Nancy.FixQueryDictionary Latest Stable](https://img.shields.io/nuget/v/Nancy.FixQueryDictionary.svg)](https://www.nuget.org/packages/Nancy.FixQueryDictionary/)
![Github Build Status](https://img.shields.io/cirrus/github/pinzi/Nancy.FixQueryDictionary/publish?style=plastic)
![stars](https://img.shields.io/github/stars/pinzi/Nancy.FixQueryDictionary.svg?style=plastic)
![release](https://img.shields.io/github/v/release/pinzi/Nancy.FixQueryDictionary?include_prereleases)
![GitHub top language](https://img.shields.io/github/languages/top/pinzi/Nancy.FixQueryDictionary?logo=github)
![GitHub License](https://img.shields.io/github/license/pinzi/Nancy.FixQueryDictionary?logo=github)
![Nuget Downloads](https://img.shields.io/nuget/dt/Nancy.FixQueryDictionary?logo=nuget)
![Nuget](https://img.shields.io/nuget/v/Nancy.FixQueryDictionary?logo=nuget)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Nancy.FixQueryDictionary?label=dev%20nuget&logo=nuget)


#### 简介
一个用来修复Nancy Http请求空参数绑定出错的nuget包
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
