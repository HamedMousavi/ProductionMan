using ProductionMan.Domain.Security;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace ProductionMan.Desktop.Converters
{

    [ValueConversion(typeof(User.LoginStates), typeof(BitmapImage))]
    public class LoginStatusIconConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var status = (User.LoginStates) value;
                switch (status)
                {
                    case User.LoginStates.Error:
                        return Application.Current.FindResource("MidError");

                    case User.LoginStates.IncorrectCredentials:
                        return Application.Current.FindResource("MidExclamation");
                }
            }

            return Application.Current.FindResource("MidError");
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
