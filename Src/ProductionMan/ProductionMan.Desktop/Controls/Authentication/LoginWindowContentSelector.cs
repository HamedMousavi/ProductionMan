using System.Collections.Generic;
using System.Windows.Controls;
using ProductionMan.Domain.Security;


namespace ProductionMan.Desktop.Controls.Authentication
{

    public class LoginWindowContentSelector
    {

        private readonly Dictionary<User.LoginStates, UserControl> _registry;


        public LoginWindowContentSelector()
        {
            _registry = new Dictionary<User.LoginStates, UserControl>();
        }


        public void AddContent(User.LoginStates state, UserControl content)
        {
            if (_registry.ContainsKey(state))
            {
                _registry[state] = content;
            }
            else
            {
                _registry.Add(state,content);
            }
        }


        public UserControl Select(User.LoginStates state)
        {
            return _registry[state];
        }
    }
}
