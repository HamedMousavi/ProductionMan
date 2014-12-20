using System.Collections.ObjectModel;
using ProductionMan.Desktop.Controls.MainTabControl;


namespace ProductionMan.Desktop
{

    public class MainWindowViewModel : BaseViewModel
    {

        private ObservableCollection<TabItemViewModel> _tabs;
        private string _username;


        public ObservableCollection<TabItemViewModel> Tabs
        {
            get { return _tabs; }
            private set
            {
                _tabs = value;
                FirePropertyChanged(this, "Tabs");
            }
        }


        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                FirePropertyChanged(this, "Username");
            }
        }


        public MainWindowViewModel()
        {
            Tabs = new ObservableCollection<TabItemViewModel>
            {
                new TabItemViewModel {HeaderLabel = "Users", HeaderIcon = "User", PageTitle = "Mangage Users", IsSelected = true},
                new TabItemViewModel {HeaderLabel = "Permissions", HeaderIcon = "", PageTitle = "Edit Permissions"},
                new TabItemViewModel {HeaderLabel = "Materials", HeaderIcon = "Package", PageTitle = "Edit Materials"},
                new TabItemViewModel {HeaderLabel = "Processes", HeaderIcon = "Process", PageTitle = "Manage Processes"},
                new TabItemViewModel {HeaderLabel = "Stores", HeaderIcon = "Stores", PageTitle = "Manage Stores"},
                new TabItemViewModel {HeaderLabel = "Settings", HeaderIcon = "Settings", PageTitle = "Settings"},
            };
        }
    }
}
