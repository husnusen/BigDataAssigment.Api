using System;
using BigDataAssigment.Api.Extensions;
using Newtonsoft.Json;

namespace BigDataAssigment.Api.ApiClients.Models
{
    public class CurrentlyForecast
    {
        [JsonProperty("time")]
        public long TimeOffset { get; set; }

        [JsonProperty("dateTime")]
        public DateTime CurrentDateTime => TimeOffset.GetDateTime();

        [JsonProperty("temperature")]
        public float Temperature { get; set; } 



    }
}
