using ProductionMan.Desktop.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;


namespace ProductionMan.Desktop.Converters
{

    [ValueConversion(typeof(Status.Levels), typeof(Brush))]
    public class StatusLevelColorConverter : IValueConverter
    {

        private static Dictionary<Status.Levels, Brush> _brushes;


        public StatusLevelColorConverter()
        {
            _brushes = new Dictionary<Status.Levels, Brush>();


            var colorRes = Application.Current.FindResource("TextColorRed") as Brush;
            _brushes.Add(Status.Levels.Failure, colorRes ?? new SolidColorBrush(Colors.Red));

            colorRes = Application.Current.FindResource("TextColorGold") as Brush;
            _brushes.Add(Status.Levels.Warning, colorRes ?? new SolidColorBrush(Colors.Gold));

            colorRes = Application.Current.FindResource("TextColorGreen") as Brush;
            _brushes.Add(Status.Levels.Success, colorRes ?? new SolidColorBrush(Colors.GreenYellow));

            colorRes = Application.Current.FindResource("TextColorGray") as Brush;
            _brushes.Add(Status.Levels.Info, colorRes ?? new SolidColorBrush(Colors.LightGray));
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Status.Levels)
            {
                var level = (Status.Levels) value;
                if (_brushes.ContainsKey(level))
                {
                    return _brushes[level];
                }
            }

            return _brushes[Status.Levels.Info];
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
