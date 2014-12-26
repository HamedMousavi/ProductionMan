using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using ProductionMan.Common;
using ProductionMan.Domain.Globalization;
using ProductionMan.Web.Api.Common.Models;

namespace ProductionMan.Desktop
{

    public class UserEditorWindowViewModel : BaseViewModel
    {

        private Language _selectedLanguage;
        private IEnumerable<Language> _languages;
        private List<UserRole> _roles;
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
                else if (e.NameIs("User") || e.NameIs("Languages"))
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


        public List<UserRole> Roles
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
