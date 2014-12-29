using System;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using ProductionMan.Desktop.Repositories;
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

        private readonly MembershipRepository _repository;


        public CreateUserCommand(MembershipRepository repository)
        {
            _repository = repository;
        }


        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public async void Execute(object parameter)
        {
            await _repository.CreateUser(parameter as UserWrite);
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
        private readonly MembershipRepository _repository;
        private readonly UserRead _user;

        public DeleteUserCommand(MembershipRepository repository, UserRead user)
        {
            _repository = repository;
            _user = user;
        }

        public bool CanExecute(object parameter)
        {
            return true;// return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public async void Execute(object parameter)
        {
            if (await _repository.DeleteUser(_user))
            {
                var window = parameter as Window;
                if (window != null) window.Close();
            }
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
