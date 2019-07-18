using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.Common.Cache
{
    public class CacheHelper
    {
        // Use Spring.Net direct inject a Cache object to it
        private static ICacheWriter CacheWriter { get; set; }
        public static void Addcache(string key, object value, DateTime expDate)
        {
            // 往缓存写: 单机 或者 分布式
            CacheWriter.AddCache(key, value, expDate);
        }

        public static object GetCache(string key)
        {
            return null;
        }
    }
}
