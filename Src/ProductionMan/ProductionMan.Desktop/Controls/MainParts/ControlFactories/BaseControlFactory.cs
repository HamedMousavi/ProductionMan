using System;
using System.Windows.Controls;
using ProductionMan.Desktop.Factories;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public abstract class BaseControlFactory<T> : IControlFactory where T : class
    {

        protected object ViewModel;
        protected TabItemViewModel TabItem;


        public virtual void CreateViewModels(ViewModelFactory viewModelFactory)
        {
            ViewModel = CreateContentViewModel(viewModelFactory);
            TabItem = CreateTabViewModel(viewModelFactory);
        }


        public UserControl CreateUserControl()
        {
            var control = Activator.CreateInstance<T>() as UserControl;
            if (control != null)
            {
                control.DataContext = ViewModel;
            }

            return control;
        }


        public TabItemViewModel CreateTabItemViewModel()
        {
            return TabItem;
        }


        protected abstract TabItemViewModel CreateTabViewModel(ViewModelFactory viewModelFactory);


        protected abstract object CreateContentViewModel(ViewModelFactory viewModelFactory);
    }
}
