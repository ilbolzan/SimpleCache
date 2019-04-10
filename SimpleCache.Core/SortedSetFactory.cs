using System;
using System.Collections.Generic;

namespace SimpleCache.Core {
    public static class SortedSetFactory {
        public static SortedSet<KeyValuePair<int, string>> GetSet(int score, string member) 
        {
            var sortedSet = new SortedSet<KeyValuePair<int, string>>(new ByScore());
            sortedSet.Add(GetItem(score, member)); 
            return sortedSet;
        }

        public static KeyValuePair<int, string> GetItem(int score, string member) 
        {
            return new KeyValuePair<int, string>(score, member);
        }
    }

    public class ByScore : IComparer<KeyValuePair<int,string>> {
        public int Compare (KeyValuePair<int,string> a, KeyValuePair<int,string> b) {
            return a.Key.CompareTo(b.Key);
        }
    }
}