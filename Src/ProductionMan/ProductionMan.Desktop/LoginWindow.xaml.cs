using System.Windows;


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
        }
    }
}
