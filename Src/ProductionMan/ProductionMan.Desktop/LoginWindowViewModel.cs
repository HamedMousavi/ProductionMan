using System.ComponentModel;
using ProductionMan.Common;
using ProductionMan.Desktop.Controls;
using ProductionMan.Domain.Security;

namespace ProductionMan.Desktop
{

    public class LoginWindowViewModel : BaseViewModel
    {

        private User _user;
        private BaseContentSelector<User.LoginStates> _activeContentSelector;
        private object _activeContent;
        private string _title;


        public LoginWindowViewModel()
        {
            PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
            {
                if (args.NameIs("ActiveContentSelector") ||
                    args.NameIs("User"))
                {
                    // Set a content based on the new user's current state
                    // or the new content selector
                    if (_user != null && _activeContentSelector != null)
                    {
                        SelectActiveContent();
                    }
                }
            };
        }



        private void WireNewUser()
        {
            _user.PropertyChanged += UserOnPropertyChanged;
        }


        private void UnWireOldUser()
        {
            _user.PropertyChanged -= UserOnPropertyChanged;
        }


        private void UserOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.NameIs("LoginStatus"))
            {
                SelectActiveContent();
            }
        }


        private void SelectActiveContent()
        {
            ActiveContent = ActiveContentSelector.Select(_user.LoginStatus);
        }


        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                FirePropertyChanged(this, "Title");
            }
        }


        public object ActiveContent
        {
            get { return _activeContent; }
            private set
            {
                _activeContent = value;
                FirePropertyChanged(this, "ActiveContent");
            }
        }


        public BaseContentSelector<User.LoginStates> ActiveContentSelector
        {
            get { return _activeContentSelector; }
            set
            {
                _activeContentSelector = value;
                FirePropertyChanged(this, "ActiveContentSelector");
            }
        }


        public User User
        {
            get { return _user; }
            set
            {
                if (_user != null) UnWireOldUser();

                _user = value;

                if (_user != null) WireNewUser();

                FirePropertyChanged(this, "User");
            }
        }
    }
}