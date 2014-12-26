using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ProductionMan.Desktop.Services;

namespace ProductionMan.Desktop.Converters
{

    [ValueConversion(typeof(Status.Levels), typeof(BitmapImage))]
    class StatusLevelIconConverter : IValueConverter
    {

        private static Dictionary<Status.Levels, BitmapImage> _images;


        public StatusLevelIconConverter()
        {
            _images = new Dictionary<Status.Levels, BitmapImage>();
            
            var bitmap = Application.Current.FindResource("StatusFailure") as BitmapImage;
            _images.Add(Status.Levels.Failure, bitmap);

            bitmap = Application.Current.FindResource("StatusWarning") as BitmapImage;
            _images.Add(Status.Levels.Warning, bitmap);

            bitmap = Application.Current.FindResource("StatusSuccess") as BitmapImage;
            _images.Add(Status.Levels.Success, bitmap);

            bitmap = Application.Current.FindResource("StatusInfo") as BitmapImage;
            _images.Add(Status.Levels.Info, bitmap);
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Status.Levels)
            {
                var level = (Status.Levels) value;
                if (_images.ContainsKey(level))
                {
                    return _images[level];
                }
            }

            return _images[Status.Levels.Info];
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
