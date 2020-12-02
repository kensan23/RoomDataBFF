using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoomDataBFF.Models;

namespace RoomDataBFF
{
    [Route("api/[controller]")]
    public class RoomDataController : ControllerBase
    {
        private readonly IRoomDataService _roomService;

        public RoomDataController(IRoomDataService roomservice)
        {
            _roomService = roomservice;
        }

        // GET api/values
        [HttpGet("{roomId}")]
        public async Task<IEnumerable<RoomData>> GetByIdDate(string roomId, DateTime fromDateTimeUtc, DateTime toDateTimeUtc)
        {
            System.Console.WriteLine(roomId);
            return await _roomService.GetRoomDataByIdAndDateUTC(roomId, fromDateTimeUtc, toDateTimeUtc);
        }

        [HttpGet("{roomId}")]
        public async Task<IEnumerable<RoomData>> GetByIdDateUnix(string roomId, double fromDateTimeUnix, double toDateTimeUnix)
        {
            System.Console.WriteLine(roomId);
            return await _roomService.GetRoomDataByIdAndDateUnix(roomId, fromDateTimeUnix, toDateTimeUnix);
        }
    }
}