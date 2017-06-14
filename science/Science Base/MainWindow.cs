﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RoboticsLibrary.Utilities;
using RoboticsLibrary.Communications;

namespace Science_Base
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EmergencyStopClick(object sender, EventArgs e)
        {
            Packet EmergencyStopPacket = new Packet((int)PacketType.StopPacket);
            CommHandler.SendAsyncPacket(EmergencyStopPacket);
        }

        private void SendPacketBtn_Click(object sender, EventArgs e)
        {

        }

        private void TimestampTextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTimeOffset DT = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(this.TimestampTextbox.Text.Replace(" ", ""), 16));
                this.InterpretationTimestamp.Text = DT.DateTime.ToLongDateString() + " " + DT.DateTime.ToLongTimeString() + " UTC";
            }
            catch(Exception Exc)
            {
                this.InterpretationTimestamp.Text = "Unknown";
            }
        }

        private void IDTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataTextbox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void UseCurrentTime_CheckedChanged(object sender, EventArgs e)
        {
            this.TimestampTextbox.Enabled = !this.UseCurrentTime.Checked;
            this.SecTimer.Enabled = this.UseCurrentTime.Checked;
            UpdateTime();
        }

        private void UpdateTime()
        {
            this.TimestampTextbox.Text = UtilMain.BytesToNiceString(Packet.GetTimestamp(), true);
        }

        private void SecTimer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }
    }
}