using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Nancy.FixQueryDictionaryTest
{
    public class HttpUtility
    {
        /// <summary>
        /// Decodes an HTML-encoded string and returns the decoded string.
        /// </summary>
        /// <param name="s">The HTML string to decode. </param>
        /// <returns>The decoded text.</returns>
        public static string HtmlDecode(string s)
        {
#if NET_4_0
			if (s == null)
				return null;

			using (var sw = new StringWriter ()) {
				HttpEncoder.Current.HtmlDecode (s, sw);
				return sw.ToString ();
			}
#else
            return HttpEncoder.HtmlDecode(s);
#endif
        }

        static int GetInt(byte b)
        {
            char c = (char)b;
            if (c >= '0' && c <= '9')
                return c - '0';

            if (c >= 'a' && c <= 'f')
                return c - 'a' + 10;

            if (c >= 'A' && c <= 'F')
                return c - 'A' + 10;

            return -1;
        }

        static int GetChar(string str, int offset, int length)
        {
            int val = 0;
            int end = length + offset;
            for (int i = offset; i < end; i++)
            {
                char c = str[i];
                if (c > 127)
                    return -1;

                int current = GetInt((byte)c);
                if (current == -1)
                    return -1;
                val = (val << 4) + current;
            }

            return val;
        }

        static void WriteCharBytes(IList buf, char ch, Encoding e)
        {
            if (ch > 255)
            {
                foreach (byte b in e.GetBytes(new char[] { ch }))
                    buf.Add(b);
            }
            else
                buf.Add((byte)ch);
        }


        public static string UrlDecode(string s, Encoding e)
        {
            if (null == s)
                return null;

            if (s.IndexOf('%') == -1 && s.IndexOf('+') == -1)
                return s;

            if (e == null)
                e = Encoding.UTF8;

            long len = s.Length;
            var bytes = new List<byte>();
            int xchar;
            char ch;

            for (int i = 0; i < len; i++)
            {
                ch = s[i];
                if (ch == '%' && i + 2 < len && s[i + 1] != '%')
                {
                    if (s[i + 1] == 'u' && i + 5 < len)
                    {
                        // unicode hex sequence
                        xchar = GetChar(s, i + 2, 4);
                        if (xchar != -1)
                        {
                            WriteCharBytes(bytes, (char)xchar, e);
                            i += 5;
                        }
                        else
                            WriteCharBytes(bytes, '%', e);
                    }
                    else if ((xchar = GetChar(s, i + 1, 2)) != -1)
                    {
                        WriteCharBytes(bytes, (char)xchar, e);
                        i += 2;
                    }
                    else
                    {
                        WriteCharBytes(bytes, '%', e);
                    }
                    continue;
                }

                if (ch == '+')
                    WriteCharBytes(bytes, ' ', e);
                else
                    WriteCharBytes(bytes, ch, e);
            }

            byte[] buf = bytes.ToArray();
            bytes = null;
            return e.GetString(buf);

        }

        public static NameValueCollection ParseQueryString(string query)
        {
            return ParseQueryString(query, Encoding.UTF8);
        }

        public static NameValueCollection ParseQueryString(string query, bool caseSensitive)
        {
            return ParseQueryString(query, Encoding.UTF8, caseSensitive);
        }

        public static NameValueCollection ParseQueryString(string query, Encoding encoding)
        {
            return ParseQueryString(query, encoding, StaticConfiguration.CaseSensitive);
        }

        public static NameValueCollection ParseQueryString(string query, Encoding encoding, bool caseSensitive)
        {
            if (query == null)
                throw new ArgumentNullException("query");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if (query.Length == 0 || (query.Length == 1 && query[0] == '?'))
                return new HttpQSCollection(caseSensitive);
            if (query[0] == '?')
                query = query.Substring(1);

            NameValueCollection result = new HttpQSCollection(caseSensitive);
            //此处更换为我们自定义的修复方法
            ParseQueryStringFix(query, encoding, result);
            return result;
        }

        #region 原方法
        internal static void ParseQueryString(string query, Encoding encoding, NameValueCollection result)
        {
            if (query.Length == 0)
                return;

            var decoded = HtmlDecode(query);

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
                var decoded = UrlDecode(segment, encoding);
                return new KeyValuePair<string, string>(decoded, decoded);
            }

            var key = UrlDecode(segment.Substring(0, indexOfEquals), encoding);
            var length = (segment.Length - indexOfEquals) - 1;
            var value = UrlDecode(segment.Substring(indexOfEquals + 1, length), encoding);
            return new KeyValuePair<string, string>(key, value);
        }
        #endregion

        #region 修复方法
        internal static void ParseQueryStringFix(string query, Encoding encoding, NameValueCollection result)
        {
            if (query.Length == 0)
                return;

            var decoded = HtmlDecode(query);
            if (decoded.IndexOf('&') == -1)
            {
                decoded = UrlDecode(decoded, encoding);
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
                segment = UrlDecode(segment, encoding);
                indexOfEquals = segment.IndexOf('=');
                if (indexOfEquals == -1)
                {
                    return new KeyValuePair<string, string>(segment, "");
                }
            }
            var key = UrlDecode(segment.Substring(0, indexOfEquals), encoding);
            var length = (segment.Length - indexOfEquals) - 1;
            var value = UrlDecode(segment.Substring(indexOfEquals + 1, length), encoding);
            var res = new KeyValuePair<string, string>(key, value);
            return res;
        }
        #endregion    
    }
}
