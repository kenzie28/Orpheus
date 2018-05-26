﻿using System;
using System.Collections.Generic;
using System.Threading;
using Scarlet.Communications;
using Scarlet.Components.Sensors;
using Scarlet.Utilities;
using Science.Library;

namespace Science_Base
{
    public static class DataHandler
    {
        public static DataSeries<float> ThermoInt = new DataSeries<float>("Thermocouple Internal", "Sensor Temperature (°C)");
        public static DataSeries<float> ThermoExt = new DataSeries<float>("Thermocouple External", "Ground Temperature (°C)");
        public static DataSeries<int> UV = new DataSeries<int>("UV Light", "UV Light (µm/cm²)");
        public static DataSeries<float> AirPollution = new DataSeries<float>("Air Pollution", "Air Pollution (PPM)");
        // TODO: Add the rest of the serieses.

        public static DataSeries<double> SupplyVoltage = new DataSeries<double>("Supply Voltage", "Supply Voltage (V)");
        public static DataSeries<double> SystemCurrent = new DataSeries<double>("System Current", "System Current (A)");
        public static DataSeries<double> DrillCurrent = new DataSeries<double>("Drill Current", "Drill Current (A)");
        public static DataSeries<double> RailCurrent = new DataSeries<double>("Rail Current", "Rail Current (A)");

        public static DataSeries<int> RandomData = new DataSeries<int>("Random", "Rubbish");
        private static Random Random;

        public static DataSeries<double> AIn = new DataSeries<double>("Analogue Input", "Input (Vb)");

        public static void Start()
        {
            Random = new Random();
            Parse.SetParseHandler(ScienceConstants.Packets.GND_SENSOR, PacketGroundSensor);
            Parse.SetParseHandler(ScienceConstants.Packets.SYS_SENSOR, PacketSysSensor);
            new Thread(new ThreadStart(DoAdds)).Start();
        }

        public static object[] GetSeries()
        {
            return new object[] { ThermoInt, ThermoExt, UV, AirPollution, SupplyVoltage, SystemCurrent, DrillCurrent, RailCurrent, RandomData, AIn };
        }

        public static void PacketGroundSensor(Packet Packet)
        {
            if(Packet == null || Packet.Data == null || Packet.Data.Payload == null || Packet.Data.Payload.Length != 16)
            {
                Log.Output(Log.Severity.WARNING, Log.Source.NETWORK, "Ground sensor packet invalid. Discarding.");
                return;
            }
            //float SoilMoisture = UtilData.ToFloat(UtilMain.SubArray(Packet.Data.Payload, 0, 4));
            int UVLight = UtilData.ToInt(UtilMain.SubArray(Packet.Data.Payload, 0, 4));
            uint ThermocoupleData = UtilData.ToUInt(UtilMain.SubArray(Packet.Data.Payload, 4, 4));
            double AInData = UtilData.ToDouble(UtilMain.SubArray(Packet.Data.Payload, 8, 8));
            ThermoExt.Data.Add(new Datum<float>(DateTime.Now, MAX31855.ConvertExternalFromRaw(ThermocoupleData)));
            ThermoInt.Data.Add(new Datum<float>(DateTime.Now, MAX31855.ConvertInternalFromRaw(ThermocoupleData)));
            UV.Data.Add(new Datum<int>(DateTime.Now, UVLight));
            AIn.Data.Add(new Datum<double>(DateTime.Now, AInData));
        }

        public static void PacketSysSensor(Packet Packet)
        {
            if (Packet == null || Packet.Data == null || Packet.Data.Payload == null || Packet.Data.Payload.Length != 40)
            {
                Log.Output(Log.Severity.WARNING, Log.Source.NETWORK, "System sensor packet invalid. Discarding.");
                return;
            }
            double SysA = UtilData.ToDouble(UtilMain.SubArray(Packet.Data.Payload, 0, 8));
            double DrillA = UtilData.ToDouble(UtilMain.SubArray(Packet.Data.Payload, 8, 8));
            double RailA = UtilData.ToDouble(UtilMain.SubArray(Packet.Data.Payload, 16, 8));
            double SysV = UtilData.ToDouble(UtilMain.SubArray(Packet.Data.Payload, 24, 8));

            DateTime Sample = new DateTime(UtilData.ToLong(UtilMain.SubArray(Packet.Data.Payload, 32, 8)));
            //Log.Output(Log.Severity.INFO, Log.Source.GUI, "Got sysA:" + SysCurrent + ", DrlA:" + DrillCurrent + ", SysV:" + SysVoltage);
            BaseMain.Window.UpdateGauges(SysV, SysA, DrillA, RailA);
            SupplyVoltage.Data.Add(new Datum<double>(Sample, SysV));
            SystemCurrent.Data.Add(new Datum<double>(Sample, SysA));
            DrillCurrent.Data.Add(new Datum<double>(Sample, DrillA));
            RailCurrent.Data.Add(new Datum<double>(Sample, RailA));
        }

        private static void DoAdds()
        {
            Log.Output(Log.Severity.INFO, Log.Source.GUI, "Beginning data addition.");
            for (int i = 0; i < 600; i++)
            {
                Thread.Sleep(150);
                RandomData.Data.Add(new Datum<int>(DateTime.Now, Random.Next(100)));
            }
            Log.Output(Log.Severity.INFO, Log.Source.GUI, "Data addition ended.");
            
        }

    }
}