using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core
{
    public class MusRequest
    {
        public MusRequest(string method, string param)
        {
            Id = "1";
            Method = method;
            Parameters = new[] { param };
            Jsonrpc = "2.0";
        }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("params")]
        public string[] Parameters { get; set; }

        /// <summary>
        /// Returns request as Json (using NewtonJsoft)
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
