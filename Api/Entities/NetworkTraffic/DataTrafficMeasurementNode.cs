using Newtonsoft.Json;

namespace KoenZomers.LeaseWebApi.Entities.NetworkTraffic
{
    public class DataTrafficMeasurementNode
    {
        [JsonProperty("in")]
        public string In { get; set; }

        [JsonProperty("out")]
        public string Out { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }
    }
}
