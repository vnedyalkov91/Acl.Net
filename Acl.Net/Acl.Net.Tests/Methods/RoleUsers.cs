using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class RoleUsers
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Get_Role_Users_With_Not_Existing_Role()
        {
            string roleName = rnd.Next(9999).ToString();
            User[] users = _acl.RoleUsers(roleName);

            Assert.AreEqual(users.Length, 0);
        }

        [Test]
        public void Get_Role_Users_With_Role_That_Is_Not_Assigned()
        {
            string roleName = rnd.Next(9999).ToString();

            _acl.Allow(new Role()
            {
                Name = roleName
            });

            User[] users = _acl.RoleUsers(roleName);

            Assert.AreEqual(users.Length, 0);
        }

        [Test]
        public void Get_Role_User_With_Signle_User ()
        {           
            string roleName = rnd.Next(9999).ToString();
            int userId = rnd.Next(999);

            _acl.Allow(new Role()
            {
                Name = roleName
            });

            _acl.AddUserRole(userId, roleName);

            User[] users = _acl.RoleUsers(roleName);

            Assert.AreEqual(users.Length, 1);
            Assert.AreEqual(users[0].UserId, userId.ToString());
            Assert.AreEqual(users[0].Roles[0].Name, roleName);
        }

        [Test]
        public void Get_Role_Users_With_Signle_User_And_Role()
        {
            string roleName = rnd.Next(9999).ToString();
            int userId = rnd.Next(999);

            Role role = _acl.Allow(new Role()
            {
                Name = roleName
            });

            _acl.AddUserRole(userId, roleName);

            User[] users = _acl.RoleUsers(role);

            Assert.AreEqual(users.Length, 1);
            Assert.AreEqual(users[0].UserId, userId.ToString());
            Assert.AreEqual(users[0].Roles[0].Name, roleName);
        }

        [Test]
        public void Get_Role_User_With_Multy_User()
        {
            string roleName = rnd.Next(9999).ToString();

            _acl.Allow(new Role()
            {
                Name = roleName
            });

            _acl.AddUserRole(rnd.Next(999), roleName);
            _acl.AddUserRole(rnd.Next(999), roleName);
            _acl.AddUserRole(rnd.Next(999), roleName);

            User[] users = _acl.RoleUsers(roleName);

            Assert.AreEqual(users.Length, 3);
        }
    }
}
