using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls.MainParts
{

    /// <summary>
    /// Interaction logic for RoleManagerWindow.xaml
    /// </summary>
    public partial class RoleManagerPage
    {

        public RoleManagerPage()
        {
            InitializeComponent();
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
                var localizedNameIsNull = string.IsNullOrWhiteSpace(localizedName);
                if (localizedNameIsNull) e.Cancel = true;
                else e.Column.Header = localizedName;
            }
            //    if ( == "Name")
            //e.Column.Header = "Task";
        }
    }
}
