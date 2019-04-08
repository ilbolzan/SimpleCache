using System;
using SimpleCache.Core;
using Xunit;

namespace SimpleCache.Test
{
    public class SimpleCacheDbTest
    {
        [Theory]
        [InlineData("name", "cache")]
        public void Set_WhenSettingAValue_ShouldReturnTrue(string name, string value)
        {
            // Arrange
            var db = new SimpleCacheDb();

            //Act
            bool result = db.Set(name, value);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("name", "cache")]
        public void Get_WhenSettingValue_ShouldReturTheSameValue(string name, string value)
        {
            //Arrange
            var db = new SimpleCacheDb();

            //Act
            db.Set(name, value);
            string returnedValue = db.Get("name");

            //Assert
            Assert.Equal(value, returnedValue);
        }
    }
}
