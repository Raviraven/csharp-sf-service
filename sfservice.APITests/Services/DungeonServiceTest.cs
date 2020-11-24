using sfservice.API.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace sfservice.APITests.Services
{
    public class DungeonServiceTest
    {
        [Fact]
        public void GetDungeons_ReturnsListOfDungeons()
        {
            var service = new DungeonService();
            var results = service.GetDungeons();

            Assert.NotNull(results);
            Assert.True(results.Count > 0);
        }
    }
}
