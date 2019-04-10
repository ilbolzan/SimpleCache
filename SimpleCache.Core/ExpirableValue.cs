using System;

namespace SimpleCache.Core 
{
    public class ExpirableValue
    {
        public string Value {get; internal set;}
        public DateTime Expiration {get; internal set;}

        public ExpirableValue(string value, int expirationSeconds){
            this.Value = value;
            this.Expiration = DateTime.Now.AddSeconds(expirationSeconds);
        }

        public ExpirableValue(string value){
            this.Value = value;
            this.Expiration = DateTime.MaxValue;
        }
    }
}