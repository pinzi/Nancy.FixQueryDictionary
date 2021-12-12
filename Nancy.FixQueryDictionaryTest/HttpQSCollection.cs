using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nancy.FixQueryDictionaryTest
{
    sealed class HttpQSCollection : NameValueCollection
    {
        public HttpQSCollection()
            : this(StaticConfiguration.CaseSensitive)
        {
        }

        public HttpQSCollection(bool caseSensitive)
            : base(caseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase)
        {
        }

        public override string ToString()
        {
            int count = Count;
            if (count == 0)
                return "";
            StringBuilder sb = new StringBuilder();
            string[] keys = AllKeys;
            for (int i = 0; i < count; i++)
            {
                sb.AppendFormat("{0}={1}&", keys[i], this[keys[i]]);
            }
            if (sb.Length > 0)
                sb.Length--;
            return sb.ToString();
        }

    }
}
