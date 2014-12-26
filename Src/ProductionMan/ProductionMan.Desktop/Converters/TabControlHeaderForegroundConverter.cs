using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ProductionMan.Desktop.Converters
{

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class TabControlHeaderForegroundConverter : IValueConverter
    {

        private static Brush _regularBrush;
        private static Brush _selectedBrush;


        public TabControlHeaderForegroundConverter()
        {
            var colorRes = Application.Current.FindResource("TextColorGray") as Brush;
            if (colorRes != null)
            {
                _regularBrush = colorRes;//new SolidColorBrush((Color)colorRes);
            }

            colorRes = Application.Current.FindResource("TextColorGold") as Brush;
            if (colorRes != null)
            {
                _selectedBrush = colorRes; //new SolidColorBrush((Color)colorRes);
            }
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var isSelected = (bool) value;
                return isSelected ? _selectedBrush : _regularBrush;
            }

            return _regularBrush;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
