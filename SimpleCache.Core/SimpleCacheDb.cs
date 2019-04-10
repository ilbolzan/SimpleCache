using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SimpleCache.Core
{
    public class SimpleCacheDb
    {
        private ConcurrentDictionary<String,ExpirableValue> _dictionaryDb = new ConcurrentDictionary<String,ExpirableValue>();
        private ConcurrentDictionary<String,SortedSet<KeyValuePair<int, string>>> _dictionarySetDb = new ConcurrentDictionary<String,SortedSet<KeyValuePair<int, string>>>();

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
            var newValue = _dictionaryDb.AddOrUpdate(key, new ExpirableValue("1"), (localKey, oldValue) => {
                var isInteger = int.TryParse(oldValue.Value, out int integerValue);
                if(!isInteger)
                {
                    throw new Exception($"Current value [{oldValue}] for key [{localKey}] is not an Integer");
                }
                integerValue++;
                return new ExpirableValue(integerValue.ToString());
            });
            return newValue.Value;
        }

        public bool Zadd(string key, int score, string member)
        {
            var value = _dictionarySetDb.AddOrUpdate(key, SortedSetFactory.GetSet(score, member), (localKey, oldValue) => {
                oldValue.Add(SortedSetFactory.GetItem(score, member));
                return oldValue;
            });
            return value != null;
        }

        public int Zcard(string key)
        {
            bool exists = _dictionarySetDb.TryGetValue(key, out var expirableValue);
            if(exists) {
                return expirableValue.Count + 1;
            }
            // Doesnt extists
            return 0;
        }

        // public int Zrank(string key, string member)
        // {
        //     _dictionarySetDb.TryGetValue(key, out var set);
        //     int rank;
        //     if(exists) {
        //         foreach(var item in set.GetEnumerator())
        //         {
        //             rank++;
        //             if(item.Value == member)
        //             {
        //                 break;
        //             }
        //         }
        //         return rank;
        //     }
        //     else
        //     {
        //         throw new Exception ()
        //     }
        //     return rank;
        // }
    }
}
