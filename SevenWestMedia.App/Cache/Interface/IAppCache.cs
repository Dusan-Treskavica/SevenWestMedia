namespace SevenWestMedia.App.Cache.Interface
{
    public interface IAppCache
    {
        /// <summary>
        /// Gets the stored data for the given cache key.
        /// </summary>
        /// <param name="cacheKey">The cache key should not contain tenant name, it will be handled in the method</param>
        /// <returns></returns>
        object GetDataForKey(string cacheKey);
        
        /// <summary>
        /// Stores the data into tenant cache under the given key.
        /// </summary>
        /// <param name="cacheKey">Key under which data will be cached</param>
        /// <param name="data">Data</param>
        void StoreData(string cacheKey, object data);
    }
}