using ProductionMan.Desktop.Controls.MainParts;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace ProductionMan.Desktop.Converters
{

    [ValueConversion(typeof(TabItemViewModel), typeof(BitmapImage))]
    public class TabControlHeaderIconConverter : IMultiValueConverter
    {

        private const string IconExtentionOn = "On";
        private const string IconExtentionOff = "Off";
        private const string IconNotFound = "MidError";

        
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length >= 2)
            {
                var isSelected = false;
                if (values[0] != null) isSelected = (bool) values[0];
                var iconName = values[1] as string;

                if (!string.IsNullOrWhiteSpace(iconName))
                {
                    var resName = iconName + (isSelected ? IconExtentionOn : IconExtentionOff);
                    var resource = Application.Current.FindResource(resName);
                    if (resource != null) return resource;
                }
            }

            return Application.Current.FindResource(IconNotFound);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
