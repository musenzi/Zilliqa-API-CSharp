using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Blockchain
{
    public class DSBlock : BlockInfo
    {
        [JsonIgnore]
        public override string BlockNum { get => Header.BlockNum; }
        [JsonIgnore]
        public override string Hash { get; set; }
        [JsonProperty("header")]
        public DsBlockHeader Header { get; set; }
        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
    public class DsBlockHeader
    {
        public string BlockNum { get; set; }
        public int Difficulty { get; set; }
        public int DifficultyDS { get; set; }
        public string GasPrice { get; set; }
        public string LeaderPubKey { get; set; }
        public IList<string> PoWWinners { get; set; }
        public string PrevHash { get; set; }
        public string Timestamp { get; set; }
    }
}
