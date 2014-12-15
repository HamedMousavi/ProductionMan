using System.Windows;
using System.Windows.Input;


namespace ProductionMan.Desktop
{

    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {

        public LoginWindow()
        {
            InitializeComponent();

            Title = Localized.Resources.WindowTitleLogin;
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }


        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var model = DataContext as LoginViewModel;
            if (model != null)
            {
                model.Password = PasswordCtrl.Password;
            }
        }
    }
}
