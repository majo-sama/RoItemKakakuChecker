﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace RoItemKakakuChecker
{
    public partial class MainForm : Form
    {
        public AppSettings settings;


        public MainForm()
        {
            settings = new AppSettings();

            InitializeComponent();
            this.TopMost = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            // 
            // chatLogModeControl
            // 
            this.chatLogModeControl = new RoItemKakakuChecker.ChatLogModeControl(this);
            this.chatLogModeControl.Location = new System.Drawing.Point(0, 52);
            this.chatLogModeControl.Name = "chatLogModeControl";
            this.chatLogModeControl.Size = new System.Drawing.Size(542, 412);
            this.chatLogModeControl.TabIndex = 15;




            this.radioButton_chatLog.CheckedChanged += RadioButton_chatLog_CheckedChanged;
            this.radioButton_storage.CheckedChanged += RadioButton_chatLog_CheckedChanged;

            this.radioButton_chatLog.Checked = true;


        }

        private void RadioButton_chatLog_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                if (radio.Name == "radioButton_chatLog")
                {
                    this.Controls.Add(this.chatLogModeControl);
                    this.Controls.Remove(this.storageObserveModeControl1);
                }
                else
                {
                    this.Controls.Add(this.storageObserveModeControl1);
                    this.Controls.Remove(this.chatLogModeControl);
                }
            }
            return;
        }



        public void UpdateToolStripLabel(string text)
        {
            toolStripStatusLabel.Text = text;
        }

        public void UpdateToolStripProgressBarSetting(int min, int max)
        {
            toolStripProgressBar.Minimum = min;
            toolStripProgressBar.Value = max;
        }

        public void UpdateToolStripProgressBarValue(int value)
        {
            toolStripProgressBar.Value = value;
        }

        public void IncrementToolStripProgressBarValue()
        {
            toolStripProgressBar.Value++;
        }

    }

}
