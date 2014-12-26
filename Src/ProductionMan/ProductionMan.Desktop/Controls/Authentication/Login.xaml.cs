using System.Windows;
using System.Windows.Input;

namespace ProductionMan.Desktop.Controls.Authentication
{

    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();

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
