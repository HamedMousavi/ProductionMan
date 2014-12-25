using System.Windows.Input;


namespace ProductionMan.Desktop
{

    public class UserEditorWindowViewModel : BaseViewModel
    {
        public ICommand SaveCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public Web.Api.Common.Models.UserWrite User { get; set; }
    }
}
