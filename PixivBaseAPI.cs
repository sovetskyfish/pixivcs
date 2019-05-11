using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PixivCS
{
    public class PixivException : Exception
    {
        public PixivException() { }
        public PixivException(string msg) : base(msg) { }
    }

    public class PixivBaseAPI
    {
        internal const string client_id = "MOBrBDS8blbauoSck0ZfDbtuzpyT";
        internal const string client_secret = "lsACyCD94FhDUtGTXi3QzcFE2uU1hqtDaKeqrdwj";

        public string access_token { get; internal set; }
        public string refresh_token { get; internal set; }
        public string user_id { get; internal set; }

        public PixivBaseAPI(string access_token, string refresh_token, string user_id)
        {
            this.access_token = access_token;
            this.refresh_token = refresh_token;
            this.user_id = user_id;
        }

        public PixivBaseAPI() : this(null, null, null) { }

        public PixivBaseAPI(PixivBaseAPI baseapi) :
            this(baseapi.access_token, baseapi.refresh_token, baseapi.user_id)
        { }

        //用于生成带参数的url
        private static string getQueryString(NameValueCollection query)
        {
            var array = (from key in query.AllKeys
                         from value in query.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), 
                         HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }

        public void require_auth()
        {
            if (access_token == null) throw new PixivException("Authentication required!");
        }

        public void csfriendly_request_call(string method, string url,
            WebHeaderCollection headers = null, NameValueCollection query = null,
            NameValueCollection body = null)
        {
            
        }
    }
}