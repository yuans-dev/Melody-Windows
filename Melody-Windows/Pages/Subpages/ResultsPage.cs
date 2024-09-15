using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Melody_Windows.Controls;

namespace Melody_Windows.Pages.Subpages
{
    public abstract class ResultsPage : Page, INotifyPropertyChanged
    {
        public ResultsPage()
        {
            Results = [];

        }
        protected readonly ObservableCollection<IMedia> Results;
        protected DownloadsViewModel DownloadsViewModel;
        protected CancellationTokenSource CancellationTokenSource;
        protected string Query;

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool _IsLoading = false;
        protected bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (QueryParams)e.Parameter;
            ArgumentNullException.ThrowIfNull(parameters, "parameters");

            DownloadsViewModel = parameters.DownloadsViewModel;
            CancellationTokenSource = parameters.CancellationTokenSource;
            if (parameters.Query != Query)
            {
                IsLoading = true;
                Query = parameters.Query;
                await Browse(Query);
                await StopLoading();
            }
        }
        protected abstract Task Browse(string query);
        protected async Task StopLoading()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });
            IsLoading = false;
        }
        protected void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var media = button.DataContext as SingleMedia;
            var downloadItem = new DownloadItem(media);
            DownloadsViewModel.Downloads.Add(downloadItem);
        }
        protected void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var media = button.DataContext as SingleMedia;
            PlayerControl.Current.CurrentMedia = media;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
