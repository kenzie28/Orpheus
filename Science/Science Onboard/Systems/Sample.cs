﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scarlet.Components;
using Scarlet.Components.Motors;
using Scarlet.IO;

namespace Science.Systems
{
    public class Sample : ISubsystem
    {
        private Servo Servo;

        public Sample(IPWMOutput ServoPWM)
        {
            this.Servo = new Servo(ServoPWM);
        }

        public void EmergencyStop()
        {
            
        }

        public void EventTriggered(object Sender, EventArgs Event)
        {

        }

        public void Initialize()
        {
            
        }

        public void UpdateState()
        {
            
        }
    }
}
