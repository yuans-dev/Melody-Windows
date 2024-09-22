using Melody;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Melody_Windows.Download
{
    public class DownloadItem : INotifyPropertyChanged, IDownloadable  
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _Title;
        private string _Authors;
        private int _Progress;
        private DownloadStatus _Status;
        private StorageFile _Output;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Authors
        {
            get
            {
                return _Authors;
            }
            set
            {
                _Authors = value;
                OnPropertyChanged(nameof(Authors));
            }
        }
        public int Progress
        {
            get
            {
                return _Progress;
            }
            set
            {
                _Progress   = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        public DownloadStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public StorageFile Output
        {
            get
            {
                if (Status != DownloadStatus.Success) return null;
                else return _Output;
            }
        }
        public SingleMedia media;
        public string ImageURL;
        public DownloadItem(SingleMedia media)
        {
            Title = media.Title;
            Authors = string.Join(", ", media.Authors);
            Progress = 0;
            Status = DownloadStatus.Pending;
            ImageURL = media.ImageURL;
        }
        public async Task Download()
        {
            Downloader.Instance.AddToDownloads(this);
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public enum DownloadStatus
        {
            Downloading, Error, Success, Pending
        }
    }
}
