using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PixivCS
{
    internal static class Utilities
    {
        //不携带SNI的连接
        public static async Task<SslStream> CreateConnectionAsync(string TargetIP, Func<X509Certificate2, bool> CertValidation)
        {
            TcpClient client = new TcpClient();
            await client.ConnectAsync(TargetIP, 443);
            var networkStream = client.GetStream();
            var sslStream = new SslStream(networkStream, false, (sender, certificate, chain, errors) => CertValidation((X509Certificate2)certificate));
            try
            {
                await sslStream.AuthenticateAsClientAsync("");
                return sslStream;
            }
            catch (Exception e)
            {
                sslStream.Dispose();
                throw new PixivException(e.Message);
            }
        }

        //用于构造一个Http报文
        public static async Task<byte[]> ConstructHTTPAsync(string Method, string Url,
            Dictionary<string, string> Headers, HttpContent Body)
        {
            StringBuilder builder = new StringBuilder();
            switch (Method.ToUpper())
            {
                case "GET":
                    builder.AppendLine(string.Format("GET {0} HTTP/1.1", Url));
                    foreach (var pair in Headers)
                        builder.AppendLine(string.Format("{0}: {1}", pair.Key, pair.Value));
                    if (!Headers.ContainsKey("Host"))
                        builder.AppendLine(string.Format("Host: {0}", new Uri(Url).Host));
                    if (!Headers.ContainsKey("Connection"))
                        builder.AppendLine("Connection: Keep-Alive");
                    if (!Headers.ContainsKey("Cache-Control"))
                        builder.AppendLine("Cache-Control: no-cache");
                    builder.AppendLine();
                    break;
                case "POST":
                    string bodyStr = "";
                    builder.AppendLine(string.Format("POST {0} HTTP/1.1", Url));
                    foreach (var pair in Headers)
                        builder.AppendLine(string.Format("{0}: {1}", pair.Key, pair.Value));
                    switch (Body)
                    {
                        case FormUrlEncodedContent form:
                            bodyStr = await form.ReadAsStringAsync();
                            if (!Headers.ContainsKey("Content-Length"))
                                builder.AppendLine(string.Format("Content-Length: {0}", Encoding.UTF8.GetByteCount(bodyStr)));
                            if (!Headers.ContainsKey("Content-Type"))
                                builder.AppendLine("Content-Type: application/x-www-form-urlencoded");
                            break;
                        default:
                            throw new PixivException("Unsupported content type");
                    }
                    if (!Headers.ContainsKey("Host"))
                        builder.AppendLine(string.Format("Host: {0}", new Uri(Url).Host));
                    if (!Headers.ContainsKey("Connection"))
                        builder.AppendLine("Connection: Keep-Alive");
                    if (!Headers.ContainsKey("Cache-Control"))
                        builder.AppendLine("Cache-Control: no-cache");
                    builder.AppendLine();
                    builder.Append(bodyStr);
                    break;
                default:
                    throw new PixivException("Unsupported method");
            }
            return Encoding.UTF8.GetBytes(builder.ToString());
        }

        public static int BinaryMatch(byte[] input, byte[] pattern)
        {
            int sLen = input.Length - pattern.Length + 1;
            for (int i = 0; i < sLen; ++i)
            {
                bool match = true;
                for (int j = 0; j < pattern.Length; ++j)
                {
                    if (input[i + j] != pattern[j])
                    {
                        match = false;
                        break;
                    }
                }
                if (match)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
