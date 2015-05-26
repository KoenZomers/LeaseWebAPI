using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoenZomers.LeaseWebApi.Entities.NetworkTraffic
{
    public class BandwidthMeasurementNode
    {
        public string @in { get; set; }
        public string @out { get; set; }
        public string total { get; set; }
    }
}
