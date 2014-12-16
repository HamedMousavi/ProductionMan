using System.Windows.Input;
using ProductionMan.Desktop.Commands;
using ProductionMan.Domain.Security;


namespace ProductionMan.Desktop
{

    public class CommandFactory
    {

        internal ICommand CreateLoginCommand(User user)
        {
            return new LoginCommand(user);
        }


        internal ICommand CreateExitCommand()
        {
            return new ExitCommand();
        }
    }
}
