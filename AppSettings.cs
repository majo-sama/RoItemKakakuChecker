using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoItemKakakuChecker
{
    public class AppSettings
    {
        private string settingFilePath = System.Windows.Forms.Application.StartupPath + @"\settings.json";

        private AppSettingsEntity settings = new AppSettingsEntity();

        public int ApiLimit { get { return settings.ApiLimit; } }
        public string ChatLogDir { get { return settings.ChatLogDir; } }
        public int DebugMode { get { return settings.DebugMode; } }
        public bool SpeechPublic { get { return settings.SpeechPublic; } set { this.settings.SpeechPublic = value; CreateOrUpdateFile(); } }
        public bool SpeechParty { get { return settings.SpeechParty; } set { this.settings.SpeechParty = value; CreateOrUpdateFile(); } }
        public bool SpeechGuild { get { return settings.SpeechGuild; } set { this.settings.SpeechGuild = value; CreateOrUpdateFile(); } }
        public bool SpeechWhisper { get { return settings.SpeechWhisper; } set { this.settings.SpeechWhisper = value; CreateOrUpdateFile(); } }
        public bool SpeechWord { get { return settings.SpeechWord; } set { this.settings.SpeechWord = value; CreateOrUpdateFile(); } }
        public string SpeechKeyWord { get { return settings.SpeechKeyWord; } set { this.settings.SpeechKeyWord = value; CreateOrUpdateFile(); } }
        public bool EnableMdYomiage { get { return settings.EnableMdYomiage; } set { this.settings.EnableMdYomiage = value; CreateOrUpdateFile(); } }
        public int MdYomiageMax { get { return settings.MdYomiageMax; } set {  this.settings.MdYomiageMax = value; CreateOrUpdateFile(); } }


        public bool ReadSettings()
        {
            if (!File.Exists(settingFilePath))
            {
                return false;
            }

            try
            {
                using (var stream = new FileStream(settingFilePath, FileMode.Open))
                {
                    using (var sr = new StreamReader(stream))
                    {
                        AppSettingsEntity settings = JsonSerializer.Deserialize<AppSettingsEntity>(sr.ReadToEnd());
                        this.settings.ApiLimit = settings.ApiLimit;
                        this.settings.ChatLogDir = settings.ChatLogDir;
                        this.settings.DebugMode = settings.DebugMode;
                        this.settings.SpeechPublic = settings.SpeechPublic;
                        this.settings.SpeechParty = settings.SpeechParty;
                        this.settings.SpeechGuild = settings.SpeechGuild;
                        this.settings.SpeechWhisper = settings.SpeechWhisper;
                        this.settings.SpeechWord = settings.SpeechWord;
                        this.settings.SpeechKeyWord = settings.SpeechKeyWord;
                        this.settings.EnableMdYomiage = settings.EnableMdYomiage;
                        this.settings.MdYomiageMax = settings.MdYomiageMax;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                File.Delete(settingFilePath);
                return false;
            }
        }

        public void SaveSettings(int limit, string path)
        {
            this.settings.ApiLimit = limit;
            this.settings.ChatLogDir = path;
            CreateOrUpdateFile();
        }

        public void SaveApiLimit(int limit)
        {
            this.settings.ApiLimit = limit;
            CreateOrUpdateFile();
        }

        public void SaveChatDir(string path)
        {
            this.settings.ChatLogDir = path;
            CreateOrUpdateFile();
        }


        private void CreateOrUpdateFile()
        {
            var settingsString = JsonSerializer.Serialize(settings);

            using (var writer = new StreamWriter(settingFilePath, false, Encoding.UTF8))
            {
                writer.Write(settingsString);
            }
        }
    }
}
