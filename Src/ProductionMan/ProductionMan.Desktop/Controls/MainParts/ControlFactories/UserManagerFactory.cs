using ProductionMan.Desktop.Controls.MainParts.ContentManagement;
using ProductionMan.Desktop.Factories;

namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public class UserManagerFactory : BaseControlFactory<GenericListManager>
    {

        protected override TabItemViewModel CreateTabViewModel(ViewModelFactory viewModelFactory)
        {
            return viewModelFactory.CreateUserManagerTabItemViewModel();
        }


        protected override object CreateContentViewModel(ViewModelFactory viewModelFactory)
        {
            return viewModelFactory.CreateUserManagerViewModel();
        }
    }
}
