using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MusZil_Core.API
{
    public class APIResponse
    {
      
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("jsonrpc")]
            public string Jsonrpc { get; set; }

            [JsonProperty("result")]
            public object Result { get; set; }
            [JsonProperty("error")]
            public object Error { get; set; }

    }
}
