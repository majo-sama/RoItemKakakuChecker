using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoItemKakakuChecker
{
    internal class StorageItem
    {
        public int ItemId { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public DateTimeOffset LimitDateTime { get; set; }
        public int Slot1ItemId { get; set; }
        public int Slot2ItemId { get; set; }
        public int Slot3ItemId { get; set; }
        public int Slot4ItemId { get; set; }
        public int Option1Key { get; set;}
        public int Option1Value { get; set;}
        public int Option2Key { get; set; }
        public int Option2Value { get; set; }
        public int Option3Key { get; set; }
        public int Option3Value { get; set; }
        public int Option4Key { get; set; }
        public int Option4Value { get; set; }
        public int Option5Key { get; set; }
        public int Option5Value { get; set; }
        public int EnhanceLevel { get; set;}
        public int CreateGrade { get; set; }
    }
}
