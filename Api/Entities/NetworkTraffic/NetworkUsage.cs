using Newtonsoft.Json;

namespace KoenZomers.LeaseWebApi.Entities.NetworkTraffic
{
    /// <summary>
    /// Information regarding the network usage
    /// </summary>
    /// <remarks>Generated using http://json2csharp.com</remarks>
    public class NetworkUsage
    {
        /// <summary>
        /// Information regarding the bandwidth used by the server
        /// </summary>
        [JsonProperty("bandwidth")]
        public Bandwidth Bandwidth { get; set; }

        /// <summary>
        /// Information regarding the amount of data caused by the server
        /// </summary>
        [JsonProperty("dataTraffic")]
        public DataTraffic DataTraffic { get; set; }
    }
}
