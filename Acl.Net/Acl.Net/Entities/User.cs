using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Acl.Net.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string[] RoleNames { get; set; } = new string[0];

        [BsonIgnore]
        public Role[] Roles { get; set; }
    }
}
