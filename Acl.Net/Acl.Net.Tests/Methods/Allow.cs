using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class Allow
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Allow_Single_Role()
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

            Assert.AreEqual(role.Name, roleName);
            Assert.AreEqual(role.Resources[0].Name, "Test");
            Assert.AreEqual(role.Resources[0].Permissions.Length, 3);
            Assert.AreEqual(role.Resources[0].Permissions[0], "Create");
            Assert.AreEqual(role.Resources[0].Permissions[2], "Update");
        }

        [Test]
        public void Allow_Single_Role_Overload()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            Role role = _acl.Allow(
                 new Role()
                 {
                     Name = roleName,
                     Resources = new Resource[]
                        {
                            new Resource()
                            {
                                Name = "Containers",
                                Permissions = new string[] { "View", "Create", "Delete", "Update" }
                            },
                            new Resource()
                            {
                                Name = "Organisations",
                                Permissions = new string[] { "View" }
                            }
                        }
                 }

            );

            Assert.AreEqual(role.Name, roleName);
        }

        [Test]
        public void Allow_Single_Role_That_Already_Exists()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            Role role = _acl.Allow(roleName, new Resource[]
            {
                new Resource()
                {
                    Name = "Test",
                    Permissions = new[] { "Create", "View", "Delete" }
                },
                new Resource()
                {
                    Name = "Test2",
                    Permissions = new[] { "Create", "View", "Delete" }
                }
            });

            Assert.AreEqual(role.Resources[0].Name, "Test");
            Assert.AreEqual(role.Resources[0].Permissions.Length, 3);
            Assert.AreEqual(role.Resources[0].Permissions[0], "Create");
            Assert.AreEqual(role.Resources[0].Permissions[2], "Delete");
            Assert.AreEqual(role.Resources[1].Name, "Test2");
            Assert.AreEqual(role.Resources[1].Permissions.Length, 3);
            Assert.AreEqual(role.Resources[1].Permissions[0], "Create");
            Assert.AreEqual(role.Resources[1].Permissions[2], "Delete");

        }

        [Test]
        public void Allow_Many_Roles()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();
            Role[] roles = _acl.Allow(
                new Role[]
                {
                    new Role()
                    {
                        Name = roleName,
                        Resources = new Resource[]
                        {
                            new Resource()
                            {
                                Name = "Containers",
                                Permissions = new string[] { "View", "Create", "Delete", "Update" }
                            },
                            new Resource()
                            {
                                Name = "Organisations",
                                Permissions = new string[] { "View" }
                            }
                        }
                    },
                    new Role()
                    {
                        Name = roleName2,
                        Resources = new Resource[]
                        {
                            new Resource()
                            {
                                Name = "Containers",
                                Permissions = new string[] { "View", "Create", "Delete", "Update" }
                            },
                            new Resource()
                            {
                                Name = "Organisations",
                                Permissions = new string[] { "View" }
                            }
                        }
                    }
                }
            );

            Assert.AreEqual(roles.Length, 2);
            Assert.AreEqual(roles[0].Name, roleName);
            Assert.AreEqual(roles[1].Name, roleName2);
        }

        [Test]
        public void Allow_Many_Roles_That_Already_Exists()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();
            Role[] roles = _acl.Allow(
                new Role[]
                {
                    new Role()
                    {
                        Name = roleName,
                        Resources = new Resource[]
                        {
                            new Resource()
                            {
                                Name = "Organisations",
                                Permissions = new string[] { "View" }
                            }
                        }
                    },
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
                        }
                    }
                }
            );

            Assert.AreEqual(roles.Length, 2);
            Assert.AreEqual(roles[0].Name, roleName);
            Assert.AreEqual(roles[1].Name, roleName2);
            Assert.AreEqual(roles[0].Resources[0].Name, "Organisations");
            Assert.AreEqual(roles[1].Resources[0].Name, "Organisations");
        }
    }
}
