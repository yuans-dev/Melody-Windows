using Melody;
using Melody_Windows.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_Windows.Downloads
{
    public class DownloadItem : DefaultViewModel
    {
        public Progress<long> Progress;
        private int _ProgressValue;
        public int ProgressValue
        {
            get
            {
                return _ProgressValue;
            }
            set
            {
                _ProgressValue = value;
                OnPropertyChanged(nameof(ProgressValue));
            }
        }
        private string _Status;
        public string Status
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
        public SingleMedia Media { get; set; }
        public DownloadItem(SingleMedia media)
        {
            this.Media = media;
            Status = "Starting";
            ProgressValue = 0;
            Progress = new Progress<long>
            (p => { ProgressValue = (int)(p * 100);
            });
        }
    }
}
