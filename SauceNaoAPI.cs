using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using System.Collections.Specialized;
using System.Net;

namespace PixivCS
{
    /// <summary>
    /// 这是一个SauceNAO的API帮助类(SauceNaoAPI_Helper)
    /// </summary>
    public static class SauceNaoAPI
    {
        private static string SauceNAO_API;
        private static WebClient GetWebClient(string url)
        {
            if (string.IsNullOrEmpty(SauceNAO_API))
            {
                throw new NullReferenceException("SauceNAO_API_Key Is Null");
            }
            WebClient WebClient = new WebClient
            {
                QueryString = new NameValueCollection
                {
                    { "db", "999" },
                    { "output_type", "2" },
                    { "numres", "16" },
                    { "api_key", SauceNAO_API },
                    { "url", url }
                }
            };
            return WebClient;
        }
        /// <summary>
        /// 设置SauceNAO的API Key
        /// </summary>
        /// <param name="SauceNAOApiKey">SauceNAO Api</param>
        public static void SetApiKey(string SauceNAOApiKey) => SauceNAO_API = SauceNAOApiKey;

        /// <summary>
        /// 搜索并返回结果
        /// </summary>
        /// <param name="url">图像链接</param>
        /// <returns>原始Json对象</returns>
        /// <exception cref="NullReferenceException">SauceNAO_API 为空</exception>
        /// <exception cref="WebException">通过组合System.Net.WebClient.BaseAddress和地址形成的URI无效。<para/> -或者- 下载资源时发生错误。</exception>
        /// <exception cref="NotSupportedException">该方法已在多个线程上同时调用。</exception>
        public static JsonObject Search(string url)
        {
            using (var web = GetWebClient(url))
            {
                return JsonObject.Parse(web.DownloadString("https://saucenao.com/search.php"));
            }
        }

        /// <summary>
        /// 异步返回搜索结果
        /// </summary>
        /// <param name="url">图像链接</param>
        /// <returns>原始Json对象</returns>
        /// <exception cref="NullReferenceException">SauceNAO_API 为空</exception>
        /// <exception cref="WebException">通过组合System.Net.WebClient.BaseAddress和地址形成的URI无效。<para/> -或者- 下载资源时发生错误。</exception>
        public static async Task<JsonObject> SearchAsync(string url)
        {
            using (var web = GetWebClient(url))
                return JsonObject.Parse(await web.DownloadStringTaskAsync("https://saucenao.com/search.php"));
        }
    }
}