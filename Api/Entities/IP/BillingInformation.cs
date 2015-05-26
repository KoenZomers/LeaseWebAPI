using Newtonsoft.Json;

namespace KoenZomers.LeaseWebApi.Entities.IP
{
    public class BillingInformation
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("endDate")]
        public object EndDate { get; set; }
    }
}
