using ProductionMan.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public abstract class BaseListViewModel<T> : NotifyPropertyChanged
    {

        private ICommand _addCommand;
        private ICommand _deleteCommand;
        private ObservableCollection<T> _items;
        private T _selectedItem;


        public ObservableCollection<T> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                FirePropertyChanged(this, "Items");

            }
        }


        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                FirePropertyChanged(this, "SelectedItem");

            }
        }


        public ICommand AddCommand
        {
            get { return _addCommand; }
            set
            {
                _addCommand = value;
                FirePropertyChanged(this, "AddCommand");

            }
        }


        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
            set
            {
                _deleteCommand = value;
                FirePropertyChanged(this, "DeleteCommand");

            }
        }
    }
}