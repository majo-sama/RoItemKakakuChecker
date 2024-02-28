using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
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
                        break;
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

                    item.EachPrice = Convert.ToInt32(parsedJson["median"].ToString());
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
