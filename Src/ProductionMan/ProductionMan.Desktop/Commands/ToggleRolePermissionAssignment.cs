using ProductionMan.Desktop.Controls.CheckedListBox;
using ProductionMan.Desktop.Repositories;
using ProductionMan.Web.Api.Common.Models;
using System;
using System.Windows.Input;


namespace ProductionMan.Desktop.Commands
{

    public class AssignRolePermissionCommand : ICommand
    {

        private readonly MembershipRepository _repository;

        public event EventHandler CanExecuteChanged;


        public AssignRolePermissionCommand(MembershipRepository repository)
        {
            _repository = repository;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }


        public async void Execute(object parameter)
        {
            var item = parameter as CheckedListItem<Permission, UserRole>;
            if (item == null) return;

            //item.Item
            if (item.IsChecked) await _repository.AssignPermissionToRole(item.ItemOwner, item.Item);
            else await _repository.RetractPermissionFromRole(item.ItemOwner, item.Item);
        }
    }
}
