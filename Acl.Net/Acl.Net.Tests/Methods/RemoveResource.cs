using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class RemoveResource
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Remove_Resource()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            for (int i = 0; i < 2; i++)
            {
                _acl.Allow(roleName + '_' + i, new Resource[]
                {
                    new Resource()
                    {
                        Name = "Remove_Resource_Test",
                        Permissions = new[] { "Create", "View", "Update" }
                    },
                    new Resource()
                    {
                        Name = "Remove_Resource_Test2",
                        Permissions = new[] { "Create", "View", "Update" }
                    }
                });
            }

            _acl.RemoveResource("Remove_Resource_Test");

            bool isAllowed =_acl.IsRoleAllowed(roleName + '_' + 1, "Remove_Resource_Test", "Create");
            Assert.AreEqual(isAllowed, false);
        }
    }
}
