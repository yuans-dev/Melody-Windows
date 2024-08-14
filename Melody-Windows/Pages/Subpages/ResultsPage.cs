using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Melody;
using Melody_Windows.Downloads;
using Melody_Windows.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Melody_Windows.Pages.Subpages
{
    public abstract class ResultsPage : Page
    {
        public ResultsPage()
        {
            Results = [];

        }
        protected readonly ObservableCollection<IMedia> Results;
        protected DownloadsViewModel DownloadsViewModel;
        protected CancellationTokenSource CancellationTokenSource;
        protected string Query;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (QueryParams)e.Parameter;
            ArgumentNullException.ThrowIfNull(parameters, "parameters");

            DownloadsViewModel = parameters.DownloadsViewModel;
            CancellationTokenSource = parameters.CancellationTokenSource;
            if (parameters.Query != Query)
            {
                Query = parameters.Query;
                Browse(Query);
            }
        }
        protected abstract void Browse(string query);
        protected void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var media = button.DataContext as SingleMedia;
            var downloadItem = new DownloadItem(media);
            DownloadsViewModel.Downloads.Add(downloadItem);
        }
    }
}
