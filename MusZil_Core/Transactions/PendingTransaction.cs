using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Transactions
{
    public class PendingTransaction
    {
        [JsonProperty("Status")]
        public virtual int Status { get; set; }
        [JsonProperty("TxnHash")]
        public virtual string Hash { get; set; }
    }
    public class PendingTransactionInfo : PendingTransaction
    {
        public PendingTransactionInfo(PendingTransaction info = null)
        {
            Hash = info.Hash;
        }
        [JsonIgnore]
        public override int Status { get => int.Parse(Code);  }
        [JsonIgnore]
        public override string Hash { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("confirmed")]
        public bool Confirmed { get; set; }
        [JsonProperty("info")]
        public string Info { get; set; }
    }
}
