using Acl.Net.Backends;
using Acl.Net.Interfaces;
using Acl.Net.Tests.Methods;
using MongoDB.Driver;
using NUnit.Framework;

namespace Acl.Net.Tests
{
    [TestFixture]
    public class MongodbTests
    {
        private const string _mongoUrl = "mongodb://localhost:27017/Acl";
        private const string _databaseName = "Acl";
        private static MongoClient db = new MongoClient(_mongoUrl);
        private static IBackend backend = new Mongodb(db, _databaseName);
        private static IAcl acl = new Acl(backend);

        [SetUp]
        public void SetUp()
        {
            db.DropDatabase(_databaseName);
        }

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
        public void Initialize_Mongo_Backend()
        {
            IBackend backend = new Backends.Mongodb(db, _databaseName);
            new Acl(backend);

            Assert.Pass();
        }

        [Test]
        public void Initialize_Mongo_With_Prefix()
        {
            IBackend backend = new Backends.Mongodb(db, _databaseName, "SMP");
            new Acl(backend);

            Assert.Pass();
        }
    }
}
