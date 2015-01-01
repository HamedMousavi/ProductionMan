using System.Windows.Input;
using ProductionMan.Common;


namespace ProductionMan.Desktop.Controls.CheckedListBox
{

    public class CheckedListItem<T,T2> : NotifyPropertyChanged
    {

        private bool _isChecked;
        private T _item;
        private ICommand _itemCheckCommand;
        private T2 _itemOwner;


        public CheckedListItem()
        {
        }


        public CheckedListItem(T item, bool isChecked = false)
        {
            _item = item;
            _isChecked = isChecked;
        }


        public T Item
        {
            get { return _item; }
            set
            {
                _item = value;
                FirePropertyChanged(this, "Item");
            }
        }


        public T2 ItemOwner 
        {
            get { return _itemOwner; }
            set
            {
                _itemOwner = value;
                FirePropertyChanged(this, "ItemOwner");
            }
        }


        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                FirePropertyChanged(this, "IsChecked");
            }
        }


        public ICommand ItemCheckCommand
        {
            get { return _itemCheckCommand; }
            set { _itemCheckCommand = value; FirePropertyChanged(this, "ItemCheckCommand"); }
        }
    }
}