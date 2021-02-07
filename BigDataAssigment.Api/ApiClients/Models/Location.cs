using Newtonsoft.Json;

namespace BigDataAssigment.Api.ApiClients.Models
{
    public class Location
    {
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
        [JsonProperty("lat")]
        public float Lat { get; set; }
        [JsonProperty("lon")]
        public float Lon { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }


       }
}
