using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class UserHasRoles
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Check_If_User_Has_Role_With_User_That_Does_Not_Exists()
        {
            int userId = rnd.Next(0, 9999);
            string roleName = rnd.Next(0, 9999).ToString();

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.UserHasRole(userId, roleName);
            });
        }

        [Test]
        public void Check_If_User_Has_Role_With_Unassigned_Role()
        {
            int userId = rnd.Next(0, 9999);
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();

            _acl.Allow(new Role()
            {
                Name = roleName
            });

            _acl.AddUserRole(userId, roleName);

            bool hasRole = _acl.UserHasRole(userId, roleName2);
            Assert.AreEqual(hasRole, false);
        }

        [Test]
        public void Check_If_User_Has_Role_With_Role()
        {
            int userId = rnd.Next(0, 9999);
            string roleName = rnd.Next(0, 9999).ToString();

            _acl.Allow(new Role()
            {
                Name = roleName
            });

            _acl.AddUserRole(userId, roleName);

            bool hasRole = _acl.UserHasRole(userId, roleName);
            Assert.AreEqual(hasRole, true);
        }

        [Test]
        public void Check_If_User_Has_Role_With_Roles()
        {
            int userId = rnd.Next(0, 9999);
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();

            _acl.Allow(new Role()
            {
                Name = roleName
            });

            Role role = _acl.Allow(new Role()
            {
                Name = roleName2
            });

            _acl.AddUserRole(userId, roleName);
            _acl.AddUserRole(userId, role);

            bool hasRole = _acl.UserHasRole(userId, role);
            Assert.AreEqual(hasRole, true);
        }
    }
}
