# Acl.Net - Access Control Lists for .Net Core
[![Run Tests Acl.Net](https://github.com/vnedyalkov91/Acl.Net/actions/workflows/dotnet.yml/badge.svg)](https://github.com/vnedyalkov91/Acl.Net/actions/workflows/dotnet.yml)

This module provides a minimalistic ACL implementation

When developing an application or site, sometimes we need control over access to various resources (endpoints). ACL can solve this problem in a flexible and elegant way.

Create roles and assign roles to users. Sometimes it may even be useful to create one role per user, to get the finest granularity possible, while in other situations you will give the asterisk permission for admin kind of functionality.

A MongoDB based backends are provided built-in in the module for now, will be added additional backend options in future. There also be an option, each user to create own backend implementation by extending the abstract 'Backend' class, provided into the module.

## Features
- Users
- Roles
- Resources
- Hierarchies

## Usage
The module provides very simple usage and flexibility in order to be extended.

### Backend

The backend can be initialized like in this example, by passing IMongoClient and database name, as well as prefix for the collection.
By default the prefix is Acl, so collection(s) that contain the persistance will be named **Acl_roles**, **Acl_users** and so on.

#### **Arguments**
```
client { IMongoClient } - mongodb client
databaseName { string } - name of the database
prefix { string } - optional database prefix. By default 'Acl'
```

```
IBackend backend = new Mongodb(IMongoClient client, string databaseName, string prefix);
IAcl acl = new Acl(backend);
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

- Allow
- RemoveAllow
- RemoveResource
- AddRoleParent
- RemoveRoleParent
- AddUserRole
- AddUserRoles
- RemoveUserRole
- UserRoles
- RoleUsers
- UserHasRole
- AllowedPermissions
- IsAllowed
- IsRoleAllowed