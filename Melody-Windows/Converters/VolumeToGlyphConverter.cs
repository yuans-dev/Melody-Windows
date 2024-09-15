using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_Windows.Converters
{
    public class VolumeToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is int volume)
            {
                if(volume > 0 && volume <= 33)
                {
                    return "\uE993";

                }else if (volume > 33 && volume <= 67)
                {
                    return "\uE994";
                }else if(volume > 67 && volume <= 100)
                {
                    return "\uE995";
                }
                else
                {
                    return "\uE992";
                }
            }
            else
            {
                return "\uE992";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
