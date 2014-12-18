using ProductionMan.Data.MsAdo;
using ProductionMan.Data.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ProductionMan.Data
{

    public class MembershipRepository
    {

        private readonly IUnitOfWork _context;


        public MembershipRepository(IUnitOfWork context)
        {
            _context = context;
        }


        public IEnumerable<User> GetUsers(string filter)
        {
            var list = new List<User>();

            _context.CreateCommand(
                false,
                CommandType.Text,
                "SELECT [UserId], [DisplayName],[Culture] FROM Users",
                null
                );

            _context.Execute(
                reader =>
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            Id = AdoConverter.Read<Int64>(reader, "UserId", -1),
                            Name = AdoConverter.Read(reader, "DisplayName", string.Empty),
                            Culture = AdoConverter.Read(reader, "Culture", string.Empty)
                        };

                        list.Add(user);

                    }
                });

            return list;
        }


        public User Find(string username, string password)
        {
            User user = null;

            _context.CreateCommand(
                false,
                CommandType.Text,
                "SELECT [UserId], [DisplayName],[Culture] FROM Users WHERE [Username]=@username AND [Password]=@password",
                new List<SqlParameter>
                {
                    new SqlParameter("@username", username),
                    new SqlParameter("@password", password)
                }
                );

            _context.Execute(
                reader =>
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            Id = AdoConverter.Read<Int64>(reader, "UserId", -1),
                            Name = AdoConverter.Read(reader, "DisplayName", string.Empty),
                            Culture = AdoConverter.Read(reader, "Culture", string.Empty)
                        };

                    }
                });

            return user;
        }
    }
}