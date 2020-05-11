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
using System.Security.Cryptography;
using Windows.Data.Json;
using Windows.UI.Xaml;

namespace PixivCS
{
    public class PixivException : Exception
    {
        public PixivException() { }
        public PixivException(string msg) : base(msg) { }
    }

    public class RefreshEventArgs : EventArgs
    {
        public string NewAccessToken { get; }
        public string NewRefreshToken { get; }
        public bool IsSuccessful { get; }

        public RefreshEventArgs(string NewAccessToken, string NewRefreshToken, bool IsSuccessful)
        {
            this.NewAccessToken = NewAccessToken;
            this.NewRefreshToken = NewRefreshToken;
            this.IsSuccessful = IsSuccessful;
        }
    }

    public class PixivBaseAPI
    {

        // 参考自下面的链接
        // https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client#create-and-initialize-httpclient
        // https://stackoverflow.com/questions/15705092/do-httpclient-and-httpclienthandler-have-to-be-disposed
        private static readonly HttpClient _client = new HttpClient();

        internal string clientID = "MOBrBDS8blbauoSck0ZfDbtuzpyT";
        internal string clientSecret = "lsACyCD94FhDUtGTXi3QzcFE2uU1hqtDaKeqrdwj";
        internal string hashSecret = "28c1fdd170a5204386cb1313c7077b34f83e4aaf4aa829ce78c231e05b0bae2c";

        public Dictionary<string, string> TargetIPs { get; set; } = new Dictionary<string, string>()
        {
            {"oauth.secure.pixiv.net","210.140.131.188" },
            {"www.pixiv.net","210.140.131.188" },
            {"app-api.pixiv.net","210.140.131.188" }
        };

        public Dictionary<string, string> TargetSubjects { get; set; } = new Dictionary<string, string>()
        {
            {"210.140.131.188","CN=*.pixiv.net, O=pixiv Inc., OU=Development department, L=Shibuya-ku, S=Tokyo, C=JP" },
            {"210.140.92.142","CN=*.pximg.net, OU=Domain Control Validated" }
        };
        public Dictionary<string, string> TargetSNs { get; set; } = new Dictionary<string, string>()
        {
            {"210.140.131.188","35C1AFCF189CA529709BDC9A" },
            {"210.140.92.142","2387DB20E84EFCF82492545C" }
        };
        public Dictionary<string, string> TargetTPs { get; set; } = new Dictionary<string, string>()
        {
            {"210.140.131.188","D684756F1EF93CED5139AA9983FEFB6EE25EA820" },
            {"210.140.92.142","F4A431620F42E4D10EB42621C6948E3CD5014FB0" }
        };

        public string AccessToken { get; internal set; }
        public string RefreshToken { get; internal set; }
        public string UserID { get; internal set; }
        public bool ExperimentalConnection { get; set; }

        private int refreshInterval;
        public int RefreshInterval
        {
            get => refreshInterval;
            set
            {
                refreshInterval = value;
                if (value > 0) refreshTimer.Interval = TimeSpan.FromMinutes(value);
            }
        }

        DispatcherTimer refreshTimer = new DispatcherTimer();

        //自动刷新登录时执行
        public event EventHandler<RefreshEventArgs> TokenRefreshed;

        public PixivBaseAPI(string AccessToken, string RefreshToken, string UserID,
            bool ExperimentalConnection = false, int RefreshInterval = 45)
        {
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken;
            this.UserID = UserID;
            this.ExperimentalConnection = ExperimentalConnection;
            this.RefreshInterval = RefreshInterval;
            refreshTimer.Interval = TimeSpan.FromMinutes(RefreshInterval);
            refreshTimer.Tick += RefreshTimer_Tick;
        }

        private async void RefreshTimer_Tick(object sender, object e)
        {
            //每隔一定的时间刷新登录
            try
            {
                _ = await AuthAsync(RefreshToken);
            }
            catch
            {
                TokenRefreshed?.Invoke(this, new RefreshEventArgs(null, null, false));
                return;
            }
            TokenRefreshed?.Invoke(this, new RefreshEventArgs(AccessToken, RefreshToken, true));
        }

        public PixivBaseAPI() : this(null, null, null) { }

        public PixivBaseAPI(PixivBaseAPI BaseAPI) :
            this(BaseAPI.AccessToken, BaseAPI.RefreshToken, BaseAPI.UserID, BaseAPI.ExperimentalConnection, BaseAPI.RefreshInterval)
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
            if (ExperimentalConnection && TargetIPs.ContainsKey(new Uri(queryUrl).Host))
            {
                #region 无  底  深  坑
                var targetIP = TargetIPs[new Uri(queryUrl).Host];
                var targetSubject = TargetSubjects[targetIP];
                var targetSN = TargetSNs[targetIP];
                var targetTP = TargetTPs[targetIP];
                using (var connection = await Utilities.CreateConnectionAsync(targetIP, (cert) =>
                    cert.Subject == targetSubject && cert.SerialNumber == targetSN && cert.Thumbprint == targetTP))
                {
                    var httpRequest = await Utilities.ConstructHTTPAsync(Method, queryUrl, Headers, Body);
                    await connection.WriteAsync(httpRequest, 0, httpRequest.Length);
                    using (var memory = new MemoryStream())
                    {
                        await connection.CopyToAsync(memory);
                        memory.Position = 0;
                        var data = memory.ToArray();
                        var index = Utilities.BinaryMatch(data, Encoding.UTF8.GetBytes("\r\n\r\n")) + 4;
                        var headers = Encoding.UTF8.GetString(data, 0, index);
                        memory.Position = index;
                        byte[] result;
                        HttpStatusCode statusCode;
                        Dictionary<string, string> headersDictionary = new Dictionary<string, string>();
                        foreach (var header in headers.Split("\r\n"))
                        {
                            if (string.IsNullOrWhiteSpace(header)) break;
                            if (!header.Contains(": "))
                            {
                                var status = header.Split(" ");
                                statusCode = (HttpStatusCode)Convert.ToInt32(status[1]);
                            }
                            else
                            {
                                var pair = header.Split(": ");
                                headersDictionary.Add(pair[0], pair[1]);
                            }
                        }
                        if (headersDictionary.ContainsKey("Content-Encoding") &&
                            headersDictionary["Content-Encoding"].Contains("gzip"))
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
                        if (headersDictionary.ContainsKey("Transfer-Encoding") &&
                            headersDictionary["Transfer-Encoding"].Contains("chunked"))
                        {
                            //处理分块传输
                            using (MemoryStream parsedChunckedResult = new MemoryStream())
                            {
                                parsedChunckedResult.Position = 0;
                                int position = 0;
                                bool lengthOrContent = false;
                                int chunkLength = 0;
                                List<byte> lengthList = new List<byte>();
                                while (position < result.Length)
                                {
                                    if (!lengthOrContent)
                                    {
                                        //分块长度信息
                                        if (result[position] == '\r')
                                        {
                                            position += 2;
                                            lengthOrContent = true;
                                            var lengthArray = lengthList.ToArray();
                                            chunkLength = Convert.ToInt32(Encoding.UTF8.GetString(lengthArray), 16);
                                            lengthList.Clear();
                                        }
                                        else
                                        {
                                            lengthList.Add(result[position]);
                                            position++;
                                        }
                                    }
                                    else
                                    {
                                        //末端
                                        if (chunkLength == 0) break;
                                        //分块内容
                                        await parsedChunckedResult.WriteAsync(result, position, chunkLength);
                                        position += chunkLength + 2;
                                        lengthOrContent = false;
                                    }
                                }
                                result = parsedChunckedResult.ToArray();
                            }
                        }
                        var res = new HttpResponseMessage();
                        res.Content = new ByteArrayContent(result);
                        foreach (var pair in headersDictionary)
                        {
                            var added = res.Headers.TryAddWithoutValidation(pair.Key, pair.Value);
                            if (!added) res.Content.Headers.Add(pair.Key, pair.Value);
                        }
                        return res;
                    }
                }
                #endregion
            }
            else //传统手段
            {
                var allowMethods = new string[] { "get", "post" };
                if (!allowMethods.Any(m => m.Equals(Method, StringComparison.OrdinalIgnoreCase)))
                    throw new PixivException("Unsupported method");

                var request = new HttpRequestMessage(new HttpMethod(Method), queryUrl);
                if (Headers != null)
                    foreach (var (k, v) in Headers)
                        request.Headers.Add(k, v);

                if (Body != null)
                    request.Content = Body;

                return await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
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

        public void SetClient(string ClientID, string ClientSecret, string HashSecret)
        {
            clientID = ClientID;
            clientSecret = ClientSecret;
            hashSecret = HashSecret;
        }

        //用户名和密码登录
        [Obsolete("Methods returning JsonObject objects will be deprecated in the future. Use AuthAsync instead.")]
        public async Task<JsonObject> Auth(string Username, string Password)
        {
            string MD5Hash(string Input)
            {
                if (string.IsNullOrEmpty(Input)) return null;
                using (var md5 = MD5.Create())
                {
                    var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(Input.Trim()));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                        builder.Append(bytes[i].ToString("x2"));
                    return builder.ToString();
                }
            }
            string url = "https://oauth.secure.pixiv.net/auth/token";
            string time = DateTime.UtcNow.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:sszzz");

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)" },
                { "X-Client-Time", time },
                { "X-Client-Hash", MD5Hash(time+hashSecret) }
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
            if (RefreshInterval > 0) refreshTimer.Start();
            return resJSON;
        }

        //用户名和密码登录
        public async Task<Objects.AuthResult> AuthAsync(string Username, string Password)
        {
            string MD5Hash(string Input)
            {
                if (string.IsNullOrEmpty(Input)) return null;
                using (var md5 = MD5.Create())
                {
                    var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(Input.Trim()));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                        builder.Append(bytes[i].ToString("x2"));
                    return builder.ToString();
                }
            }
            string url = "https://oauth.secure.pixiv.net/auth/token";
            string time = DateTime.UtcNow.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:sszzz");

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "User-Agent", "PixivAndroidApp/5.0.64 (Android 6.0)" },
                { "X-Client-Time", time },
                { "X-Client-Hash", MD5Hash(time+hashSecret) }
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
            var resJSON = Objects.AuthResult.FromJson(await GetResponseString(res));
            AccessToken = resJSON.Response.AccessToken;
            UserID = resJSON.Response.User.Id;
            RefreshToken = resJSON.Response.RefreshToken;
            if (RefreshInterval > 0) refreshTimer.Start();
            return resJSON;
        }

        //RefreshToken登录
        [Obsolete("Methods returning JsonObject objects will be deprecated in the future. Use AuthAsync instead.")]
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
            if (RefreshInterval > 0) refreshTimer.Start();
            return resJSON;
        }

        //RefreshToken登录
        public async Task<Objects.AuthResult> AuthAsync(string RefreshToken)
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
            var resJSON = Objects.AuthResult.FromJson(await GetResponseString(res));
            AccessToken = resJSON.Response.AccessToken;
            UserID = resJSON.Response.User.Id;
            this.RefreshToken = resJSON.Response.RefreshToken;
            if (RefreshInterval > 0) refreshTimer.Start();
            return resJSON;
        }
    }
}