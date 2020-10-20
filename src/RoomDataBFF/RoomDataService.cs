using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using RoomDataBFF.Models;
using RoomDataBFF.Repository;

namespace RoomDataBFF
{
    public interface IRoomDataService
    {
        Task<IEnumerable<RoomData>> GetRoomDataByInterval();
        Task<IEnumerable<RoomData>> GetRoomDataByIdAndDateUTC(string roomId, DateTime fromUtc, DateTime toUtc);
    }

    public class RoomDataService : IRoomDataService
    {

        private IRoomDataRepository _roomDataRepository;
        public RoomDataService(IRoomDataRepository roomDataRepository)
        {
            this._roomDataRepository = roomDataRepository;
        }

        public async Task<IEnumerable<RoomData>> GetRoomDataByInterval()
        {
            //AmazonDynamoDBClient client = new AmazonDynamoDBClient();

            //   DynamoDBContext context = new DynamoDBContext(client);

            return await GetRoomDataByIdAndDateUTC("StudyNorth", DateTime.Now, DateTime.Now);

        }
        public async Task<IEnumerable<RoomData>> GetRoomDataByIdAndDateUTC(string roomId, DateTime fromUtc, DateTime toUtc)
        {

            double fromUnixTime = new DateTimeOffset(fromUtc).ToUnixTimeSeconds();
            double toUnixTime = new DateTimeOffset(toUtc).ToUnixTimeSeconds();
            var latestReplies = await _roomDataRepository.GetRoomDataByIdAndUnixDateAsync(roomId, fromUnixTime, toUnixTime);
            return latestReplies;
        }

        public async Task<IEnumerable<RoomData>> GetRoomDataByIdAndDateUnix(string roomId, double fromUtc, double toUtc)
        {
            var latestReplies = await _roomDataRepository.GetRoomDataByIdAndUnixDateAsync(roomId, fromUtc, toUtc);
            return latestReplies;
        }

        public async Task<IEnumerable<RoomDataSummary>> GetRoomDataSummaryByDateUTC(string roomId, DateTime fromUtc, DateTime toUtc, Func<RoomData, object> groupProperty)
        {
            var data = await GetRoomDataByIdAndDateUTC(roomId, fromUtc, toUtc);
            return GetRoomSummaryData(data, groupProperty);
        }

        public async Task<IEnumerable<RoomDataSummary>> GetRoomDataSummaryByUnixDate(string roomId, double fromUtc, double toUtc, Func<RoomData, object> groupProperty)
        {
            var data = await GetRoomDataByIdAndDateUnix(roomId, fromUtc, toUtc);
            return GetRoomSummaryData(data, groupProperty);
        }

        private static IEnumerable<RoomDataSummary> GetRoomSummaryData(IEnumerable<RoomData> roomData, Func<RoomData, object> groupProperty)
        {
            return roomData
           .GroupBy(groupProperty)
           .Select(x =>
            new RoomDataSummary
            {
                RoomId = x.FirstOrDefault().RoomId,
                Month = x.FirstOrDefault().DateTimeUTC.Month,
                Year = x.FirstOrDefault().DateTimeUTC.Year,
                AirQualityPercentAverage = x.Average(a => a.AirQualityPercent),
                GasResistanceOhmsAverage = x.Average(a => a.GasResistanceOhms),
                HumidityPercentAverage = x.Average(a => a.HumidityPercent),
                PressureHpaAverage = x.Average(a => a.PressureHpa),
                TemperatureCelsiusAverage = x.Average(a => a.TemperatureCelsius),
            });
        }

    }


}