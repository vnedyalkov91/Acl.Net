using Acl.Net.Entities;
using Acl.Net.Interfaces;
using System;
using System.Linq;

namespace Acl.Net.Backends
{
    public abstract class Backend: IBackend
    {
        public Role AddRoleParents(Role role, string[] parents)
        {
            role.Parents = Utils.RemoveDuplicates<string>(
                    Utils.JoinArray<string>(role.Parents, parents)
                );

            return role;
        }

        public Role RemoveRoleParents(Role role, string[] parents)
        {
            role.Parents = role.Parents.Where(
                    p => !Array.Exists(parents, pe => pe == p)
                ).ToArray();

            return role;
        }

        public abstract void DeletePermission(string permissionName);
        public abstract void DeleteResource(string resourceName);
        public abstract void DeleteRole(Role role);
        public abstract Role GetRole(string roleName);
        public abstract User GetUser(string userId);
        public abstract Role CreateRole(Role role);
        public abstract User CreateUser(User user);
        public abstract Role UpdateRole(Role role);
        public abstract User UpdateUser(User user);
        public abstract User[] GetUsers(string roleName);
    }
}
