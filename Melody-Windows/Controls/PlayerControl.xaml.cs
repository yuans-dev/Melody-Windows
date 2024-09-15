using Melody;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Melody_Windows.Controls
{
    public sealed partial class PlayerControl : UserControl, INotifyPropertyChanged
    {
        public PlayerControl()
        {
            this.InitializeComponent();
            if (LocalSettings.Values["playerVolume"] == null)
            {
                LocalSettings.Values["playerVolume"] = 80;
            }
            Volume = (int)LocalSettings.Values["playerVolume"];
            Current = this;
           
        }
        ApplicationDataContainer LocalSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public event PropertyChangedEventHandler PropertyChanged;
        public static PlayerControl Current { get; set; }
        private bool _Expanded = true;
        private bool Expanded
        {
            get
            {
                return _Expanded;
            }
            set
            {
                _Expanded = value;
                OnPropertyChanged(nameof(Expanded));
            }
        }
        private int _Volume = 50;
        private int Volume
        {
            get
            {
                return _Volume;
            }
            set
            {
                _Volume = value;
                LocalSettings.Values["playerVolume"] = value;
                OnPropertyChanged(nameof(Volume));
            }
        }
        private SingleMedia _CurrentMedia = null;
        public SingleMedia CurrentMedia
        {
            get
            {
                return _CurrentMedia;
            }
            set
            {
                _CurrentMedia = value;
                OnPropertyChanged(nameof(CurrentMedia));
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ExpandCollapse_Click(object sender, RoutedEventArgs e)
        {
            if (!Expanded)
            { 
                expand.Begin();
            }
            else
            {
                collapse.Begin();
            }
            Expanded = !Expanded;
            
        }

        private void VolumeButton_Click(object sender, RoutedEventArgs e)
        {
            if(Volume == 0)
            {
                Volume = 80;
            }
            else
            {
                Volume = 0;
            }
        }
    }
}
