using Newtonsoft.Json;

namespace MusZil_Core.Transactions
{
    public class Receipt
    {
        [JsonProperty("cumulative_gas")]
        public long CumulativeGas { get; set; }

        [JsonProperty("epoch_num")]
        public long EpochNum { get; set; }

        [JsonProperty("event_logs")]
        public EventLog[] EventLogs { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("transitions")]
        public Transition[] Transitions { get; set; }
    }
}
