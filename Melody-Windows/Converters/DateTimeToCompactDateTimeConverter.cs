using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_Windows.Converters
{
    class DateTimeToCompactDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime d) {
                if (d == DateTime.MinValue)
                {
                    return "";
                }
                else
                {
                    return d.ToString("MMMM d, yyyy");
                }
            }
            else if (value is TimeSpan t)
            {
                return t.ToString("m':'ss");
            }
            else
            {
                return "";
            }
                    
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
