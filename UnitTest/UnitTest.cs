using System;
using System.Net.Http;
using KoenZomers.LeaseWebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Configuration;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// The API key to use to authenticate to the LeaseWeb API
        /// </summary>
        private string _leaseWebApiKey;

        /// <summary>
        /// The Server ID to query statistics from through the LeaseWeb API
        /// </summary>
        private string _leaseWebServerId;

        /// <summary>
        /// Initialize data to be used by all Unit Tests
        /// </summary>
        [TestInitialize]
        public void InitializeUnitTests()
        {
            _leaseWebApiKey = ConfigurationManager.AppSettings["LeaseWebApiKey"];
            _leaseWebServerId = ConfigurationManager.AppSettings["LeaseWebServerID"];
        }

        /// <summary>
        /// test222
        /// </summary>
        [TestMethod]
        public void GetDataTrafficTestMethod()
        {
            var leaseWebApi = new LeaseWebApi(_leaseWebApiKey);

            KoenZomers.LeaseWebApi.Entities.NetworkTraffic.NetworkUsage apiResponse = null;
            Task.Run(async () =>
            {
                apiResponse = await leaseWebApi.GetLeaseWebDataTraffic(_leaseWebServerId, DateTime.Now.AddDays(-30), DateTime.Now);
            }).GetAwaiter().GetResult();
            
            Assert.IsNotNull(apiResponse);
            Assert.IsTrue(!string.IsNullOrEmpty(apiResponse.DataTraffic.Measurement.Total));
        }

        [TestMethod]
        public void GetIpAddressesTestMethod()
        {
            var leaseWebApi = new LeaseWebApi(_leaseWebApiKey);

            KoenZomers.LeaseWebApi.Entities.IP.IPS apiResponse = null;
            Task.Run(async () =>
            {
                apiResponse = await leaseWebApi.GetLeaseWebIPAddresses(_leaseWebServerId);
            }).GetAwaiter().GetResult();

            Assert.IsNotNull(apiResponse);
            Assert.IsTrue(apiResponse.IPs.Count == 9);
        }

        [TestMethod]
        public void HttpClientTestMethod()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Lsw-Auth", _leaseWebApiKey);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            string response = null;
            Task.Run(async () =>
            {
                response = await httpClient.GetStringAsync("https://api.leaseweb.com/v1/colocationServers/" + _leaseWebServerId + "/ips");
            }).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
        }
    }
}
