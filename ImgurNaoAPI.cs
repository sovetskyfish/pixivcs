//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Collections.Specialized;
//using System.Net;

//namespace PixivCS
//{
//    public class ImgurNaoAPI
//    {
//        private string SauceNAO_API;
//        private string Imgur_API;

//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        /// <param name="SauceNAOApiKey">SauceNAO Api</param>
//        /// <param name="ImgurApiKey">Imgur Api</param>
//        public ImgurNaoAPI(string SauceNAOApiKey, string ImgurApiKey)
//        {
//            SauceNAO_API = SauceNAOApiKey;
//            Imgur_API = ImgurApiKey;
//        }

//        /// <summary>
//        /// 上传图像
//        /// </summary>
//        /// <param name="Image">图像字节数组</param>
//        /// <returns>包含图像链接的Json对象</returns>
//        public JsonObject UpLoad(byte[] Image)
//        {
//            WebClient WebClient;
//            WebClient = new WebClient();
//            WebClient.Headers.Add("Authorization: Client-ID " + Imgur_API);
//            var values = new NameValueCollection
//            {
//                { "image", Convert.ToBase64String(Image) }
//            };
//            return JsonObject.Parse(Encoding.UTF8.GetString(WebClient.UploadValues("https://api.imgur.com/3/upload", values))).GetNamedObject("data");
//        }

//        /// <summary>
//        /// 下载Json对象
//        /// </summary>
//        /// <param name="url">图像链接</param>
//        /// <returns>经过处理的包含Pixiv ID , title , member_name , member_id , ext_urls 的Json对象</returns>
//        public JsonObject DownLoad(string url)
//        {
//            WebClient WebClient = new WebClient();
//            WebClient.QueryString.Add("db", "999");
//            WebClient.QueryString.Add("output_type", "2");
//            WebClient.QueryString.Add("numres", "16");
//            WebClient.QueryString.Add("api_key", SauceNAO_API);
//            WebClient.QueryString.Add("url", url);
//            try
//            {
//                string response = WebClient.DownloadString("https://saucenao.com/search.php");
//                JsonObject dynObj = JsonObject.Parse(response);
//                return dynObj.GetNamedArray("results")[0].GetObject().GetNamedObject("data");
//            }
//            catch (Exception e)
//            {
//                System.Diagnostics.Debug.WriteLine(e.Message);
//            }
//            return null;
//        }
//    }
//}
