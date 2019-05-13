using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace PixivCS
{
    public class PixivAppAPI : PixivBaseAPI
    {
        public PixivAppAPI(string AccessToken, string RefreshToken, string UserID) :
            base(AccessToken, RefreshToken, UserID)
        { }

        public PixivAppAPI() : base() { }

        public PixivAppAPI(PixivBaseAPI BaseAPI) : base(BaseAPI) { }

        public async Task<HttpResponseMessage> RequestCall(string Method, string Url,
            Dictionary<string, string> Headers = null, List<(string, string)> Query = null,
            HttpContent Body = null, bool RequireAuth = true)
        {
            var headers = Headers ?? new Dictionary<string, string>();
            if (!(headers.ContainsKey("User-Agent") || headers.ContainsKey("user-agent")))
            {
                headers.Add("App-OS", "ios");
                headers.Add("App-OS-Version", "10.3.1");
                headers.Add("App-Version", "6.7.1");
                headers.Add("User-Agent", "PixivIOSApp/6.7.1 (iOS 10.3.1; iPhone8,1)");
            }
            if (RequireAuth) headers.Add("Authorization", string.Format("Bearer {0}", AccessToken));
            return await base.RequestCall(Method, Url, headers, Query, Body);
        }

        //用户详情
        public async Task<JsonObject> UserDetail(string UserID, string Filter = "for_ios",
            bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v1/user/detail";
            List<(string, string)> query = new List<(string, string)>
            {
                ("user_id", UserID),
                ("filter", Filter)
            };
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //用户作品
        public async Task<JsonObject> UserIllusts(string UserID, string IllustType = "illust",
            string Filter = "for_ios", string Offset = null, bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v1/user/illusts";
            List<(string, string)> query = new List<(string, string)>
            {
                ("user_id", UserID),
                ("filter", Filter)
            };
            if (!string.IsNullOrEmpty(IllustType)) query.Add(("type", IllustType));
            if (!string.IsNullOrEmpty(Offset)) query.Add(("offset", Offset));
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //用户收藏
        public async Task<JsonObject> UserBookmarksIllust(string UserID, string Restrict = "public",
            string Filter = "for_ios", string MaxBookmarkID = null, string Tag = null,
            bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v1/user/bookmarks/illust";
            List<(string, string)> query = new List<(string, string)>
            {
                ("user_id", UserID),
                ("restrict", Restrict),
                ("filter", Filter)
            };
            if (!string.IsNullOrEmpty(MaxBookmarkID)) query.Add(("max_bookmark_id", MaxBookmarkID));
            if (!string.IsNullOrEmpty(Tag)) query.Add(("tag", Tag));
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //关注者的新作品
        public async Task<JsonObject> IllustFollow(string Restrict = "public", string Offset = null,
            bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v2/illust/follow";
            List<(string, string)> query = new List<(string, string)>
            {
                ("restrict", Restrict)
            };
            if (!string.IsNullOrEmpty(Offset)) query.Add(("offset", Offset));
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //作品详情
        public async Task<JsonObject> IllustDetail(string IllustID, bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v1/illust/detail";
            List<(string, string)> query = new List<(string, string)>
            {
                ("illust_id", IllustID)
            };
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //作品评论
        //IncludeTotalComments决定是否在返回的JSON中包含总评论数
        public async Task<JsonObject> IllustComments(string IllustID, string Offset = null,
            bool? IncludeTotalComments = null, bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v1/illust/comments";
            List<(string, string)> query = new List<(string, string)>
            {
                ("illust_id", IllustID)
            };
            if (!string.IsNullOrEmpty(Offset)) query.Add(("offset", Offset));
            if (IncludeTotalComments != null) query.Add(("include_total_comments",
                IncludeTotalComments.Value ? "true" : "false"));
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //相关作品
        public async Task<JsonObject> IllustRelated(string IllustID, string Filter = "for_ios",
            List<string> SeedIllustIDs = null, bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v2/illust/related";
            List<(string, string)> query = new List<(string, string)>
            {
                ("illust_id", IllustID),
                ("filter", Filter)
            };
            if (SeedIllustIDs != null)
            {
                foreach (var i in SeedIllustIDs)
                    query.Add(("seed_illust_ids[]", i));
            }
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //首页推荐
        //content_type: [illust, manga]
        public async Task<JsonObject> IllustRecommended(string ContentType = "illust",
            bool IncludeRankingLabel = true, string Filter = "for_ios",
            string MaxBookmarkIDForRecommended = null,
            string MinBookmarkIDForRecentIllust = null, string Offset = null,
            bool? IncludeRankingIllusts = null, List<string> BookmarkIllustIDs = null,
            string IncludePrivacyPolicy = null, bool RequireAuth = true)
        {
            string url = RequireAuth ? "https://app-api.pixiv.net/v1/illust/recommended" :
                "https://app-api.pixiv.net/v1/illust/recommended-nologin";
            List<(string, string)> query = new List<(string, string)>
            {
                ("content_type", ContentType),
                ("include_ranking_label", IncludeRankingLabel ? "true" : "false"),
                ("filter", Filter)
            };
            if (!string.IsNullOrEmpty(MaxBookmarkIDForRecommended))
                query.Add(("max_bookmark_id_for_recommend", MaxBookmarkIDForRecommended));
            if (!string.IsNullOrEmpty(MinBookmarkIDForRecentIllust))
                query.Add(("min_bookmark_id_for_recent_illust", MinBookmarkIDForRecentIllust));
            if (!string.IsNullOrEmpty(Offset)) query.Add(("offset", Offset));
            if (IncludeRankingIllusts != null)
                query.Add(("include_ranking_illusts", IncludeRankingIllusts.Value ? "true" : "false"));
            string ids = "";
            if (BookmarkIllustIDs != null)
                foreach (var i in BookmarkIllustIDs)
                    ids += (i + ",");
            if (ids != "")
            {
                ids.TrimEnd(',');
                query.Add(("bookmark_illust_ids", ids));
            }
            if (!string.IsNullOrEmpty(IncludePrivacyPolicy))
                query.Add(("include_privacy_policy", IncludePrivacyPolicy));
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //作品排行
        //mode: [day, week, month, day_male, day_female, week_original, week_rookie, day_manga]
        //date: yyyy-mm-dd
        public async Task<JsonObject> IllustRanking(string Mode = "day", string Filter = "for_ios",
            string Date = null, string Offset = null, bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v1/illust/ranking";
            List<(string, string)> query = new List<(string, string)>
            {
                ("mode", Mode),
                ("filter", Filter)
            };
            if (!string.IsNullOrEmpty(Date)) query.Add(("date", Date));
            if (!string.IsNullOrEmpty(Offset)) query.Add(("offset", Offset));
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //趋势标签
        public async Task<JsonObject> TrendingTagsIllust(string Filter = "for_ios", bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v1/trending-tags/illust";
            List<(string, string)> query = new List<(string, string)>
            {
                ("filter", Filter)
            };
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }

        //搜索
        //search_target - 搜索类型
        //  partial_match_for_tags  - 标签部分一致
        //  exact_match_for_tags    - 标签完全一致
        //  title_and_caption       - 标题说明文
        //sort: [date_desc, date_asc]
        //duration: [within_last_day, within_last_week, within_last_month]
        public async Task<JsonObject> SearchIllust(string Word, string SearchTarget = "partial_match_for_tags",
            string Sort = "date_desc", string Duration = null, string Filter = "for_ios", string Offset = null,
            bool RequireAuth = true)
        {
            string url = "https://app-api.pixiv.net/v1/search/illust";
            List<(string, string)> query = new List<(string, string)>
            {
                ("word", Word),
                ("search_target", SearchTarget),
                ("sort", Sort),
                ("filter", Filter)
            };
            if (!string.IsNullOrEmpty(Duration)) query.Add(("duration", Duration));
            if (!string.IsNullOrEmpty(Offset)) query.Add(("offset", Offset));
            var res = await RequestCall("GET", url, Query: query, RequireAuth: RequireAuth);
            return JsonObject.Parse(await GetResponseString(res));
        }
    }
}
