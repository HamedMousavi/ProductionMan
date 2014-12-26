using System.Collections.Generic;
using System.Windows.Controls;

namespace ProductionMan.Desktop.Controls
{

    public class BaseContentSelector<T>
    {

        protected readonly Dictionary<T, UserControl> Registry;


        protected BaseContentSelector()
        {
            Registry = new Dictionary<T, UserControl>();
        }


        protected void AddContent(T state, UserControl content)
        {
            if (Registry.ContainsKey(state))
            {
                Registry[state] = content;
            }
            else
            {
                Registry.Add(state, content);
            }
        }


        public UserControl Select(T index)
        {
            return Registry[index];
        }
    }
}
