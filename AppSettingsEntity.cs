using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoItemKakakuChecker
{
    internal class AppSettingsEntity
    {
        public string ChatLogDir { get; set; } = "";

        public int ApiLimit { get; set; } = 0;
        public int DebugMode {  get; set; } = 0;
    }
}
