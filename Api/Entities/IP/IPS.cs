using System.Collections.Generic;
using Newtonsoft.Json;

namespace KoenZomers.LeaseWebApi.Entities.IP
{
    /// <summary>
    /// Information regarding the IP addresses in use
    /// </summary>
    public class IPS
    {
        /// <summary>
        /// The IP addresses that are in use
        /// </summary>
        [JsonProperty("ips")]
        public List<IP> IPs { get; set; }
    }
}
