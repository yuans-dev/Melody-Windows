using Melody_Windows.Downloads;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_Windows.ViewModels
{
    public class DownloadsViewModel
    {
        public ObservableCollection<DownloadItem> Downloads { get; set; }

        public DownloadsViewModel()
        {
            Downloads = [];
            Downloads.CollectionChanged += Downloads_CollectionChanged;
        }

        private async void Downloads_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MainWindow.CurrentInstance.DownloadInfoBadgeValue = 1;
            foreach (DownloadItem newItem in e.NewItems)
            {
                var downloadedFile = await Downloader.Download(newItem);
                await downloadedFile.SetMetadata(newItem.Media);
               
            }
        }
    }
}
