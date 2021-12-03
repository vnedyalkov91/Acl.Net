using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class AddUserRole
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Add_User_Role_With_Non_Existing_Role()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            int userId = 1;

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.AddUserRole(userId, roleName);
            });
        }

        [Test]
        public void Add_User_Role_With_Integer()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            int userId = rnd.Next(0, 9999);

            _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Update" }
                }
            });

            _acl.AddUserRole(userId, roleName);
            Assert.Pass();
        }

        [Test]
        public void Add_User_Role_With_String()
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
            Assert.Pass();
        }

        [Test]
        public void Add_User_Role_To_Existing_User()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();
            int userId = rnd.Next(0, 9999);

            _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Update" }
                }
            });

            _acl.Allow(roleName2, new Resource[]
            {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Update" }
                }
            });

            _acl.AddUserRole(userId, roleName);
            _acl.AddUserRole(userId, roleName2);
            Assert.Pass();
        }
    }
}
