//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace PixivCS.SauceNao
{
  public static class Imgur {
    public static string Upload(byte[] Image, string apiKey) {
      using (var w = new WebClient()) {
        w.Headers.Add("Authorization: Client-ID " + apiKey);
        var values = new NameValueCollection {
          { "image", Convert.ToBase64String(Image) }
        };

        string response = System.Text.Encoding.UTF8.GetString(w.UploadValues("https://api.imgur.com/3/upload", values));
        System.Diagnostics.Debug.WriteLine(response);
        JsonObject dynObj = JsonObject.Parse(response);
        return dynObj.GetNamedObject("data").GetNamedString("link");
      }
    }
  }
}
