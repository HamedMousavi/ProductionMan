using ProductionMan.Common;
using ProductionMan.Desktop.Controls.CheckedListBox;
using ProductionMan.Web.Api.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class RoleManagerPageViewModel : NotifyPropertyChanged
    {

        // Role editor toolbar
        private ICommand _addCommand;
        private ICommand _editCommand;
        private ICommand _deleteCommand;

        private ObservableCollection<CheckedListItem<Permission, UserRole>> _permissions;
        private ObservableCollection<UserRole> _roles;
        private UserRole _selectedRole;
        private ICommand _toggleRolePermissionAssignment;
        private UserRole _selectedItem;


        public RoleManagerPageViewModel()
        {
            PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.NameIs("SelectedRole"))
                {
                    ReloadCheckedItems();

                    // Needed Selected item for some generic controls :-/
                    SelectedItem = SelectedRole;
                }
            };
        }


        public void SetPermissions(IEnumerable<Permission> permissions)
        {
            CreatePermissions(permissions);

            ReloadCheckedItems();
        }


        private void ReloadCheckedItems()
        {
            foreach (var item in Permissions)
            {
                item.IsChecked = false;
                item.ItemOwner = SelectedRole;
            }

            if (SelectedRole != null && SelectedRole.Permissions != null)
            {
                foreach (var permission in SelectedRole.Permissions)
                {
                    UpdateCheckedState(permission);
                }
            }
        }


        private void UpdateCheckedState(Permission permission)
        {
            foreach (var item in Permissions)
            {
                if (item.Item.PermissionId == permission.PermissionId) item.IsChecked = true;
            }
        }


        private void CreatePermissions(IEnumerable<Permission> permissions)
        {
            Permissions = new ObservableCollection<CheckedListItem<Permission, UserRole>>();
            foreach (var permission in permissions)
            {
                if (!string.IsNullOrWhiteSpace(permission.Description))
                {
                    Permissions.Add(new CheckedListItem<Permission, UserRole>
                    {
                        IsChecked = false,
                        Item = permission,
                        ItemCheckCommand = ToggleRolePermissionAssignment,
                        ItemOwner = null
                    });
                }
            }
        }


        public ObservableCollection<CheckedListItem<Permission, UserRole>> Permissions
        {
            get { return _permissions; }
            set
            {
                _permissions = value; 
                FirePropertyChanged(this, "Permissions");
            }
        }


        public ObservableCollection<UserRole> Roles
        {
            get { return _roles; }
            set { _roles = value; FirePropertyChanged(this, "Roles"); }
        }


        public UserRole SelectedRole
        {
            get { return _selectedRole; }
            set { _selectedRole = value; FirePropertyChanged(this, "SelectedRole"); }
        }


        public UserRole SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; FirePropertyChanged(this, "SelectedItem"); }
        }


        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; FirePropertyChanged(this, "AddCommand"); }
        }


        public ICommand EditCommand
        {
            get { return _editCommand; }
            set { _editCommand = value; FirePropertyChanged(this,  "EditCommand"); }
        }


        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; FirePropertyChanged(this, "DeleteCommand"); }
        }


        public ICommand ToggleRolePermissionAssignment
        {
            get { return _toggleRolePermissionAssignment; }
            set { _toggleRolePermissionAssignment = value; FirePropertyChanged(this, "ToggleRolePermissionAssignment"); }
        }
    }
}
