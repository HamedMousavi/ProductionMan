using System.Windows.Input;

namespace ProductionMan.Desktop.Commands
{
    public class ConfirmDeleteWindowViewModel : BaseViewModel
    {
        public string MessageDetail { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public Web.Api.Common.Models.User User { get; set; }
    }
}
