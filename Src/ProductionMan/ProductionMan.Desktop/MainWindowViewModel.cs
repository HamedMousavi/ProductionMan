using ProductionMan.Common;
using ProductionMan.Desktop.Controls;
using ProductionMan.Desktop.Controls.MainParts;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace ProductionMan.Desktop
{

    public class MainWindowViewModel : BaseViewModel
    {

        private object _activeContent;
        private BaseContentSelector<TabItemViewModel> _activeContentSelector;
        private ObservableCollection<TabItemViewModel> _tabs;
        private TabItemViewModel _selectedItem;
        private LogonBoxViewModel _logonBoxModel;


        public ObservableCollection<TabItemViewModel> Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                FirePropertyChanged(this, "Tabs");
            }
        }


        public LogonBoxViewModel LogonBoxModel
        {
            get { return _logonBoxModel; }
            set
            {
                _logonBoxModel = value;
                FirePropertyChanged(this, "LogonBoxModel");
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
