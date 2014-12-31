using ProductionMan.Desktop.Controls.MainParts.ContentManagement;
using ProductionMan.Desktop.Factories;


namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public class PermissionManagerFactory : BaseControlFactory<GenericListManager>
    {

        protected override TabItemViewModel CreateTabViewModel(ViewModelFactory viewModelFactory)
        {
            return viewModelFactory.CreatePermissionManagerTabItemViewModel();
        }


        protected override object CreateContentViewModel(ViewModelFactory viewModelFactory)
        {
            return viewModelFactory.CreatePermissionManagerViewModel();
        }
    }
}
