namespace ProductionMan.Desktop.Controls.MainParts.ControlFactories
{

    public class UserManagerViewModelFactory : BaseControlFactory<GenericListManager>
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
