using System;
using System.Windows;
using System.Windows.Input;
using AutoMapper;
using ProductionMan.Desktop.Repositories;
using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Commands
{
    public class RoleWindowEditorAddCommand : ICommand
    {

        private readonly IUserRolesWindowManager _windowManager;


        public RoleWindowEditorAddCommand(IUserRolesWindowManager windowManager)
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
            _windowManager.DisplayRoleAddWindow(new UserRole());
        }
    }
   
    
    public class RoleAddCommand : ICommand
    {

        private readonly MembershipRepository _repository;


        public RoleAddCommand(MembershipRepository repository)
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
            await _repository.CreateRole(parameter as UserRole);
        }
    }
   
    
    public class RoleWindowEditorEditCommand : ICommand
    {

        private readonly IUserRolesWindowManager _windowManager;


        public RoleWindowEditorEditCommand(IUserRolesWindowManager windowManager)
        {
            _windowManager = windowManager;
        }


        public bool CanExecute(object parameter)
        {
            return true; // return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            var role = parameter as UserRole;
            if (role != null)
            {
                _windowManager.DisplayRoleEditWindow(role);
            }
        }
    }


    public class RoleWindowDeleteCommand : ICommand
    {

        private readonly IUserRolesWindowManager _windowManager;


        public RoleWindowDeleteCommand(IUserRolesWindowManager windowManager)
        {
            _windowManager = windowManager;
        }


        public bool CanExecute(object parameter)
        {
            return true; // return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            var role = parameter as UserRole;
            if (role != null)
            {
                _windowManager.RequestPermissionToDelete(role);
            }
        }
    }


    public class RoleUpdateCommand : ICommand
    {

        private readonly MembershipRepository _repository;


        public RoleUpdateCommand(MembershipRepository repository)
        {
            _repository = repository;
        }


        public bool CanExecute(object parameter)
        {
            return true; // return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public async void Execute(object parameter)
        {
            await _repository.UpdateRole(parameter as UserRole);
        }
    }


    public class RoleDeleteCommand : ICommand
    {

        private readonly MembershipRepository _repository;
        private readonly UserRole _role;


        public RoleDeleteCommand(MembershipRepository membershipRepository, UserRole role)
        {
            _repository = membershipRepository;
            _role = role;
        }


        public bool CanExecute(object parameter)
        {
            return true; // return parameter != null;
        }


        public event EventHandler CanExecuteChanged;


        public async void Execute(object parameter)
        {
            if (await _repository.DeleteRole(_role))
            {
                var window = parameter as Window;
                if (window != null) window.Close();
            }
        }
    }
}
