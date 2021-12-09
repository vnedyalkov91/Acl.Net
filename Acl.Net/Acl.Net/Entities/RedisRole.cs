using StackExchange.Redis;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Acl.Net.Entities
{
    public class RedisRole: Role
    {
        public HashEntry[] Serialize()
        {
            HashEntry[] data = new HashEntry[] {
                new HashEntry("Name", Encoding.UTF8.GetBytes(Name)),
                new HashEntry("Resources", Encoding.UTF8.GetBytes(JsonSerializer.Serialize(Resources))),
                new HashEntry("Parents", Encoding.UTF8.GetBytes(JsonSerializer.Serialize(Parents)))
            };

            return data;
        }

        public void SetDeserializeName(string value)
        {
            SetObjectProperty("Name", value, this);
        }

        public void SetDeserializeResources(string value)
        {
            SetObjectProperty("Resources", JsonSerializer.Deserialize<Resource[]>(value), this);
        }

        public void SetDeserializeParents(string value)
        {
            SetObjectProperty("Parents", JsonSerializer.Deserialize<string[]>(value), this);
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
