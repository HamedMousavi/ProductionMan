namespace ProductionMan.Data.MsAdo
{

    public class UnitOfWorkFactory
    {

        public static IUnitOfWork Create()
        {
            return new AdoNetUnitOfWork(Settings.ConnectionString);
        }
    }
}
