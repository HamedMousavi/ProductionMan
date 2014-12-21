using System.ComponentModel;
using ProductionMan.Common;
using ProductionMan.Desktop.Controls;
using ProductionMan.Desktop.Controls.MainTabControl;
using ProductionMan.Domain.Security;
using System.Collections.ObjectModel;


namespace ProductionMan.Desktop
{

    public class MainWindowViewModel : BaseViewModel
    {

        private User _user;
        private object _activeContent;
        private BaseContentSelector<TabItemViewModel> _activeContentSelector;
        private ObservableCollection<TabItemViewModel> _tabs;
        private TabItemViewModel _selectedItem;


        public ObservableCollection<TabItemViewModel> Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                FirePropertyChanged(this, "Tabs");
            }
        }

        
        public User User
        {
            get { return _user; }
            set
            {
                _user= value;
                FirePropertyChanged(this, "User");
            }
        }


        public object ActiveContent
        {
            get { return _activeContent; }
            private set
            {
                _activeContent = value;
                FirePropertyChanged(this, "ActiveContent");
            }
        }


        public BaseContentSelector<TabItemViewModel> ActiveContentSelector
        {
            get { return _activeContentSelector; }
            set
            {
                _activeContentSelector = value;
                FirePropertyChanged(this, "ActiveContentSelector");
            }
        }


        public TabItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                FirePropertyChanged(this, "SelectedItem");
            }
        }


        public MainWindowViewModel()
        {
            PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
            {
                if (args.NameIs("ActiveContentSelector") ||
                    args.NameIs("SelectedItem"))
                {
                    // Set a content based on the new user's current state
                    // or the new content selector
                    if (SelectedItem != null && ActiveContentSelector != null)
                    {
                        ActiveContent = ActiveContentSelector.Select(SelectedItem);
                    }
                }
            };
        }
    }
}
