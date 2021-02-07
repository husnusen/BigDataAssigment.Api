using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BigDataAssigment.Api.ApiClients.Models
{
    public class DailyForecast
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("data")]
        public IList<DailyForecastData> Data { get; set; }
       

    }
}
