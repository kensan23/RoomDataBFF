using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using RoomDataBFF.Models;

namespace RoomDataBFF.Repository
{
    public interface IRoomDataRepository : IDisposable
    {

        Task<RoomData> GetRoomDataAsync(string RoomId, double UnixDateTime);
        Task<IEnumerable<RoomData>> GetRoomDataByIdAndUnixDateAsync(string roomId, double fromUnixDateTime, double toUnixDateTime);
    }

    public class DynamoRoomDataRepository : IRoomDataRepository, IDisposable
    {

        private IDynamoDBContext _dynamoContext;
        public DynamoRoomDataRepository(IDynamoDBContext dynamoContext)
        {
            this._dynamoContext = dynamoContext;
        }

        public async Task<RoomData> GetRoomDataAsync(string RoomId, double UnixDateTime)
        {
            var returnData = await _dynamoContext.LoadAsync<RoomData>(RoomId, UnixDateTime);
            return returnData;

        }
        public async Task<IEnumerable<RoomData>> GetRoomDataByIdAndUnixDateAsync(string roomId, double fromUnixDateTime, double toUnixDateTime)
        {
            var latestReplies =
                   await _dynamoContext.QueryAsync<RoomData>(roomId,
                    QueryOperator.Between,
                     new object[] { fromUnixDateTime, toUnixDateTime }
                     ).GetRemainingAsync();
            return latestReplies;
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dynamoContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}