using System;
using Newtonsoft.Json;

namespace timezonedb
{
    public class TimeZoneData
    {
        [JsonProperty("zoneName")]
        public string zone { get; set; }
        [JsonProperty("abbreviation")]
        public string abv { get; set; }
        [JsonProperty("gmtOffset")]
        public string gmt { get; set; }
    }
}

