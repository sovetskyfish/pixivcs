using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace PixivCS.SauceNao {
  public class Result {
    public float Similarity;
    public Sauce Response;

    public Result(JsonObject header, JsonObject data) {
      this.Similarity = (float)Convert.ToDouble(header.GetNamedString("similarity"));
      this.Response   = new Response(data).GetResponse();
    }

    public bool HasRecognizableSauce() {
      return Response != null;
    }

    public override string ToString() {
      return String.Format("Similarity: {0}\n{1}", Similarity, Response.ToString());
    }
  }
}
