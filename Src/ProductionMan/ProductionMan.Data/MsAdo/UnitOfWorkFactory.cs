namespace ProductionMan.Data.MsAdo
{

    public static class UnitOfWorkFactory
    {

        public static IUnitOfWork Create()
        {
            return new AdoNetUnitOfWork(Settings.ConnectionString);
        }
    }
}
