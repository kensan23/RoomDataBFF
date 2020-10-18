using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RoomDataBFF.Models;
using RoomDataBFF.Repository;
using Xunit;
using Xunit.Abstractions;

namespace RoomDataBFF.Tests
{
    public class RoomDataServiceTest
    {
        private readonly IRoomDataService _roomService;
        private readonly ITestOutputHelper output;

        public RoomDataServiceTest(ITestOutputHelper output)
        {
            var config = GetConfiguration();
            var client = config.GetAWSOptions().CreateServiceClient<IAmazonDynamoDB>();
            var context = new DynamoDBContext(client);
            var roomDataRepository = new DynamoRoomDataRepository(context);
            _roomService = new RoomDataService(roomDataRepository);
            this.output = output;

        }
        static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        [Fact]
        public async Task TestSomeMoo()
        {
            var result = await _roomService.GetRoomDataByIdAndDateUTC("StudyNorth", DateTime.Now.AddHours(-1), DateTime.Now);
            //output.WriteLine($"*************{result.Count()}***************");
            foreach (var item in result)
            {
                output.WriteLine(JsonSerializer.Serialize<RoomData>(item));
            }
        }

        [Fact]
        public async Task TestDate()
        {
            //var expected = "10/17/2020 17:31:01";
            List<RoomData> data = new List<RoomData>();
            var startDate = 1571295921;
            for (int i = 0; i < 12; i++)
            {
                RoomData testData = new RoomData
                {
                    RoomId = "RoomId",
                    DateTimeUnix = startDate,
                    AirQualityPercent = 80m,
                    GasResistanceOhms = 50m,
                    HumidityPercent = 50m,
                    PressureHpa = 123m,
                    TemperatureCelsius = 0m

                };
                startDate += 2592000; //add one month
                data.Add(testData);
                //output.WriteLine(testData.DateTimeUTC.ToLocalTime().ToString());
            }
            startDate = 1571295921;

            for (int i = 0; i < 2; i++)
            {
                RoomData testData = new RoomData
                {
                    RoomId = "RoomId",
                    DateTimeUnix = startDate,
                    AirQualityPercent = 80m,
                    GasResistanceOhms = 50m,
                    HumidityPercent = 50m,
                    PressureHpa = 123m,
                    TemperatureCelsius = 100m

                };
                startDate += 2592000; //add one month
                data.Add(testData);
               // output.WriteLine(testData.DateTimeUTC.ToLocalTime().ToString());
            }
            var repo = new Mock<IRoomDataRepository>();
            repo.Setup(x =>x.GetRoomDataByIdAndUnixDateAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(data);

            var service = new RoomDataService(repo.Object);
            var result = await service.GetRoomDataSummaryByUnixDate("blah",1, 2);
            foreach (var group in result)
            {
                output.WriteLine(JsonSerializer.Serialize<RoomDataSummary>(group));
            }


        }
    }

    public class RoomDataTest
    {
        private readonly ITestOutputHelper output;
        public RoomDataTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void TestDateConvert()
        {
            var expected = "10/17/2020 17:31:01";

            RoomData testData = new RoomData
            {
                DateTimeUnix = 1602916261.174
            };

            Assert.Equal(expected, testData.DateTimeUTC.ToLocalTime().ToString());
        }
    }
}