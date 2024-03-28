using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoItemKakakuChecker
{
    public class AppSettings
    {
        private string settingFilePath = System.Windows.Forms.Application.StartupPath + @"\settings.json";

        private AppSettingsEntity settings = new AppSettingsEntity();

        public int ApiLimit { get { return settings.ApiLimit; } }
        public string ChatLogDir { get { return settings.ChatLogDir; } }

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
