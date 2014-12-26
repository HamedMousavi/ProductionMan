using System;
using System.Windows.Controls;
using ProductionMan.Desktop.Controls.MainParts.ContentManagement;
using ProductionMan.Desktop.Factories;

namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public abstract class BaseControlFactory<T> : IControlFactory where T : class
    {
        private object _viewModel;
        private TabItemViewModel _tabItem;


        public virtual void CreateViewModels(ViewModelFactory viewModelFactory)
        {
            _viewModel = CreateContentViewModel(viewModelFactory);
            _tabItem = CreateTabViewModel(viewModelFactory);
        }


        public UserControl CreateUserControl()
        {
            var control = Activator.CreateInstance<T>() as UserControl;
            if (control != null)
            {
                control.DataContext = _viewModel;
            }

            return control;
        }


        public TabItemViewModel CreateTabItemViewModel()
        {
            return _tabItem;
        }


        protected abstract TabItemViewModel CreateTabViewModel(ViewModelFactory viewModelFactory);


        protected abstract object CreateContentViewModel(ViewModelFactory viewModelFactory);
    }
}
