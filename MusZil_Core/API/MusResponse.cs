using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.API
{
    public class MusResponse
    {
      
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("jsonrpc")]
            public string Jsonrpc { get; set; }

            [JsonProperty("result")]
            public object Result { get; set; }
        
    }
}
