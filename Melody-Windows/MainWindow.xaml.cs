using Melody_Windows.Controls;
using Melody_Windows.Pages;
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
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Melody_Windows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            pageHistoryStack = new Stack<Type>();
            CurrentInstance = this;
        }
        private readonly Stack<Type> pageHistoryStack;
        private bool IsGoingBack = false;
            
        public static MainWindow CurrentInstance;
        private double _DownloadInfoBadgeValue = 0;
        public double DownloadInfoBadgeValue
        {
            get
            {
                return _DownloadInfoBadgeValue;
            }
            set
            {
                _DownloadInfoBadgeValue = value;
                OnPropertyChanged(nameof(DownloadInfoBadgeValue));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            Type pageType = null;
            if (args.IsSettingsSelected)
            {
                sender.Header = "Settings";
                pageType = typeof(SettingsPage);
            }
            else
            {
                var selectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;
                if (selectedItem != null)
                {
                    string selectedItemTag = ((string)selectedItem.Tag);
                    sender.Header = selectedItemTag;
                    string pageName = "Melody_Windows.Pages." + selectedItemTag;
                    Debug.WriteLine(pageName + "Page");
                    pageType = Type.GetType(pageName + "Page");
                }
            }
            if(pageType != null)
            {
                pageHistoryStack.Push(pageType);
                contentFrame.Navigate(pageType);
            }
        }
        

        private void navView_Loaded(object sender, RoutedEventArgs e)
        {
            navView.SelectedItem = navView.MenuItems[0];
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void navView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (pageHistoryStack.Count != 0) { 
                IsGoingBack = true;
                var pageType = pageHistoryStack.Pop();
                sender.SelectedItem = SelectNavigationViewItemFromTag(pageType.Name.Substring(0, pageType.Name.Length - 4));

                contentFrame.Navigate(pageType);
            }
        }
        private NavigationViewItem SelectNavigationViewItemFromTag(string tag)
        {
            foreach(NavigationViewItem item in navView.MenuItems)
            {
                if((string)item.Tag == tag)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
