using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace PixivCS
{
    public class PixivAppAPI : PixivBaseAPI
    {
        public PixivAppAPI(string AccessToken, string RefreshToken, string UserID) :
            base(AccessToken, RefreshToken, UserID)
        { }

        public PixivAppAPI() : base() { }

        public PixivAppAPI(PixivBaseAPI BaseAPI) : base(BaseAPI) { }

        public async Task<HttpResponseMessage> RequestCall(string Method, string Url,
            Dictionary<string, string> Headers = null, Dictionary<string, string> Query = null,
            HttpContent Body = null, bool RequireAuth = true)
        {
            var headers = (Headers == null) ? new Dictionary<string, string>() : Headers;
            if (!(headers.ContainsKey("User-Agent") || headers.ContainsKey("user-agent")))
            {
                headers.Add("App-OS", "ios");
                headers.Add("App-OS-Version", "10.3.1");
                headers.Add("App-Version", "6.7.1");
                headers.Add("User-Agent", "PixivIOSApp/6.7.1 (iOS 10.3.1; iPhone8,1)");
            }
            if (RequireAuth) headers.Add("Authorization", string.Format("Bearer {0}", AccessToken));
            return await base.RequestCall(Method, Url, headers, Query, Body);
        }

        //用户详情
        public async Task<JsonObject> UserDetail(string UserID, string Filter = "for_ios",
            bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v1/user/detail";
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("user_id", UserID);
            query.Add("filter", Filter);
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //用户作品
        public async Task<JsonObject> UserIllusts(string UserID, string IllustType = "illust",
            string Filter = "for_ios", string Offset = null, bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v1/user/illusts";
            Dictionary<string, string> query = new Dictionary<string, string>();
            query.Add("user_id", UserID);
            query.Add("filter", Filter);
            if (!string.IsNullOrEmpty(IllustType)) query.Add("type", IllustType);
            if (!string.IsNullOrEmpty(Offset)) query.Add("offset", Offset);
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }
    }
}
