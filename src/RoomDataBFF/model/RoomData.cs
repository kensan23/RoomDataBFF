
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace RoomDataBFF.Models
{
    [DynamoDBTable("RoomData")]
    public class RoomData
    {
        [DynamoDBHashKey("roomid")]
        public string RoomId { get; set; }

        [DynamoDBRangeKey("datetimeunix")]
        public double DateTimeUnix { get; set; }

        [DynamoDBProperty("airquality_percent")]
        public decimal AirQualityPercent { get; set; }

        [DynamoDBProperty("gasresistance_Ohms")]
        public decimal GasResistanceOhms { get; set; }

        [DynamoDBProperty("humidity_percent")]
        public decimal HumidityPercent { get; set; }

        [DynamoDBProperty("pressure_hpa")]
        public decimal PressureHpa { get; set; }

        [DynamoDBProperty("temperature_C")]
        public decimal TemperatureCelsius { get; set; }

    }
}