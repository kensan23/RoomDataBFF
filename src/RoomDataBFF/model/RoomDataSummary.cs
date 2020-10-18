
using System;
using System.Collections.Generic;

namespace RoomDataBFF.Models
{
    public class RoomDataSummary
    {
        public string RoomId { get; set; }
        public int Month { get; set; }

        public int Year { get; set; }
        public decimal AirQualityPercentAverage { get; set; }

        public decimal GasResistanceOhmsAverage { get; set; }

        public decimal HumidityPercentAverage { get; set; }

        public decimal PressureHpaAverage { get; set; }

        public decimal TemperatureCelsiusAverage { get; set; }

    }
}