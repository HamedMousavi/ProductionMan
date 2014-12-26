using ProductionMan.Desktop.Controls.MainParts.ControlFactories;


namespace ProductionMan.Desktop.Controls.MainParts
{

    public class MainWindowFactory
    {

        internal IControlFactory CreateAboutUsFactory()
        {
            return new AboutUsFactory();
        }


        internal IControlFactory CreateSettingsManagerFactory()
        {
            return new SettingsManagerFactory();
        }

        internal IControlFactory CreateUsersFactory()
        {
            return new UserManagerFactory();
        }

        internal IControlFactory CreateFPermissionsactory()
        {
            return null;
        }

        internal IControlFactory CreateMaterialsFactory()
        {
            return null;
        }

        internal IControlFactory CreateProcessesFactory()
        {
            return null;
        }

        internal IControlFactory CreateStoresFactory()
        {
            return null;
        }
    }
}