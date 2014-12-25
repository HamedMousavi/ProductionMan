using System;
using System.Windows;
using System.Windows.Input;

namespace ProductionMan.Desktop.Commands
{
    public class CloseWindowCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = parameter as Window;
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
