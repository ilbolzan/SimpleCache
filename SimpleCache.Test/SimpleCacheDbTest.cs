using System;
using System.Threading;
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

        [Theory]
        [InlineData("name", "Bob", 1)]
        public void Set_WhenSettingValueWithExpiration_ShouldRespectExpirationTime(string key, string value, int expirationSeconds)
        {
            //Arrange
            var db = new SimpleCacheDb();

            //Act
            db.Set(key, value, expirationSeconds);
            string returnedValueBeforeExpiration = db.Get(key);
            Console.WriteLine(returnedValueBeforeExpiration);
            Thread.Sleep(1000);
            string returnedValueAfterExpiration = db.Get(key);

            //Assert
            Assert.Equal(value, returnedValueBeforeExpiration);
            Assert.Null(returnedValueAfterExpiration);
        }

        [Theory]
        [InlineData("nonExistentKey")]
        public void Get_WhenGettingAKeyThatDoesNotExists_ShouldReturnNull(string key)
        {
            //Arrange
            var db = new SimpleCacheDb();

            //Act
            string value = db.Get(key);

            //Assert
            Assert.Null(value);
        }

        [Theory]
        [InlineData("nonExistentKey", "1")]
        public void Incr_WhenIncrementingNonExistentKey_ShouldReturn1(string key, string expectedResult)
        {
            //Arrange
            var db = new SimpleCacheDb();

            //Act
            string value = db.Incr(key);

            //Assert
            Assert.Equal(expectedResult, value);
        }
    }
}
