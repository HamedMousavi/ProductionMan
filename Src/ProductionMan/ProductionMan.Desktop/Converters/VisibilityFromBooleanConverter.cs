using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace ProductionMan.Desktop.Converters

{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class VisibilityFromBooleanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var visible = (bool)value;
                return visible ? Visibility.Visible : Visibility.Hidden;
            }

            return Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
