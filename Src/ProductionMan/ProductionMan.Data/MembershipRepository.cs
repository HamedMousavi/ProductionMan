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
                "SELECT [Users].[UserId], [Users].[DisplayName], [Users].[Culture], [Roles].[RoleId], [Roles].[RoleName] FROM Users, Roles WHERE [Users].[IsEnabled]=1 AND [Roles].[RoleId]=[Users].[RoleId]",
                null
                );

            _context.Execute(
                reader =>
                {
                    while (reader.Read())
                    {
                        list.Add(MapUser(reader));
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
                "SELECT [Users].[UserId], [Users].[DisplayName], [Users].[Culture], [Roles].[RoleId], [Roles].[RoleName] FROM Users, Roles WHERE [Username]=@username AND [Password]=@password AND [Users].[IsEnabled]=1 AND [Roles].[RoleId]=[Users].[RoleId]",
                new List<SqlParameter>
                {
                    new SqlParameter("@username", username),
                    new SqlParameter("@password", password)
                }
                );

            _context.Execute(reader =>
            {
                if (reader.Read())
                {
                    user = MapUser(reader);
                }
            });

            return user;
        }


        private User MapUser(IDataReader reader)
        {
            return new User
            {
                UserId = AdoConverter.Read<Int64>(reader, "UserId", -1),
                DisplayName = AdoConverter.Read(reader, "DisplayName", string.Empty),
                Culture = AdoConverter.Read(reader, "Culture", string.Empty),
                Role = new Role
                {
                    RoleName = AdoConverter.Read(reader, "RoleName", string.Empty),
                    RoleId = AdoConverter.Read(reader, "RoleId", -1)
                }
            };
        }

        public void Insert(User newUser)
        {
            _context.CreateCommand(
                false,
                CommandType.Text,
                "INSERT INTO [USERS] ([Username], [Password], [DisplayName], [Culture], [RoleId], [IsEnabled]) VALUES (@Username, @Password, @DisplayName, @Culture, @RoleId, 1); SELECT SCOPE_IDENTITY();",
                new List<SqlParameter>
                {
                    new SqlParameter("@username", newUser.Username),
                    new SqlParameter("@password", newUser.Password),
                    new SqlParameter("@DisplayName", newUser.DisplayName),
                    new SqlParameter("@Culture", newUser.Culture),
                    new SqlParameter("@RoleId", newUser.Role.RoleId)
                }
                );

            _context.Execute(reader =>
            {
                if (reader.Read())
                {
                    var user = MapUser(reader);
                    newUser.UserId = user.UserId;
                }
            });
        }
    }
}