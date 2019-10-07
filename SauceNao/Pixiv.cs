using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace PixivCS.SauceNao.Sauces {
  class Pixiv : Sauce {
    static string ENDPOINT = "http://www.pixiv.net/member_illust.php?mode=medium&illust_id=";
    public new string Url {
      get { return GetUrl(); }
    }

    public Pixiv(JsonObject data) : base(data.GetNamedString("title"), (int) data.GetNamedNumber("pixiv_id"), data.GetNamedString("member_name"), (int) data.GetNamedNumber("member_id")) {}

    public override string ToString() {
      return String.Format("Title: {0}\nPixiv ID: {1}\nAuthor: {2} (#{3})\nURL: {4}", Title, SauceId, AuthorName, AuthorId, Url);
    }
    
    protected override string GetUrl() {
      return ENDPOINT + SauceId;
    }

    public static bool IsTheRightAdapterFor(JsonObject data) {
      try{
        data.GetNamedNumber("pixiv_id");
        return true;
      }catch{
        return false;
      }
    }
  }
}
