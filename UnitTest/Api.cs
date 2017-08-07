using System;
using System.Net.Http;
using KoenZomers.LeaseWebApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Configuration;

namespace UnitTest
{
    /// <summary>
    /// Unit Tests for the API
    /// </summary>
    [TestClass]
    public class Api
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
        /// Validates that an exception will be thrown if an invalid API key is being used
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Exception), "Failed to query the LeaseWeb API. Status code: Forbidden.")]
        public void InvalidApiKeyTestMethod()
        {
            var leaseWebApi = new LeaseWebApi("test-invalid");

            Task.Run(async () =>
            {
                await leaseWebApi.GetLeaseWebDataTraffic(_leaseWebServerId, DateTime.Now.AddDays(-30), DateTime.Now);
            }).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Validates that a valid response will be returned when we query for the server data traffic of the last 30 days
        /// </summary>
        [TestMethod]
        public void GetDataTrafficLast30DaysTestMethod()
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

        /// <summary>
        /// Validates that a valid response will be returned when we query for the server data traffic of this month
        /// </summary>
        [TestMethod]
        public void GetDataTrafficThisMonthTestMethod()
        {
            var leaseWebApi = new LeaseWebApi(_leaseWebApiKey);

            KoenZomers.LeaseWebApi.Entities.NetworkTraffic.NetworkUsage apiResponse = null;
            Task.Run(async () =>
            {
                apiResponse = await leaseWebApi.GetLeaseWebDataTrafficForThisMonth(_leaseWebServerId);
            }).GetAwaiter().GetResult();            

            Assert.IsNotNull(apiResponse);
            Assert.IsTrue(!string.IsNullOrEmpty(apiResponse.DataTraffic.Measurement.Total));
        }

        /// <summary>
        /// Validates that a valid response will be returned when we query for the server data traffic of last month
        /// </summary>
        [TestMethod]
        public void GetDataTrafficLastMonthTestMethod()
        {
            var leaseWebApi = new LeaseWebApi(_leaseWebApiKey);

            KoenZomers.LeaseWebApi.Entities.NetworkTraffic.NetworkUsage apiResponse = null;
            Task.Run(async () =>
            {
                apiResponse = await leaseWebApi.GetLeaseWebDataTrafficForLastMonth(_leaseWebServerId);
            }).GetAwaiter().GetResult();

            Assert.IsNotNull(apiResponse);
            Assert.IsTrue(!string.IsNullOrEmpty(apiResponse.DataTraffic.Measurement.Total));
        }

        /// <summary>
        /// Validates that a valid IP address set will be returned when we query for IP addresses assigned to our server
        /// </summary>
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

        /// <summary>
        /// Connects directly to the LeaseWeb API, bypassing this API framework to test the functionality directly
        /// </summary>
        [TestMethod]
        public void HttpClientTestMethod()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Lsw-Auth", _leaseWebApiKey);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            string response = null;
            Task.Run(async () =>
            {
                response = await httpClient.GetStringAsync("https://api.leaseweb.com/v1/ips");
            }).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
        }
    }
}
