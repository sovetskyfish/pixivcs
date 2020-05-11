//此文件由app.quicktype.io生成
//我吹爆！

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PixivCS.Objects
{

    public partial class ShowcaseArticle
    {
        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("body")]
        public Body[] Body { get; set; }
    }

    public partial class Body
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("entry")]
        public Entry Entry { get; set; }

        [JsonProperty("tags")]
        public EntryTag[] Tags { get; set; }

        [JsonProperty("thumbnailUrl")]
        public Uri ThumbnailUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("publishDate")]
        public long PublishDate { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("subCategory")]
        public string SubCategory { get; set; }

        [JsonProperty("subCategoryLabel")]
        public string SubCategoryLabel { get; set; }

        [JsonProperty("subCategoryIntroduction")]
        public string SubCategoryIntroduction { get; set; }

        [JsonProperty("introduction")]
        public string Introduction { get; set; }

        [JsonProperty("footer")]
        public string Footer { get; set; }

        [JsonProperty("illusts")]
        public BodyIllust[] Illusts { get; set; }

        [JsonProperty("relatedArticles")]
        public RelatedArticle[] RelatedArticles { get; set; }

        [JsonProperty("followingUserIds")]
        public dynamic[] FollowingUserIds { get; set; }

        [JsonProperty("isOnlyOneUser")]
        public bool IsOnlyOneUser { get; set; }
    }

    public partial class Entry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("pure_title")]
        public string PureTitle { get; set; }

        [JsonProperty("catchphrase")]
        public string Catchphrase { get; set; }

        [JsonProperty("header")]
        public string Header { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("footer")]
        public string Footer { get; set; }

        [JsonProperty("sidebar")]
        public string Sidebar { get; set; }

        [JsonProperty("publish_date")]
        public long PublishDate { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("pixivision_category_slug")]
        public string PixivisionCategorySlug { get; set; }

        [JsonProperty("pixivision_category")]
        public PixivisionCategory PixivisionCategory { get; set; }

        [JsonProperty("pixivision_subcategory_slug")]
        public string PixivisionSubcategorySlug { get; set; }

        [JsonProperty("pixivision_subcategory")]
        public PixivisionSubcategory PixivisionSubcategory { get; set; }

        [JsonProperty("tags")]
        public EntryTag[] Tags { get; set; }

        [JsonProperty("article_url")]
        public Uri ArticleUrl { get; set; }

        [JsonProperty("intro")]
        public string Intro { get; set; }

        [JsonProperty("facebook_count")]
        public string FacebookCount { get; set; }

        [JsonProperty("twitter_count")]
        public string TwitterCount { get; set; }
    }

    public partial class PixivisionCategory
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("introduction")]
        public string Introduction { get; set; }
    }

    public partial class PixivisionSubcategory
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("label_en")]
        public string LabelEn { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("introduction")]
        public string Introduction { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("big_image_url")]
        public string BigImageUrl { get; set; }
    }

    public partial class EntryTag
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class BodyIllust
    {
        [JsonProperty("spotlight_article_id")]
        public long SpotlightArticleId { get; set; }

        [JsonProperty("illust_id")]
        public long IllustId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("illust_user_id")]
        public string IllustUserId { get; set; }

        [JsonProperty("illust_title")]
        public string IllustTitle { get; set; }

        [JsonProperty("illust_ext")]
        public string IllustExt { get; set; }

        [JsonProperty("illust_width")]
        public string IllustWidth { get; set; }

        [JsonProperty("illust_height")]
        public string IllustHeight { get; set; }

        [JsonProperty("illust_restrict")]
        public string IllustRestrict { get; set; }

        [JsonProperty("illust_x_restrict")]
        public string IllustXRestrict { get; set; }

        [JsonProperty("illust_create_date")]
        public string IllustCreateDate { get; set; }

        [JsonProperty("illust_upload_date")]
        public string IllustUploadDate { get; set; }

        [JsonProperty("illust_server_id")]
        public string IllustServerId { get; set; }

        [JsonProperty("illust_hash")]
        public string IllustHash { get; set; }

        [JsonProperty("illust_type")]
        public string IllustType { get; set; }

        [JsonProperty("illust_sanity_level")]
        public long IllustSanityLevel { get; set; }

        [JsonProperty("illust_book_style")]
        public string IllustBookStyle { get; set; }

        [JsonProperty("illust_page_count")]
        public string IllustPageCount { get; set; }

        [JsonProperty("illust_custom_thumbnail_upload_datetime")]
        public dynamic IllustCustomThumbnailUploadDatetime { get; set; }

        [JsonProperty("illust_comment")]
        public string IllustComment { get; set; }

        [JsonProperty("user_account")]
        public string UserAccount { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("user_comment")]
        public string UserComment { get; set; }

        [JsonProperty("url")]
        public Url Url { get; set; }

        [JsonProperty("ugoira_meta")]
        public dynamic UgoiraMeta { get; set; }

        [JsonProperty("user_icon")]
        public Uri UserIcon { get; set; }
    }

    public partial class Url
    {
        [JsonProperty("1200x1200")]
        public Uri The1200X1200 { get; set; }

        [JsonProperty("768x1200")]
        public Uri The768X1200 { get; set; }

        [JsonProperty("ugoira600x600")]
        public string Ugoira600X600 { get; set; }

        [JsonProperty("ugoira1920x1080")]
        public string Ugoira1920X1080 { get; set; }
    }

    public partial class RelatedArticle
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ja")]
        public PrivacyPolicy Ja { get; set; }

        [JsonProperty("en")]
        public PrivacyPolicy En { get; set; }

        [JsonProperty("zh")]
        public PrivacyPolicy Zh { get; set; }

        [JsonProperty("zh_tw")]
        public PrivacyPolicy ZhTw { get; set; }

        [JsonProperty("publish_date")]
        public long PublishDate { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("pixivision_category_slug")]
        public string PixivisionCategorySlug { get; set; }

        [JsonProperty("pixivision_subcategory_slug")]
        public string PixivisionSubcategorySlug { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("thumbnail_illust_id")]
        public string ThumbnailIllustId { get; set; }

        [JsonProperty("has_body")]
        public string HasBody { get; set; }

        [JsonProperty("is_pr")]
        public string IsPr { get; set; }

        [JsonProperty("pr_client_name")]
        public string PrClientName { get; set; }

        [JsonProperty("edit_status")]
        public string EditStatus { get; set; }

        [JsonProperty("translation_status")]
        public string TranslationStatus { get; set; }

        [JsonProperty("is_sample")]
        public string IsSample { get; set; }

        [JsonProperty("illusts")]
        public dynamic[] Illusts { get; set; }

        [JsonProperty("novel_ids")]
        public dynamic[] NovelIds { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }

        [JsonProperty("facebook_count")]
        public string FacebookCount { get; set; }

        [JsonProperty("tweet_count")]
        public string TweetCount { get; set; }

        [JsonProperty("tweet_max_count")]
        public string TweetMaxCount { get; set; }

        [JsonProperty("tags")]
        public dynamic[] Tags { get; set; }

        [JsonProperty("tag_ids")]
        public dynamic TagIds { get; set; }

        [JsonProperty("numbered_tags")]
        public dynamic[] NumberedTags { get; set; }

        [JsonProperty("main_abtest_pattern_id")]
        public string MainAbtestPatternId { get; set; }

        [JsonProperty("advertisement_id")]
        public string AdvertisementId { get; set; }
    }

    public partial class PrivacyPolicy
    {
    }

    public partial class UgoiraMetadata
    {
        [JsonProperty("ugoira_metadata")]
        public UgoiraMetadataClass UgoiraMetadataUgoiraMetadata { get; set; }
    }

    public partial class UgoiraMetadataClass
    {
        [JsonProperty("zip_urls")]
        public Urls ZipUrls { get; set; }

        [JsonProperty("frames")]
        public Frame[] Frames { get; set; }
    }

    public partial class Frame
    {
        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("delay")]
        public long Delay { get; set; }
    }

    public partial class Urls
    {
        [JsonProperty("medium")]
        public Uri Medium { get; set; }
    }

    public partial class UserList
    {
        [JsonProperty("users")]
        public dynamic[] Users { get; set; }
    }

    public partial class UserFollowList
    {
        [JsonProperty("user_previews")]
        public UserPreview[] UserPreviews { get; set; }

        [JsonProperty("next_url")]
        public Uri NextUrl { get; set; }
    }

    public partial class UserPreview
    {
        [JsonProperty("user")]
        public IllustUser User { get; set; }

        [JsonProperty("illusts")]
        public UserPreviewIllust[] Illusts { get; set; }

        [JsonProperty("novels")]
        public dynamic[] Novels { get; set; }

        [JsonProperty("is_muted")]
        public bool IsMuted { get; set; }
    }

    public partial class UserPreviewIllust
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("image_urls")]
        public ImageUrls ImageUrls { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("restrict")]
        public long Restrict { get; set; }

        [JsonProperty("user")]
        public IllustUser User { get; set; }

        [JsonProperty("tags")]
        public IllustTag[] Tags { get; set; }

        [JsonProperty("tools")]
        public string[] Tools { get; set; }

        [JsonProperty("create_date")]
        public string CreateDate { get; set; }

        [JsonProperty("page_count")]
        public long PageCount { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("sanity_level")]
        public long SanityLevel { get; set; }

        [JsonProperty("x_restrict")]
        public long XRestrict { get; set; }

        [JsonProperty("series")]
        public Series Series { get; set; }

        [JsonProperty("meta_single_page")]
        public MetaSinglePage MetaSinglePage { get; set; }

        [JsonProperty("meta_pages")]
        public MetaPage[] MetaPages { get; set; }

        [JsonProperty("total_view")]
        public long TotalView { get; set; }

        [JsonProperty("total_bookmarks")]
        public long TotalBookmarks { get; set; }

        [JsonProperty("is_bookmarked")]
        public bool IsBookmarked { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("is_muted")]
        public bool IsMuted { get; set; }

        [JsonProperty("total_comments", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalComments { get; set; }
    }

    public partial class ImageUrls
    {
        [JsonProperty("square_medium")]
        public Uri SquareMedium { get; set; }

        [JsonProperty("medium")]
        public Uri Medium { get; set; }

        [JsonProperty("large")]
        public Uri Large { get; set; }

        [JsonProperty("original", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Original { get; set; }
    }

    public partial class MetaPage
    {
        [JsonProperty("image_urls")]
        public ImageUrls ImageUrls { get; set; }
    }

    public partial class MetaSinglePage
    {
        [JsonProperty("original_image_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri OriginalImageUrl { get; set; }
    }

    public partial class Series
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }

    public partial class IllustTag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("translated_name")]
        public string TranslatedName { get; set; }
    }

    public partial class IllustUser
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("profile_image_urls")]
        public Urls ProfileImageUrls { get; set; }

        [JsonProperty("is_followed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsFollowed { get; set; }

        [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }
    }

    public partial class UserBookmarkTags
    {
        [JsonProperty("bookmark_tags")]
        public dynamic[] BookmarkTags { get; set; }

        [JsonProperty("next_url")]
        public Uri NextUrl { get; set; }
    }

    public partial class IllustBookmarkDetail
    {
        [JsonProperty("bookmark_detail")]
        public BookmarkDetail BookmarkDetail { get; set; }
    }

    public partial class BookmarkDetail
    {
        [JsonProperty("is_bookmarked")]
        public bool IsBookmarked { get; set; }

        [JsonProperty("tags")]
        public BookmarkDetailTag[] Tags { get; set; }

        [JsonProperty("restrict")]
        public string Restrict { get; set; }
    }

    public partial class BookmarkDetailTag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_registered")]
        public bool IsRegistered { get; set; }
    }

    public partial class SearchIllustResult
    {
        [JsonProperty("illusts")]
        public UserPreviewIllust[] Illusts { get; set; }

        [JsonProperty("next_url")]
        public Uri NextUrl { get; set; }

        [JsonProperty("search_span_limit")]
        public long SearchSpanLimit { get; set; }
    }

    public partial class TrendingTagsIllust
    {
        [JsonProperty("trend_tags")]
        public TrendTag[] TrendTags { get; set; }
    }

    public partial class TrendTag
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("translated_name")]
        public string TranslatedName { get; set; }

        [JsonProperty("illust")]
        public UserPreviewIllust Illust { get; set; }
    }

    public partial class UserIllusts
    {
        [JsonProperty("illusts")]
        public UserPreviewIllust[] Illusts { get; set; }

        [JsonProperty("next_url")]
        public Uri NextUrl { get; set; }
    }

    public partial class IllustRecommended
    {
        [JsonProperty("illusts")]
        public UserPreviewIllust[] Illusts { get; set; }

        [JsonProperty("ranking_illusts")]
        public dynamic[] RankingIllusts { get; set; }

        [JsonProperty("contest_exists")]
        public bool ContestExists { get; set; }

        [JsonProperty("privacy_policy")]
        public PrivacyPolicy PrivacyPolicy { get; set; }

        [JsonProperty("next_url")]
        public Uri NextUrl { get; set; }
    }

    public partial class IllustCommentAddResult
    {
        [JsonProperty("comment")]
        public Comment Comment { get; set; }
    }

    public partial class Comment
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("comment")]
        public string CommentComment { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("user")]
        public IllustUser User { get; set; }

        [JsonProperty("has_replies", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasReplies { get; set; }

        [JsonProperty("parent_comment", NullValueHandling = NullValueHandling.Ignore)]
        public Comment ParentComment { get; set; }
    }

    public partial class IllustComments
    {
        [JsonProperty("total_comments")]
        public long TotalComments { get; set; }

        [JsonProperty("comments")]
        public Comment[] Comments { get; set; }

        [JsonProperty("next_url")]
        public Uri NextUrl { get; set; }
    }

    public partial class IllustDetail
    {
        [JsonProperty("illust")]
        public UserPreviewIllust Illust { get; set; }
    }

    public partial class UserDetail
    {
        [JsonProperty("user")]
        public IllustUser User { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }

        [JsonProperty("profile_publicity")]
        public ProfilePublicity ProfilePublicity { get; set; }

        [JsonProperty("workspace")]
        public Workspace Workspace { get; set; }
    }

    public partial class Profile
    {
        [JsonProperty("webpage")]
        public dynamic Webpage { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("birth")]
        public string Birth { get; set; }

        [JsonProperty("birth_day")]
        public string BirthDay { get; set; }

        [JsonProperty("birth_year")]
        public long BirthYear { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("address_id")]
        public long AddressId { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("job_id")]
        public long JobId { get; set; }

        [JsonProperty("total_follow_users")]
        public long TotalFollowUsers { get; set; }

        [JsonProperty("total_mypixiv_users")]
        public long TotalMypixivUsers { get; set; }

        [JsonProperty("total_illusts")]
        public long TotalIllusts { get; set; }

        [JsonProperty("total_manga")]
        public long TotalManga { get; set; }

        [JsonProperty("total_novels")]
        public long TotalNovels { get; set; }

        [JsonProperty("total_illust_bookmarks_public")]
        public long TotalIllustBookmarksPublic { get; set; }

        [JsonProperty("total_illust_series")]
        public long TotalIllustSeries { get; set; }

        [JsonProperty("total_novel_series")]
        public long TotalNovelSeries { get; set; }

        [JsonProperty("background_image_url")]
        public Uri BackgroundImageUrl { get; set; }

        [JsonProperty("twitter_account")]
        public string TwitterAccount { get; set; }

        [JsonProperty("twitter_url")]
        public Uri TwitterUrl { get; set; }

        [JsonProperty("pawoo_url")]
        public Uri PawooUrl { get; set; }

        [JsonProperty("is_premium")]
        public bool IsPremium { get; set; }

        [JsonProperty("is_using_custom_profile_image")]
        public bool IsUsingCustomProfileImage { get; set; }
    }

    public partial class ProfilePublicity
    {
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("birth_day")]
        public string BirthDay { get; set; }

        [JsonProperty("birth_year")]
        public string BirthYear { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("pawoo")]
        public bool Pawoo { get; set; }
    }

    public partial class Workspace
    {
        [JsonProperty("pc")]
        public string Pc { get; set; }

        [JsonProperty("monitor")]
        public string Monitor { get; set; }

        [JsonProperty("tool")]
        public string Tool { get; set; }

        [JsonProperty("scanner")]
        public string Scanner { get; set; }

        [JsonProperty("tablet")]
        public string Tablet { get; set; }

        [JsonProperty("mouse")]
        public string Mouse { get; set; }

        [JsonProperty("printer")]
        public string Printer { get; set; }

        [JsonProperty("desktop")]
        public string Desktop { get; set; }

        [JsonProperty("music")]
        public string Music { get; set; }

        [JsonProperty("desk")]
        public string Desk { get; set; }

        [JsonProperty("chair")]
        public string Chair { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("workspace_image_url")]
        public Uri WorkspaceImageUrl { get; set; }
    }

    public partial class AuthResult
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("user")]
        public ResponseUser User { get; set; }

        [JsonProperty("device_token")]
        public string DeviceToken { get; set; }
    }

    public partial class ResponseUser
    {
        [JsonProperty("profile_image_urls")]
        public ProfileImageUrls ProfileImageUrls { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("mail_address")]
        public string MailAddress { get; set; }

        [JsonProperty("is_premium")]
        public bool IsPremium { get; set; }

        [JsonProperty("x_restrict")]
        public long XRestrict { get; set; }

        [JsonProperty("is_mail_authorized")]
        public bool IsMailAuthorized { get; set; }
    }

    public partial class ProfileImageUrls
    {
        [JsonProperty("px_16x16")]
        public Uri Px16X16 { get; set; }

        [JsonProperty("px_50x50")]
        public Uri Px50X50 { get; set; }

        [JsonProperty("px_170x170")]
        public Uri Px170X170 { get; set; }
    }

    public partial class ShowcaseArticle
    {
        public static ShowcaseArticle FromJson(string json) => JsonConvert.DeserializeObject<ShowcaseArticle>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class UgoiraMetadata
    {
        public static UgoiraMetadata FromJson(string json) => JsonConvert.DeserializeObject<UgoiraMetadata>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class UserList
    {
        public static UserList FromJson(string json) => JsonConvert.DeserializeObject<UserList>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class UserMyPixiv
    {
        public static UserFollowList FromJson(string json) => JsonConvert.DeserializeObject<UserFollowList>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class UserFollower
    {
        public static UserFollowList FromJson(string json) => JsonConvert.DeserializeObject<UserFollowList>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class UserFollowList
    {
        public static UserFollowList FromJson(string json) => JsonConvert.DeserializeObject<UserFollowList>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class UserBookmarkTags
    {
        public static UserBookmarkTags FromJson(string json) => JsonConvert.DeserializeObject<UserBookmarkTags>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class IllustBookmarkDetail
    {
        public static IllustBookmarkDetail FromJson(string json) => JsonConvert.DeserializeObject<IllustBookmarkDetail>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class SearchIllustResult
    {
        public static SearchIllustResult FromJson(string json) => JsonConvert.DeserializeObject<SearchIllustResult>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class TrendingTagsIllust
    {
        public static TrendingTagsIllust FromJson(string json) => JsonConvert.DeserializeObject<TrendingTagsIllust>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class IllustRanking
    {
        public static UserIllusts FromJson(string json) => JsonConvert.DeserializeObject<UserIllusts>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class IllustRecommended
    {
        public static IllustRecommended FromJson(string json) => JsonConvert.DeserializeObject<IllustRecommended>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class IllustRelated
    {
        public static UserIllusts FromJson(string json) => JsonConvert.DeserializeObject<UserIllusts>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class IllustCommentAddResult
    {
        public static IllustCommentAddResult FromJson(string json) => JsonConvert.DeserializeObject<IllustCommentAddResult>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class IllustComments
    {
        public static IllustComments FromJson(string json) => JsonConvert.DeserializeObject<IllustComments>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class IllustDetail
    {
        public static IllustDetail FromJson(string json) => JsonConvert.DeserializeObject<IllustDetail>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class IllustFollow
    {
        public static UserIllusts FromJson(string json) => JsonConvert.DeserializeObject<UserIllusts>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class UserBookmarksIllust
    {
        public static UserIllusts FromJson(string json) => JsonConvert.DeserializeObject<UserIllusts>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class UserIllusts
    {
        public static UserIllusts FromJson(string json) => JsonConvert.DeserializeObject<UserIllusts>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class UserDetail
    {
        public static UserDetail FromJson(string json) => JsonConvert.DeserializeObject<UserDetail>(json, PixivCS.Objects.Converter.Settings);
    }

    public partial class AuthResult
    {
        public static AuthResult FromJson(string json) => JsonConvert.DeserializeObject<AuthResult>(json, PixivCS.Objects.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ShowcaseArticle self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this UgoiraMetadata self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this UserList self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this UserFollowList self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this UserBookmarkTags self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this IllustBookmarkDetail self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this SearchIllustResult self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this TrendingTagsIllust self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this UserIllusts self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this IllustRecommended self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this IllustCommentAddResult self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this IllustComments self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this IllustDetail self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this UserDetail self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
        public static string ToJson(this AuthResult self) => JsonConvert.SerializeObject(self, PixivCS.Objects.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
