
using System;
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

        public DateTime DateTimeUTC
        {
            get
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(DateTimeUnix);
            }
            set { this.DateTimeUTC = value; }
        }

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