using System;
using BigDataAssigment.Api.Extensions;
using Newtonsoft.Json;

namespace BigDataAssigment.Api.ApiClients.Models
{
    public class DailyForecastData
    {
        [JsonProperty("time")]
        public long TimeOffset { get; set; }

        [JsonProperty("dateTime")]
        public DateTime CurrentDateTime => TimeOffset.GetDateTime();

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("precipType")]
        public string PrecipType { get; set; }

       
        [JsonProperty("temperatureMin")]
        public float TemperatureMin { get; set; }

        [JsonProperty("temperatureMax")]
        public float TemperatureMax { get; set; }

        [JsonProperty("temperatureHigh")]
        public float TemperatureHigh { get; set; }

        [JsonProperty("temperatureHighTime")]
        public long TemperatureHighTimeOffset { get; set; }

        [JsonProperty("temperatureHighDateTime")]
        public DateTime TemperatureHighDateTime => TemperatureHighTimeOffset.GetDateTime();

        [JsonProperty("temperatureLow")]
        public float TemperatureLow { get; set; }

        [JsonProperty("temperatureLowTime")]
        public long TemperatureLowTimeOffset { get; set; }

        [JsonProperty("temperatureLowDateTime")]
        public DateTime TemperatureLowDateTime => TemperatureLowTimeOffset.GetDateTime();

    }
}
