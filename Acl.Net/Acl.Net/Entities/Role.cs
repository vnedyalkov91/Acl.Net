using System;

namespace Acl.Net.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Resource[] Resources { get; set; } = new Resource[0];
        public string[] Parents { get; set; } = new string[0];
    }
}
