using System;

namespace Acl.Net.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string[] Permissions { get; set; } = new string[0];
    }
}
