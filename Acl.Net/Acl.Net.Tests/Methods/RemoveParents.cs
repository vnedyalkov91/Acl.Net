using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class RemoveParents
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Remove_Role_Parents()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();

            _acl.Allow(new Role { Name = roleName });
            _acl.Allow(new Role { Name = roleName2 });

            Role role = _acl.AddRoleParents(roleName, new string[] { roleName2 });
            Assert.AreEqual(role.Parents[0], roleName2);

            role = _acl.RemoveRoleParents(roleName, new string[] { roleName2 });
            Assert.AreEqual(role.Parents.Length, 0);
        }

        [Test]
        public void Remove_Role_Parent()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();

            _acl.Allow(new Role { Name = roleName });
            _acl.Allow(new Role { Name = roleName2 });

            Role role = _acl.AddRoleParents(roleName, new string[] { roleName2 });
            Assert.AreEqual(role.Parents[0], roleName2);

            role = _acl.RemoveRoleParent(roleName, roleName2);
            Assert.AreEqual(role.Parents.Length, 0);
        }

        [Test]
        public void Remove_Role_Parents_With_Not_Existing_Role()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.RemoveRoleParents(roleName, new string[] { roleName2 });
            });
        }

        [Test]
        public void Remove_Role_Parents_With_Not_Existing_Parent_Role()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.RemoveRoleParents(roleName, new string[] { roleName2 });
            });
        }
    }
}
