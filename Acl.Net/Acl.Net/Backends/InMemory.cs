using Acl.Net.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acl.Net.Backends
{
    public class InMemory : Backend
    {
        private List<Role> _roleStorage;
        private List<User> _userStorage;

        public InMemory()
        {
            this._roleStorage = new List<Role>();
            this._userStorage = new List<User>();
        }

        public override Role CreateRole(Role role)
        {
            _roleStorage.Add(role);

            return GetRole(role.Name);
        }

        public override User CreateUser(User user)
        {
            _userStorage.Add(user);

            return GetUser(user.UserId);
        }

        public override void DeletePermission(string permissionName)
        {
            Role[] roles = _roleStorage.FindAll(
                r => r.Resources.FirstOrDefault(
                    res => res.Permissions.Contains(permissionName)) == null ? false : true)
                .ToArray();

            foreach (Role role in roles)
            {
                for (int i = 0; i < role.Resources.Length; i++)
                {
                    role.Resources[i].Permissions = role.Resources[i]
                        .Permissions.Where(p => p != permissionName).ToArray();
                }

                UpdateRole(role);
            }
        }

        public override void DeleteResource(string resourceName)
        {
            Role[] roles = _roleStorage.FindAll(
                r => r.Resources.FirstOrDefault(
                    res => res.Name == resourceName) == null ? false : true)
                .ToArray();

            foreach (Role role in roles)
            {
                role.Resources = role.Resources.Where(res => res.Name != resourceName).ToArray();
                UpdateRole(role);
            }
        }

        public override void DeleteRole(Role role)
        {
            _roleStorage.Remove(_roleStorage.Find(r => r.Name == role.Name));

            Role[] roles = _roleStorage.FindAll(r => r.Parents.Contains(role.Name)).ToArray();
            foreach (Role r in roles)
            {
                RemoveRoleParents(r, new string[] { role.Name });
            }

            User[] users = _userStorage.FindAll(u => u.RoleNames.Contains(role.Name)).ToArray();
            foreach (User user in users)
            {
                user.RoleNames = user.RoleNames.Where(r => r != role.Name).ToArray();
                UpdateUser(user);
            }

        }

        public override Role GetRole(string roleName)
        {
            Role targetRole = _roleStorage.Find(r => r.Name == roleName);

            return targetRole;
        }

        public override User GetUser(string userId)
        {
            User targetUser = _userStorage.Find(u => u.UserId == userId);

            return targetUser;
        }

        public override User[] GetUsers(string roleName)
        {
            return _userStorage
                .FindAll(u => u.RoleNames.Contains(roleName)).ToArray();
        }

        public override Role UpdateRole(Role role)
        {
            int index = _roleStorage.FindIndex(r => r.Name == role.Name);

            if (index == -1)
                return null;

            _roleStorage[index].Parents = role.Parents;
            _roleStorage[index].Resources = role.Resources;

            return GetRole(role.Name);
        }

        public override User UpdateUser(User user)
        {
            int index = _userStorage.FindIndex(u => u.UserId == user.UserId);

            if (index == -1)
                return null;

            _userStorage[index].RoleNames = Utils.RemoveDuplicates<string>(user.RoleNames);

            return GetUser(user.UserId);
        }
    }
}
