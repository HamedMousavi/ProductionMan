using System;
using System.Windows;
using System.Windows.Data;


namespace ProductionMan.Desktop.Converters
{

    [ValueConversion(typeof(string), typeof(FlowDirection))]
    public class FlowDirectionConverter : IValueConverter
    {

        private const string LeftToRight = "LTR";
        private const string RightToLeft = "RTL";


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var txt = value as string;
            if (!string.IsNullOrWhiteSpace(txt))
            {
                if (string.Equals(txt, RightToLeft, StringComparison.InvariantCultureIgnoreCase))
                {
                    return FlowDirection.RightToLeft;
                }
            }

            return FlowDirection.LeftToRight;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dir = (FlowDirection)value;
            if (dir == FlowDirection.RightToLeft)
            {
                return RightToLeft;
            }

            return LeftToRight;
        }
    }
}
