using System;
using System.Windows.Input;
using AutoMapper;
using ProductionMan.Domain.WebServices;
using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Commands
{

    public class VisualAddUserCommand : ICommand
    {

        private readonly IUserWindowManager _windowManager;


        public VisualAddUserCommand(IUserWindowManager windowManager)
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
            _windowManager.DisplayUserAddWindow(new UserWrite());
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
            var userRead = parameter as UserRead;
            if (userRead != null)
            {
                _windowManager.DisplayUserEditWindow(Mapper.Map<UserWrite>(userRead));
            }
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
            var user = parameter as UserRead;
            if (user != null)
            {
                _windowManager.RequestPermissionToDelete(user);
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
