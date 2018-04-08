﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace HuskyRobotics.UI
{
    /// <summary>
    /// Interaction logic for SettingsPanel.xaml
    /// </summary>
    public partial class SettingsPanel : UserControl, INotifyPropertyChanged
    {
        private MapConfiguration _mapConfig;
        private Settings _settings;
        public Settings Settings {
            get { return _settings; }
            set {
                _settings = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Settings"));
            }
        }
        private ObservableCollection<string> _mapSets = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<string> MapSets
        {
            get => _mapSets;
        }
        public MapConfiguration MapConfig {
            get { return _mapConfig; }
            set {
                _mapConfig = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MapConfig"));
            }
        }

        public SettingsPanel()
        {
            
            DataContext = this;
            MapConfig = new MapConfiguration();
            initMapFiles();
            InitializeComponent();
        }


        private void initMapFiles()
        {
            _mapSets.Clear();
            string[] files = Directory.GetFiles
                (Directory.GetCurrentDirectory() + @"\Images", "*.map");
            foreach (string file in files)
            {
                _mapSets.Add(System.IO.Path.GetFileName(file));
            }
        }

        private void Button_GetMap(object sender, RoutedEventArgs e)
        {
            MapTileDownloadManager.DownloadNewTileSet(MapConfig);
            initMapFiles();
            Settings.CurrentMapFile = MapConfig.MapSetName + ".map";
        }
    }
}
