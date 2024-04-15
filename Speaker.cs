﻿using System;
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

        private SpeechSynthesizer synthesizerForName;
        private SpeechSynthesizer synthesizerForBody;
        public string SpeakerName { get; set; }
        public string MessageBody { get; set; }

        public bool Enabled { get { return synthesizerForName != null && synthesizerForBody != null; } }


        public Speaker()
        {
            synthesizerForName = CreateSpeechSynthesizer(100, 2);
            synthesizerForBody = CreateSpeechSynthesizer(100, 0);

            synthesizerForName.SpeakCompleted += (sender, e) => {
                string body = MessageBody;
                if (MessageBody == null)
                {
                    return;
                }
                synthesizerForBody.SpeakAsync(body);
                SpeakerName = null;
                MessageBody = null;
            };
        }

        public void Speak()
        {
            string name = SpeakerName;
            if (SpeakerName == null || MessageBody == null)
            {
                return;
            }
            synthesizerForName.SpeakAsyncCancelAll();
            synthesizerForBody.SpeakAsyncCancelAll();

            synthesizerForName.SpeakAsync(name);
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
            synthesizerForName.Dispose();
            synthesizerForBody.Dispose();
        }

    }



}
