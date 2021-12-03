using Acl.Net.Entities;
using Acl.Net.Interfaces;

namespace Acl.Net.Backends
{
    public abstract class Backend: IBackend
    {
        private readonly string _prefix;
        private readonly string _databaseName;

        public Backend(string databaseName, string prefix = "acl")
        {
            this._prefix = prefix;
            this._databaseName = databaseName;
        }

        public abstract Role AddRoleParents(Role role, string[] parents);
        public abstract Role RemoveRoleParents(Role role, string[] parents);
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
