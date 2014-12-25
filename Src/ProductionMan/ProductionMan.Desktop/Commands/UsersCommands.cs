using System;
using System.Windows.Input;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Desktop.Commands
{

    public class VisualAddUserCommands : ICommand
    {

        private readonly IUserWindowManager _windowManager;
        private readonly Membership _membershipService;


        public VisualAddUserCommands(IUserWindowManager windowManager, Membership membershipService)
        {
            _windowManager = windowManager;
            _membershipService = membershipService;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            _windowManager.DisplayUserAddWindow(new UserEditorWindowViewModel
            {
                SaveCommand = new CreateUserCommand(_membershipService),
                CancelCommand = new CloseWindowCommand(),
                User = new User()
            });
        }
    }


    /// <summary>
    /// Opens a window to edit selected user
    /// </summary>
    public class VisualEditUserCommand : ICommand
    {
 
        private readonly IUserWindowManager _windowManager;
        private readonly Membership _membershipService;


        public VisualEditUserCommand(IUserWindowManager windowManager, Membership membershipService)
        {
            _windowManager = windowManager;
            _membershipService = membershipService;
        }


        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            _windowManager.DisplayUserEditorWindow(new UserEditorWindowViewModel
            {
                SaveCommand = new UpdateUserCommand(_membershipService),
                CancelCommand = new CloseWindowCommand(),
                User = parameter as User
            });
        }
    }


    public class VisualDeleteUserCommand : ICommand
    {
 
        private readonly IUserWindowManager _windowManager;
        private readonly Membership _membershipService;


        public VisualDeleteUserCommand(IUserWindowManager windowManager, Membership membershipService)
        {
            _windowManager = windowManager;
            _membershipService = membershipService;
        }


        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            var user = parameter as User;
            if (user != null)
            {
                _windowManager.RequestPermissionToDelete(
                    new ConfirmDeleteWindowViewModel
                    {
                        MessageDetail = string.Format("User, name= {0}", user.Name),
                        DeleteCommand = new DeleteUserCommand(_membershipService),
                        CancelCommand = new CloseWindowCommand(),
                        User = user
                    });
            }
        }
    }



    public class CreateUserCommand : ICommand
    {
        private readonly Membership _membershipService;

        public CreateUserCommand(Membership membershipService)
        {
            _membershipService = membershipService;
        }


        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            _membershipService.CreateUser(parameter as User);
        }
    }


    public class UpdateUserCommand : ICommand
    {
        private readonly Membership _membershipService;

        public UpdateUserCommand(Membership membershipService)
        {
            _membershipService = membershipService;
        }

        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            _membershipService.UpdateUser(parameter as User);
        }
    }


    public class DeleteUserCommand : ICommand
    {
        private readonly Membership _membershipService;

        public DeleteUserCommand(Membership membershipService)
        {
            _membershipService = membershipService;
        }

        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            _membershipService.DeleteUser(parameter as User);
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
