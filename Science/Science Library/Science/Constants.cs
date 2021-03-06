﻿namespace Science.Library
{
    public static class ScienceConstants
    {
        public const string CLIENT_NAME = "RoverScience";
        public const string DEFAULT_SERVER_IP = "192.168.0.5";

        public const int DEFAULT_PORT_TCP = 10765;
        public const int DEFAULT_PORT_UDP = 11765;

        public static class Packets
        {
            #region Bidirectional (0x4C through 0x64)
            public const byte WATCHDOG_PING = 0x00;
            public const byte ERROR = 0x4C;
            #endregion

            #region Base to Rover (0xA6 through 0xB1)
            public const byte EMERGENCY_STOP = 0x80;
            public const byte DRILL_SPEED_SET = 0xA6;
            public const byte SERVO_SET = 0xA7;
            public const byte RAIL_TARGET_SET = 0xA8;
            public const byte RAIL_SPEED_SET = 0xA9;
            #endregion

            #region Rover to Base (0xDC through 0xE4)
            public const byte AUX_SENSOR = 0xDC;
            public const byte GND_SENSOR = 0xDD;
            public const byte SYS_SENSOR = 0xDE;
            public const byte SYS_TELEMETRY = 0xDF;
            public const byte RAIL_STATUS = 0xE0;
            #endregion
        }
    }
}
