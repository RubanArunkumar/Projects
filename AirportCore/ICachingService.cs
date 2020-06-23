using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportCore.Services
{
    public interface ICachingService
    {
        bool IsCacheExists(string key);
        T Get<T>(string key) where T : class;

        void Put<T>(string key, T item, CacheExpiryTime cacheExpiryTime);

        T GetOrPut<T>(string key, Func<T> func, CacheExpiryTime cacheExpiryTime) where T : class;

        bool Remove(string key);

    }
}
