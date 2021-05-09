using Core.Caching.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Caching
{
    public class InMemoryCache : IStandingCache
    {
        #region Fields

        private readonly IMemoryCache _cache;

        /// <summary>
        /// keys
        /// </summary>        
        private static readonly ConcurrentDictionary<string, bool> _Keys;

        /// <summary>
        /// Cancellation token for clear cache
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        #endregion

        #region Ctor

        static InMemoryCache()
        {
            _Keys = new ConcurrentDictionary<string, bool>();
        }

        public InMemoryCache(IMemoryCache cache)
        {
            this._cache = cache;
            this._cancellationTokenSource = new CancellationTokenSource();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Create entry options to item of memory cache
        /// </summary>
        /// <param name="cacheTime">Cache time</param>
        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions(TimeSpan cacheTime)
        {
            var options = new MemoryCacheEntryOptions()
                // add cancellation token for clear cache
                .AddExpirationToken(new CancellationChangeToken(this._cancellationTokenSource.Token))
                //add post eviction callback
                .RegisterPostEvictionCallback(this.PostEviction);

            //set cache time
            options.AbsoluteExpirationRelativeToNow = cacheTime;

            return options;
        }

        /// <summary>
        /// Add key to dictionary
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>Itself key</returns>
        private string AddKey(string key)
        {
            _Keys.TryAdd(key, true);
            return key;
        }

        /// <summary>
        /// Remove key from dictionary
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>Itself key</returns>
        private string RemoveKey(string key)
        {
            this.TryRemoveKey(key);
            return key;
        }

        /// <summary>
        /// Try to remove a key from dictionary, or mark a key as not existing in cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        private void TryRemoveKey(string key)
        {
            //try to remove key from dictionary
            if (!_Keys.TryRemove(key, out _))
            {
                //if not possible to remove key from dictionary, then try to mark key as not existing in cache
                _Keys.TryUpdate(key, false, true);
            }
        }

        /// <summary>
        /// Remove all keys marked as not existing
        /// </summary>
        private void ClearKeys()
        {
            foreach (var key in _Keys.Where(p => !p.Value).Select(p => p.Key).ToList())
            {
                this.RemoveKey(key);
            }
        }

        /// <summary>
        /// Post eviction
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="value">Value of cached item</param>
        /// <param name="reason">Eviction reason</param>
        /// <param name="state">State</param>
        private void PostEviction(object key, object value, EvictionReason reason, object state)
        {
            //if cached item just change, then nothing doing
            if (reason == EvictionReason.Replaced)
            {
                return;
            }

            //try to remove all keys marked as not existing
            this.ClearKeys();

            //try to remove this key from dictionary
            this.TryRemoveKey(key.ToString());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="callBack">Function to load item if it's not in the cache yet</param>        
        /// <returns>The cached value associated with the specified key</returns>
        public T Get<T>(string key, Func<T> callBack)
        {
            //item already is in cache, so return it
            if (this._cache.TryGetValue(key, out T value))
            {
                return value;
            }
            //or create it using passed function
            var result = callBack();

            return result;
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="callBack">Async function to load item if it's not in the cache yet</param>      
        /// <returns>The cached value associated with the specified key</returns>
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> callBack)
        {
            //item already is in cache, so return it
            if (this._cache.TryGetValue(key, out T value))
            {
                return value;
            }
            //or create it using passed function
            var result = await callBack();

            return result;
        }

        /// <summary>
        /// Adds the specified key and object to the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="data">Value for caching</param>        
        public void Set(string key, object data)
        {
            if (data != null)
            {
                this._cache.Set(this.AddKey(key), data);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>True if item already is in cache; otherwise false</returns>
        public bool IsSet(string key)
        {
            return this._cache.TryGetValue(key, out var _);
        }

        /// <summary>
        /// Perform some action with exclusive in-memory lock
        /// </summary>
        /// <param name="key">The key we are locking on</param>
        /// <param name="expirationTime">The time after which the lock will automatically be expired</param>
        /// <param name="action">Action to be performed with locking</param>
        /// <returns>True if lock was callBackd and action was performed; otherwise false</returns>
        public bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action)
        {
            //ensure that lock is callBackd
            if (!_Keys.TryAdd(key, true))
            {
                return false;
            }

            try
            {
                this._cache.Set(key, key, this.GetMemoryCacheEntryOptions(expirationTime));

                //perform action
                action();

                return true;
            }
            finally
            {
                //release lock even if action fails
                this.Remove(key);
            }
        }

        /// <summary>
        /// Perform some action with exclusive in-memory lock
        /// </summary>
        /// <param name="key">The key we are locking on</param>
        /// <param name="expirationTime">The time after which the lock will automatically be expired</param>
        /// <param name="action">Action to be performed with locking</param>
        /// <returns>True if lock was callBackd and action was performed; otherwise false</returns>
        public async Task<bool> PerformActionWithLockAsync(string key, TimeSpan expirationTime, Func<Task> action)
        {
            //ensure that lock is callBackd
            if (!_Keys.TryAdd(key, true))
            {
                return false;
            }

            try
            {
                this._cache.Set(key, key, this.GetMemoryCacheEntryOptions(expirationTime));

                //perform action
                await action();

                return true;
            }
            finally
            {
                //release lock even if action fails
                this.Remove(key);
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        public void Remove(string key)
        {
            this._cache.Remove(this.RemoveKey(key));
        }

        /// <summary>
        /// Removes items by key pattern
        /// </summary>
        /// <param name="pattern">String key pattern</param>
        public void RemoveByPrefix(string pattern)
        {
            //get cache keys that matches pattern
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matchesKeys = _Keys.Where(p => p.Value).Select(p => p.Key).Where(key => regex.IsMatch(key)).ToList();

            //remove matching values
            foreach (var key in matchesKeys)
            {
                this._cache.Remove(this.RemoveKey(key));
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public void Clear()
        {
            //send cancellation request
            this._cancellationTokenSource.Cancel();

            //releases all resources used by this cancellation token
            this._cancellationTokenSource.Dispose();

            //recreate cancellation token
            this._cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Dispose cache manager
        /// </summary>
        public void Dispose()
        {
            //nothing special
        }
        #endregion
    }
}
