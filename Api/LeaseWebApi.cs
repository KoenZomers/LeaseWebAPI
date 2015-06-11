using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KoenZomers.LeaseWebApi
{
    /// <summary>
    /// Helper class to communicate with the LeaseWeb API
    /// </summary>
    public class LeaseWebApi
    {
        #region Constants

        /// <summary>
        /// Defines the base URL on which the LeaseWeb API is available
        /// </summary>
        private static readonly Uri LeaseWebBaseApiUrl = new Uri("https://api.leaseweb.com/v1/");

        #endregion

        #region Properties

        /// <summary>
        /// Gets the LeaseWeb API key. Set it through the constructor.
        /// </summary>
        public string ApiKey { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new LeaseWebApi instance using the provided LeaseWeb API key
        /// </summary>
        /// <param name="apiKey">LeaseWeb API key to use to communicate with the services</param>
        public LeaseWebApi(string apiKey)
        {
            ApiKey = apiKey;
        }

        /// <summary>
        /// Initializes a new LeaseWebApi instance using the settings from the AppSettings in the config file
        /// </summary>
        public LeaseWebApi() : this(ConfigurationManager.AppSettings["LeaseWebApiKey"])
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes a query against the LeaseWeb API
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <param name="extraParams">Key value pairs to include in the querystring. The Api Key is already added by default.</param>
        /// <returns>NetworkUsage entity with the result</returns>
        public async Task<T> ExecuteLeaseWebApiRequest<T>(string command, Dictionary<string, string> extraParams = null)
        {
            // Build up the base URL including the API key
            var leaseWebApiUriBuilder = new StringBuilder();
            leaseWebApiUriBuilder.Append(LeaseWebBaseApiUrl);
            leaseWebApiUriBuilder.Append(command);

            if (extraParams != null && extraParams.Count > 0)
            {                
                // Add possible additional parameters to the URL
                for (var paramCount = 0; paramCount < extraParams.Count; paramCount++)
                {
                    var extraParam = extraParams.ElementAt(paramCount);

                    leaseWebApiUriBuilder.Append(paramCount == 0 ? "?" : "&");
                    leaseWebApiUriBuilder.AppendFormat("{0}={1}", extraParam.Key, extraParam.Value);
                }
            }

            // Retrieve the JSON data from the LeaseWeb API
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-Lsw-Auth", ApiKey);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            // Send a request asynchronously continue when complete
            var response = await httpClient.GetAsync(leaseWebApiUriBuilder.ToString());

            // Check that response was successful
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format("Failed to query the LeaseWeb API. Status code: {0}.", response.StatusCode));
            }

            // Read response asynchronously and parse the JSON
            var content = await response.Content.ReadAsAsync<T>();
            return content;            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves the data usage for the server with the provided ID through the LeaseWeb API for the current month
        /// </summary>
        /// <param name="serverId">ID of the server for which to fetch this months data traffic</param>
        /// <returns>NetworkUsage entity containing the results</returns>
        public async Task<Entities.NetworkTraffic.NetworkUsage> GetLeaseWebDataTrafficForThisMonth(string serverId)
        {
            // Calculate the dates between which to query the data usage
            var dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            return await GetLeaseWebDataTraffic(serverId, dateFrom, dateTo);
        }

        /// <summary>
        /// Retrieves the data usage for the server with the provided ID through the LeaseWeb API for last month
        /// </summary>
        /// <param name="serverId">ID of the server for which to fetch last months data traffic</param>
        /// <returns>NetworkUsage entity containing the results</returns>
        public async Task<Entities.NetworkTraffic.NetworkUsage> GetLeaseWebDataTrafficForLastMonth(string serverId)
        {
            // Calculate the dates between which to query the data usage
            var todayLastMonth = DateTime.Now.AddMonths(-1);
            var dateFrom = new DateTime(todayLastMonth.Year, todayLastMonth.Month, 1);
            var dateTo = new DateTime(todayLastMonth.Year, todayLastMonth.Month, DateTime.DaysInMonth(todayLastMonth.Year, todayLastMonth.Month));

            return await GetLeaseWebDataTraffic(serverId, dateFrom, dateTo);
        }

        /// <summary>
        /// Retrieves the data usage for the server with the provided ID through the LeaseWeb API for the provided period
        /// </summary>
        /// <param name="serverId">ID of the server for which to fetch this months data traffic</param>
        /// <param name="dateFrom">Date of the start of the period to get data statistics from</param>
        /// <param name="dateTo">Date of the end of the period to get data statistics from</param>
        /// <returns>Typed entity containing the results</returns>
        public async Task<Entities.NetworkTraffic.NetworkUsage> GetLeaseWebDataTraffic(string serverId, DateTime dateFrom, DateTime dateTo)
        {
            // Make sure that the method arguments were provided
            if (string.IsNullOrEmpty(serverId))
            {
                throw new ArgumentNullException("serverId", "Server ID has not been provided");
            }
          
            // Define the parameters needed to perform the request
            var extraParams = new Dictionary<string, string>
            {
                { "dateFrom", dateFrom.ToString("dd-MM-yyyy") },
                { "dateTo", dateTo.ToString("dd-MM-yyyy") }
            };

            // Execute the request
            var result = await ExecuteLeaseWebApiRequest<Entities.NetworkTraffic.NetworkUsage>(string.Format("colocationServers/{0}/networkUsage", serverId), extraParams);
            return result;
        }

        /// <summary>
        /// Retrieves the IP addresses assigned to the LeaseWeb account
        /// </summary>
        /// <param name="serverId">ID of the server for which to fetch the IP addresses</param>
        /// <returns>Typed entity containing the results</returns>
        public async Task<Entities.IP.IPS> GetLeaseWebIPAddresses(string serverId)
        {
            // Execute the request
            var result = await ExecuteLeaseWebApiRequest<Entities.IP.IPS>(string.Format("colocationServers/{0}/ips", serverId));
            return result;
        }

        #endregion
    }
}