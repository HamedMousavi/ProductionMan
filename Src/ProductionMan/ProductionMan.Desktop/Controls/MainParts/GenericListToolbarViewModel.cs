using System.Windows.Input;
using ProductionMan.Common;

namespace ProductionMan.Desktop.Controls.MainParts
{

    public class GenericListToolbarViewModel : NotifyPropertyChanged
    {

        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;

        
        public ICommand AddCommand
        {
            get { return _addCommand; }
            set
            {
                _addCommand = value;
                FirePropertyChanged(this, "AddCommand");

            }
        }


        public ICommand EditCommand
        {
            get { return _editCommand; }
            set
            {
                _editCommand = value;
                FirePropertyChanged(this, "EditCommand");

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
