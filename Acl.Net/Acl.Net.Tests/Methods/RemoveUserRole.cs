using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class RemoveUserRole
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Remove_User_Role_With_Non_Existing_Role()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string userId = rnd.Next(0, 9999).ToString();

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.RemoveUserRole(userId, roleName);
            });
        }

        [Test]
        public void Remove_User_Role_With_String()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string userId = rnd.Next(0, 9999).ToString();

            _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Update" }
                }
            });

            _acl.AddUserRole(userId, roleName);
            _acl.RemoveUserRole(userId, roleName);
            Assert.Pass();
        }

        [Test]
        public void Remove_User_Role_With_Integer()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            int userId = 1;

            _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Update" }
                }
            });

            _acl.AddUserRole(userId, roleName);
            _acl.RemoveUserRole(userId, roleName);
            Assert.Pass();
        }

    }
}
