using Newtonsoft.Json;

namespace KoenZomers.LeaseWebApi.Entities.NetworkTraffic
{
    /// <summary>
    /// Information regarding the amount of data caused by the server
    /// </summary>
    public class DataTraffic
    {
        [JsonProperty("measurement")]
        public DataTrafficMeasurement Measurement { get; set; }

        [JsonProperty("overusage")]
        public string Overusage { get; set; }

        [JsonProperty("serverId")]
        public int ServerId { get; set; }

        [JsonProperty("serverName")]
        public string ServerName { get; set; }

        [JsonProperty("interval")]
        public DataTrafficInterval Interval { get; set; }

        [JsonProperty("monthlyThreshold")]
        public string MonthlyThreshold { get; set; }
    }
}
