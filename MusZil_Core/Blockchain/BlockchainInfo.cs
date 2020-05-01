using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Blockchain
{
    public class BlockchainInfo
    {
        public string CurrentDSEpoch { get; set; }
        public string CurrentMiniEpoch { get; set; }
        public double DSBlockRate { get; set; }
        public string NumDSBlocks { get; set; }
        public int NumPeers { get; set; }
        public string NumTransactions { get; set; }
        public string NumTxBlocks { get; set; }
        public string NumTxnsDSEpoch { get; set; }
        public string NumTxnsTxEpoch { get; set; }
        public ShardingStructure ShardingStructure { get; set; }
    }
    public class ShardingStructure
    {
        public IList<int> NumPeers { get; set; }
    }
}
