﻿using ProductionMan.Data.MsAdo;
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


        public User FindUser(string username, string password)
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


        public void InsertUser(User newUser)
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


        public void UpdateUser(User user)
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

            foreach (var role in list)
            {
                role.Permissions = GetPermissionsByRoleId(role.RoleId);
            }

            return list;
        }


        public void InsertRole(Role role)
        {
            _context.CreateCommand(
                false,
                CommandType.Text,
                "INSERT INTO [Roles] ([RoleName],[IsEnabled]) OUTPUT INSERTED.RoleId VALUES (@RoleName, 1);",
                new List<SqlParameter>
                {
                    new SqlParameter("@RoleName", role.RoleName)
                }
                );

            var output = _context.ExecuteScalar();
            role.RoleId = AdoConverter.ReadResult(output, -1);
        }


        public void UpdateRole(Role role)
        {
            _context.CreateCommand(
                false,
                CommandType.Text,
                "UPDATE [Roles] SET [RoleName]=@RoleName WHERE [RoleId]=@RoleId;",
                new List<SqlParameter>
                {
                    new SqlParameter("@RoleName", role.RoleName),
                    new SqlParameter("@RoleId", role.RoleId)
                }
                );

            _context.ExecuteScalar();
        }


        public void DeleteRole(long id)
        {
            _context.CreateCommand(
                false,
                CommandType.Text,
                "UPDATE [Roles] SET [IsEnabled] = 0 WHERE [RoleId] = @RoleId",
                new List<SqlParameter>
                {
                    new SqlParameter("@RoleId", id)
                }
                );

            _context.ExecuteScalar();
        }


        public IEnumerable<Permission> GetPermissions(string filter)
        {
            var list = new List<Permission>();

            _context.CreateCommand(
                false,
                CommandType.Text,
                "SELECT [PermissionId],[PermissionResource],[PermissionTypeId],[Description] FROM [Permissions] ORDER BY [PermissionResource]",
                null);

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


        public IEnumerable<Permission> GetPermissionsByRoleId(int roleId)
        {
            var list = new List<Permission>();

            _context.CreateCommand(
                false,
                CommandType.Text,
                "SELECT [PermissionId], [PermissionResource], [PermissionTypeId],[Description] FROM [Permissions] WHERE [PermissionId] IN (SELECT [PermissionId] FROM [RolePermissions] WHERE [RoleId]=@RoleId);",
                new List<SqlParameter>
                {
                    new SqlParameter("@RoleId", roleId)
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


        public void AssignPermissionToRole(int roleId, int permissionId)
        {
            _context.CreateCommand(
                false,
                CommandType.Text,
                "INSERT INTO [RolePermissions] ([RoleId],[PermissionId]) VALUES (@RoleId,@PermissionId);",
                new List<SqlParameter>
                {
                    new SqlParameter("@RoleId", roleId),
                    new SqlParameter("@PermissionId", permissionId)
                }
                );

            _context.ExecuteScalar();
        }


        public void RetractPermissionFromRole(int roleId, int permissionId)
        {
            _context.CreateCommand(
                false,
                CommandType.Text,
                "DELETE FROM [RolePermissions] WHERE [RoleId]= @RoleId AND [PermissionId] = @PermissionId;",
                new List<SqlParameter>
                {
                    new SqlParameter("@RoleId", roleId),
                    new SqlParameter("@PermissionId", permissionId)
                }
                );

            _context.ExecuteScalar();
        }


        public Role RoleGetById(int roleId)
        {
            Role role = null;

            _context.CreateCommand(
                false,
                CommandType.Text,
                "SELECT [RoleId],[RoleName] FROM [Roles] WHERE [IsEnabled]=1 AND [RoleId]=@RoleId",
                new List<SqlParameter>
                {
                    new SqlParameter("@RoleId", roleId)
                }
                );

            _context.Execute(
                reader =>
                {
                    if (reader.Read())
                    {
                        role = MapRole(reader);
                    }
                });

            if (role != null) role.Permissions = GetPermissionsByRoleId(role.RoleId);

            return role;
        }


        private Permission MapPermission(IDataReader reader)
        {
            return new Permission
            {
                PermissionId = AdoConverter.Read(reader, "PermissionId", -1),
                ResourceName = AdoConverter.Read(reader, "PermissionResource", string.Empty),
                Description = AdoConverter.Read(reader, "Description", string.Empty),
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