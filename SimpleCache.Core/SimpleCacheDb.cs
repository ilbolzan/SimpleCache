using System;
using System.Collections.Concurrent;

namespace SimpleCache.Core
{
    public class SimpleCacheDb
    {
        private ConcurrentDictionary<String,ExpirableValue> _dictionaryDb = new ConcurrentDictionary<String,ExpirableValue>();

        public bool Set(string key, string value)
        {
            return _dictionaryDb.TryAdd(key, new ExpirableValue(value));
        }

        public bool Set(string key, string value, int expirationSeconds)
        {
            return _dictionaryDb.TryAdd(key, new ExpirableValue(value, expirationSeconds));
        }

        public string Get(string key)
        {
            ExpirableValue expirableValue;
            bool exists = _dictionaryDb.TryGetValue(key, out expirableValue);
            if(existsAndIsExpired(exists, expirableValue))
            {
                this.Del(key);
                expirableValue = new ExpirableValue(null);
            }
            else if(!exists)
            {
                expirableValue = new ExpirableValue(null);
            }
            return expirableValue.Value;
        }

        private bool existsAndIsExpired(bool exists, ExpirableValue expirableValue){
            return exists && expirableValue.Expiration < DateTime.Now;
        }

        public bool Del(string key)
        {
            ExpirableValue expirableValue;
            bool status = _dictionaryDb.TryRemove(key, out expirableValue);
            return status;
        }

        public string Incr(string key){
            string value = this.Get(key);
            if(value == null)
            {
                value = "1";
                this.Set(key, value);
            }
            return value;
        }
    }
}
