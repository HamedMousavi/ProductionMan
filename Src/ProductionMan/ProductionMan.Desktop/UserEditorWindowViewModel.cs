using ProductionMan.Common;
using ProductionMan.Domain.Globalization;
using ProductionMan.Web.Api.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace ProductionMan.Desktop
{

    public class UserEditorWindowViewModel : BaseViewModel
    {

        private Language _selectedLanguage;
        private IEnumerable<Language> _languages;
        private ObservableCollection<UserRole> _roles;
        private UserWrite _user;
        private ICommand _cancelCommand;
        private ICommand _saveCommand;


        public UserEditorWindowViewModel()
        {
            PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.NameIs("SelectedLanguage"))
                {
                    if (User != null && SelectedLanguage != null)
                    {
                        if (!string.Equals(SelectedLanguage.LocaleName, User.Culture,
                            StringComparison.InvariantCultureIgnoreCase))
                        {
                            User.Culture = SelectedLanguage.LocaleName;
                        }
                    }
                }
                else if (e.NameIs("User") || e.NameIs("Languages") || e.NameIs("Roles"))
                {
                    if (User != null && Languages != null)
                    {
                        foreach (var language in Languages)
                        {
                            if (string.Equals(language.LocaleName, User.Culture,
                                StringComparison.InvariantCultureIgnoreCase))
                            {
                                SelectedLanguage = language;
                            }
                        }
                    }
                    else if (User != null && User.Role != null && Roles != null)
                    {
                        foreach (var role in Roles)
                        {
                            if (role.RoleId == User.Role.RoleId)
                            {
                                // FORCE SELECTED ROLE BINDING IN USER EDITOR ROLE COMBO BOX
                                // BY DEFAULT USERS DON'T HAVE A ROLE BUT ONLY A ROLE ID
                                User.Role = role;
                            }
                        }
                    }
                }
            };
        }


        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set
            {
                _saveCommand = value;
                FirePropertyChanged(this, "SaveCommand");
            }
        }


        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            set
            {
                _cancelCommand = value;
                FirePropertyChanged(this, "CancelCommand");
            }
        }


        public UserWrite User
        {
            get { return _user; }
            set
            {
                _user = value;
                FirePropertyChanged(this, "User");
            }
        }


        public ObservableCollection<UserRole> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                FirePropertyChanged(this, "Roles");
            }
        }


        public IEnumerable<Language> Languages
        {
            get { return _languages; }
            set
            {
                _languages = value;
                FirePropertyChanged(this, "Languages");
            }
        }


        public Language SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    FirePropertyChanged(this, "SelectedLanguage");
                }
            }
        }
    }
}
