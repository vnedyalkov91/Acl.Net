using Acl.Net.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using Role = Acl.Net.Entities.Role;

namespace Acl.Net.Backends
{
    public class Redisdb : Backend
    {
        private readonly IDatabase database;
        private readonly IServer server;
        private readonly string prefixRoles;
        private readonly string prefixUsers;
        private readonly int databaseNumber;

        public Redisdb(ConnectionMultiplexer redis, int databaseNumber = 0, string prefix = "acl")
        {
            this.databaseNumber = databaseNumber;
            database = redis.GetDatabase(databaseNumber);
            var endpoint = redis.GetEndPoints(true);
            server = redis.GetServer(redis.GetEndPoints(true)[0]);
            this.prefixRoles = prefix + "_roles";
            this.prefixUsers = prefix + "_users";
        }

        public override Role CreateRole(Role role)
        {
            database.HashSet(
                prefixRoles + ':' + role.Name, 
                (   
                    new RedisRole()
                    {
                        Name = role.Name,
                        Resources = role.Resources,
                        Parents = role.Parents
                    }
                ).Serialize()
            );

            return role;
        }

        public override User CreateUser(User user)
        {
            database.HashSet(
                prefixUsers + ':' + user.UserId,
                (
                    new RedisUser()
                    {
                        UserId = user.UserId,
                        RoleNames = user.RoleNames,
                    }
                ).Serialize()
            );

            return user;
        }

        public override void DeletePermission(string permissionName)
        {
            var roleKeys = server.Keys(databaseNumber, $"*{prefixRoles}*");

            foreach (RedisKey key in roleKeys)
            {
                Role fetchedRole = GetRoleDirect(key.ToString());

                if (fetchedRole.Resources.FirstOrDefault(res => res.Permissions.Contains(permissionName)) == null ? false : true)
                {
                    for (int i = 0; i < fetchedRole.Resources.Length; i++)
                    {
                        fetchedRole.Resources[i].Permissions = fetchedRole.Resources[i]
                            .Permissions.Where(p => p != permissionName).ToArray();
                    }

                    UpdateRole(fetchedRole);
                }
            }
        }

        public override void DeleteResource(string resourceName)
        {
            var roleKeys = server.Keys(databaseNumber, $"*{prefixRoles}*");

            foreach (RedisKey key in roleKeys)
            {
                Role fetchedRole = GetRoleDirect(key.ToString());

                if  (fetchedRole.Resources.FirstOrDefault(res => res.Name == resourceName) == null ? false : true)
                {
                    fetchedRole.Resources = fetchedRole.Resources.Where(res => res.Name != resourceName).ToArray();
                    UpdateRole(fetchedRole);
                }
            }
        }

        public override void DeleteRole(Role role)
        {
            database.KeyDelete(prefixRoles + ':' + role.Name);

            var roleKeys = server.Keys(databaseNumber, $"*{prefixRoles}*");
            var userKeys = server.Keys(databaseNumber, $"*{prefixUsers}*");

            List<Role> roles = new List<Role>();

            foreach (RedisKey key in roleKeys)
            {
                Role fetchedRole = GetRoleDirect(key.ToString());

                if (fetchedRole.Parents.Contains(role.Name))
                {
                    roles.Add(fetchedRole);
                }
            }

            foreach (Role r in roles)
            {
                RemoveRoleParents(r, new string[] { role.Name });
            }

            foreach (RedisKey key in userKeys)
            {
                User user = GetUserDirect(key.ToString());

                user.RoleNames = user.RoleNames.Where(r => r != role.Name).ToArray();
                UpdateUser(user);
            }
        }

        public override Role GetRole(string roleName)
        {
            return GetRoleDirect(prefixRoles + ':' + roleName);
        }

        public override User GetUser(string userId)
        {
            return GetUserDirect(prefixUsers + ':' + userId);
        }

        public override User[] GetUsers(string roleName)
        {
            List<User> usersList = new List<User>();

            var users = server.Keys(databaseNumber, $"*{prefixUsers}*");

            foreach (RedisKey key in users)
            {
                User user = GetUserDirect(key.ToString());

                if (user.RoleNames.Contains(roleName))
                {
                    usersList.Add(user);
                }
            }

            return usersList.ToArray();
        }

        public override Role UpdateRole(Role role)
        {
            return CreateRole(role);
        }

        public override User UpdateUser(User user)
        {
            return CreateUser(user);
        }

        private Role GetRoleDirect(string roleName)
        {
            HashEntry[] hashEntries = database.HashGetAll(roleName);

            if (hashEntries.Length == 0)
            {
                return null;
            }

            RedisRole role = new RedisRole();

            foreach (HashEntry hashEntry in hashEntries)
            {
                switch (hashEntry.Name.ToString())
                {
                    case "Name":
                        role.SetDeserializeName(hashEntry.Value.ToString());
                        break;
                    case "Parents":
                        role.SetDeserializeParents(hashEntry.Value.ToString());
                        break;
                    case "Resources":
                        role.SetDeserializeResources(hashEntry.Value.ToString());
                        break;
                }
            }

            return role;
        }

        private User GetUserDirect(string userId)
        {
            HashEntry[] hashEntries = database.HashGetAll(userId);

            if (hashEntries.Length == 0)
            {
                return null;
            }

            RedisUser user = new RedisUser();

            foreach (HashEntry hashEntry in hashEntries)
            {
                switch (hashEntry.Name.ToString())
                {
                    case "UserId":
                        user.SetDeserializeUserId(hashEntry.Value.ToString());
                        break;
                    case "RoleNames":
                        user.SetDeserializeRoleNames(hashEntry.Value.ToString());
                        break;
                }
            }

            return user;
        }
    }
}
