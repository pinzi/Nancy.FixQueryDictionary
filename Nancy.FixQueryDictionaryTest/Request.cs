using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nancy.FixQueryDictionaryTest
{
    public class Request
    {
        /// <summary>
        /// Gets the query string data of the requested resource.
        /// </summary>
        /// <value>A <see cref="DynamicDictionary"/>instance, containing the key/value pairs of query string data.</value>
        public dynamic Query { get; set; }
        private dynamic form = new DynamicDictionary();
        public Request(Url url)
        {
            this.Query = url.Query.AsQueryDictionary();
            this.ParseFormData();
        }

        private void ParseFormData()
        {
            //if (this.Headers.ContentType == null)
            //{
            //    return;
            //}

            //var contentType = this.Headers.ContentType;
            //if (contentType.Matches("application/x-www-form-urlencoded"))
            //{
            //    var reader = new StreamReader(this.Body);
            //    this.form = reader.ReadToEnd().AsQueryDictionary();
            //    this.Body.Position = 0;
            //}

            //if (!contentType.Matches("multipart/form-data"))
            //{
            //    return;
            //}

            //var boundary = Regex.Match(contentType, @"boundary=""?(?<token>[^\n\;\"" ]*)").Groups["token"].Value;
            //var multipart = new HttpMultipart(this.Body, boundary);

            //var formValues =
            //    new NameValueCollection(StaticConfiguration.CaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);

            //foreach (var httpMultipartBoundary in multipart.GetBoundaries())
            //{
            //    if (string.IsNullOrEmpty(httpMultipartBoundary.Filename))
            //    {
            //        var reader =
            //            new StreamReader(httpMultipartBoundary.Value);
            //        formValues.Add(httpMultipartBoundary.Name, reader.ReadToEnd());

            //    }
            //    else
            //    {
            //        this.files.Add(new HttpFile(httpMultipartBoundary));
            //    }
            //}

            //foreach (var key in formValues.AllKeys.Where(key => key != null))
            //{
            //    this.form[key] = formValues[key];
            //}

            //this.Body.Position = 0;
        }
    }
}
