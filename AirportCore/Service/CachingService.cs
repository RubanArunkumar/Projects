using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace AirportCore.Services
{
    public class CachingService : ICachingService
    {

        public bool IsCacheExists(string key)
        {
            var value = MemoryCache.Default.FirstOrDefault(x => x.Key == key).Value;
            if (value != null)
                return true;
            return false;
        }
        public T Get<T>(string key) where T : class
        {
            return MemoryCache.Default.Get(key) as T;
        }

        public void Put<T>(string key, T item, CacheExpiryTime cacheExpiryTime)
        {
            MemoryCache.Default.Add(key, item, DateTime.Now.AddSeconds((int)cacheExpiryTime));
        }

        public T GetOrPut<T>(string key, Func<T> func, CacheExpiryTime cacheExpiryTime) where T : class
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            var result = this.Get<T>(key) as T;
            if (result != null)
            {
                return result;
            }

            result = func.Invoke();
            MemoryCache.Default.Add(key, result, DateTime.Now.AddSeconds((int)cacheExpiryTime));

            return result;
        }

        public bool Remove(string key)
        {
            return MemoryCache.Default.Remove(key) != null;
        }

    }
    public enum CacheExpiryTime
    {
        NoCache = -1,
        FiveSeconds = 5,
        OneMinute = 60,
        FiveMinutes = 300,
        TenMinutes = 600,
        FifteenMinutes = 900,
        ThirtyMinutes = 1800,
        OneHour = 3600,
        TwoHours = 7200,
        FourHours = 14400,
        EightHours = 28800,
        OneDay = 86400
    }
    public enum CachingLevel
    {
        User,

        Global
    }
}
