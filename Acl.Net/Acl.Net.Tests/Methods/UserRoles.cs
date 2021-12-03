using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class UserRoles
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Get_User_Roles_WithUser_That_Does_Not_Exists()
        {
            string userId = rnd.Next(0, 9999).ToString();

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.UserRoles(userId);
            });
        }

        [Test]
        public void Get_User_Roles_With_String()
        {
            string userId = rnd.Next(0, 9999).ToString();
            string roleName = rnd.Next(0, 9999).ToString();

            _acl.Allow(new Role() { 
                Name = roleName
            });

            _acl.AddUserRole(userId, roleName);

            Role[] userRoles = _acl.UserRoles(userId);

            Assert.AreEqual(userRoles.Length, 1);
            Assert.AreEqual(userRoles[0].Name, roleName);
        }

        [Test]
        public void Get_User_Roles_With_Integer()
        {
            Random rnd = new Random();
            int userId = rnd.Next(999);
            string roleName = rnd.Next(0, 9999).ToString();

            _acl.Allow(new Role()
            {
                Name = roleName
            });

            _acl.AddUserRole(userId, roleName);

            Role[] userRoles = _acl.UserRoles(userId);

            Assert.AreEqual(userRoles.Length, 1);
            Assert.AreEqual(userRoles[0].Name, roleName);
        }

        [Test]
        public void Get_User_Roles()
        {
            int userId = rnd.Next(999);
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();

            _acl.Allow(new Role()
            {
                Name = roleName
            });

            _acl.Allow(new Role()
            {
                Name = roleName2
            });

            _acl.AddUserRole(userId, roleName);
            _acl.AddUserRole(userId, roleName2);

            Role[] userRoles = _acl.UserRoles(userId);

            Assert.AreEqual(userRoles.Length, 2);
            Assert.AreEqual(userRoles[0].Name, roleName);
            Assert.AreEqual(userRoles[1].Name, roleName2);
        }
    }
}
