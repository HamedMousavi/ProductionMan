﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using ProductionMan.Desktop.Commands;
using ProductionMan.Domain.Security;
using ProductionMan.Domain.WebServices;


namespace ProductionMan.Desktop
{

    public class CommandFactory
    {

        private enum CommandKey
        {
            Exit,
            Login,
            RetryLogin
        }

        private readonly Dictionary<CommandKey, ICommand> _commands;


        public CommandFactory()
        {
            _commands = new Dictionary<CommandKey, ICommand>();
        }


        private ICommand CreateCommand<TCommandType>(CommandKey commandKey) where TCommandType : ICommand
        {
            if (!_commands.ContainsKey(commandKey))
            {
                _commands.Add(commandKey, Activator.CreateInstance<TCommandType>());
            }

            return _commands[commandKey];
        }


        private ICommand CreateCommand(CommandKey commandKey, Type type, object[] parameters)
        {
            if (!_commands.ContainsKey(commandKey))
            {
                _commands.Add(commandKey, (ICommand)Activator.CreateInstance(type, parameters));
            }

            return _commands[commandKey];
        }


        internal ICommand CreateLoginCommand(User user)
        {
            return CreateCommand(CommandKey.Login, typeof (LoginCommand), new object[] {user});
        }


        internal ICommand CreateExitCommand()
        {
            return CreateCommand<ExitCommand>(CommandKey.Exit);
        }


        public ICommand CreateRetryLoginCommand(User user)
        {
            return CreateCommand(CommandKey.RetryLogin, typeof(LoginRetryCommand), new object[] { user });
        }


        internal ICommand CreateAddUserCommand(IUserWindowManager windowManager, Membership membershipService)
        {
            return new VisualAddUserCommands(windowManager, membershipService);
        }


        internal ICommand CreateDeleteUserCommand(IUserWindowManager windowManager, Membership membershipService)
        {
            return new VisualDeleteUserCommand(windowManager, membershipService);
        }


        public ICommand ToggleUserCommand()
        {
            return new EnableUserCommand();
        }


        internal ICommand CreateEditUserCommand(IUserWindowManager windowManager, Membership membershipService)
        {
            return new VisualEditUserCommand(windowManager, membershipService);
        }
    }
}
