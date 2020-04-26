using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Contracts
{
    public class Contract
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("state")]
        public List<StateItem> State { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }

    }

    public class StateItem
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("vname")]
        public string Name { get; set; }
    }
}

