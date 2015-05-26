using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoenZomers.LeaseWebApi.Entities.NetworkTraffic
{
    public class BandwidthMeasurement
    {
        public BandwidthMeasurementNode node { get; set; }
        public string total { get; set; }
        public string average { get; set; }
    }
}
