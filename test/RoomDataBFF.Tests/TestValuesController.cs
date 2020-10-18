using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using RoomDataBFF.Models;
using Xunit;

namespace RoomDataBFF.Tests
{
    public class TestValuesController : IClassFixture<WebApplicationFactory<Startup>>
    {
        public TestValuesController(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private readonly WebApplicationFactory<Startup> _factory;

        [Fact]
        public async Task TestSomeMoo()
        {
            using var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IRoomDataService, TestValuesService>();
                });
            }).CreateClient();

            var index = await client.GetStringAsync("/api/values");
            Assert.Equal("[\"value1\"]", index);
        }
    }

    public class TestValuesService : IRoomDataService
    {
        public Task<IEnumerable<RoomData>> GetRoomDataByIdAndDateUTC(string roomId, System.DateTime fromUtc, System.DateTime toUtc)
        {
            throw new System.NotImplementedException();
        }

        Task<IEnumerable<RoomData>> IRoomDataService.GetRoomDataByInterval()
        {
            throw new System.NotImplementedException();
        }
    }
}