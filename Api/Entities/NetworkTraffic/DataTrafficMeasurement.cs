using Newtonsoft.Json;

namespace KoenZomers.LeaseWebApi.Entities.NetworkTraffic
{
    public class DataTrafficMeasurement
    {
        [JsonProperty("node")]
        public DataTrafficMeasurementNode Node { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }
    }
}
