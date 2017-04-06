using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// MemoryCache缓存管理类(实现微软缓存机制 引用System.Runtime.Caching.dll)
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存健</param>
        /// <returns></returns>
        public virtual T Get<T>(string key)
        {
            return (T)MemoryCache.Default.Get(key);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存健</param>
        /// <param name="second">缓存时间</param>
        /// <param name="func">当取不到缓存时,设置缓存的方法</param>
        /// <returns></returns>
        public virtual T Get<T>(string key, int second, Func<T> func)
        {
            if (key == null)
            {
                throw new ArgumentNullException("参数key不能为空");
            }

            var setted = Exists(key);
            if (setted)
            {
                return Get<T>(key);
            }

            var result = func();
            if (result != null)
            {
                Set(key, result, second);
            }

            return (T)result;
        }
        /// <summary>
        /// 获取缓存(默认缓存60秒)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存健</param>
        /// <param name="func">当取不到缓存时,设置缓存的方法</param>
        /// <returns></returns>
        public virtual T Get<T>(string key, Func<T> func)
        {
            return Get<T>(key, 60, func);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存健</param>
        /// <param name="data">缓存值</param>
        /// <param name="seconds">缓存时间</param>
        public virtual void Set<T>(string key, T data, int seconds)
        {
            if (key == null)
            {
                throw new ArgumentNullException("参数key不能为空");
            }

            if (data != null)
            {
                throw new ArgumentNullException("参数data不能为空");
            }

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromSeconds(seconds);
            MemoryCache.Default.Add(new CacheItem(key, data), policy);

        }

        /// <summary>
        /// 是否已经设置缓存
        /// </summary>
        /// <param name="key">缓存健</param>
        /// <returns></returns>
        public virtual bool Exists(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存健</param>
        public virtual void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }
        /// <summary>
        /// 根据键值对的模式删除缓存
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        public virtual void RemoveByPattern(string pattern)
        {
            foreach (string key in GetCacheKeysByPattern(pattern))
            {
                this.Remove(key);
            }
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public virtual void Clear()
        {
            foreach (var item in MemoryCache.Default)
            {
                this.Remove(item.Key);
            }
        }


        /// <summary>
        /// 根据键值对的模式获取缓存
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        private IEnumerable<string> GetCacheKeysByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (var item in MemoryCache.Default)
            {
                if (regex.IsMatch(item.Key))
                {
                    yield return item.Key;
                }
            }
        }
    }
}
