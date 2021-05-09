using System;
using System.Threading.Tasks;

namespace Core.Caching.Interfaces
{
    /// <summary>
    /// for long term caches, there is need of this interface for "The Liskov Substitution Principle". My be we will need other type of cache
    /// </summary>
    public interface IStandingCache : ICache
    {
        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>        
        /// <returns>The cached value associated with the specified key</returns>
        T Get<T>(string key, Func<T> callBack);

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Async function to load item if it's not in the cache yet</param>        
        /// <returns>The cached value associated with the specified key</returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> callBack);

        /// <summary>
        /// Adds the specified key and object to the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="data">Value for caching</param>
        /// <param name="cacheTime">Cache time in minutes</param>
        void Set(string key, object data);

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>True if item already is in cache; otherwise false</returns>
        bool IsSet(string key);

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        void Remove(string key);

        /// <summary>
        /// Clear cache
        /// </summary>
        void Clear();
    }
}
