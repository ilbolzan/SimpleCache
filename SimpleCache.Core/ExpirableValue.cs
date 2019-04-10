using System;

namespace SimpleCache.Core 
{
    public class ExpirableValue<TValue>
    {
        public TValue Value {get; internal set;}
        public DateTime Expiration {get; internal set;}

        public ExpirableValue(TValue value, int expirationSeconds){
            this.Value = value;
            this.Expiration = DateTime.Now.AddSeconds(expirationSeconds);
        }

        public ExpirableValue(TValue value){
            this.Value = value;
            this.Expiration = DateTime.MaxValue;
        }
    }
}