using System.Windows.Input;


namespace ProductionMan.Desktop.Controls.MainParts
{
    /// <summary>
    /// Interaction logic for CrusherWindow.xaml
    /// </summary>
    public partial class CrusherPage
    {
        public CrusherPage()
        {
            InitializeComponent();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
