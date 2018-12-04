using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace PYJ_WatchDog.Converter
{
    public class RunStopColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Brushes.Green : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
