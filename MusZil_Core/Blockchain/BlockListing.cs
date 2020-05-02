using Newtonsoft.Json;

namespace MusZil_Core.Blockchain
{
    public class BlockListing
    { 
        [JsonProperty("data")]
        public BlockInfo[] Data { get; set; }
        [JsonProperty("maxPages")]
        public int MaxPages { get; set; }
    }
}
