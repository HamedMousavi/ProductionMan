using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Input;
using ProductionMan.Desktop.Services;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;


namespace ProductionMan.Desktop.Commands
{

    public class VisualAddUserCommands : ICommand
    {

        private readonly IUserWindowManager _windowManager;
        private readonly Membership _membershipService;
        private readonly ILanguageService _languageService;


        public VisualAddUserCommands(IUserWindowManager windowManager, Membership membershipService, ILanguageService languageService)
        {
            _windowManager = windowManager;
            _membershipService = membershipService;
            _languageService = languageService;
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
                User = new UserWrite(),
                Roles = new List<UserRole>(),
                Languages = _languageService.Languages
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
            var userWrite = new UserWrite();
            // UNDONE: MAP FROM USER READ parameter as UserRead

            _windowManager.DisplayUserEditorWindow(new UserEditorWindowViewModel
            {
                SaveCommand = new UpdateUserCommand(_membershipService),
                CancelCommand = new CloseWindowCommand(),
                User = userWrite
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
            var user = parameter as UserRead;
            if (user != null)
            {
                _windowManager.RequestPermissionToDelete(
                    new ConfirmDeleteWindowViewModel
                    {
                        MessageDetail = string.Format("User, name= {0}", user.DisplayName),
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
            _membershipService.CreateUser(parameter as UserWrite);
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
            _membershipService.UpdateUser(parameter as UserWrite);
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
            var userWrite = new UserWrite();
            // UNDONE: MAP FROM USER READ parameter as UserRead
            _membershipService.DeleteUser(userWrite);
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
