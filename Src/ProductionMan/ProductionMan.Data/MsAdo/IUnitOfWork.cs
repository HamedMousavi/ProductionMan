using System;
using System.Collections.Generic;
using System.Data;

namespace ProductionMan.Data.MsAdo
{
    public interface IUnitOfWork : IDisposable
    {
        //void SaveChanges();
        void Execute(Action<IDataReader> func);
        object ExecuteScalar();
        void CreateCommand(bool transactional, CommandType commandType, string query, IReadOnlyCollection<IDataParameter> parameters);
    }
}
