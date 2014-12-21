using System.Collections.Generic;
using System.Windows.Controls;


namespace ProductionMan.Desktop.Controls
{

    public class BaseContentSelector<T>
    {

        private readonly Dictionary<T, UserControl> _registry;


        public BaseContentSelector()
        {
            _registry = new Dictionary<T, UserControl>();
        }


        public void AddContent(T state, UserControl content)
        {
            if (_registry.ContainsKey(state))
            {
                _registry[state] = content;
            }
            else
            {
                _registry.Add(state, content);
            }
        }


        public UserControl Select(T index)
        {
            return _registry[index];
        }
    }
}
