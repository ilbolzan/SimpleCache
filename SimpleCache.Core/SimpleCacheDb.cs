using System;
using System.Collections.Concurrent;

namespace SimpleCache.Core
{
    public class SimpleCacheDb
    {
        private ConcurrentDictionary<String,String> _dictionaryDb = new ConcurrentDictionary<String,String>();

        public bool Set(string key, string value)
        {
            return _dictionaryDb.TryAdd(key, value);
        }

        public string Get(string key)
        {
            string value;
            bool exists = _dictionaryDb.TryGetValue(key, out value);
            return value; 
        }

        public bool Del(string key)
        {
            string value;
            bool status = _dictionaryDb.TryRemove(key, out value);
            return status;
        }
    }
}
