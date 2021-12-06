using Acl.Net.Entities;

namespace Acl.Net.Interfaces
{
    public interface IAcl
    {
        /// <summary>
        /// Adds the given permissions to the given roles over the given resources.
        /// </summary>
        /// <param name="roles">An array of role to be allowed</param>
        /// <returns>Allowed Roles</returns>
        Role[] Allow(Role[] roles);

        /// <summary>
        /// Adds the given permissions to the given roles over the given resources.
        /// </summary>
        /// <param name="role">Role to be allowed</param>
        /// <returns>Allowed Role</returns>
        Role Allow(Role role);

        /// <summary>
        /// Adds the given permissions to the given roles over the given resources.
        /// </summary>
        /// <param name="roleName">Name of the role</param>
        /// <param name="resources">Resources to be allowed for this role</param>
        /// <returns>Allowed Role</returns>
        Role Allow(string roleName, Resource[] resources);

        /// <summary>
        /// Removes permissions from the given roles owned by the given role.
        /// </summary>
        /// <param name="roleName">The name of the role to be removed</param>
        void RemoveAllow(string roleName);

        /// <summary>
        /// Removes permissions from the given roles owned by the given role.
        /// </summary>
        /// <param name="roleName">The name of the role from which it will be removed</param>
        /// <param name="resources">The names of the resources to be removed</param>
        /// <returns>Updated role</returns>
        Role RemoveAllow(string roleName, string[] resources);

        /// <summary>
        /// Removes permissions from the given roles owned by the given role.
        /// </summary>
        /// <param name="roleName">The name of the role from which it will be removed</param>
        /// <param name="resources">The names of the resources to be removed</param>
        /// <param name="permissions">Permissions to be removed</param>
        /// <returns>Updated role</returns>
        Role RemoveAllow(string roleName, string[] resources, string[] permissions);

        /// <summary>
        /// Removes a resource from the system.
        /// </summary>
        /// <param name="resourceName">The resource name to be removed from the system</param>
        void RemoveResource(string resourceName);

        /// <summary>
        /// Removes a permission from the system.
        /// </summary>
        /// <param name="permissionName">The name of the permission to be removed from the system</param>
        void RemovePermission(string permissionName);

        /// <summary>
        /// Adds a parent to role.
        /// </summary>
        /// <param name="roleName">The name of the role</param>
        /// <param name="parentName">The name of the parent</param>
        /// <returns>Updated Role</returns>
        Role AddRoleParent(string roleName, string parentName);

        /// <summary>
        /// Adds parent list to role.
        /// </summary>
        /// <param name="roleName">The name of the role</param>
        /// <param name="parents">An array of parent role names</param>
        /// <returns>Updated Role</returns>
        Role AddRoleParents(string roleName, string[] parents);

        /// <summary>
        /// Remove a parent from the role.
        /// </summary>
        /// <param name="roleName">The name of the role</param>
        /// <param name="parentName">The name of the parent</param>
        /// <returns>Updated Role</returns>
        Role RemoveRoleParent(string roleName, string parentName);

        /// <summary>
        /// Remove parent list from the role.
        /// </summary>
        /// <param name="roleName">The name of the role</param>
        /// <param name="parents">An array of parent role names</param>
        /// <returns>Updated Role</returns>
        Role RemoveRoleParents(string roleName, string[] parents);

        /// <summary>
        /// Adds role to a given user id.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="roleName">Already allowed role</param>
        void AddUserRole(int userId, string roleName);

        /// <summary>
        /// Adds role to a given user id.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="roleName">Already allowed role</param>
        void AddUserRole(string userId, string roleName);

        /// <summary>
        /// Adds role to a given user id.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="role">Already allowed role or role to be allowed</param>
        void AddUserRole(int userId, Role role);

        /// <summary>
        /// Adds role to a given user id.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="role">Already allowed role or role to be allowed</param>
        void AddUserRole(string userId, Role role);

        /// <summary>
        /// Adds list of roles to a given user id.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="roleNames">An list of allowed Roles</param>
        void AddUserRoles(int userId, string[] roleNames);

        /// <summary>
        /// Adds list of roles to a given user id.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="roleNames">An list of allowed Roles</param>
        void AddUserRoles(string userId, string[] roleNames);

        /// <summary>
        /// Adds list of roles to a given user id.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="roles">Allowed roles or roles to be allowed</param>
        void AddUserRoles(int userId, Role[] roles);

        /// <summary>
        /// Adds list of roles to a given user id.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="roles">Allowed roles or roles to be allowed</param>
        void AddUserRoles(string userId, Role[] roles);

        /// <summary>
        /// Remove role from a given user.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="roleName">The name of the role to be removed</param>
        void RemoveUserRole(string userId, string roleName);

        /// <summary>
        /// Remove role from a given user.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="roleName">The name of the role to be removed</param>
        void RemoveUserRole(int userId, string roleName);

        /// <summary>
        /// Return all the roles from a given user.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <returns>Array of Roles</returns>
        Role[] UserRoles(string userId);

        /// <summary>
        /// Return all the roles from a given user.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <returns>Array of Roles</returns>
        Role[] UserRoles(int userId);

        /// <summary>
        ///  Return all the users from a given role.
        /// </summary>
        /// <param name="roleName">The name of the role</param>
        /// <returns>Array of Users</returns>
        User[] RoleUsers(string roleName);

        /// <summary>
        ///  Return all the users from a given role.
        /// </summary>
        /// <param name="role">Allowed Role</param>
        /// <returns>Array of Users</returns>
        User[] RoleUsers(Role role);

        /// <summary>
        /// Return boolean whether user has the role
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>true or false</returns>
        bool UserHasRole(string userId, string roleName);

        /// <summary>
        /// Return boolean whether user has the role
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="role">Allowed role</param>
        /// <returns>true or false</returns>
        bool UserHasRole(string userId, Role role);

        /// <summary>
        /// Return boolean whether user has the role
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="roleName">The name of the role</param>
        /// <returns>true or false</returns>
        bool UserHasRole(int userId, string roleName);

        /// <summary>
        /// Return boolean whether user has the role
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="role">Allowed role</param>
        /// <returns>true or false</returns>
        bool UserHasRole(int userId, Role role);

        /// <summary>
        /// Returns all the allowable permissions a given user have to access the given resources.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="resource">The name of the resource</param>
        /// <returns> Array of permissions </returns>
        string[] AllowedPermissions(string userId, string resource);

        /// <summary>
        /// Returns all the allowable permissions a given user have to access the given resources.
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="resource">The name of the resource</param>
        /// <returns> Array of permissions </returns>
        string[] AllowedPermissions(int userId, string resource);

        /// <summary>
        /// Checks if the given user is allowed to access the resource for the given permissions 
        /// (note: it must fulfill all the permissions).
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="resource">The name of the resource</param>
        /// <param name="permission">the name of the permission</param>
        /// <returns>true or false</returns>
        bool IsAllowed(string userId, string resource, string permission);

        /// <summary>
        /// Checks if the given user is allowed to access the resource for the given permissions 
        /// (note: it must fulfill all the permissions).
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="resource">The name of the resource</param>
        /// <param name="permission">the name of the permission</param>
        /// <returns>true or false</returns>
        bool IsAllowed(int userId, string resource, string permission);

        /// <summary>
        /// Checks if the given user is allowed to access the resource for the given permissions 
        /// (note: it must fulfill all the permissions).
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="resource">Resource with permissions</param>
        /// <returns>true or false</returns>
        bool IsAllowed(string userId, Resource resource);

        /// <summary>
        /// Checks if the given user is allowed to access the resource for the given permissions 
        /// (note: it must fulfill all the permissions).
        /// </summary>
        /// <param name="userId">User Identificator</param>
        /// <param name="resource">Resource with permissions</param>
        /// <returns>true or false</returns>
        bool IsAllowed(int userId, Resource resource);

        /// <summary>
        /// Check if a given role has a resource and permission.
        /// </summary>
        /// <param name="roleName">The name of the target role</param>
        /// <param name="resource">The name of the resource</param>
        /// <param name="permission">The name of the permission</param>
        /// <returns>true or false</returns>
        bool IsRoleAllowed(string roleName, string resource, string permission);

        /// <summary>
        /// Check if a given role has a resource and permission.
        /// </summary>
        /// <param name="roleName">The name of the target role</param>
        /// <param name="resource">The resource</param>
        /// <returns>true or false</returns>
        bool IsRoleAllowed(string roleName, Resource resource);
    }
}
