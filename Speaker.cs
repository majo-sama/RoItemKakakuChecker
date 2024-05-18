using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoItemKakakuChecker
{
    public class Speaker
    {

        private SpeechSynthesizer synthesizer;
        public string Message { get; set; }

        public bool Enabled { get { return synthesizer != null; } }


        public Speaker()
        {
            synthesizer = CreateSpeechSynthesizer(100, 2);

            synthesizer.SpeakCompleted += (sender, e) => {
                Message = null;
            };
        }

        public void Speak()
        {
            string message = Message;
            if (message == null || message.Length == 0)
            {
                return;
            }
            synthesizer.SpeakAsyncCancelAll();

            synthesizer.SpeakAsync(message);
        }


        private SpeechSynthesizer CreateSpeechSynthesizer(int volume, int speed)
        {
            var sz = new SpeechSynthesizer();
            // 設定
            sz.Volume = volume;
            sz.Rate = speed;

            // 使用できる音声合成エンジンを探す
            CultureInfo cultureInfo = Application.CurrentCulture;
            // voicesに使用でききる音声合成エンジンが格納される
            ReadOnlyCollection<InstalledVoice> voices = sz.GetInstalledVoices(cultureInfo);
            if (voices == null || voices.Count == 0)
            {
                return null;
            }
            sz.SelectVoice(voices[0].VoiceInfo.Name);
            return sz;
        }

        public void Dispose()
        {
            synthesizer.Dispose();
        }

    }



}
