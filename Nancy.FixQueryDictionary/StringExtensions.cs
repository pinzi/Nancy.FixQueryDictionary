using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using HttpUtility = Nancy.Helpers.HttpUtility;

namespace Nancy.FixQueryDictionary
{
    /// <summary>
    /// Nancy Http请求参数字典解析错误修复扩展方法
    /// </summary>
    public static class NancyFixQueryDictionaryExtensions
    {
        /// <summary>
        /// 修复Http请求参数字典解析错误
        /// </summary>
        /// <param name="ctx">NancyContext对象</param>
        /// <returns>NancyContext对象</returns>
        public static NancyContext FixQueryDictionary(this NancyContext ctx)
        {
            if (ctx == null)
            {
                return ctx;
            }
            ctx.Request.Query = ctx.Request.Url.Query.AsQueryDictionary();
            return ctx;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static DynamicDictionary AsQueryDictionary(this string queryString)
        {
            var coll = ParseQueryString(queryString);
            var ret = new DynamicDictionary();
            var found = 0;
            foreach (var key in coll.AllKeys.Where(key => key != null))
            {
                ret[key] = coll[key];
                found++;
                if (found >= StaticConfiguration.RequestQueryFormMultipartLimit)
                {
                    break;
                }
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static NameValueCollection ParseQueryString(string query)
        {
            return ParseQueryString(query, Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public static NameValueCollection ParseQueryString(string query, bool caseSensitive)
        {
            return ParseQueryString(query, Encoding.UTF8, caseSensitive);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static NameValueCollection ParseQueryString(string query, Encoding encoding)
        {
            return ParseQueryString(query, encoding, StaticConfiguration.CaseSensitive);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="encoding"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static NameValueCollection ParseQueryString(string query, Encoding encoding, bool caseSensitive)
        {
            if (query == null)
                throw new ArgumentNullException("query");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if (query.Length == 0 || (query.Length == 1 && query[0] == '?'))
                return new NameValueCollection(StringComparer.Ordinal);
            if (query[0] == '?')
                query = query.Substring(1);

            NameValueCollection result = new NameValueCollection(StringComparer.Ordinal);
            ParseQueryStringFix(query, encoding, result);
            return result;
        }

        #region 原方法
        internal static void ParseQueryString(string query, Encoding encoding, NameValueCollection result)
        {
            if (query.Length == 0)
                return;

            var decoded = HttpUtility.HtmlDecode(query);

            var segments = decoded.Split(new[] { '&' }, StringSplitOptions.None);

            foreach (var segment in segments)
            {
                var keyValuePair = ParseQueryStringSegment(segment, encoding);
                if (!Equals(keyValuePair, default(KeyValuePair<string, string>)))
                    result.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        private static KeyValuePair<string, string> ParseQueryStringSegment(string segment, Encoding encoding)
        {
            if (String.IsNullOrWhiteSpace(segment))
                return default(KeyValuePair<string, string>);

            var indexOfEquals = segment.IndexOf('=');
            if (indexOfEquals == -1)
            {
                var decoded = HttpUtility.UrlDecode(segment, encoding);
                return new KeyValuePair<string, string>(decoded, decoded);
            }

            var key = HttpUtility.UrlDecode(segment.Substring(0, indexOfEquals), encoding);
            var length = (segment.Length - indexOfEquals) - 1;
            var value = HttpUtility.UrlDecode(segment.Substring(indexOfEquals + 1, length), encoding);
            return new KeyValuePair<string, string>(key, value);
        }
        #endregion

        #region 修复方法
        internal static void ParseQueryStringFix(string query, Encoding encoding, NameValueCollection result)
        {
            if (query.Length == 0)
                return;

            var decoded = HttpUtility.HtmlDecode(query);
            if (decoded.IndexOf('&') == -1)
            {
                decoded = HttpUtility.UrlDecode(decoded, encoding);
            }
            var segments = decoded.Split(new[] { '&' }, StringSplitOptions.None);

            foreach (var segment in segments)
            {
                var keyValuePair = ParseQueryStringSegmentFix(segment, encoding);
                if (!Equals(keyValuePair, default(KeyValuePair<string, string>)))
                    result.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        private static KeyValuePair<string, string> ParseQueryStringSegmentFix(string segment, Encoding encoding)
        {
            if (String.IsNullOrWhiteSpace(segment))
                return default(KeyValuePair<string, string>);

            var indexOfEquals = segment.IndexOf('=');
            if (indexOfEquals == -1)
            {
                segment = HttpUtility.UrlDecode(segment, encoding);
                indexOfEquals = segment.IndexOf('=');
                if (indexOfEquals == -1)
                {
                    return new KeyValuePair<string, string>(segment, "");
                }
            }
            var key = HttpUtility.UrlDecode(segment.Substring(0, indexOfEquals), encoding);
            var length = (segment.Length - indexOfEquals) - 1;
            var value = HttpUtility.UrlDecode(segment.Substring(indexOfEquals + 1, length), encoding);
            var res = new KeyValuePair<string, string>(key, value);
            return res;
        }
        #endregion
    }
}