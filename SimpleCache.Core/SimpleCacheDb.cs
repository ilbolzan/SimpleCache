using System;
using System.Collections.Concurrent;

namespace SimpleCache.Core
{
    public class SimpleCacheDb
    {
        private ConcurrentDictionary<String,String> _dictionaryDb = new ConcurrentDictionary<String,String>();

        public bool Set(string name, string value)
        {
            return _dictionaryDb.TryAdd(name, value);
        }

        public string Get(string name)
        {
            string value;
            bool exists = _dictionaryDb.TryGetValue(name, out value);
            return value; 
        }
    }
}
