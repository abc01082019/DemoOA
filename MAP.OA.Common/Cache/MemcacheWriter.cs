using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.Common.Cache
{
    public class MemcacheWriter : ICacheWriter
    {
        private MemcachedClient memcacheClient;
        public MemcacheWriter()
        {
            //分布Memcachedf服务IP 端口
            //string[] servers = { "192.168.1.9:11211", "192.168.202.128:11211" };

            // Get the servers setting from 'Web.config' file
            string strAppMemcachedServer = System.Configuration.ConfigurationManager.AppSettings["MemcachedServerList"];
            string[] servers = strAppMemcachedServer.Split(',');

            // Init Sock pool
            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            memcacheClient = new Memcached.ClientLibrary.MemcachedClient();
            memcacheClient.EnableCompression = false;
        }

        public void AddCache(string key, object value, DateTime expDate)
        {
            memcacheClient.Add(key, value, expDate);
        }

        public void AddCache(string key, object value)
        {
            memcacheClient.Add(key, value);
        }

        public object GetCache(string key)
        {
            return memcacheClient.Get(key);
        }

        public T GetCache<T>(string key)
        {
            return (T)memcacheClient.Get(key);
        }

        public void SetCache(string key, object value, DateTime expDate)
        {
            memcacheClient.Set(key, value, expDate);
        }

        public void SetCache(string key, object value)
        {
            memcacheClient.Set(key, value);
        }
    }
}
