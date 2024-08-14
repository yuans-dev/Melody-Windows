using Melody_Windows.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Melody_Windows.Pages.Subpages
{
    internal class QueryParams
    {
        public QueryParams(string query, DownloadsViewModel downloadsViewModel, CancellationTokenSource cancellationTokenSource)
        {
            this.Query = query;
            this.DownloadsViewModel = downloadsViewModel;
            this.CancellationTokenSource = cancellationTokenSource;
        }
        public string Query { get; set; }
        public DownloadsViewModel DownloadsViewModel { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
    }
}
