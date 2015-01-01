using ProductionMan.Desktop.Controls.MainParts.ContentManagement;
using ProductionMan.Desktop.Factories;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public class RoleManagerFactory : BaseControlFactory<RoleManagerWindow>
    {

        protected override TabItemViewModel CreateTabViewModel(ViewModelFactory viewModelFactory)
        {
            return viewModelFactory.CreateRoleManagerTabItemViewModel();
        }


        protected override object CreateContentViewModel(ViewModelFactory viewModelFactory)
        {
            return viewModelFactory.CreateRoleManagerViewModel();
        }
    }
}
