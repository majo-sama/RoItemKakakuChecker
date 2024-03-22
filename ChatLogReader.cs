using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoItemKakakuChecker
{
    internal class ChatLogReader
    {
        public static IEnumerable<Item> SearchItemLogs(IEnumerable<FileInfo> files)
        {
            // 最新のファイルおよび、最新から1秒以内に作られたファイルを対象とする
            var latestFileCreatetionTime = files.First().CreationTime;
            var targetFiles = files.Where(f => f.CreationTime.AddSeconds(1.0) >= latestFileCreatetionTime);

            var targetItems = new List<Item>();
            var regex = new Regex(@"^(.+) (\d+) 個獲得$");

            foreach (FileInfo file in targetFiles)
            {
                Match match = null;
                string[] lines = File.ReadAllLines(file.FullName, Encoding.GetEncoding("shift_jis"));
                var items = lines.Where(line =>
                    {
                        match = regex.Match(line);
                        return match.Groups.Count == 3;
                    })
                    .Select(line =>
                    {
                        return new Item() { Name = match.Groups[1].Value, Count = Convert.ToInt32(match.Groups[2].Value) };
                    });
                if (items.Any())
                {
                    var uniqueList = new List<Item>();
                    foreach (var item in items)
                    {
                        if (!uniqueList.Any())
                        {
                            uniqueList.Add(item);
                            continue;
                        }

                        var uniqItem = uniqueList.FirstOrDefault(uqitem => uqitem.Name == item.Name);
                        if (uniqItem == null)
                        {
                            uniqueList.Add(item);
                        }
                        else
                        {
                            uniqItem.Count += item.Count;
                        }
                    }

                    return uniqueList;
                }
            }
            return new List<Item>();
        }
    }
}
