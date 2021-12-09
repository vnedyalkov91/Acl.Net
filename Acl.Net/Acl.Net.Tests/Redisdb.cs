using Acl.Net.Backends;
using Acl.Net.Interfaces;
using Acl.Net.Tests.Methods;
using NUnit.Framework;
using StackExchange.Redis;

namespace Acl.Net.Tests
{
    [TestFixture]
    public class RedisDataabse
    {
        private const int _databaseIndex = 1;
        private const string _url = "localhost,allowAdmin=true";
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_url);
        private static IBackend backend = new Redisdb(redis, _databaseIndex);
        private static IAcl acl = new Acl(backend);

        [SetUp]
        public void SetUp()
        {
            var endpoint = redis.GetEndPoints(true);
            IServer server = redis.GetServer(redis.GetEndPoints(true)[0]);
            server.FlushDatabase(_databaseIndex);
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
        public void Initialize_Redis_Backend()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_url);
            IBackend backend = new Redisdb(redis, _databaseIndex);
            IAcl acl = new Acl(backend);

            Assert.Pass();
        }
    }
}
