using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Infrastructure.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存健</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存健</param>
        /// <param name="data">缓存值</param>
        /// <param name="seconds">缓存时间</param>
        void Set<T>(string key, T data, int seconds);

        /// <summary>
        /// 是否已经设置缓存
        /// </summary>
        /// <param name="key">缓存健</param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存健</param>
        void Remove(string key);
        /// <summary>
        /// 根据键值对的模式删除缓存
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();
    }
}
