using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
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

        public Dictionary<string, string> TargetIPs { get; set; } = new Dictionary<string, string>()
        {
            {"oauth.secure.pixiv.net","210.140.131.224" },
            {"i.pximg.net","210.140.92.142" },
            {"www.pixiv.net","210.140.131.224" },
            {"app-api.pixiv.net","210.140.131.224" }
        };

        public Dictionary<string, string> TargetSubjects { get; set; } = new Dictionary<string, string>()
        {
            {"210.140.131.224","CN=*.pixiv.net, O=pixiv Inc., OU=Development department, L=Shibuya-ku, S=Tokyo, C=JP" },
            {"210.140.92.142","CN=*.pximg.net, OU=Domain Control Validated" }
        };
        public Dictionary<string, string> TargetSNs { get; set; } = new Dictionary<string, string>()
        {
            {"210.140.131.224","281941D074A6D4B07B72D729" },
            {"210.140.92.142","2387DB20E84EFCF82492545C" }
        };
        public Dictionary<string, string> TargetTPs { get; set; } = new Dictionary<string, string>()
        {
            {"210.140.131.224","352FCC13B920E12CD15F3875E52AEDB95B62972B" },
            {"210.140.92.142","F4A431620F42E4D10EB42621C6948E3CD5014FB0" }
        };

        public string AccessToken { get; internal set; }
        public string RefreshToken { get; internal set; }
        public string UserID { get; internal set; }
        public bool ExperimentalConnection { get; set; }

        public PixivBaseAPI(string AccessToken, string RefreshToken, string UserID,
            bool ExperimentalConnection = false)
        {
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken;
            this.UserID = UserID;
            this.ExperimentalConnection = ExperimentalConnection;
        }

        public PixivBaseAPI() : this(null, null, null) { }

        public PixivBaseAPI(PixivBaseAPI BaseAPI) :
            this(BaseAPI.AccessToken, BaseAPI.RefreshToken, BaseAPI.UserID, BaseAPI.ExperimentalConnection)
        { }

        //用于生成带参数的url
        private static string GetQueryString(List<(string, string)> query)
        {
            var array = (from i in query
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(i.Item1),
                         HttpUtility.UrlEncode(i.Item2)))
                .ToArray();
            return "?" + string.Join("&", array);
        }

        public void RequireAuth()
        {
            if (AccessToken == null) throw new PixivException("Authentication required!");
        }

        public async Task<HttpResponseMessage> RequestCall(string Method, string Url,
            Dictionary<string, string> Headers = null, List<(string, string)> Query = null,
            HttpContent Body = null)
        {
            string queryUrl = Url + ((Query != null) ? GetQueryString(Query) : "");
            if (ExperimentalConnection)
            {
                var targetIP = TargetIPs[new Uri(queryUrl).Host];
                var targetSubject = TargetSubjects[targetIP];
                var targetSN = TargetSNs[targetIP];
                var targetTP = TargetTPs[targetIP];
                using (var connection = await Task.Run(() => Utilities.CreateConnection(targetIP, (cert) =>
                      cert.Subject == targetSubject && cert.SerialNumber == targetSN && cert.Thumbprint == targetTP
                    )))
                {
                    var httpRequest = await Utilities.ConstructHTTPAsync(Method, queryUrl, Headers, Body);
                    await connection.WriteAsync(httpRequest, 0, httpRequest.Length);
                    using (var memory = new MemoryStream())
                    {
                        await connection.CopyToAsync(memory);
                        memory.Position = 0;
                        var data = memory.ToArray();
                        var index = Utilities.BinaryMatch(data, Encoding.ASCII.GetBytes("\r\n\r\n")) + 4;
                        var headers = Encoding.ASCII.GetString(data, 0, index);
                        memory.Position = index;
                        byte[] result;
                        if (headers.IndexOf("Content-Encoding: gzip") > 0)
                        {
                            using (GZipStream decompressionStream = new GZipStream(memory, CompressionMode.Decompress))
                            using (var decompressedMemory = new MemoryStream())
                            {
                                await decompressionStream.CopyToAsync(decompressedMemory);
                                decompressedMemory.Position = 0;
                                result = decompressedMemory.ToArray();
                            }
                        }
                        else
                        {
                            using (var resultMemory = new MemoryStream())
                            {
                                await memory.CopyToAsync(resultMemory);
                                result = resultMemory.ToArray();
                            }
                        }
                        var res = new HttpResponseMessage();
                        res.Content = new ByteArrayContent(result);
                        foreach (var header in headers.Split("\r\n"))
                        {
                            if (string.IsNullOrWhiteSpace(header)) break;
                            if (!header.Contains(": "))
                            {
                                var status = header.Split(" ");
                                res.StatusCode = (HttpStatusCode)Convert.ToInt32(status[1]);
                            }
                            else
                            {
                                var pair = header.Split(": ");
                                var added = res.Headers.TryAddWithoutValidation(pair[0], pair[1]);
                                if (!added) res.Content.Headers.Add(pair[0], pair[1]);
                            }
                        }
                        return res;
                    }
                }
            }
            else
                using (HttpClient client = new HttpClient())
                {
                    if (Headers != null)
                        foreach ((var k, var v) in Headers)
                            client.DefaultRequestHeaders.Add(k, v);
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

        //以字符串形式拿回Response
        public static async Task<string> GetResponseString(HttpResponseMessage Response)
        {
            return await Response.Content.ReadAsStringAsync();
        }

        //以流形式拿回Response
        public static async Task<Stream> GetResponseStream(HttpResponseMessage Response)
        {
            return await Response.Content.ReadAsStreamAsync();
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
        public async Task<JsonObject> Auth(string Username, string Password)
        {
            string url = "https://oauth.secure.pixiv.net/auth/token";
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)" }
            };
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "get_secure_url", "1" },
                { "client_id", clientID },
                { "client_secret", clientSecret },
                { "grant_type", "password" },
                { "username", Username },
                { "password", Password }
            };
            var res = await RequestCall("POST", url, headers, Body: new FormUrlEncodedContent(data));
            int status = (int)res.StatusCode;
            if (!(status == 200 || status == 301 || status == 302))
                throw new PixivException("[ERROR] Auth() failed! Check Username and Password.");
            var resJSON = JsonObject.Parse(await GetResponseString(res));
            AccessToken = resJSON["response"].GetObject()["access_token"].GetString();
            UserID = resJSON["response"].GetObject()["user"].GetObject()["id"].GetString();
            RefreshToken = resJSON["response"].GetObject()["refresh_token"].GetString();
            return resJSON;
        }

        //RefreshToken登录
        public async Task<JsonObject> Auth(string RefreshToken)
        {
            string url = "https://oauth.secure.pixiv.net/auth/token";
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)" }
            };
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "get_secure_url", "1" },
                { "client_id", clientID },
                { "client_secret", clientSecret },
                { "grant_type", "refresh_token" },
                { "refresh_token", RefreshToken }
            };
            var res = await RequestCall("POST", url, headers, Body: new FormUrlEncodedContent(data));
            int status = (int)res.StatusCode;
            if (!(status == 200 || status == 301 || status == 302))
                throw new PixivException("[ERROR] Auth() failed! Check Username and Password.");
            var resJSON = JsonObject.Parse(await GetResponseString(res));
            AccessToken = resJSON["response"].GetObject()["access_token"].GetString();
            UserID = resJSON["response"].GetObject()["user"].GetObject()["id"].GetString();
            this.RefreshToken = resJSON["response"].GetObject()["refresh_token"].GetString();
            return resJSON;
        }
    }
}