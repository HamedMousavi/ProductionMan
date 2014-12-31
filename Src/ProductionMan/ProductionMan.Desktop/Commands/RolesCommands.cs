using System;
using System.Windows.Input;
using ProductionMan.Desktop.Repositories;
using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop.Commands
{
    public class RoleWindowEditorCommand : ICommand
    {

        private readonly IUserRolesWindowManager _windowManager;


        public RoleWindowEditorCommand(IUserRolesWindowManager windowManager)
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
}
