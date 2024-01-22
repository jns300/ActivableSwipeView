using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivableSwipeViewSample.Converters
{
    public class ThresholdConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value == null ? "0" : ((double)value).ToString();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            double.TryParse((string?)value ?? "0", out double threshold);
            return threshold;
        }
    }

}
