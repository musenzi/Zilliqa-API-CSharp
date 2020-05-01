using MusZil_Core.Blockchain;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestMusZIL.IntegrationTests
{
    public class BlockchainTests:MusTest
    {
        [Test]
        public async Task NetworkIdNotEmpty()
        {
            var res = await _client.GetNetworkId();
            Assert.AreEqual("333", res.Result);
        }
        [Test]
        public async Task GetBlockchainInfoNotEmpty()
        {
            var res = await _client.GetBlockchainInfo();
            var objStr = JsonConvert.SerializeObject(res.Result);
            var str = res.Result.ToString();
            var musres = JsonConvert.DeserializeObject<BlockchainInfo>(objStr);
            Assert.IsFalse(String.IsNullOrEmpty(objStr));
        }

    }
}
