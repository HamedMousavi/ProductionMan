using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ProductionMan.Data.MsAdo
{

    public class AdoNetUnitOfWork : IUnitOfWork
    {

        private readonly string _connectionString;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IDbCommand _command;


        public AdoNetUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }


        public void CreateCommand(bool transactional, CommandType commandType, string query, IEnumerable<IDataParameter> parameters)
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                _transaction = transactional ? _connection.BeginTransaction() : null;
            }

            _command = _connection.CreateCommand();
            _command.CommandType = commandType;
            _command.CommandText = query;
            if (_transaction != null) _command.Transaction = _transaction;

            if (parameters != null)
            {
                foreach (var parameter in parameters) { if (parameter.Value == null) { parameter.Value = DBNull.Value; } }
                foreach (var parameter in parameters) _command.Parameters.Add(parameter);
            }
        }


        public void Execute(Action<IDataReader> func)
        {
            try
            {
                if (_transaction == null)
                {
                    if (_command == null)
                    {
                        CloseTransaction();
                        CloseConnection();
                        throw new InvalidOperationException("No command to execute. Call CreateCommand first!");
                    }

                    func(_command.ExecuteReader());
                }
                else
                {
                    _transaction.Commit();
                    func(_command.ExecuteReader());
                }

                _transaction = null;
                CloseConnection();
            }
            catch (Exception)
            {
                try
                {
                    CloseTransaction();
                    CloseConnection();
                }
// ReSharper disable once EmptyGeneralCatchClause
                catch{}

                throw;
            }

        }


        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction have already been commited. Check your transaction handling.");

            _transaction.Commit();
            _transaction = null;

            CloseConnection();
        }


        public void Dispose()
        {
            CloseTransaction();
            CloseConnection();
        }


        private void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }


        private void CloseConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
