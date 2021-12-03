using System.Collections.Generic;

namespace Acl.Net
{
    internal static class Utils
    {
        public static T[] RemoveDuplicates<T>(T[] s)
        {
            HashSet<T> set = new HashSet<T>(s);
            T[] result = new T[set.Count];
            set.CopyTo(result);
            return result;
        }

        public static T[] JoinArray<T>(T[] arr1, T[] arr2)
        {
            var newArray = new T[arr1.Length + arr2.Length];
            arr1.CopyTo(newArray, 0);
            arr2.CopyTo(newArray, arr1.Length);

            return newArray;
        }
    }
}
