using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace PixivCS
{
    /// <summary>
    /// 贴图库图床的API
    /// </summary>
    public static class TietukuAPI
    {
        private const string URI = "http://up.imgapi.com/";
        private static string Token;        
        private static int AID;
        private static int Deadline => 60 + Timestamp;
        private static int Timestamp => (int)(DateTime.Now -
            TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local)).TotalSeconds;
        /// <summary>
        /// 设置Token和相册ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="aid"></param>
        public static void SetToken(string token,int aid)
        {
            AID = aid;
            Token = token;
        }
        /// <summary>
        /// 异步上传 懒得搞同步版了
        /// </summary>
        /// <param name="Image">图片数组</param>
        /// <returns></returns>
        public static async Task<JsonObject> Upload(byte[] Image)
        {
            if (string.IsNullOrEmpty(Token))
            {
                throw new NullReferenceException("Token Is Null");
            }
            if (AID==0)
            {
                throw new NullReferenceException("AID is Not Set");
            }

            var param = new Dictionary<string, object>
            {
                { "deadline", Deadline },
                { "aid", AID },
                { "from", "file" }
            };
            param.Clear();
            param.Add("Token", Token);
            param.Add("file", Image);


            string formDataBoundary = string.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;
            byte[] formData;
            using (var formDataStream = new System.IO.MemoryStream())
            {
                bool needsCLRF = false;
                foreach (var item in param)
                {
                    if (needsCLRF)
                        await formDataStream.WriteAsync(Encoding.UTF8.GetBytes("\r\n"), 0, Encoding.UTF8.GetByteCount("\r\n"));
                    needsCLRF = true;
                    if (item.Value is byte[])
                    {
                        string header = $"--{formDataBoundary}\r\nContent-Disposition: form-data; name=\"{item.Key}\";" +
                            $" filename=\"{Timestamp.ToString()}\"\r\nContent-Type: {"application/octet-stream"}\r\n\r\n";
                        await formDataStream.WriteAsync(Encoding.UTF8.GetBytes(header), 0, Encoding.UTF8.GetByteCount(header));
                        await formDataStream.WriteAsync(Image, 0, Image.Length);
                    }
                    else
                    {
                        string postData = $"--{formDataBoundary}\r\nContent-Disposition: form-data; name=\"{item.Key}\"\r\n\r\n{item.Value}";
                        await formDataStream.WriteAsync(Encoding.UTF8.GetBytes(postData), 0, Encoding.UTF8.GetByteCount(postData));
                    }
                }
                string footer = "\r\n--" + formDataBoundary + "--\r\n";
                await formDataStream.WriteAsync(Encoding.UTF8.GetBytes(footer), 0, Encoding.UTF8.GetByteCount(footer));
                formDataStream.Position = 0;
                formData = new byte[formDataStream.Length];
                await formDataStream.ReadAsync(formData, 0, formData.Length);
            }

            if (!(WebRequest.Create(URI) is HttpWebRequest request))
            {
                throw new NullReferenceException("这不是一个HTTP请求/request is not a http request");
            }

            request.Method = "POST";
            request.ContentType = contentType;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;

            using (System.IO.Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            using (var reader = new System.IO.StreamReader(stream, Encoding.UTF8))
            {
                return JsonObject.Parse(reader.ReadToEnd());
            }
        }
    }
}
