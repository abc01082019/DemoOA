﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.OA.Common.Cache
{
    public interface ICacheWriter
    {
        void AddCache(string key, object value, DateTime expDate);

        void AddCache(string key, object value);

        object GetCache(string key);

        T GetCache<T>(string key);
    }
}