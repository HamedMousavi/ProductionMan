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


        private void OnGridGeneratingColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            LocalizeColumnName(e);
        }


        private void LocalizeColumnName(DataGridAutoGeneratingColumnEventArgs e)
        {
            var currentName = e.Column.Header.ToString();
            if (currentName.Contains("Id")) e.Cancel = true;
            else
            {
                var localizedName = Services.SharedApplicationServices.Instanse.GetTextResource("ColumnHeader" + currentName);
                e.Column.Header = !string.IsNullOrWhiteSpace(localizedName) ? localizedName : currentName;
            }
            //    if ( == "Name")
            //e.Column.Header = "Task";
        }
    }
}
