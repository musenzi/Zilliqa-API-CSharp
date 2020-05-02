using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Transactions
{
    public class Info
    {
        
        //"Info": "Non-contract txn, sent to shard",
        //"TranID": "2d1eea871d8845472e98dbe9b7a7d788fbcce226f52e4216612592167b89042c"
        [JsonProperty("Info")]
        public string InfoMessage { get; set; }
        [JsonProperty("TranID")]
        public string TransactionId { get; set; }

    }
}
