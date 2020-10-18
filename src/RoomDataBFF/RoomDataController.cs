using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoomDataBFF.Models;

namespace RoomDataBFF
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IRoomDataService _values;

        public ValuesController(IRoomDataService values)
        {
            _values = values;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<RoomData>> Get()
        {
            return await _values.GetRoomDataByInterval();
        }

        // GET api/values/5

    }
}