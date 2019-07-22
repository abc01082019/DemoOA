using Spring.Context;
using Spring.Context.Support;
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
        // CacheWriter can be create by MemcacheWriter or HttpRuntimeCacheWriter
        // Problem: CacheWriter is static, it means if Spring didn't inject an object to it before we use it, CacheWriter will be empty.
        private static ICacheWriter CacheWriter { get; set; }

        static CacheHelper()
        {
            // Create object using Spring .Net
            // Init container
            IApplicationContext ctx = ContextRegistry.GetContext();
            // Inject to CacheWriter via. Spring .Net
            ctx.GetObject("CacheWriter");
            // Or you can do it in another way
            //CacheWriter = ctx.GetObject("MemcacheWriter") as ICacheWriter;
        }

        public static void AddCache(string key, object value, DateTime expDate)
        {
            // 往缓存写: 单机 或者 分布式
            CacheWriter.AddCache(key, value, expDate);
        }

        public static void AddCache(string key, object value)
        {
            CacheWriter.AddCache(key, value);
        }

        public static object GetCache(string key)
        {
            return CacheWriter.GetCache(key);
        }

        public static void SetCache(string key, object value, DateTime expDate)
        {
            CacheWriter.SetCache(key, value, expDate);
        }

        public static void SetCache(string key, object value)
        {
            CacheWriter.SetCache(key, value);
        }
    }
}
