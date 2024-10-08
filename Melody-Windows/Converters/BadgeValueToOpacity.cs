﻿using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_Windows.Converters
{
    public class BadgeValueToOpacity : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is int i)
            {
                if(i > 0)
                {
                    return (double)1;
                }
                else
                {
                    return (double)0;
                }
            }
            else
            {
                return (double)0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
