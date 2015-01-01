using System.Windows;
using System.Windows.Input;


namespace ProductionMan.Desktop
{

    /// <summary>
    /// Interaction logic for RoleEditorWindow.xaml
    /// </summary>
    public partial class RoleEditorWindow : Window
    {
        public RoleEditorWindow()
        {
            InitializeComponent();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
