using Acl.Net.Entities;
using Acl.Net.Interfaces;
using NUnit.Framework;
using System;

namespace Acl.Net.Tests.Methods
{
    public abstract class AllowedPermissions
    {
        protected IAcl _acl { get; set; }
        private Random rnd = new Random();

        [SetUp]
        public abstract void SetUp();

        [Test]
        public void Get_Allowed_Permissions_With_Not_Existing_User()
        {
            string userId = rnd.Next(0, 9999).ToString();

            Assert.Throws(Is.InstanceOf<Exception>(), () =>
            {
                _acl.AllowedPermissions(userId, "Containers");
            });
        }

        [Test]
        public void Get_Allowed_Permissions_With_Not_Existing_Rsource()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();
            string roleName3 = rnd.Next(0, 9999).ToString();

            int userId = rnd.Next(0, 9999);

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
                                Name = "Devices",
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
                        Name = roleName3,
                        Resources = new Resource[]
                        {
                            new Resource()
                            {
                                Name = "Containers",
                                Permissions = new string[] { "View" }
                            }
                        }
                    }
                }
            );

            _acl.AddRoleParent(roleName2, roleName3);
            _acl.AddUserRoles(userId, new string[] { roleName, roleName2 });

            string[] permissions = _acl.AllowedPermissions(userId, "Foo");
            Assert.AreEqual(0, permissions.Length);
        }

        [Test]
        public void Get_Allowed_Permissions_With_String()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();
            string roleName3 = rnd.Next(0, 9999).ToString();

            string userId = rnd.Next(0, 9999).ToString();

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
                                Name = "Devices",
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
                        Name = roleName3,
                        Resources = new Resource[]
                        {
                            new Resource()
                            {
                                Name = "Containers",
                                Permissions = new string[] { "View" }
                            }
                        }
                    }
                }
            );

            _acl.AddRoleParent(roleName2, roleName3);
            _acl.AddUserRoles(userId, new string[] { roleName, roleName2 });

            string[] permissions = _acl.AllowedPermissions(userId, "Containers");
            Assert.AreEqual(4, permissions.Length);
        }

        [Test]
        public void Get_Allowed_Permissions_With_Integer()
        {
            string roleName = rnd.Next(0, 9999).ToString();
            string roleName2 = rnd.Next(0, 9999).ToString();
            string roleName3 = rnd.Next(0, 9999).ToString();

            int userId = rnd.Next(0, 9999);

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
                                Name = "Devices",
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
                        Name = roleName3,
                        Resources = new Resource[]
                        {
                            new Resource()
                            {
                                Name = "Containers",
                                Permissions = new string[] { "View" }
                            }
                        }
                    }
                }
            );

            _acl.AddRoleParent(roleName2, roleName3);
            _acl.AddUserRoles(userId, new string[] { roleName, roleName2 });

            string[] permissions = _acl.AllowedPermissions(userId, "Containers");
            Assert.AreEqual(4, permissions.Length);
        }
    }
}
