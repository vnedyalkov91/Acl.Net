using Acl.Net.Entities;

namespace Acl.Net.Interfaces
{
    public interface IAcl
    {
        Role[] Allow(Role[] roles);
        Role Allow(Role role);
        Role Allow(string roleName, Resource[] resources);

        void RemoveAllow(string roleName);
        Role RemoveAllow(string roleName, string[] resources);
        Role RemoveAllow(string roleName, string[] resources, string[] permissions);

        void RemoveResource(string resourceName);
        void RemovePermission(string permissionName);

        Role AddRoleParent(string roleName, string parentName);
        Role AddRoleParents(string roleName, string[] parents);

        Role RemoveRoleParent(string roleName, string parentName);
        Role RemoveRoleParents(string roleName, string[] parents);

        void AddUserRole(int userId, string roleName);
        void AddUserRole(string userId, string roleName);
        void AddUserRole(int userId, Role role);
        void AddUserRole(string userId, Role role);

        void AddUserRoles(int userId, string[] roleNames);
        void AddUserRoles(string userId, string[] roleNames);
        void AddUserRoles(int userId, Role[] roles);
        void AddUserRoles(string userId, Role[] roles);

        void RemoveUserRole(string userId, string roleName);
        void RemoveUserRole(int userId, string roleName);

        Role[] UserRoles(string userId);
        Role[] UserRoles(int userId);

        User[] RoleUsers(string roleName);
        User[] RoleUsers(Role role);

        bool UserHasRole(string userId, string roleName);
        bool UserHasRole(string userId, Role role);
        bool UserHasRole(int userId, string roleName);
        bool UserHasRole(int userId, Role role);

        string[] AllowedPermissions(string userId, string resource);
        string[] AllowedPermissions(int userId, string resource);

        bool IsAllowed(string userId, string resource, string permission);
        bool IsAllowed(int userId, string resource, string permission);
        bool IsAllowed(string userId, Resource resource);
        bool IsAllowed(int userId, Resource resource);

        bool IsRoleAllowed(string roleName, string resource, string permission);
        bool IsRoleAllowed(string roleName, Resource resource);
    }
}
