using MusZil_Core;
using MusZil_Core.Accounts;
using MusZil_Core.API;
using MusZil_Core.Contracts;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestMusZIL.IntegrationTests
{
    public class AcountTests : MusTest
    {

        [Test]
        public async Task GetBalanceWithAccount()
        {
            var res = await _client.GetBalance(_account);
            var resp = res.Result;
            Assert.AreNotEqual(-1,(decimal)resp);
        }
        [Test]
        public async Task GetBalanceWithAddress()
        {
            var res = await _client.GetBalance(_address);
            Assert.AreNotEqual(-1, (decimal)res.Result);
        }
        [Test]
        public async Task GetBalanceWithString()
        {
            var res = await _client.GetBalance("4C352ba2Bd33245CDA180699e6B5c6334AB5dC26");
            Assert.AreNotEqual(-1, (decimal)res.Result);
        }

    }
}
