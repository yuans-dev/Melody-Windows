using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_Windows.Converters
{
    public class StatusToActionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is string s)
            {
                switch (s)
                {
                    case "Success":
                        return "Finished";
                    case "Error":
                        return "Retry";
                    default:
                        return "Cancel";
                }
            }
            else
            {
                return "Something went wrong";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
