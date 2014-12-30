using ProductionMan.Desktop.Commands;
using ProductionMan.Desktop.Repositories;
using ProductionMan.Domain.Security;
using ProductionMan.Web.Api.Common.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;


namespace ProductionMan.Desktop.Factories
{

    public class CommandFactory
    {

        private readonly IUserWindowManager _windowManager;


        private enum CommandKey
        {
            Exit,
            Login,
            RetryLogin
        }

        private readonly Dictionary<CommandKey, ICommand> _commands;
        private readonly MembershipRepository _membershipRepository;


        public CommandFactory(WindowManager windowManager, MembershipRepository membershipRepository)
        {
            _windowManager = windowManager;
            _membershipRepository = membershipRepository;
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


        internal ICommand AddUserWindowCommand()
        {
            return new VisualAddUserCommand(_windowManager);
        }


        internal ICommand DeleteUserConfirmWindowCommand()
        {
            return new VisualDeleteUserCommand(_windowManager);
        }


        public ICommand ToggleUserCommand()
        {
            return new EnableUserCommand();
        }


        internal ICommand EditUserWindowCommand()
        {
            return new VisualEditUserCommand(_windowManager);
        }

        internal ICommand NavigateToWebsiteCommand()
        {
            return new NavigateToWebsiteCommand();
        }

        internal ICommand UpdateUserCommand()
        {
            return new UpdateUserCommand(_membershipRepository);
        }

        internal ICommand CloseWindowCommand()
        {
            return new CloseWindowCommand();
        }

        internal ICommand DeleteUserCommand(UserRead user)
        {
            return new DeleteUserCommand(_membershipRepository, user);
        }

        internal ICommand CreateUserCommand()
        {
            return new CreateUserCommand(_membershipRepository);
        }
    }
}
