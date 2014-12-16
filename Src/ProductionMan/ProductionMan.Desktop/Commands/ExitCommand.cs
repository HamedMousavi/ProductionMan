using System.Windows.Input;


namespace ProductionMan.Desktop.Commands
{

    public class ExitCommand : ICommand
    {

        public ExitCommand()
        {
            
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }


        public event System.EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
