using System;
using System.Windows.Input;


namespace ProductionMan.Desktop.Commands
{

    public class VisualAddUserCommands : ICommand
    {

        private readonly IUserWindowManager _windowManager;


        public VisualAddUserCommands(IUserWindowManager windowManager)
        {
            _windowManager = windowManager;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            _windowManager.DisplayUserAddWindow(new UserEditorWindowViewModel());
        }
    }


    /// <summary>
    /// Opens a window to edit selected user
    /// </summary>
    public class VisualEditUserCommand : ICommand
    {
 
        private readonly IUserWindowManager _windowManager;


        public VisualEditUserCommand(IUserWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        
        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            _windowManager.DisplayUserEditorWindow(new UserEditorWindowViewModel());
        }
    }


    public class VisualDeleteUserCommand : ICommand
    {
 
        private readonly IUserWindowManager _windowManager;


        public VisualDeleteUserCommand(IUserWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        
        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            _windowManager.RequestPermissionToDelete(new UserEditorWindowViewModel());
        }
    }


    /// <summary>
    /// Tries updating database
    /// </summary>
    public class UpdateUserCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {

        }
    }


    public class EnableUserCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {

        }
    }
}
