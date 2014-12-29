using System.Windows.Input;

namespace ProductionMan.Desktop
{
    public class ConfirmDeleteWindowViewModel : BaseViewModel
    {
        public string MessageDetail { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public object DeletingItem { get; set; }
    }
}
