using System;
using System.ComponentModel.DataAnnotations;

namespace BigDataAssigment.Api.Entities
{
    public class Forecast
    {
        public int Id { get; set; }

        public string LocationId { get; set; }

        public DateTime QueryDateTime { get; set; }

        public string Summary { get; set; }

        public float CurrentTemperature { get; set; }

        public float TodayMinTemperature { get; set; }

        public float TodayMaxTemperature { get; set; }

        public float MinTemperatureWeekly { get; set; }

        public float MaxTemperatureWeekly { get; set; }

        public Location Location { get; set; }

    }
}
