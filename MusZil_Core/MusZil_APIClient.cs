using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusZil_Core
{
    public class MusZil_APIClient
    {
        const string JSONRPC = "2.0";

        const string DEV_URL = "https://dev-api.zilliqa.com/";

        /// <summary>
        /// Calls a API method of the Zilliqa API
        /// </summary>
        /// <param name="req">MusRequest object to pass request</param>
        /// <returns></returns>
        public async static Task<string> CallMethod(MusRequest req)
        {
            HttpClient httpClient = new HttpClient();

            //specify to use TLS 1.2 as default connection
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            httpClient.BaseAddress = new Uri("https://foobar.com/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var json = req.ToJson();//JsonConvert.SerializeObject(request);

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            string result = "";
            using (var response = await httpClient.PostAsync(DEV_URL, data))
            {
                 result = response.Content.ReadAsStringAsync().Result;
            }
            return result;
        }
    }
}
