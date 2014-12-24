using System.Windows.Input;


namespace ProductionMan.Desktop.Commands
{

    public class NavigateToWebsiteCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event System.EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                var url = parameter as string;
                if (!string.IsNullOrWhiteSpace(url))
                {
                    System.Diagnostics.Process.Start(url);
                }
            }
        }
    }
}
