using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class IsRoleAllowed
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Check_If_Role_Is_Allowed_With_Not_Existing_Role()
        {
            string roleName = rnd.Next(9999).ToString();
            string resource = rnd.Next(9999).ToString();
            string permission = rnd.Next(9999).ToString();

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.IsRoleAllowed(roleName, resource, permission);
            });
        }

        [Test]
        public void Check_If_Role_Is_Allowed_With_Strings()
        {
            string roleName = rnd.Next(9999).ToString();
            string resource = rnd.Next(9999).ToString();
            string permission = rnd.Next(9999).ToString();

            _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = resource,
                    Permissions = new[] { permission }
                }
            });

            bool isAllowed = _acl.IsRoleAllowed(roleName, resource, permission);
            bool isAllowed2 = _acl.IsRoleAllowed(roleName, resource + "Foo", permission);
            bool isAllowed3 = _acl.IsRoleAllowed(roleName, resource, permission + "Foo");

            Assert.AreEqual(isAllowed, true);
            Assert.AreEqual(isAllowed2, false);
            Assert.AreEqual(isAllowed3, false);
        }

        [Test]
        public void Check_If_Role_Is_Allowed_With_Resource()
        {
            string roleName = rnd.Next(9999).ToString();
            string resource = rnd.Next(9999).ToString();
            string permission = rnd.Next(9999).ToString();

            Role role = _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = resource,
                    Permissions = new[] { permission }
                }
            });

            bool isAllowed = _acl.IsRoleAllowed(roleName, new Resource()
            {
                Name = resource,
                Permissions = new[] { permission }
            });
            bool isAllowed2 = _acl.IsRoleAllowed(roleName, new Resource()
            {
                Name = resource + "Foo",
                Permissions = new[] { permission }
            });
            bool isAllowed3 = _acl.IsRoleAllowed(roleName, new Resource()
            {
                Name = resource,
                Permissions = new[] { permission + "Foo" }
            });

            Assert.AreEqual(isAllowed, true);
            Assert.AreEqual(isAllowed2, false);
            Assert.AreEqual(isAllowed3, false);
        }
    }
}
