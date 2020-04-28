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

        #region Accounts

        public async Task<(decimal,string)> GetBalance(string address)
        {
            var zil = new Zilliqa(this);
            var req = new MusRequest("GetBalance", address);
            
            var result = await CallMethod(req);
            
            var musres = JsonConvert.DeserializeObject<MusResponse>(result);
            
            return ResponseHandler.GetBalanceFromResult(ref musres);
        }


        #endregion

        #region Contracts
        public async Task<string> GetContractCode(string address)
        {
            var zil = new Zilliqa(this);
            var req = new MusRequest("GetSmartContractCode", address.TrimStart('0').TrimStart('x'));
            var result = await CallMethod(req);
            var musres = JsonConvert.DeserializeObject<MusResponse>(result);
            return ResponseHandler.GetContractCode(ref musres);
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
                var response = await httpClient.PostAsync(DEV_URL, data);
                result = response.Content.ReadAsStringAsync().Result;
            }

            return result;
        }
        #endregion
    }
}
