using System;
using CacheManager.Core;

namespace RESTFul.CacheManager
{
    public static class CacheDataManager<T>
    {
        public static void AddItem(string keyName, T data)
        {
            var cache = CacheFactory.Build<T>("EasyNETFramework", settings =>
            {
                settings
                    .WithSystemRuntimeCacheHandle("handle1");
            });
            var item = new CacheItem<T>(
                keyName, data, ExpirationMode.Absolute, TimeSpan.FromMinutes(15));
            cache.Add(item);
        }
        public static void AddItem(string keyName, object data)
        {
            var cache = CacheFactory.Build<object>("EasyNETFramework", settings =>
            {
                settings
                    .WithSystemRuntimeCacheHandle("handle1");
            });
            var item = new CacheItem<object>(
                keyName, data, ExpirationMode.Absolute, TimeSpan.FromMinutes(15));
            cache.Add(item);
        }

        public static T GetItem(string keyName)
        {
            var cache = CacheFactory.Build<T>("EasyNETFramework", settings =>
            {
                settings
                    .WithSystemRuntimeCacheHandle("handle1");
            });
            return cache.Get(keyName);
        }
        public static object GetItem(object keyName)
        {
            var cache = CacheFactory.Build<object>("EasyNETFramework", settings =>
            {
                settings
                    .WithSystemRuntimeCacheHandle("handle1");
            });
            return cache.Get(keyName.ToString());
        }
    }
}
