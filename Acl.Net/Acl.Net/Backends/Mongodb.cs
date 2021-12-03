using Acl.Net.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Acl.Net.Backends
{
    public class Mongodb : Backend
    {
        private const string roleCollectionName = "_roles";
        private const string userCollectionName = "_users";
        
        private readonly IMongoCollection<Role> roleCollection;
        private readonly IMongoCollection<User> userCollection;
        private readonly FilterDefinitionBuilder<Role> roleFilterBuilder = Builders<Role>.Filter;
        private readonly FilterDefinitionBuilder<User> userFilterBuilder = Builders<User>.Filter;
        private readonly UpdateDefinitionBuilder<Role> roleUpdateBuilder = Builders<Role>.Update;
        private readonly UpdateDefinitionBuilder<User> userUpdateBuilder = Builders<User>.Update;

        public Mongodb(IMongoClient client, string databaseName, string prefix = "acl") : base(databaseName, prefix)
        {
            IMongoDatabase database = client.GetDatabase(databaseName);
            this.roleCollection = database.GetCollection<Role>(prefix + roleCollectionName);
            this.userCollection = database.GetCollection<User>(prefix + userCollectionName);
        }

        public override Role AddRoleParents(Role role, string[] parents)
        {
            role.Parents = Utils.RemoveDuplicates<string>(
                    Utils.JoinArray<string>(role.Parents, parents)
                );

            return role;
        }

        public override Role RemoveRoleParents(Role role, string[] parents)
        {
            role.Parents = role.Parents.Where(
                    p => !Array.Exists(parents, pe => pe == p)
                ).ToArray();

            return role;
        }

        public override void DeleteResource(string resourceName)
        {
            // Setup
            FilterDefinition<Role> filter = roleFilterBuilder
                .ElemMatch(role => role.Resources, resource => resource.Name == resourceName);
            UpdateDefinition<Role> update = roleUpdateBuilder
                .PullFilter(r => r.Resources, res => res.Name == resourceName);

            // Update all existing roles that match filter requirement
            roleCollection.UpdateMany(filter, update);
        }

        public override void DeletePermission(string permissionName)
        {
            // Setup
            BsonDocument filter = new BsonDocument { {
                    "Resources.Permissions",  permissionName
                } };

            // Find all roles for update
            Role[] roles = roleCollection.Find(filter).ToList().ToArray();

            foreach (Role role in roles)
            {
                role.Resources = role.Resources.Select(r =>
                {
                    r.Permissions = r.Permissions.Where(p => p != permissionName).ToArray();
                    return r;
                })
                    .ToArray();

                UpdateRole(role);
            }
        }

        public override void DeleteRole(Role role)
        {
            // Setup
            FilterDefinition<Role> filter = roleFilterBuilder.Eq(u => u.Name, role.Name);
            FilterDefinition<Role> filterParents = roleFilterBuilder
                .Eq("Parents", role.Name);
            FilterDefinition<User> userFilter = userFilterBuilder.AnyEq(u => u.RoleNames, role.Name);

            // Delete role
            roleCollection.DeleteOne(filter);

            // Delete role from parents in each role that exist
            Role[] roles = roleCollection.Find(filterParents).ToList().ToArray();
            foreach (Role r in roles)
            {
                RemoveRoleParents(r, new string[] { role.Name });
            }

            // Delete role from RoleNames in each user that exist
            User[] users = userCollection.Find(userFilter).ToList().ToArray();
            foreach (User user in users)
            {
                user.RoleNames = user.RoleNames.Where(r => r != role.Name).ToArray();
                UpdateUser(user);
            }
        }

        public override Role GetRole(string roleName)
        {
            // Setup
            FilterDefinition<Role> filter = roleFilterBuilder.Eq(r => r.Name, roleName);

            // Find role
            Role existingRole = roleCollection.Find(filter).SingleOrDefault();

            // Return role
            return existingRole;
        }

        public override Role UpdateRole(Role role)
        {
            // Setup
            FilterDefinition<Role> filter = roleFilterBuilder.Eq(u => u.Name, role.Name);
            UpdateDefinition<Role> update = roleUpdateBuilder
                .Set(s => s.Parents, role.Parents)
                .Set(s => s.Resources, role.Resources);

            // Update target role
            roleCollection.UpdateOne(filter, update);

            // Return role
            return GetRole(role.Name);
        }

        public override Role CreateRole(Role role)
        {
            // Insert role
            roleCollection.InsertOne(role);

            // Return role
            return GetRole(role.Name);
        }

        public override User GetUser(string userId)
        {
            User existingUser = userCollection.Find(u => u.UserId == userId).FirstOrDefault();

            return existingUser;
        }

        public override User CreateUser(User user)
        {
            userCollection.InsertOne(user);

            return GetUser(user.UserId);
        }

        public override User UpdateUser(User user)
        {
            // Setup
            FilterDefinition<User> filter = userFilterBuilder.Eq(u => u.UserId, user.UserId);
            UpdateDefinition<User> update = userUpdateBuilder
                   .Set(u => u.RoleNames, Utils.RemoveDuplicates<string>(
                       user.RoleNames
                   )
               );

            // Update target user
            userCollection.UpdateOne(filter, update);

            // Return user
            return GetUser(user.UserId);
        }

        public override User[] GetUsers(string roleName)
        {
            User[] users = userCollection.Find(u => u.RoleNames.Contains(roleName)).ToList().ToArray();
            return users;
        }
    }
}
