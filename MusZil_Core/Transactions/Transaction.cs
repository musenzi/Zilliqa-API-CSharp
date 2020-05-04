using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Transactions
{
    public class Transaction
    {
        private bool _normalTx;
        private long _gasLimit;
        private int _version;

        [JsonProperty("ID")]
        public string Id { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("gasLimit")]
        public long GasLimit { 
            get => _gasLimit;
            set { 
                   if (_normalTx && value <= 1000000000000) {
                    var zVal = value / 1000000000000;
                    throw new ArgumentOutOfRangeException($"Normal transactions should have 1 gas, {zVal} found instead");
                }
                _gasLimit = value;
            }
        }

        [JsonProperty("gasPrice")]
        public long GasPrice { get; set; }

        [JsonProperty("nonce")]
        public long Nonce { get; set; }

        [JsonProperty("receipt")]
        public Receipt Receipt { get; set; }

        [JsonProperty("senderPubKey")]
        public string SenderPubKey { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("toAddr")]
        public string ToAddr { get; set; }

        [JsonProperty("version")]
        public int Version {get;set;}

        public class Info
        {
            [JsonProperty("Info")]
            public string InfoMessage { get; set; }
            [JsonProperty("TranID")]
            public string TransactionId { get; set; }
        }

    }

    public partial class EventLog
    {
        [JsonProperty("_eventname")]
        public string Eventname { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("params")]
        public Param[] Params { get; set; }
    }

    public partial class Param
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("vname")]
        public string Vname { get; set; }
    }

    public partial class Transition
    {
        [JsonProperty("addr")]
        public string Addr { get; set; }

        [JsonProperty("depth")]
        public long Depth { get; set; }

        [JsonProperty("msg")]
        public Msg Msg { get; set; }
    }
}
