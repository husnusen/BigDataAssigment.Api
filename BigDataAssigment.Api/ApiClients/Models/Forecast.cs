using System;
using Newtonsoft.Json;

namespace BigDataAssigment.Api.ApiClients.Models
{
    public class Forecast
    {
        [JsonProperty("currently")]
        public CurrentlyForecast CurrentlyForecast { get; set; }

        [JsonProperty("daily")]
        public DailyForecast Daily { get; set; }

        [JsonProperty("latitude")]
        public long Latitude { get; set; }

        [JsonProperty("longitude")]
        public long Longitude { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

    }
}
