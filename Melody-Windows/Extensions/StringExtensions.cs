using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_Windows.Extensions
{
    internal static class StringExtensions
    {
        internal static string MakeSafeForFiles(this string input)
        {
            string tmp = input;
            tmp = tmp.Replace('/', '-');
            tmp = tmp.Replace('|', ' ');
            tmp = tmp.Replace('\"', ' ');
            tmp = tmp.Replace('[', ' ');
            tmp = tmp.Replace(']', ' ');
            tmp = tmp.Replace('{', ' ');
            tmp = tmp.Replace('}', ' ');
            tmp = tmp.Replace('\'', ' ');
            tmp = tmp.Replace('.', ' ');
            tmp = tmp.Replace(':', ' ');
            tmp = tmp.Replace('?', ' ');
            tmp = tmp.Replace('*', ' ');
            return tmp;
        }
    }
}
