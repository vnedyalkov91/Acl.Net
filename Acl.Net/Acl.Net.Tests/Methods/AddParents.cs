using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class AddParents
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Add_Role_Parents()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();

            _acl.Allow(new Role { Name = roleName });
            _acl.Allow(new Role { Name = roleName2 });

            Role role = _acl.AddRoleParents(roleName, new string[] { roleName2 });
            Assert.AreEqual(role.Parents[0], roleName2);
        }

        [Test]
        public void Add_Role_Parent()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();

            _acl.Allow(new Role { Name = roleName });
            _acl.Allow(new Role { Name = roleName2 });

            Role role = _acl.AddRoleParent(roleName, roleName2);
            Assert.AreEqual(role.Parents[0], roleName2);
        }

        [Test]
        public void Add_Role_Parents_That_Does_Not_Exists()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string parentName = rnd.Next(0, 9999).ToString();

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.AddRoleParents(roleName, new string[] { parentName });
            });
        }

        [Test]
        public void Add_Role_Parents_That_Parent_Does_Not_Exists()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();

            Role role = _acl.Allow(roleName, new Resource[]
           {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Update" }
                }
           });

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.AddRoleParents(roleName, new string[] { roleName2 });
            });
        }
    }
}
