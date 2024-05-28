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
        public bool SpeechPublic { get; set; } = false;
        public bool SpeechParty { get; set; } = false;
        public bool SpeechGuild { get; set; } = false;
        public bool SpeechWhisper { get; set; } = false;
        public bool SpeechWord { get; set; } = false;
        public string SpeechKeyWord { get; set; } = "";
        public bool EnableMdYomiage { get; set; } = false;
        public int MdYomiageMax { get; set; } = 0;
        public string NetworkInterfaceName { get; set; } = "";
    }
}
