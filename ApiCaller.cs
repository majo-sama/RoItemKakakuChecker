using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoItemKakakuChecker
{
    internal class ApiCaller
    {

        public async Task<Item> GetItemAsync(string itemName)
        {
            Item item = await GetItemGeneralInfo(itemName);
            if (item == null)
            {
                return null;
            }
            Item priceInfo = await GetItemPriceInfo(item);
            return priceInfo;
        }

        private async Task<Item> GetItemGeneralInfo(string itemName)
        {
            HttpClient client = new HttpClient();
            var baseUrl = "https://rotool.gungho.jp/item/prediction_conversion_search_name/?item_name=";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            // ＜エンチャ名＞ を除く
            itemName = new Regex(" ＜.+$").Replace(itemName, "");

            // +1 の表記を除く
            itemName = new Regex(@"^\+\d+ ").Replace(itemName, "");

            // この時点で、itemName はアイテム名＋カードprefix/suffixのみになっているはず
            string[] arr =itemName.Split(' ');
            if (arr.Length > 1)
            {
                // カードが刺さっている
                if (arr[1].StartsWith("オブ"))
                {
                    // suffixの場合のカード名はオブ-から始まる
                    itemName = arr[0];
                }
                else
                {
                    // カード名がprefixの場合
                    itemName = arr[1];
                }

            }


            // ゲーム内のアイテム名が "メイル [1]" であっても、
            // RO公式ツールでは "メイル[1]" のようにスペースが無く検索できないため、その対応
            if (Regex.IsMatch(itemName, @" \[\d\]$"))
            {
                itemName = TrimLastSpace(itemName);
            }

            try
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + itemName);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    JsonNode parsedJson = JsonArray.Parse(json);

                    Item item = null;
                    var array = parsedJson.AsArray();
                    for (int i = 0; i < array.Count; i++)
                    {
                        
                        item = new Item();

                        item.ItemId = Convert.ToInt32(array[i]["item_id"].ToString());
                        item.Name = System.Net.WebUtility.HtmlDecode(array[i]["item_name"].ToString());

                        // 
                        if (item.Name == itemName)
                        {
                            break;
                        }
                        
                    }
                    return item;
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string TrimLastSpace(string input)
        {
            int lastSpaceIndex = input.LastIndexOf(' ');
            if (lastSpaceIndex >= 0)
            {
                return input.Remove(lastSpaceIndex, 1);
            }
            else
            {
                return input;
            }
        }


        private async Task<Item> GetItemPriceInfo(Item item)
        {
            HttpClient client = new HttpClient();
            var baseUrl = "https://rotool.gungho.jp/item_trade_log_summary_filtered_search/?make_flag=0&item_id=";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + item.ItemId);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var parsedJson = JsonObject.Parse(json);

                    item.EachPrice = Convert.ToInt64(parsedJson["median"].ToString());
                    return item;
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
