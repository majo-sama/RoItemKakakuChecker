using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoItemKakakuChecker
{
    internal class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int EachPrice {  get; set; }
        public DateTime LastFetchedAt { get; set; }
        public int TotalPrice { get; set; }
    }
}
