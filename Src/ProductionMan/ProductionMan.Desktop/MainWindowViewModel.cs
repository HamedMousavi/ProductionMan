using System.Collections.ObjectModel;
using System.Diagnostics;
using ProductionMan.Desktop.Controls.MainTabControl;


namespace ProductionMan.Desktop
{

    public class MainWindowViewModel : BaseViewModel
    {

        public ObservableCollection<TabItemViewModel> Tabs { get; set; }


        public MainWindowViewModel()
        {
            Tabs = new ObservableCollection<TabItemViewModel>
            {
                new TabItemViewModel {HeaderLabel = "Users", HeaderIcon = "Users", PageTitle = "Mangage Users", IsSelected = true},
                new TabItemViewModel {HeaderLabel = "Permissions", PageTitle = "Edit Permissions"},
                new TabItemViewModel {HeaderLabel = "Materials", PageTitle = "Edit Materials"},
                new TabItemViewModel {HeaderLabel = "Settings", PageTitle = "Settings"},
            };
        }

        public string Username { get; set; }
    }
}
