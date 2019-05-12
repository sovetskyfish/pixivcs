using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Windows.Data.Json;

namespace PixivCS
{
    public class PixivException : Exception
    {
        public PixivException() { }
        public PixivException(string msg) : base(msg) { }
    }

    public class PixivBaseAPI
    {
        internal string clientID = "MOBrBDS8blbauoSck0ZfDbtuzpyT";
        internal string clientSecret = "lsACyCD94FhDUtGTXi3QzcFE2uU1hqtDaKeqrdwj";

        public string AccessToken { get; internal set; }
        public string RefreshToken { get; internal set; }
        public string UserID { get; internal set; }

        public PixivBaseAPI(string AccessToken, string RefreshToken, string UserID)
        {
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken;
            this.UserID = UserID;
        }

        public PixivBaseAPI() : this(null, null, null) { }

        public PixivBaseAPI(PixivBaseAPI BaseAPI) :
            this(BaseAPI.AccessToken, BaseAPI.RefreshToken, BaseAPI.UserID)
        { }

        //用于生成带参数的url
        private static string getQueryString(Dictionary<string, string> query)
        {
            var array = (from key in query.Keys
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key),
                         HttpUtility.UrlEncode(query[key])))
                .ToArray();
            return "?" + string.Join("&", array);
        }

        public void RequireAuth()
        {
            if (AccessToken == null) throw new PixivException("Authentication required!");
        }

        public async Task<HttpResponseMessage> RequestCall(string Method, string Url,
            Dictionary<string, string> Headers = null, Dictionary<string, string> Query = null,
            HttpContent Body = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (Headers != null)
                    foreach ((var k, var v) in Headers)
                        client.DefaultRequestHeaders.Add(k, v);
                string queryUrl = Url + ((Query != null) ? getQueryString(Query) : "");
                switch (Method.ToLower())
                {
                    case "get":
                        return await client.GetAsync(queryUrl);
                    case "post":
                        return await client.PostAsync(queryUrl, Body);
                    default:
                        throw new PixivException("Unsupported method");
                }
            }
        }

        public async Task<string> GetResponseString(HttpResponseMessage Response)
        {
            return await Response.Content.ReadAsStringAsync();
        }

        public void SetAuth(string AccessToken, string RefreshToken = null)
        {
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken;
        }

        public void SetClient(string ClientID, string ClientSecret)
        {
            clientID = ClientID;
            clientSecret = ClientSecret;
        }

        //用户名和密码登录
        public async Task Auth(string Username, string Password)
        {
            string url = "https://oauth.secure.pixiv.net/auth/token";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)");
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("get_secure_url", "1");
            data.Add("client_id", clientID);
            data.Add("client_secret", clientSecret);
            data.Add("grant_type", "password");
            data.Add("username", Username);
            data.Add("password", Password);
            var res = await RequestCall("POST", url, headers, Body: new FormUrlEncodedContent(data));
            int status = (int)res.StatusCode;
            if (!(status == 200 || status == 301 || status == 302))
                throw new PixivException("[ERROR] Auth() failed! Check Username and Password.");
            var resJSON = JsonObject.Parse(await GetResponseString(res));
            AccessToken = resJSON["response"].GetObject()["access_token"].GetString();
            UserID = resJSON["response"].GetObject()["user"].GetObject()["id"].GetString();
            RefreshToken = resJSON["response"].GetObject()["refresh_token"].GetString();
        }

        //RefreshToken登录
        public async Task Auth(string RefreshToken)
        {
            string url = "https://oauth.secure.pixiv.net/auth/token";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)");
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("get_secure_url", "1");
            data.Add("client_id", clientID);
            data.Add("client_secret", clientSecret);
            data.Add("grant_type", "refresh_token");
            data.Add("refresh_token", RefreshToken);
            var res = await RequestCall("POST", url, headers, Body: new FormUrlEncodedContent(data));
            int status = (int)res.StatusCode;
            if (!(status == 200 || status == 301 || status == 302))
                throw new PixivException("[ERROR] Auth() failed! Check Username and Password.");
            var resJSON = JsonObject.Parse(await GetResponseString(res));
            AccessToken = resJSON["response"].GetObject()["access_token"].GetString();
            UserID = resJSON["response"].GetObject()["user"].GetObject()["id"].GetString();
            this.RefreshToken = resJSON["response"].GetObject()["refresh_token"].GetString();
        }
    }
}