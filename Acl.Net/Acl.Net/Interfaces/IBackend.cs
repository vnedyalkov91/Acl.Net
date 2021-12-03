using Acl.Net.Entities;

namespace Acl.Net.Interfaces
{
    public interface IBackend
    {
        Role CreateRole(Role role);
        Role UpdateRole(Role role);
        void DeleteRole(Role role);
        Role GetRole(string roleName);
        void DeleteResource(string resourceName);
        void DeletePermission(string permissionName);
        Role AddRoleParents(Role role, string[] parents);
        Role RemoveRoleParents(Role role, string[] parents);
        User GetUser(string userId);
        User[] GetUsers(string roleName);
        User CreateUser(User user);
        User UpdateUser(User user);
    }
}
