﻿using System;
using System.Windows.Input;
using ProductionMan.Desktop.Controls.Authentication;
using ProductionMan.Domain.Security;

namespace ProductionMan.Desktop.Commands
{

    public class LoginCommand : ICommand
    {


        private readonly User _user;
        public event EventHandler CanExecuteChanged;


        public LoginCommand(User user)
        {
            _user = user;
        }


        public bool CanExecute(object parameter)
        {
            return (_user != null && _user.LoginStatus != User.LoginStates.SignedIn);
        }


        public async void Execute(object parameter)
        {
            var credentials = (LoginViewModel)parameter;
            if (credentials == null)
            {
                await _user.LoginAsync(null, null);
            }
            else
            {
                await _user.LoginAsync(credentials.Username, credentials.Password);
            }
        }
    }
}
