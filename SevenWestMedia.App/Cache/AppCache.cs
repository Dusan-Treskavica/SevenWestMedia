using System.Collections.Concurrent;
using SevenWestMedia.App.Cache.Interface;

namespace SevenWestMedia.App.Cache
{
    public class AppCache : IAppCache
    {
        private ConcurrentDictionary<string, object> _cacheData = new();
        
        public object GetDataForKey(string cacheKey)
        {
            return _cacheData.TryGetValue(cacheKey, out object value) ? value : null;

        }

        public void StoreData(string cacheKey, object data)
        {
            _cacheData[cacheKey] = data;
        }

    }
}