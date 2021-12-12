using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nancy.FixQueryDictionaryTest
{
    public class Url
    {
        private string query;


        /// <summary>
        /// Initializes a new instance of the <see cref="Url" /> class, with
        /// the provided <paramref name="url"/>.
        /// </summary>
        /// <param name="url">A <see cref="string" /> containing a URL.</param>
        public Url(string url)
        {
            var uri = new Uri(url);
            this.Query = uri.Query;
        }

        /// <summary>
        /// Gets the querystring data of the requested resource.
        /// </summary>
        public string Query
        {
            get { return this.query; }
            set { this.query = GetQuery(value); }
        }
        private static string GetQuery(string query)
        {
            return string.IsNullOrEmpty(query) ? string.Empty : (query[0] == '?' ? query : '?' + query);
        }
    }
}
