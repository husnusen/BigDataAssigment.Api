using System;
using System.Collections.Generic;

namespace BigDataAssigment.Api.Entities
{
    public class Location
    { 
        public string PlaceId { get; set; } 
        public float Lat { get; set; } 
        public float Lon { get; set; } 
        public string DisplayName { get; set; }
        public string LocationName { get; set; }


    }
}
