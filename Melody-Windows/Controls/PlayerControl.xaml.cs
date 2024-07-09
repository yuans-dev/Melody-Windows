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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Melody_Windows.Controls
{
    public sealed partial class PlayerControl : UserControl, INotifyPropertyChanged
    {
        public PlayerControl()
        {
            this.InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private bool Expanded { get; set; } = true;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void expandCollapse_Click(object sender, RoutedEventArgs e)
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
            NotifyPropertyChanged("Expanded");
        }
    }
}
