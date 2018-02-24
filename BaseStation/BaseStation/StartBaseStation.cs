﻿using System;
using HuskyRobotics.UI;
using System.Threading;
using System.Windows;

/// <summary>
/// Contains the entry point to the base station system.
/// </summary>
namespace HuskyRobotics.BaseStation.Start {
    public static class StartBaseStation {
        /// <summary>
        /// The entry point of the base station system. Starts the base station user interface
		/// and communication with the rover.
        /// </summary>
        /// <param name="args">command-line arguments</param>
		[STAThread]
        public static void Main(String[] args) {
			//temporary example code
			new Thread(StartServer).Start();
			Application app = new Application();
			app.Exit += (sd, ev) => Scarlet.Communications.Server.Stop();
			app.Run(new MainWindow());
		}

		private static void StartServer() {
			Scarlet.Communications.Server.Start(50000, 50000);
		}
    }
}
