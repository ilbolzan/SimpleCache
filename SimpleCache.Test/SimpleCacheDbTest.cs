using System;
using SimpleCache.Core;
using Xunit;

namespace SimpleCache.Test
{
    public class SimpleCacheDbTest
    {
        [Theory]
        [InlineData("name", "Bob")]
        public void Set_WhenSettingAValue_ShouldReturnTrue(string key, string value)
        {
            // Arrange
            var db = new SimpleCacheDb();

            //Act
            bool result = db.Set(key, value);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("name", "Bob")]
        public void Get_WhenSettingValue_ShouldReturTheSameValue(string key, string value)
        {
            //Arrange
            var db = new SimpleCacheDb();

            //Act
            db.Set(key, value);
            string returnedValue = db.Get(key);

            //Assert
            Assert.Equal(value, returnedValue);
        }

        [Theory]
        [InlineData("name", "Bob")]
        public void Del_WhenDeletingAnExistentValue_ShouldReturTrue(string key, string value)
        {
            //Arrange
            var db = new SimpleCacheDb();

            //Act
            db.Set(key, value);
            bool returnedStatus = db.Del(key);

            //Assert
            Assert.True(returnedStatus);
        }

        [Theory]
        [InlineData("name")]
        public void Del_WhenDeletingAnNonExistentValue_ShouldReturTrue(string key)
        {
            //Arrange
            var db = new SimpleCacheDb();

            //Act
            bool returnedStatus = db.Del(key);

            //Assert
            Assert.False(returnedStatus);
        }
    }
}
