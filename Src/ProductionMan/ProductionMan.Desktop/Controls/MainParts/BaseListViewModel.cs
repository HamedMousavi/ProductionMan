using System.Collections.ObjectModel;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public abstract class BaseListViewModel<T> : GenericListToolbarViewModel
    {

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
    }
}