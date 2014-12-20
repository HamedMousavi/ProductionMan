using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace ProductionMan.Desktop.Converters
{

    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class TabControlHeaderIconConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var iconeName = value as string;
            if (!string.IsNullOrWhiteSpace(iconeName))
            {
                return Application.Current.FindResource(iconeName);
            }

            return Application.Current.FindResource("MidError");
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
