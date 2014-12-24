using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts
{

    /// <summary>
    /// Interaction logic for GenericListManager.xaml
    /// </summary>
    public partial class GenericListManager : UserControl
    {

        public GenericListManager()
        {
            InitializeComponent();
            //DataContextChanged += delegate
            //{
            //    var model = DataContext as INotifyPropertyChanged;
            //    if (model != null)
            //    {
            //        model.PropertyChanged += (o, args) => CommandManager.InvalidateRequerySuggested();
            //    }
            //};
        }
    }
}
