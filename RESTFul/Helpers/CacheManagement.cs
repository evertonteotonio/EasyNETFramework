using LazyCache;
using System;
using System.Collections.Generic;
using EFN.Data;

namespace RESTFul.Helpers
{
    public static class CacheManagement<T>
    {
        static IAppCache cache = new CachingService();
        public static T AddGetItem(string keyName, T value)
        {
            LogHandler.Info($"Cache - Adding {keyName}");
            return cache.GetOrAdd(keyName,
                () => value,
                DateTimeOffset.Now.AddMinutes(5));
        }
        public static List<T> AddGetItemList(string keyName, List<T> value)
        {
            LogHandler.Info($"Cache - Adding {keyName}");
            return cache.GetOrAdd(keyName,
                () => value,
                DateTimeOffset.Now.AddMinutes(5));
        }
        public static T GetItem(string keyName)
        {
            LogHandler.Info($"Cache - Getting {keyName}");
            return cache.Get<T>(keyName);
        }
        public static List<T> GetItemList(string keyName)
        {
            LogHandler.Info($"Cache - Getting {keyName}");
            return cache.Get<List<T>>(keyName);
        }
        public static void Remove(string keyName)
        {
            LogHandler.Info($"Cache - Removeing {keyName}");
            cache.Remove(keyName);
        }
    }
}
