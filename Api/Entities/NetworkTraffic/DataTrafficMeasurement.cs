using System;
using System.Dynamic;
using Newtonsoft.Json;

namespace KoenZomers.LeaseWebApi.Entities.NetworkTraffic
{
    public class DataTrafficMeasurement
    {
        #region Properties

        [JsonProperty("node")]
        public DataTrafficMeasurementNode Node { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }

        #endregion


        #region Methods

        /// <summary>
        /// Calculates the amount of data that is remaining based on the total amount of data used and the data limit provided
        /// </summary>
        /// <param name="limitInBytes">Data limit in bytes to use for the calculation</param>
        /// <returns>Amount of data remaining in bytes</returns>
        public long DataTrafficRemaining(long limitInBytes)
        {
            if (string.IsNullOrWhiteSpace(Total))
            {
                throw new ArgumentException("Total must be available in order to calculate the remaining traffic");
            }

            var remaining = limitInBytes - Utilities.BytesConversions.FriendlyTextToBytes(Total);
            return remaining;
        }

        #endregion

    }
}
