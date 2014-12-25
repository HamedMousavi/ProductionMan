using ProductionMan.Web.Api.Common.Models;
using System.Windows;
using System.Windows.Input;


namespace ProductionMan.Desktop
{

    /// <summary>
    /// Interaction logic for UserEditorWindow.xaml
    /// </summary>
    public partial class UserEditorWindow : Window
    {

        public UserEditorWindow()
        {
            InitializeComponent();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }


        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var model = DataContext as UserWrite;
            if (model != null)
            {
                model.Password = PasswordCtrl.Password;
            }
        }
    }
}
