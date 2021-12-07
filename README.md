# Acl.Net - Access Control Lists for .Net Core
[![Run Tests Acl.Net](https://github.com/vnedyalkov91/Acl.Net/actions/workflows/dotnet.yml/badge.svg)](https://github.com/vnedyalkov91/Acl.Net/actions/workflows/dotnet.yml) [![CodeQL](https://github.com/vnedyalkov91/Acl.Net/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/vnedyalkov91/Acl.Net/actions/workflows/codeql-analysis.yml)

This module provides a minimalistic ACL implementation

When developing an application or site, sometimes we need control over access to various resources (endpoints). ACL can solve this problem in a flexible and elegant way.

Create roles and assign roles to users. Sometimes it may even be useful to create one role per user, to get the finest granularity possible, while in other situations you will give the asterisk permission for admin kind of functionality.

Backends are provided built-in in the module for now, will be added additional backend options in future. There also be an option, each user to create own backend implementation by extending the abstract 'Backend' class, provided into the module.

## Features
- Users
- Roles
- Resources
- Hierarchies

## Usage
The module provides very simple usage and flexibility in order to be extended.

```
IBackend backend = new Mongodb(IMongoClient client, string databaseName, string prefix);
IAcl acl = new Acl(backend);
```

### Backend

The backend can be initialized like in this example, by passing IMongoClient and database name, as well as prefix for the collection.
By default the prefix is Acl, so collection(s) that contain the persistance will be named **Acl_roles**, **Acl_users** and so on.

#### **Options**

- [Mongodb](#mongodb)
- [In Memory](#inmemory)

#### **Mongodb**

Provides persistance into mongodb database.

```
client { IMongoClient } - mongodb client
databaseName { string } - name of the database
prefix { string } - optional database prefix. By default 'Acl'
```

```
IBackend backend = new Mongodb(IMongoClient client, string databaseName, string prefix);
```

#### **InMemory**

Provides in memory persistance, which means that information will be kept as long as the application is alive.

```
IBackend backend = new InMemory();
```

#### **Devs**
In order to create your own backend, you just have to extend **Backend** class and implement the methods to store, get, update and delete the data in your storage.

```
public class YourBackend : Backend
{
    public YourBackend(yourDriver, string databaseName, string prefix = "acl") : base(databaseName, prefix)
    {}

    public override Role CreateRole(Role role)
    {
        throw new NotImplementedException();
    }

    public override User CreateUser(User user)
    {
        throw new NotImplementedException();
    }
}
```

## Documentation

- [Allow](#allow)
- [RemoveAllow](#removeallow)
- [RemoveResource](#removeresource)
- [RemovePermission](#removepermission)
- [AddRoleParent](#addroleparent)
- [AddRoleParents](#addroleparents)
- [RemoveRoleParent](#removeroleparent)
- [RemoveRoleParents](#removeroleparents)
- [AddUserRole](#adduserrole)
- [AddUserRoles](#adduserroles)
- [RemoveUserRole](#removeuserrole)
- [UserRoles](#userroles)
- [RoleUsers](#roleusers)
- [UserHasRole](#userhasrole)
- [AllowedPermissions](#allowedpermissions)
- [IsAllowed](#isallowed)
- [IsRoleAllowed](#isroleallowed)

## Methods

### Allow

Adds the given permissions to the given roles over the given resources.

```
/// <summary>
/// Adds the given permissions to the given roles over the given resources.
/// </summary>
/// <param name="roleName">Name of the role</param>
/// <param name="resources">Resources to be allowed for this role</param>
/// <returns>Allowed Role</returns>
```

> Role Allow(string roleName, Resource[] resources);

**Overloads**

```
/// <summary>
/// Adds the given permissions to the given roles over the given resources.
/// </summary>
/// <param name="role">Role to be allowed</param>
/// <returns>Allowed Role</returns>
```

> Role Allow(Role role);

```
/// <summary>
/// Adds the given permissions to the given roles over the given resources.
/// </summary>
/// <param name="roles">An array of role to be allowed</param>
/// <returns>Allowed Roles</returns>
```

> Role[] Allow(Role[] roles);

---------------------------------------

### RemoveAllow

Removes permissions from the given roles owned by the given role.

```
/// <summary>
/// Removes permissions from the given roles owned by the given role.
/// </summary>
/// <param name="roleName">The name of the role to be removed</param>
```
> void RemoveAllow(string roleName);

**Overloads**

```
/// <summary>
/// Removes permissions from the given roles owned by the given role.
/// </summary>
/// <param name="roleName">The name of the role from which it will be removed</param>
/// <param name="resources">The names of the resources to be removed</param>
/// <returns>Updated role</returns>
```

> Role RemoveAllow(string roleName, string[] resources);

```
/// <summary>
/// Removes permissions from the given roles owned by the given role.
/// </summary>
/// <param name="roleName">The name of the role from which it will be removed</param>
/// <param name="resources">The names of the resources to be removed</param>
/// <param name="permissions">Permissions to be removed</param>
/// <returns>Updated role</returns>
```

> Role RemoveAllow(string roleName, string[] resources, string[] permissions);

---------------------------------------

### RemoveResource

Removes a resource from the system.

```
/// <summary>
/// Removes a resource from the system.
/// </summary>
/// <param name="resourceName">The resource name to be removed</param>
```

> void RemoveResource(string resourceName);

---------------------------------------

### RemovePermission

Removes a permission from the system.

```
/// <summary>
/// Removes a permission from the system.
/// </summary>
/// <param name="permissionName">The name of the permission to be removed from the system</param>
```

> void RemovePermission(string permissionName);

---------------------------------------

### AddRoleParent

Adds a parent to role.

```
/// <summary>
/// Adds a parent to role.
/// </summary>
/// <param name="roleName">The name of the role</param>
/// <param name="parentName">The name of the parent</param>
/// <returns>Updated Role</returns>
```

> Role AddRoleParent(string roleName, string parentName);

---------------------------------------

### AddRoleParents

Adds parent list to role.

```
/// <summary>
/// Adds parent list to role.
/// </summary>
/// <param name="roleName">The name of the role</param>
/// <param name="parents">An array of parent role names</param>
/// <returns>Updated Role</returns>
```

> Role AddRoleParents(string roleName, string[] parents);

---------------------------------------

### RemoveRoleParent

Remove a parent from the role.

```
/// <summary>
/// Remove a parent from the role.
/// </summary>
/// <param name="roleName">The name of the role</param>
/// <param name="parentName">The name of the parent</param>
/// <returns>Updated Role</returns>
```

> Role RemoveRoleParent(string roleName, string parentName);

---------------------------------------

### RemoveRoleParents

Remove parent list from the role.

```
/// <summary>
/// Remove parent list from the role.
/// </summary>
/// <param name="roleName">The name of the role</param>
/// <param name="parents">An array of parent role names</param>
/// <returns>Updated Role</returns>
```

> Role RemoveRoleParents(string roleName, string[] parents);

---------------------------------------

### AddUserRole

Adds role to a given user id.

```
/// <summary>
/// Adds role to a given user id.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="roleName">Already allowed role</param>
```

> void AddUserRole(string userId, string roleName);

**Overloads**

```
/// <summary>
/// Adds role to a given user id.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="roleName">Already allowed role</param>
```

> void AddUserRole(int userId, string roleName);

```
/// <summary>
/// Adds role to a given user id.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="role">Already allowed role or role to be allowed</param>
```

> void AddUserRole(int userId, Role role);

```
/// <summary>
/// Adds role to a given user id.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="role">Already allowed role or role to be allowed</param>
```

> void AddUserRole(string userId, Role role);

---------------------------------------

### AddUserRoles

Adds list of roles to a given user id.

```
/// <summary>
/// Adds list of roles to a given user id.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="roleNames">A list of allowed Roles</param>
```

> void AddUserRoles(int userId, string[] roleNames);

**Overloads**

```
/// <summary>
/// Adds list of roles to a given user id.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="roleNames">A list of allowed Roles</param>
```

> void AddUserRoles(string userId, string[] roleNames);

```
/// <summary>
/// Adds list of roles to a given user id.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="roles">Allowed roles or roles to be allowed</param>
```

> void AddUserRoles(int userId, Role[] roles);

```
/// <summary>
/// Adds list of roles to a given user id.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="roles">Allowed roles or roles to be allowed</param>
```

> void AddUserRoles(string userId, Role[] roles);

---------------------------------------

### RemoveUserRole

Remove role from a given user.

```
/// <summary>
/// Adds list of roles to a given user id.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="roleNames">A list of allowed Roles</param>
```

> void AddUserRoles(int userId, string[] roleNames);

**Overloads**

```
/// <summary>
/// Remove role from a given user.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="roleName">The name of the role to be removed</param>
```

> void RemoveUserRole(int userId, string roleName);

---------------------------------------

### UserRoles

Return all the roles from a given user.

```
/// <summary>
/// Return all the roles from a given user.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <returns>Array of Roles</returns>
```

> Role[] UserRoles(string userId);

**Overloads**

```
/// <summary>
/// Return all the roles from a given user.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <returns>Array of Roles</returns>
```

> Role[] UserRoles(int userId);

---------------------------------------

### RoleUsers

Return all the users from a given role.

```
/// <summary>
///  Return all the users from a given role.
/// </summary>
/// <param name="roleName">The name of the role</param>
/// <returns>Array of Users</returns>
```

> User[] RoleUsers(string roleName);

**Overloads**

```
/// <summary>
///  Return all the users from a given role.
/// </summary>
/// <param name="role">Allowed Role</param>
/// <returns>Array of Users</returns>
```

> User[] RoleUsers(Role role);

---------------------------------------

### UserHasRole

Return boolean whether user has the role

```
/// <summary>
/// Return boolean whether user has the role
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="roleName">The name of the role</param>
/// <returns>true or false</returns>
```

> bool UserHasRole(string userId, string roleName);

**Overloads**

```
/// <summary>
/// Return boolean whether user has the role
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="role">Allowed role</param>
/// <returns>true or false</returns>
```

> bool UserHasRole(string userId, Role role);

```
/// <summary>
/// Return boolean whether user has the role
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="roleName">The name of the role</param>
/// <returns>true or false</returns>
```

> bool UserHasRole(int userId, string roleName);

```
/// <summary>
/// Return boolean whether user has the role
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="role">Allowed role</param>
/// <returns>true or false</returns>
```

> bool UserHasRole(int userId, Role role);

---------------------------------------

### AllowedPermissions

Returns all the allowable permissions a given user have to access the given resources.

```
/// <summary>
/// Returns all the allowable permissions a given user have to access the given resources.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="resource">The name of the resource</param>
/// <returns> Array of permissions </returns>
```

> string[] AllowedPermissions(string userId, string resource);

**Overloads**

```
/// <summary>
/// Returns all the allowable permissions a given user have to access the given resources.
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="resource">The name of the resource</param>
/// <returns> Array of permissions </returns>
```

> string[] AllowedPermissions(int userId, string resource);

---------------------------------------

### IsAllowed

Checks if the given user is allowed to access the resource for the given permissions
(note: it must fulfill all the permissions).

```
/// <summary>
/// Checks if the given user is allowed to access the resource for the given permissions
/// (note: it must fulfill all the permissions).
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="resource">The name of the resource</param>
/// <param name="permission">the name of the permission</param>
/// <returns>true or false</returns>
```

> bool IsAllowed(string userId, string resource, string permission);

**Overloads**

```
/// <summary>
/// Checks if the given user is allowed to access the resource for the given permissions
/// (note: it must fulfill all the permissions).
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="resource">The name of the resource</param>
/// <param name="permission">the name of the permission</param>
/// <returns>true or false</returns>
```

> bool IsAllowed(int userId, string resource, string permission);

```
/// <summary>
/// Checks if the given user is allowed to access the resource for the given permissions
/// (note: it must fulfill all the permissions).
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="resource">Resource with permissions</param>
/// <returns>true or false</returns>
```

> bool IsAllowed(string userId, Resource resource);

```
/// <summary>
/// Checks if the given user is allowed to access the resource for the given permissions
/// (note: it must fulfill all the permissions).
/// </summary>
/// <param name="userId">User Identificator</param>
/// <param name="resource">Resource with permissions</param>
/// <returns>true or false</returns>
```

> bool IsAllowed(int userId, Resource resource);

---------------------------------------

### IsRoleAllowed

Check if a given role has a resource and permission.

```
/// <summary>
/// Check if a given role has a resource and permission.
/// </summary>
/// <param name="roleName">The name of the target role</param>
/// <param name="resource">The name of the resource</param>
/// <param name="permission">The name of the permission</param>
/// <returns>true or false</returns>
```

> bool IsRoleAllowed(string roleName, string resource, string permission);

**Overloads**

```
/// <summary>
/// Check if a given role has a resource and permission.
/// </summary>
/// <param name="roleName">The name of the target role</param>
/// <param name="resource">The resource</param>
/// <returns>true or false</returns>
```

> bool IsRoleAllowed(string roleName, Resource resource);

---------------------------------------

## License

MIT License

Copyright (c) 2021 Vasil Nedyalkov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
