using Melody;
using Melody_Windows.Downloads;
using Melody_Windows.Pages.Subpages;
using Melody_Windows.ViewModels;
using Melody_Windows.Search;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Melody_Windows.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BrowsePage : Page
    {
        public BrowsePage()
        {

            this.InitializeComponent();
            cancellationTokenSource = new CancellationTokenSource();
            DownloadsViewModel = (DownloadsViewModel)Application.Current.Resources["DownloadsViewModel"];
        }
        
        
        private readonly DownloadsViewModel DownloadsViewModel;
        private CancellationTokenSource cancellationTokenSource;
        private bool IsLoading = false;
        
        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            var selectedItem = BrowseSelectorBar.SelectedItem;

            ExecuteQuery(sender.Text, selectedItem.Tag.ToString().ParseSearchType());
        }
        private readonly ObservableCollection<Media> Results;

        private void BrowseSelectorBar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            ExecuteQuery(SearchBox.Text, sender.SelectedItem.Tag.ToString().ParseSearchType());
        }

        private void BrowseSelectorBar_Loaded(object sender, RoutedEventArgs e)
        {
            var selectorBar = sender as SelectorBar;
            selectorBar.SelectedItem = selectorBar.Items[0];
        }
        private void ExecuteQuery(string query, SearchType searchType)
        {
            if (string.IsNullOrEmpty(query))
            {
                return;
            }
            Debug.WriteLine($"Executing query : {query} : {searchType}");
            switch (searchType)
            {
                case SearchType.Tracks:
                    {
                        ResultsFrame.Navigate(typeof(TracksPage), new QueryParams(query, DownloadsViewModel, cancellationTokenSource));
                        break;
                    }
                case SearchType.Albums:
                    {
                        ResultsFrame.Navigate(typeof(AlbumsPage), new QueryParams(query, DownloadsViewModel, cancellationTokenSource));
                        break;
                    }
                case SearchType.SpotifyPlaylists:
                    {
                        ResultsFrame.Navigate(typeof(PlaylistsPage), new QueryParams(query, DownloadsViewModel, cancellationTokenSource));
                        break;
                    }
                case SearchType.Videos:
                    {
                        ResultsFrame.Navigate(typeof(VideosPage), new QueryParams(query, DownloadsViewModel, cancellationTokenSource));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        
    }
}
