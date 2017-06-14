﻿using System;
using RoboticsLibrary.Utilities;

namespace RoboticsLibrary.Sensors
{
    public class LimitSwitch : Sensor
    {
        private int Pin;
        private bool NormallyLow;
        private bool PrevToggled = false;
        public event EventHandler<LimitSwitchToggle> SwitchToggle;

        public LimitSwitch(int Pin, bool NormallyLow)
        {
            this.Pin = Pin;
            this.NormallyLow = NormallyLow;
        }
        
        public override bool Test()
        {
            // TODO: Call a GPIO library to check functionality.
            Log.Output(Log.Severity.WARNING, Log.Source.SENSORS, "Limit switch testing not implemented properly.");
            return true;
        }

        public override void UpdateState()
        {
            // TODO: Call a GPIO library to update the state.
            Log.Output(Log.Severity.WARNING, Log.Source.SENSORS, "Limit switch updating not implemented properly.");
            Random RandomGen = new Random();
            bool NowToggled = RandomGen.NextDouble() > 0.5;
            if (!this.NormallyLow) { NowToggled = !NowToggled; } // Invert the input if it is normally high.

            if (this.PrevToggled != NowToggled)
            {
                LimitSwitchToggle Event = new LimitSwitchToggle() { CurrentState = NowToggled };
                OnSwitchToggle(Event);
            }
            this.PrevToggled = NowToggled;
        }

        protected virtual void OnSwitchToggle(LimitSwitchToggle Event)
        {
            SwitchToggle?.Invoke(this, Event);
        }
    }

    public class LimitSwitchToggle : EventArgs
    {
        public bool CurrentState { get; set; }
    }
}