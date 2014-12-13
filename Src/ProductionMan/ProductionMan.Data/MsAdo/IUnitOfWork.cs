using System;
using System.Collections.Generic;
using System.Data;


namespace ProductionMan.Data.MsAdo
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        void Execute(Action<IDataReader> func);
        void CreateCommand(bool transactional, CommandType commandType, string query, IEnumerable<IDataParameter> parameters);
    }
}
