using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoItemKakakuChecker
{
    internal class ApiCaller
    {
        DataGridView dataGridView;
        MainForm mainForm;
        ComboBox comboApiLimit;

        public ApiCaller(DataGridView dataGridView, MainForm mainForm, ComboBox comboApiLimit)
        {
            this.dataGridView = dataGridView;
            this.mainForm = mainForm;
            this.comboApiLimit = comboApiLimit;
        }

        public async Task OnClickedFetchKakakuButton()
        {
            mainForm.isFetching = true;
            var items = (IEnumerable<Item>)dataGridView.DataSource;

            LoadCache(items);

            mainForm.UpdateToolStripProgressBarSetting(0, items.Count());
            mainForm.UpdateToolStripProgressBarValue(0);

            SortableBindingList<Item> newList = new SortableBindingList<Item>();
            List<Item> forUpdate = new List<Item>();

            int count = 1;
            foreach (var item in items)
            {
                if (mainForm.stopFlag)
                {
                    mainForm.UpdateToolStripLabel("取得を中断しました。");
                    mainForm.UpdateToolStripProgressBarValue(0);
                    mainForm.isFetching = false;
                    mainForm.stopFlag = false;
                    return;
                }


                mainForm.UpdateToolStripLabel($"価格情報取得中 ({count++}/{items.Count()})");

                Item dataFetchedItem = null;

                int limit = Convert.ToInt32(comboApiLimit.SelectedItem);
                if (item.LastFetchedAt >= DateTime.Now.AddDays(limit * -1))
                {
                    dataFetchedItem = item;
                }
                else
                {
                    dataFetchedItem = await GetItemAsync(item.Name);
                    if (dataFetchedItem == null)
                    {
                        mainForm.IncrementToolStripProgressBarValue();
                        continue;
                    }
                    forUpdate.Add(dataFetchedItem);
                }



                mainForm.IncrementToolStripProgressBarValue();
                dataFetchedItem.Count = item.Count;
                dataFetchedItem.LastFetchedAt = DateTime.Now;
                dataFetchedItem.TotalPrice = dataFetchedItem.EachPrice * dataFetchedItem.Count;
                newList.Add(dataFetchedItem);


            }

            mainForm.UpdateToolStripLabel($"価格情報取得完了");
            //dataGridView.DataSource = newList;

            dataGridView.Invoke((MethodInvoker)delegate { dataGridView.DataSource = newList; });


            SaveItemsCache(forUpdate);

            mainForm.isFetching = false;
        }


        private void LoadCache(IEnumerable<Item> items)
        {
            string cacheFilePath = Application.StartupPath + @"\cache";

            if (!File.Exists(cacheFilePath))
            {
                return;
            }

            try
            {
                using (var stream = new FileStream(cacheFilePath, FileMode.Open))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        var cachedItems = JsonSerializer.Deserialize<IEnumerable<Item>>(sr.ReadToEnd());

                        foreach (var item in items)
                        {
                            var cachedItem = cachedItems.FirstOrDefault(ci => item.Name == ci.Name);
                            if (cachedItem != null)
                            {
                                item.LastFetchedAt = cachedItem.LastFetchedAt;
                                item.ItemId = cachedItem.ItemId;
                                item.EachPrice = cachedItem.EachPrice;
                            }
                        }

                        return;
                    }
                }
            }
            catch (Exception)
            {
                File.Delete(cacheFilePath);
            }

        }


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
            // まずローカル定義の一覧からIDの取得を試みる
            var keyValue = mainForm.itemIdNameMap.Map.FirstOrDefault(e => e.Value == itemName);
            mainForm.itemIdNameMap.ValueKeyMap.TryGetValue(aaaaaaaaaaaaaaaaaaaaaa)
            if (!keyValue.Equals(default(KeyValuePair<int, string>)))
            {
                var item = new Item();
                item.ItemId = keyValue.Key;
                item.Name = keyValue.Value;
                return item;
            }


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



        private void SaveItemsCache(IEnumerable<Item> updatedItems)
        {

            string cacheFilePath = Application.StartupPath + @"\cache";

            List<Item> cachedItems = new List<Item>();

            if (File.Exists(cacheFilePath))
            {
                try
                {
                    using (var stream = new FileStream(cacheFilePath, FileMode.Open))
                    {
                        using (var sr = new StreamReader(stream))
                        {

                            cachedItems = JsonSerializer.Deserialize<List<Item>>(sr.ReadToEnd());
                        }
                    }
                }
                catch (Exception)
                {
                    File.Delete(cacheFilePath);
                }
            }


            foreach (Item updatedItem in updatedItems)
            {
                var cachedItem = cachedItems.FirstOrDefault(ci => updatedItem.ItemId == ci.ItemId);
                if (cachedItem != null)
                {
                    cachedItem.EachPrice = updatedItem.EachPrice;
                    cachedItem.LastFetchedAt = updatedItem.LastFetchedAt;
                }
                else
                {
                    cachedItems.Add(updatedItem);
                }
            }


            string jsonStr = JsonSerializer.Serialize(cachedItems);

            using (var writer = new StreamWriter(cacheFilePath, false, Encoding.UTF8))
            {
                writer.Write(jsonStr);
            }
        }


    }
}
