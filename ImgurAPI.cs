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
    /// Imgur API 这东西在国内会被墙的
    /// </summary>
    public static class ImgurAPI
    {
        private static string Imgur_API;
        private static NameValueCollection GetValue(byte[] Image) => new NameValueCollection { { "image", Convert.ToBase64String(Image) } };
        private static WebClient GetWebClient()
        {
            if (string.IsNullOrEmpty(Imgur_API))
            {
                throw new NullReferenceException("Imgur_API_Key Is Null");
            }
            return new WebClient { Headers = new WebHeaderCollection { "Authorization: Client-ID " + Imgur_API } };
        }

        /// <summary>
        /// 设置Imgur的API Key
        /// </summary>
        /// <param name="ImgurApiKey">Imgur Api</param>
        public static void SetApiKey(string api_key) => Imgur_API = api_key;

        /// <summary>
        /// 上传图像
        /// </summary>
        /// <param name="Image">图像Byte[]</param>
        /// <returns>返回Json对象</returns>
        /// <exception cref="NullReferenceException">Imgur_API_Key为null</exception>
        /// <exception cref="ArgumentNullException">Image 为null</exception>
        /// <exception cref="WebException">通过组合System.Net.WebClient.BaseAddress和地址形成的URI无效。<para/>
        /// -或-数据为空。<para/>
        /// -或-从承载资源的服务器没有响应。<para/>
        /// -或-打开流时发生错误。<para/>
        /// -或-内容类型标头不为null 或“ application/x-www-form-urlencoded”。</exception>
        public static JsonObject Upload(byte[] Image)
        {
            using (var web = GetWebClient())
                return JsonObject.Parse(Encoding.UTF8.GetString(web.UploadValues("https://api.imgur.com/3/upload", GetValue(Image))));
        }

        /// <summary>
        /// 上传图像(异步)
        /// </summary>
        /// <param name="Image">图像字节数组</param>
        /// <returns>包含图像链接的Json对象</returns>
        /// <exception cref="NullReferenceException">Imgur_API_Key为null</exception>
        /// <exception cref="ArgumentNullException">Image 为null</exception>
        /// <exception cref="WebException">通过组合System.Net.WebClient.BaseAddress和地址形成的URI无效。<para/>
        /// -或-数据为空。<para/>
        /// -或-从承载资源的服务器没有响应。<para/>
        /// -或-打开流时发生错误。<para/>
        /// -或-内容类型标头不为null 或“ application/x-www-form-urlencoded”。</exception>
        public static async Task<JsonObject> UploadAsync(byte[] Image)
        {
            using (var web = GetWebClient())
                return JsonObject.Parse(Encoding.UTF8.GetString(await web.UploadValuesTaskAsync("https://api.imgur.com/3/upload", GetValue(Image))));
        }
    }
}
