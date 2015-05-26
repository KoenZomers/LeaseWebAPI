namespace KoenZomers.LeaseWebApi.Entities.NetworkTraffic
{
    public class Bandwidth
    {
        public BandwidthMeasurement measurement { get; set; }
        public string overusage { get; set; }
        public int serverId { get; set; }
        public string serverName { get; set; }
        public BandwidthInterval interval { get; set; }
        public string monthlyThreshold { get; set; }
    }
}
