using Acl.Net.Backends;
using Acl.Net.Interfaces;
using Acl.Net.Tests.Methods;
using NUnit.Framework;

namespace Acl.Net.Tests
{
    [TestFixture]
    public class InMemorydb
    {
        private static IBackend backend = new InMemory();
        private static IAcl acl = new Acl(backend);

        [TestFixture]
        private class AllowMethod : Allow
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class RemoveAllowMethod : RemoveAllow
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class RemoveResourceMethod : RemoveResource
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class RemovePermissionMethod : RemovePermission
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class AddParentsMethod : AddParents
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class RemoveParentsMethod : RemoveParents
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class AddUserRoleMethod : AddUserRole
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class RemoveUserRoleMethod : RemoveUserRole
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class UserRolesMethod : UserRoles
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class RoleUsersMethod : RoleUsers
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class UserHasRolesMethod : UserHasRoles
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class AllowedPermissionsMethod : AllowedPermissions
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class IsAllowedMethod : IsAllowed
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [TestFixture]
        private class IsRoleAllowedMethod : IsRoleAllowed
        {
            public override void SetUp()
            {
                this._acl = acl;
            }
        }

        [Test]
        public void Initialize_InMemory_Backend()
        {
            IBackend backend = new Backends.InMemory();
            new Acl(backend);

            Assert.Pass();
        }
    }
}
