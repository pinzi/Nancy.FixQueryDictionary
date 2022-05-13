# Nancy.FixQueryDictionary
#### Build status

[![Nancy.FixQueryDictionary Latest Stable](https://img.shields.io/nuget/v/Nancy.FixQueryDictionary.svg)](https://www.nuget.org/packages/Nancy.FixQueryDictionary/)

![Github Build Status](https://img.shields.io/cirrus/github/pinzi/Nancy.FixQueryDictionary/publish?style=plastic)

![languages](https://img.shields.io/github/languages/top/pinzi/Nancy.FixQueryDictionary.svg)

![license](https://img.shields.io/badge/license-MIT-blue.svg)

![stars](https://img.shields.io/github/stars/pinzi/Nancy.FixQueryDictionary.svg?style=plastic)

![release](https://img.shields.io/github/v/release/pinzi/Nancy.FixQueryDictionary?include_prereleases)

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
