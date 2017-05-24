using LazyCache;
using System;

namespace Data
{
    public static class CacheManagement<T>
    {
        static IAppCache cache = new CachingService();
        public static T AddGetItem(string keyName, T value)
        {
 
           return cache.GetOrAdd(keyName,
               () => value ,
               DateTimeOffset.Now.AddMinutes(5));
        }
        public static T GetItem(string keyName)
        {
            return cache.Get<T>(keyName);
        }
        public static void Remove(string keyName)
        {
            cache.Remove(keyName);
        }
    }
}
