using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Contracts
{
    public class SmartContract
    {
        public Address Address;
        [JsonConstructor]
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
        public StateItem State { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }

    }

    public class StateItem
    {
        [JsonProperty("_balance")]
        public decimal _balance { get; set; }
        public Balance Balance { get { return new Balance(_balance); } }
        public object AllValues { get; set; }
        
    }

    public class SmartContractParameter
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
        public List<SmartContractParameter> Parameters;

        public SmartContractInit(List<SmartContractParameter>parameters = null)
        {
            Parameters = parameters ?? new List<SmartContractParameter>();
        }
    }


}

