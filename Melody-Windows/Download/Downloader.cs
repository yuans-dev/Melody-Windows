using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_Windows.Download
{
    public class Downloader
    {
        private Downloader()
        {

        }
        private static Downloader _Instance;
        private string DownloadFolder;
        private ObservableCollection<IDownloadable> Downloads;
        public static Downloader Instance
        {
            get
            {
                _Instance ??= new Downloader();
                return _Instance;
            }
        }
        public void SetDownloadFolder(string path)
        {
            DownloadFolder = path;
        }
        public void AddToDownloads(DownloadItem item)
        {
            Downloads.Add(item);

        }
 
    }
}
