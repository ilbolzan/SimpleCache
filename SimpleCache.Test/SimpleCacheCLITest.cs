using System;
using SimpleCache.CLI;
using Xunit;

namespace SimpleCache.Test
{
    public class SimpleCacheCLITest
    {
        [Fact]
        public void Set_WhenSettingAValue_ShouldReturnTrue()
        {
            //Act
            string result = CLIParser.Cmd("SET name bob");

            //Assert
            Assert.Equal(result, "True");
        }

        
    }
}
