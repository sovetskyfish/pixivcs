哟！这里是API介绍文档！  
这个文档介绍的不是Pixiv API 所以如果你想找Pixiv API相关的文档...  
emmm...我只能告诉你 你找错地方了

---
Imgur API
---
你可以在ImgurAPI类中找到以下方法：  
```Csharp
public static void SetApiKey(string API_Key);// 设置API Key
public static JsonObject Upload(byte[] Image);// 同步上传图像
public static async Task<JsonObject> UploadAsync(byte[] Image);// 异步上传图像
```
没什么好说的...文档注释说的很明白了..(大概?)  
我要说的是上传方法的返回值  
它的结构是这样的
```json
{
    "data":{
        "id":string, //
        "title":null,
        "description": null,
        "datetime": number,
        "type": string,
        "animated": boolean,
        "width": number,
        "height": number,
        "size": number,
        "views": number,
        "bandwidth": number,
        "vote": null,
        "favorite": boolean,
        "nsfw": null,
        "section": null,
        "account_url": null,
        "account_id": number,
        "is_ad": boolean,
        "in_most_viral": boolean,
        "has_sound": boolean,
        "tags": [],
        "ad_type": number,
        "ad_url": string,
        "edited": string,
        "in_gallery": boolean,
        "deletehash": string,
        "name": string,
        "link": string // 图片URL
    },
    "success": boolean,
    "status": number
}
```
那么...我要说的是...我们只需要用 data=>link 的值...别的也用不到...不是嘛？  
你问其他的..？我不知道！

---
Tietuku API
---
贴图库的返回值很简单
```json
{
    "width": number,
    "height": number,
    "type": string,
    "size": number,
    "ubburl": "[img]${Url}[/img]",
    "linkurl": "${Url}",
    "htmlurl": "<img src='${Url}' />",
    "markdown": "![Markdown](${Url})",
    "s_url": "${Url}",
    "t_url": "${Url}",
    "findurl": string
}
```
其中${Url}就是图片的URL  
它返回了各种格式的图片链接...这里只用 linkurl 的值

---
SauceNAO API
---
这个的返回值就比较厉害了
我们只用 results[0]=>data=>pixiv_id  
以下是上传pid=36545288的返回结果..我反正是懒得处理了
```json
{
    "header": {
        "user_id": "28613",
        "account_type": "1",
        "short_limit": "6",
        "long_limit": "200",
        "long_remaining": 199,
        "short_remaining": 5,
        "status": 0,
        "results_requested": 16,
        "index": {
            "0": {
                "status": 0,
                "parent_id": 0,
                "id": 0,
                "results": 16
            },
            "2": {
                "status": 0,
                "parent_id": 2,
                "id": 2,
                "results": 16
            },
            "5": {
                "status": 0,
                "parent_id": 5,
                "id": 5,
                "results": 16
            },
            "51": {
                "status": 0,
                "parent_id": 5,
                "id": 51,
                "results": 16
            },
            "52": {
                "status": 0,
                "parent_id": 5,
                "id": 52,
                "results": 16
            },
            "53": {
                "status": 0,
                "parent_id": 5,
                "id": 53,
                "results": 16
            },
            "6": {
                "status": 0,
                "parent_id": 6,
                "id": 6,
                "results": 16
            },
            "8": {
                "status": 0,
                "parent_id": 8,
                "id": 8,
                "results": 16
            },
            "9": {
                "status": 0,
                "parent_id": 9,
                "id": 9,
                "results": 128
            },
            "10": {
                "status": 0,
                "parent_id": 10,
                "id": 10,
                "results": 16
            },
            "11": {
                "status": 0,
                "parent_id": 11,
                "id": 11,
                "results": 16
            },
            "12": {
                "status": 1,
                "parent_id": 9,
                "id": 12
            },
            "16": {
                "status": 0,
                "parent_id": 16,
                "id": 16,
                "results": 16
            },
            "18": {
                "status": 0,
                "parent_id": 18,
                "id": 18,
                "results": 16
            },
            "19": {
                "status": 0,
                "parent_id": 19,
                "id": 19,
                "results": 16
            },
            "20": {
                "status": 0,
                "parent_id": 20,
                "id": 20,
                "results": 16
            },
            "21": {
                "status": 0,
                "parent_id": 21,
                "id": 21,
                "results": 16
            },
            "211": {
                "status": 0,
                "parent_id": 21,
                "id": 211,
                "results": 16
            },
            "22": {
                "status": 0,
                "parent_id": 22,
                "id": 22,
                "results": 16
            },
            "23": {
                "status": 0,
                "parent_id": 23,
                "id": 23,
                "results": 16
            },
            "24": {
                "status": 0,
                "parent_id": 24,
                "id": 24,
                "results": 16
            },
            "25": {
                "status": 1,
                "parent_id": 9,
                "id": 25
            },
            "26": {
                "status": 1,
                "parent_id": 9,
                "id": 26
            },
            "27": {
                "status": 1,
                "parent_id": 9,
                "id": 27
            },
            "28": {
                "status": 1,
                "parent_id": 9,
                "id": 28
            },
            "29": {
                "status": 1,
                "parent_id": 9,
                "id": 29
            },
            "30": {
                "status": 1,
                "parent_id": 9,
                "id": 30
            },
            "31": {
                "status": 0,
                "parent_id": 31,
                "id": 31,
                "results": 16
            },
            "32": {
                "status": 0,
                "parent_id": 32,
                "id": 32,
                "results": 16
            },
            "33": {
                "status": 0,
                "parent_id": 33,
                "id": 33,
                "results": 16
            },
            "34": {
                "status": 0,
                "parent_id": 34,
                "id": 34,
                "results": 16
            },
            "35": {
                "status": 0,
                "parent_id": 35,
                "id": 35,
                "results": 16
            },
            "36": {
                "status": 0,
                "parent_id": 36,
                "id": 36,
                "results": 16
            },
            "37": {
                "status": 0,
                "parent_id": 37,
                "id": 37,
                "results": 16
            }
        },
        "search_depth": "128",
        "minimum_similarity": 43.78,
        "query_image_display": "userdata/S1WwSwZS3.jpg.png",
        "query_image": "S1WwSwZS3.jpg",
        "results_returned": 16
    },
    "results": [
        {
            "header": {
                "similarity": "96.67",
                "thumbnail": "https://img1.saucenao.com/res/seiga_illust/320/3203913.jpg?auth=qT60NtrCcziKC2DZXGvL7Q&exp=1579526032",
                "index_id": 8,
                "index_name": "Index #8: Nico Nico Seiga - 3203913.jpg"
            },
            "data": {
                "ext_urls": [
                    "https://seiga.nicovideo.jp/seiga/im3203913"
                ],
                "title": "ぜかまし",
                "seiga_id": 3203913,
                "member_name": "まめでんきゅう",
                "member_id": 1127576
            }
        },
        {
            "header": {
                "similarity": "95.96",
                "thumbnail": "https://img1.saucenao.com/res/pixiv/3654/36545288_m.jpg?auth=qUx_Th8RYwoQl7FywSZZpg&exp=1579526032",
                "index_id": 5,
                "index_name": "Index #5: Pixiv Images - 36545288_m.jpg"
            },
            "data": {
                "ext_urls": [
                    "https://www.pixiv.net/member_illust.php?mode=medium&illust_id=36545288"
                ],
                "title": "ぜかまし",
                "pixiv_id": 36545288,
                "member_name": "まめでんきゅう",
                "member_id": 138129
            }
        },
        {
            "header": {
                "similarity": "96.91",
                "thumbnail": "https://img3.saucenao.com/booru/1/a/1a67d91a8482cccd5dd48e0d1fd63dc5_0.jpg",
                "index_id": 9,
                "index_name": "Index #9: Danbooru - 1a67d91a8482cccd5dd48e0d1fd63dc5_0.jpg"
            },
            "data": {
                "ext_urls": [
                    "https://danbooru.donmai.us/post/show/1446438",
                    "https://gelbooru.com/index.php?page=post&s=view&id=1915468",
                    "https://chan.sankakucomplex.com/post/show/3181620"
                ],
                "danbooru_id": 1446438,
                "gelbooru_id": 1915468,
                "sankaku_id": 3181620,
                "creator": "mamedenkyuu (berun)",
                "material": "kantai collection",
                "characters": "rensouhou-chan, shimakaze (kantai collection)",
                "source": "http://i2.pixiv.net/img12/img/berun/36545288.jpg"
            }
        },
        {
            "header": {
                "similarity": "45.1",
                "thumbnail": "https://img1.saucenao.com/res/mangadex/525/525293/M11.jpg?auth=sTMPTmvG4Y-fegfTn9ENDQ&exp=1579526032",
                "index_id": 37,
                "index_name": "Index #37: MangaDex - M11.jpg"
            },
            "data": {
                "ext_urls": [
                    "https://mangadex.org/chapter/525293/",
                    "https://www.mangaupdates.com/series.html?id=515",
                    "https://myanimelist.net/manga/2459/"
                ],
                "md_id": 525293,
                "mu_id": 515,
                "mal_id": 2459,
                "source": "Komatta Toki ni wa Hoshi ni Kike!",
                "part": " - Chapter 6",
                "artist": "Abe Miyuki",
                "author": "Abe Miyuki"
            }
        },
        {
            "header": {
                "similarity": "41.3",
                "thumbnail": "https://img1.saucenao.com/res/pixiv/2892/manga/28928415_p0.jpg?auth=MbWo3vUVcJX_TYXcszKWPQ&exp=1579526032",
                "index_id": 5,
                "index_name": "Index #5: Pixiv Images - 28928415_p0.jpg"
            },
            "data": {
                "ext_urls": [
                    "https://www.pixiv.net/member_illust.php?mode=medium&illust_id=28928415"
                ],
                "title": "【腐向け】俺は基本的には強気です。【日六】",
                "pixiv_id": 28928415,
                "member_name": "青梨",
                "member_id": 539242
            }
        },
        {
            "header": {
                "similarity": "42.78",
                "thumbnail": "https://img1.saucenao.com/res/pixiv/6144/manga/61441949_p7.jpg?auth=PeH5dLDm4S8mVOEt3EI1rg&exp=1579526032",
                "index_id": 5,
                "index_name": "Index #5: Pixiv Images - 61441949_p7.jpg"
            },
            "data": {
                "ext_urls": [
                    "https://www.pixiv.net/member_illust.php?mode=medium&illust_id=61441949"
                ],
                "title": "3/20家宝新刊サンプル+アンケ",
                "pixiv_id": 61441949,
                "member_name": "むつば",
                "member_id": 17139524
            }
        },
        {
            "header": {
                "similarity": "41.28",
                "thumbnail": "https://img1.saucenao.com/res/nhentai/264200%20%281371127%29%20--%20%5BOkadatei%20%28Okada%20Kou%29%5D%20Irotoridori%20Vol.%201%20%5BDigital%5D/40.jpg?auth=Bcg3a9SCAKq4O5FaFLGIzQ&exp=1579526032",
                "index_id": 18,
                "index_name": "Index #18: H-Misc - 40.jpg"
            },
            "data": {
                "source": "Irotoridori Vol. 1 [Digital]",
                "creator": [
                    "okada kou"
                ],
                "eng_name": "[Okadatei (Okada Kou)] Irotoridori Vol. 1",
                "jp_name": "[おかだ亭 (岡田コウ)] イロトリドリVOL.01"
            }
        },
        {
            "header": {
                "similarity": "41.07",
                "thumbnail": "https://img1.saucenao.com/res/nhentai/61368%20%28370742%29%20--%20%5BYagami%20Kenkirou%5D%20Bakkinkei/132.jpg?auth=rbIvc_arfwWOpFF10CzQpg&exp=1579526032",
                "index_id": 18,
                "index_name": "Index #18: H-Misc - 132.jpg"
            },
            "data": {
                "source": "Bakkinkei",
                "creator": [
                    "yagami kenkirou"
                ],
                "eng_name": "[Yagami Kenkirou] Bakkinkei",
                "jp_name": "[矢上健喜朗] 縛禁刑"
            }
        },
        {
            "header": {
                "similarity": "40.87",
                "thumbnail": "https://img1.saucenao.com/res/nhentai/137546%20%28827320%29%20--%20%5BOnikubo%20Hirohisa%5D%20Trick-Ster%20%5BChinese%5D/52.jpg?auth=5a5oRw7IlbcuKAK_7Jedww&exp=1579526032",
                "index_id": 18,
                "index_name": "Index #18: H-Misc - 52.jpg"
            },
            "data": {
                "source": "Trick-Ster [Chinese]",
                "creator": [
                    "onikubo hirohisa"
                ],
                "eng_name": "[Onikubo Hirohisa] Trick-Ster",
                "jp_name": "[鬼窪浩久] TRICK-STER"
            }
        },
        {
            "header": {
                "similarity": "40.84",
                "thumbnail": "https://img1.saucenao.com/res/nhentai/232644%20%281222283%29%20--%20%28Lyrical%20Magical%2015%29%20%5BEUNOXLINE%20%28U-1%29%5D%20Yuno%20Fei%20%28Mahou%20Shoujo%20Lyrical%20Nanoha%29/4.jpg?auth=ATmmQgHKpf810BOyRBBxwQ&exp=1579526032",
                "index_id": 18,
                "index_name": "Index #18: H-Misc - 4.jpg"
            },
            "data": {
                "source": "Yuno Fei",
                "creator": [
                    "u-1"
                ],
                "eng_name": "(Lyrical Magical 15) [EUNOXLINE (U-1)] Yuno Fei (Mahou Shoujo Lyrical Nanoha)",
                "jp_name": "(リリカルマジカル15) [EUNOXLINE (U-1)] ゆの☆ふぇ (魔法少女リリカルなのは)"
            }
        },
        {
            "header": {
                "similarity": "40.75",
                "thumbnail": "https://img1.saucenao.com/res/nhentai/76843%20%28500321%29%20--%20%5BSanbun%20Kyoden%5D%20Sayuki%20no%20Sato%20%5BDecensored%5D/14.jpg?auth=mCEiI8hucixD_EecRtk_dA&exp=1579526032",
                "index_id": 18,
                "index_name": "Index #18: H-Misc - 14.jpg"
            },
            "data": {
                "source": "Sayuki no Sato [Decensored]",
                "creator": [
                    "sanbun kyoden"
                ],
                "eng_name": "[Sanbun Kyoden] Sayuki no Sato",
                "jp_name": "[山文京伝] 沙雪の里 (無修正版)"
            }
        },
        {
            "header": {
                "similarity": "40.74",
                "thumbnail": "https://img1.saucenao.com/res/nhentai/58793%20%28345013%29%20--%20%5BSanbun%20Kyoden%5D%20Sayuki%20no%20Sato/13.jpg?auth=dTA7oNyknkPqa9kB5Ctifw&exp=1579526032",
                "index_id": 18,
                "index_name": "Index #18: H-Misc - 13.jpg"
            },
            "data": {
                "source": "Sayuki no Sato",
                "creator": [
                    "sanbun kyoden"
                ],
                "eng_name": "[Sanbun Kyoden] Sayuki no Sato",
                "jp_name": "[山文京伝] 沙雪の里"
            }
        },
        {
            "header": {
                "similarity": "40.69",
                "thumbnail": "https://img1.saucenao.com/res/nhentai/250326%20%281302676%29%20--%20%5BSanbun%20Kyoden%5D%20Sayuki%20no%20Sato%20Ch.01%20%28ENGLISH%29%20%28UNCENSORED%29%20%28Original%29%20%5BBrolen%2BFaytear%2Blordhell%5D/14.jpg?auth=c5XO3w_NvcceyMC8_aLsfg&exp=1579526032",
                "index_id": 18,
                "index_name": "Index #18: H-Misc - 14.jpg"
            },
            "data": {
                "source": "Sayuki no Sato Ch.01",
                "creator": [
                    "sanbun kyoden"
                ],
                "eng_name": "[Sanbun Kyoden] Sayuki no Sato Ch.01 (ENGLISH) (UNCENSORED) (Original)",
                "jp_name": "[山文京伝] 沙雪の里 Ch.01 (ENGLISH) (UNCENSORED) (Original)"
            }
        },
        {
            "header": {
                "similarity": "40.45",
                "thumbnail": "https://img1.saucenao.com/res/nhentai/70662%20%28452459%29%20--%20%28C81%29%20%5BTakumi%20na%20Muchi%5D%20The%20Workout%20%28WORKING%21%21%29%20%5BEnglish%5D%20%5BLife4Kaoru%5D/32.jpg?auth=KqyUJEjPtm_Sz1jeAPqcGw&exp=1579526032",
                "index_id": 18,
                "index_name": "Index #18: H-Misc - 32.jpg"
            },
            "data": {
                "source": "The Workout",
                "creator": [
                    "Unknown"
                ],
                "eng_name": "(C81) [Takumi na Muchi] The Workout (WORKING!!)",
                "jp_name": null
            }
        },
        {
            "header": {
                "similarity": "40.31",
                "thumbnail": "https://img1.saucenao.com/res/nhentai/20616%20%2873136%29%20--%20%5BUeno%20Naoya%5D%20Broken%20Body/20.jpg?auth=2EsD2ESf3g6ayZrRf_EVkg&exp=1579526032",
                "index_id": 18,
                "index_name": "Index #18: H-Misc - 20.jpg"
            },
            "data": {
                "source": "Broken Body",
                "creator": [
                    "ueno naoya",
                    "kamiya naoya"
                ],
                "eng_name": "[Ueno Naoya] Broken Body",
                "jp_name": "[ウエノ直哉] ブロークン ボディ"
            }
        },
        {
            "header": {
                "similarity": "40.25",
                "thumbnail": "https://img3.saucenao.com/madokami/Manga/W/WE/WEEK/Weekly Shonen Jump/Weekly Shonen Jump 2014 No.44.cbz/P00034.jpg",
                "index_id": 36,
                "index_name": "Index #36: Madokami (Manga) - P00034.jpg"
            },
            "data": {
                "source": "Weekly Shonen Jump 2014 No.44",
                "part": "Weekly Shonen Jump 2014 No.44",
                "type": "Manga"
            }
        }
    ]
}
```