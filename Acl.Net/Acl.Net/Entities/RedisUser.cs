using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Acl.Net.Entities
{
    public class RedisUser: User
    {
        public HashEntry[] Serialize()
        {
            HashEntry[] data = new HashEntry[] {
                new HashEntry("UserId", Encoding.UTF8.GetBytes(UserId)),
                new HashEntry("RoleNames", Encoding.UTF8.GetBytes(JsonSerializer.Serialize(RoleNames))),
            };

            return data;
        }

        public void SetDeserializeUserId(string value)
        {
            SetObjectProperty("UserId", value, this);
        }

        public void SetDeserializeRoleNames(string value)
        {
            SetObjectProperty("RoleNames", JsonSerializer.Deserialize<string[]>(value), this);
        }

        private void SetObjectProperty<T>(string propertyName, T value, object obj)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(obj, value, null);
            }
        }
    }
}
