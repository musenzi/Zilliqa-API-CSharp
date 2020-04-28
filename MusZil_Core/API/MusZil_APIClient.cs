using MusZil_Core.Accounts;
using MusZil_Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusZil_Core.API
{
    public class MusZil_APIClient
    {
        //put this in config file of app
        const string JSONRPC = "2.0";
        const string DEV_URL = "https://dev-api.zilliqa.com/";

        private readonly object requestLock = new object();

        public string Url { get; private set; }
        public MusZil_APIClient(string url = DEV_URL)
        {
            Url = url;
        }
        #region Accounts

        public async Task<MusResult> GetBalance(string address)
        {
            var req = new MusRequest("GetBalance", address);
            var result = await CallMethod(req);
            var musres = JsonConvert.DeserializeObject<APIResponse>(result);
            
            return ResponseHandler.GetBalanceFromResult(ref musres);
        }
        public async Task<MusResult> GetBalance(Address address)
        {
            return await GetBalance(address.Raw);
        }
        public async Task<MusResult> GetBalance(Account acc)
        {
            return await GetBalance(acc.Address.Raw);
        }

        #endregion

        #region Contracts
        /// <summary>
        /// Gets contractCode, overloaded with Address,Contract
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<MusResult> GetContractCode(string address)
        {
            var req = new MusRequest("GetSmartContractCode", address.TrimStart('0').TrimStart('x'));
            var result = await CallMethod(req);
            var musres = JsonConvert.DeserializeObject<APIResponse>(result);
            return ResponseHandler.GetContractCode(ref musres);
        }
        public async Task<MusResult> GetContractCode(Address address)
        {
           return await GetContractCode(address.Raw);
        }
        public async Task<MusResult> GetContractCode(Contract c)
        {
            return await GetContractCode(c.Address.Raw);
        }
        /// <summary>
        /// Gets Contract Balance, overloaded with: Address,Contract
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<MusResult> GetContractBalance(string address)
        {
            var req = new MusRequest("GetSmartContractState", address.TrimStart('0').TrimStart('x'));
            var result = await CallMethod(req);
            var musres = JsonConvert.DeserializeObject<APIResponse>(result);
            return ResponseHandler.GetContractBalance(ref musres);
        }
        public async Task<MusResult> GetContractBalance(Address address)
        {
            return await GetContractBalance(address.Raw);
        }
        public async Task<MusResult> GetContractBalance(Contract con)
        {
            return await GetContractBalance(con.Address.Raw);
        }

        /// <summary>
        /// Gets all contracts for one account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<MusResult> GetContracts(Account account)
        {
            throw new NotImplementedException("Coming...");
            //return await GetContractCode(address.Raw);
        }
        #endregion

        #region Helpers

        private HttpClient GetClient()
        {
            HttpClient httpClient = null;
            lock (requestLock)
            {
                httpClient = new HttpClient();

                //specify to use TLS 1.2 as default connection
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            

            return httpClient;
        }

        /// <summary>
        /// Calls a API method of the Zilliqa API
        /// </summary>
        /// <param name="req">MusRequest object to pass request</param>
        /// <returns></returns>
        private async Task<string> CallMethod(MusRequest req)
        {
            string result = "";
            var json = req.ToJson();
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = GetClient())
            {
                var response = await httpClient.PostAsync(Url, data);
                result = response.Content.ReadAsStringAsync().Result;
            }

            return result;
        }
        #endregion
    }
}
