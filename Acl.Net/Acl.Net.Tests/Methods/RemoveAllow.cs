using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class RemoveAllow
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Remove_Allow()
        {
            string roleName = rnd.Next(0, 9999).ToString();

            Role role = _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Update" }
                }
            });

            _acl.RemoveAllow(roleName);

            Assert.Pass();
        }

        [Test]
        public void Remove_Allow_With_Childs()
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

            _acl.Allow(
                new Role()
                {
                    Name = roleName2,
                    Resources = new Resource[]
                        {
                            new Resource()
                            {
                                Name = "Organisations",
                                Permissions = new string[] { "View" }
                            }
                        },
                    Parents = new string[] { roleName }
                }
            );

            _acl.RemoveAllow(roleName);

            Assert.Pass();
        }

        [Test]
        public void Remove_Allow_That_Does_Not_Exists()
        {
            string roleName = rnd.Next(0, 9999).ToString();

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.RemoveAllow(roleName);
            });
        }

        [Test]
        public void Remove_Allow_Overload()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            Role role = _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Update" }
                },
                new Resource()
                {
                    Name = "Test2",
                    Permissions = new[] { "Create", "View", "Update" }
                }
            });

            Role newRole = _acl.RemoveAllow(roleName, new string[] { "Test" });

            Assert.AreEqual(newRole.Resources[0].Name, "Test2");
        }

        [Test]
        public void Remove_Allow_With_Role_That_Does_Not_Exists()
        {
            string roleName = rnd.Next(0, 9999).ToString();

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.RemoveAllow(roleName, new string[] { "Test" });
            });
        }

        [Test]
        public void Remove_Allow_With_Resource_And_Permissions()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            Role role = _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Update" }
                },
                new Resource()
                {
                    Name = "Test2",
                    Permissions = new[] { "Create", "View", "Update" }
                }
            });

            Role newRole = _acl.RemoveAllow(roleName, new string[] { "Test" }, new string[] { "Update", "Create" });

            Assert.AreEqual(newRole.Resources[0].Name, "Test2");
            Assert.AreEqual(newRole.Resources[0].Permissions[0], "View");
            Assert.AreEqual(newRole.Resources[0].Permissions.Length, 1);
        }

        [Test]
        public void Remove_Allow_With_Permissions()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            Role role = _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Update" }
                },
                new Resource()
                {
                    Name = "Test2",
                    Permissions = new[] { "Create", "View", "Update" }
                }
            });

            Role newRole = _acl.RemoveAllow(roleName, null, new string[] { "Update", "Create" });

            Assert.AreEqual(newRole.Resources[0].Name, "Test");
            Assert.AreEqual(newRole.Resources[0].Permissions[0], "View");
            Assert.AreEqual(newRole.Resources[0].Permissions.Length, 1);
        }
    }
}
