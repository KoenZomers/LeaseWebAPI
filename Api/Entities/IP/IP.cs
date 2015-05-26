using Newtonsoft.Json;

namespace KoenZomers.LeaseWebApi.Entities.IP
{
    /// <summary>
    /// Information regarding one IP address
    /// </summary>
    public class IP
    {
        [JsonProperty("ip")]
        public IPDetails IPDetails { get; set; }
    }
}
