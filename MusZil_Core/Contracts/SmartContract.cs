using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Contracts
{
    public class SmartContract
    {
        public Address Address;
        public SmartContract(string address = "", string code = "")
        {
            Address = new Address(address);
            Code = code;
        }
        public SmartContract(Address address, string code = "")
        {
            Address = address;
            Code = code;
        }
        private Address _address;
        [JsonProperty("address")]
        public string AddressProperty { 
            get => _address.Raw;
            set => _address = new Address(value); 
        }
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

    public class SmartContractInit
    {
        [JsonProperty("_scilla_version")]
        public string ScillaVersion { get; set; }
        [JsonProperty("owner")]
        public string Owner { get; set; }
        [JsonProperty("_creation_block")]
        public string CreationBlock { get; set; }

        [JsonProperty("_this_address")]
        public string Address { get; set; }
    }
}

