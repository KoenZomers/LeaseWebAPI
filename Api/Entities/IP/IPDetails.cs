using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KoenZomers.LeaseWebApi.Entities.IP
{
    /// <summary>
    /// Detailed information regarding one IP address
    /// </summary>
    public class IPDetails
    {
        [JsonProperty("ip")]
        public string IPAddress { get; set; }

        [JsonProperty("reverseLookup")]
        public string ReverseLookup { get; set; }

        [JsonProperty("nullRouted")]
        public bool NullRouted { get; set; }

        [JsonProperty("billingInformation")]
        public BillingInformation BillingInformation { get; set; }

        [JsonProperty("serverId")]
        public string ServerId { get; set; }

        [JsonProperty("serverType")]
        public string ServerType { get; set; }

        [JsonProperty("serverName")]
        public string ServerName { get; set; }

        [JsonProperty("ipDetails")]
        public IPNetworkInformation IpNetworkInformationDetails { get; set; }
    }
}
