using Acl.Net.Entities;
using Acl.Net.Exceptions;
using Acl.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acl.Net
{
    public class Acl : IAcl
    {
        private readonly IBackend _backendService;

        public Acl(IBackend backendService)
        {
            this._backendService = backendService;
        }

        public Role Allow(Role role)
        {
            Role existingRole = _backendService.GetRole(role.Name);
            Role allowedRole;

            if (existingRole == null)
            {
                allowedRole = _backendService.CreateRole(role);
            }
            else
            {
                allowedRole = _backendService.UpdateRole(role);
            }

            return allowedRole;
        }

        public Role[] Allow(Role[] roles)
        {
            List<Role> savedRoles = new List<Role>();

            foreach (Role role in roles)
            {
                savedRoles.Add( Allow(role) );
            }

            return savedRoles.ToArray();
        }

        public Role Allow(string roleName, Resource[] resources)
        {
            Role newRole = new Role() { Name = roleName, Resources = resources };

            return Allow(newRole);
        }
        
        public void RemoveAllow(string roleName)
        {
            Role existingRole = _backendService.GetRole(roleName);

            if (existingRole == null)
            {
                throw new RoleNotFoundException();
            }

            _backendService.DeleteRole(existingRole);
        }

        public Role RemoveAllow(string roleName, string[] resources)
        {
            Role existingRole = _backendService.GetRole(roleName);

            if (existingRole == null)
            {
                throw new RoleNotFoundException();
            }

            if (resources != null)
            {
                existingRole.Resources = existingRole.Resources.Where(r => !Array.Exists(resources, n => n == r.Name)).ToArray();
            }

            _backendService.UpdateRole(existingRole);

            return existingRole;
        }

        public Role RemoveAllow(string roleName, string[] resources, string[] permissions)
        {
            Role existingRole = RemoveAllow(roleName, resources);

            if (permissions != null)
            {
                existingRole.Resources = existingRole.Resources.Where(r =>
                {
                    r.Permissions = r.Permissions.Where(p => !Array.Exists(permissions, pe => pe == p)).ToArray();
                    return true;
                }).ToArray();
            }

            _backendService.UpdateRole(existingRole);

            return existingRole;
        }

        public void RemoveResource(string resourceName)
        {
            _backendService.DeleteResource(resourceName);
        }

        public void RemovePermission(string permissionName)
        {
            _backendService.DeletePermission(permissionName);
        }

        public Role AddRoleParents(string roleName, string[] parents)
        {
            Role existingRole = _backendService.GetRole(roleName);

            if (existingRole == null)
            {
                throw new RoleNotFoundException();
            }

            foreach (string parentName in parents)
            {
                Role parent = _backendService.GetRole(parentName);

                if (parent == null)
                {
                    throw new RoleParentNotFoundException();
                }
            }

            Role role = _backendService.AddRoleParents(existingRole, parents);

            role = _backendService.UpdateRole(role);

            return role;
        }

        public Role AddRoleParent(string roleName, string parentName)
        {
            Role role = AddRoleParents(roleName, new string[] { parentName });

            return role;
        }

        public Role RemoveRoleParents(string roleName, string[] parents)
        {
            Role existingRole = _backendService.GetRole(roleName);

            if (existingRole == null)
            {
                throw new RoleNotFoundException();
            }

            Role role = _backendService.RemoveRoleParents(existingRole, parents);

            role = _backendService.UpdateRole(role);

            return role;
        }

        public Role RemoveRoleParent(string roleName, string parentName)
        {
            Role role = RemoveRoleParents(roleName, new string[] { parentName });

            return role;
        }

        public void AddUserRole(string userId, string roleName)
        {
            Role role = _backendService.GetRole(roleName);

            if (role == null)
            {
                throw new RoleNotFoundException();
            }

            User existingUser = _backendService.GetUser(userId);

            if (existingUser == null)
            {
                _backendService.CreateUser(new User
                {
                    UserId = userId,
                    RoleNames = new string[] { role.Name }
                });
            }
            else
            {
                List<string> roleNames = existingUser.RoleNames.ToList();
                roleNames.Add(role.Name);
                existingUser.RoleNames = roleNames.ToArray();
                _backendService.UpdateUser(existingUser);
            }
        }

        public void AddUserRole(int userId, string roleName)
        {
            AddUserRole(userId.ToString(), roleName);
        }

        public void AddUserRole(string userId, Role role)
        {
            Role allowedRole = Allow(role);
            AddUserRole(userId, allowedRole.Name);
        }

        public void AddUserRole(int userId, Role role)
        {
            Role allowedRole = Allow(role);
            AddUserRole(userId.ToString(), allowedRole.Name);
        }

        public void AddUserRoles(int userId, string[] roleNames)
        {
            foreach (string roleName in roleNames)
            {
                AddUserRole(userId.ToString(), roleName);
            }
        }

        public void AddUserRoles(string userId, string[] roleNames)
        {
            foreach (string roleName in roleNames)
            {
                AddUserRole(userId, roleName);
            }
        }

        public void AddUserRoles(string userId, Role[] roles)
        {
            foreach (Role role in roles)
            {
                Role allowedRole = Allow(role);
                AddUserRole(userId, allowedRole.Name);
            }
        }

        public void AddUserRoles(int userId, Role[] roles)
        {
            foreach (Role role in roles)
            {
                Role allowedRole = Allow(role);
                AddUserRole(userId.ToString(), allowedRole.Name);
            }
        }

        public void RemoveUserRole(string userId, string roleName)
        {
            Role role = _backendService.GetRole(roleName);

            if (role == null)
            {
                throw new RoleNotFoundException();
            }

            User user = _backendService.GetUser(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            List<string> roleNames = user.RoleNames.ToList();
            roleNames.Remove(roleName);
            user.RoleNames = roleNames.ToArray();
            _backendService.UpdateUser(user);
        }

        public void RemoveUserRole(int userId, string roleName)
        {
            RemoveUserRole(userId.ToString(), roleName);
        }

        public Role[] UserRoles(string userId)
        {
            User user = _backendService.GetUser(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            List<Role> roles = new List<Role>();

            foreach (string roleName in user.RoleNames)
            {
                Role role = _backendService.GetRole(roleName);
                roles.Add(role);
            }

            return roles.ToArray();
        }

        public Role[] UserRoles(int userId)
        {
            return UserRoles(userId.ToString());
        }

        public bool UserHasRole(string userId, string roleName)
        {
            User user = _backendService.GetUser(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user.RoleNames.Contains(roleName);
        }

        public bool UserHasRole(string userId, Role role)
        {
            return UserHasRole(userId, role.Name);
        }

        public bool UserHasRole(int userId, string roleName)
        {
            return UserHasRole(userId.ToString(), roleName);
        }

        public bool UserHasRole(int userId, Role role)
        {
            return UserHasRole(userId, role.Name);
        }

        public User[] RoleUsers(string roleName)
        {
            User[] users = _backendService.GetUsers(roleName);

            foreach (User user in users)
            {
                int length = user.RoleNames.Length;
                user.Roles = new Role[length];

                for (int i = 0; i < length; i++)
                {
                    user.Roles[i] = _backendService.GetRole(user.RoleNames[i]);
                }
            }

            return users;
        }

        public User[] RoleUsers(Role role)
        {
            return RoleUsers(role.Name);
        }

        public string[] AllowedPermissions(string userId, string resource)
        {
            Role[] roles = UserRoles(userId);

            List<Resource> resources = new List<Resource>();

            foreach (Role role in roles)
            {
                Resource[] roleResorces = RecursiveGetRoleResources(role.Name);
                foreach (Resource res in roleResorces)
                {
                    resources.Add(res);
                }
            }

            Resource[] targetResources = resources
                .Where(r => r.Name == resource).ToArray();
            
            List<string> permissions = new List<string>();

            foreach (Resource res in targetResources)
            {
                foreach (string permission in res.Permissions)
                {
                    if(!permissions.Contains(permission))
                    {
                        permissions.Add(permission);
                    }
                }
                
            }

            return permissions.ToArray();

        }

        public string[] AllowedPermissions(int userId, string resource)
        {
            return AllowedPermissions(userId.ToString(), resource);
        }

        private Resource[] RecursiveGetRoleResources(string roleName)
        {
            List<Resource> resources = new List<Resource>();

            Role role = _backendService.GetRole(roleName);

            foreach (Resource resource in role.Resources)
            {
                resources.Add(resource);
            }

            foreach (string parent in role.Parents)
            {
                Resource[] parentResources = RecursiveGetRoleResources(parent);
                foreach (Resource parentResource in parentResources)
                {
                    resources.Add(parentResource);
                }
            }

            return resources.ToArray();
        }

        public bool IsAllowed(string userId, string resource, string permission)
        {
            string[] permissions = AllowedPermissions(userId, resource);
            return permissions.Contains(permission);
        }

        public bool IsAllowed(int userId, string resource, string permission)
        {
            string[] permissions = AllowedPermissions(userId, resource);
            return permissions.Contains(permission);
        }

        public bool IsAllowed(string userId, Resource resource)
        {
            string[] permissions = AllowedPermissions(userId, resource.Name);
            bool isAllowed = true;

            foreach (string perm in resource.Permissions)
            {
                if (!permissions.Contains(perm))
                {
                    isAllowed = false;
                    break;
                }
            }

            return isAllowed;
        }

        public bool IsAllowed(int userId, Resource resource)
        {
            return IsAllowed(userId.ToString(), resource);
        }

        public bool IsRoleAllowed(string roleName, string resource, string permission)
        {
            Role role = _backendService.GetRole(roleName);

            if (role == null)
            {
                throw new RoleNotFoundException();
            }

            Resource targetResource = Array.Find(role.Resources, res => res.Name == resource);

            if (targetResource == null)
            {
                return false;
            }

            return targetResource.Permissions.Contains(permission);

        }

        public bool IsRoleAllowed(string roleName, Resource resource)
        {
            Role role = _backendService.GetRole(roleName);

            if (role == null)
            {
                throw new RoleNotFoundException();
            }

            Resource targetResource = Array.Find(role.Resources, res => res.Name == resource.Name);

            if (targetResource == null)
            {
                return false;
            }

            bool isAllowed = true;

            foreach (string perm in resource.Permissions)
            {
                if (!targetResource.Permissions.Contains(perm))
                {
                    isAllowed = false;
                    break;
                }
            }

            return isAllowed;
        }
    }
}
