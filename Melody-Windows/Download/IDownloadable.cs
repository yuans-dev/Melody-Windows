﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_Windows.Download
{
    public interface IDownloadable
    {
        Task Download();
    }
}
