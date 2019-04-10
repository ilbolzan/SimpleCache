using System;
using System.Collections.Concurrent;

namespace SimpleCache.Core
{
    public class SimpleCacheDb
    {
        private ConcurrentDictionary<String,ExpirableValue<String>> _dictionaryDb = new ConcurrentDictionary<String,ExpirableValue<String>>();

        public bool Set(string key, string value)
        {
            return _dictionaryDb.TryAdd(key, new ExpirableValue<String>(value));
        }

        public bool Set(string key, string value, int expirationSeconds)
        {
            return _dictionaryDb.TryAdd(key, new ExpirableValue<String>(value, expirationSeconds));
        }

        public string Get(string key)
        {
            ExpirableValue<String> expirableValue;
            bool exists = _dictionaryDb.TryGetValue(key, out expirableValue);
            if(existsAndIsExpired(exists, expirableValue))
            {
                this.Del(key);
                expirableValue = new ExpirableValue<String>(null);
            }
            else if(!exists)
            {
                expirableValue = new ExpirableValue<String>(null);
            }
            return expirableValue.Value;
        }

        private bool existsAndIsExpired(bool exists, ExpirableValue<String> expirableValue){
            return exists && expirableValue.Expiration < DateTime.Now;
        }

        public bool Del(string key)
        {
            ExpirableValue<String> expirableValue;
            bool status = _dictionaryDb.TryRemove(key, out expirableValue);
            return status;
        }

        public string Incr(string key){
            var newValue = _dictionaryDb.AddOrUpdate(key, new ExpirableValue<String>("1"), (localKey, oldValue) => {
                var isInteger = int.TryParse(oldValue.Value, out int integerValue);
                if(!isInteger)
                {
                    throw new Exception($"Current value [{oldValue}] for key [{localKey}] is not an Integer");
                }
                integerValue++;
                return new ExpirableValue<String>(integerValue.ToString());
            });
            return newValue.Value;
        }
    }
}
