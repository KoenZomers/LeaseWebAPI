using Newtonsoft.Json;

namespace KoenZomers.LeaseWebApi.Entities.IP
{
    public class IPNetworkInformation
    {
        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        [JsonProperty("mask")]
        public string NetworkMask { get; set; }
    }
}
