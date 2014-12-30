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

        public void Insert(User newUser)
        {
            _context.CreateCommand(
                false,
                CommandType.Text,
                "INSERT INTO [USERS] ([Username], [Password], [DisplayName], [Culture], [RoleId], [IsEnabled]) OUTPUT INSERTED.UserId VALUES (@Username, @Password, @DisplayName, @Culture, @RoleId, 1);",
                new List<SqlParameter>
                {
                    new SqlParameter("@username", newUser.Username),
                    new SqlParameter("@password", newUser.Password),
                    new SqlParameter("@DisplayName", newUser.DisplayName),
                    new SqlParameter("@Culture", newUser.Culture),
                    new SqlParameter("@RoleId", newUser.Role.RoleId)
                }
                );

            var output = _context.ExecuteScalar();
            newUser.UserId = AdoConverter.ReadResult<Int64>(output, -1); //output.Value as int? ?? default(int);
        }


        public void Update(User user)
        {
            _context.CreateCommand(
                false,
                CommandType.Text,
                "UPDATE [USERS] SET [Username]=@Username, [Password]=@Password, [DisplayName]=@DisplayName, [Culture]=@Culture, [RoleId]=@RoleId WHERE [UserId]=@UserId;",
                new List<SqlParameter>
                {
                    new SqlParameter("@username", user.Username),
                    new SqlParameter("@password", user.Password),
                    new SqlParameter("@DisplayName", user.DisplayName),
                    new SqlParameter("@Culture", user.Culture),
                    new SqlParameter("@RoleId", user.Role.RoleId),
                    new SqlParameter("@UserId", user.UserId),
                }
                );

            _context.ExecuteScalar();
        }


        public void DeleteUser(long id)
        {
            _context.CreateCommand(
                false,
                CommandType.Text,
                "UPDATE [USERS] SET [IsEnabled] = 0 WHERE [UserId] = @UserId",
                new List<SqlParameter>
                {
                    new SqlParameter("@UserId", id)
                }
                );

            _context.ExecuteScalar();
        }


        public IEnumerable<Role> GetRoles(string filter)
        {
            var list = new List<Role>();

            _context.CreateCommand(
                false,
                CommandType.Text,
                "SELECT [RoleId],[RoleName] FROM Roles WHERE [IsEnabled]=1",
                null
                );

            _context.Execute(
                reader =>
                {
                    while (reader.Read())
                    {
                        list.Add(MapRole(reader));
                    }
                });

            return list;
        }


        public List<Permission> GetPermissions(User user)
        {
            var list = new List<Permission>();

            _context.CreateCommand(
                false,
                CommandType.Text,
                "SELECT [Permissions].[PermissionId],[PermissionResource],[PermissionTypeId] FROM [Permissions], [RolePermissions] WHERE [RoleId] = @RoleId AND [RolePermissions].[PermissionId] = [Permissions].[PermissionId]",
                new List<SqlParameter>
                {
                    new SqlParameter("@RoleId", user.Role.RoleId)
                }
                );

            _context.Execute(
                reader =>
                {
                    while (reader.Read())
                    {
                        list.Add(MapPermission(reader));
                    }
                });

            return list;
        }


        private Permission MapPermission(IDataReader reader)
        {
            return new Permission
            {
                PermissionId = AdoConverter.Read(reader, "PermissionId", -1),
                ResourceName = AdoConverter.Read(reader, "PermissionResource", string.Empty),
                Operation = (Permission.OperationType)AdoConverter.Read(reader, "PermissionTypeId", 0)
            };
        }
        

        private Role MapRole(IDataReader reader)
        {
            return new Role
            {
                RoleId = AdoConverter.Read(reader, "RoleId", -1),
                RoleName = AdoConverter.Read(reader, "RoleName", string.Empty)
            };
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
    }
}