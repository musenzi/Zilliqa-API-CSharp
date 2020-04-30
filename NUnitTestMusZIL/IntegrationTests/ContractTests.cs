using MusZil_Core;
using MusZil_Core.API;
using MusZil_Core.Contracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestMusZIL.IntegrationTests
{
    public class ContractTests : MusTest
    {
        private Contract _contract;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _address.Raw = "0x96b324cbdacbf7087f1fb1cdbbe6601a6e8c04c5";
            _contract = new Contract(_address);
        }
        [Test]
        public async Task GetBalance()
        {
            var res = await _client.GetContractBalance(_address);
            Assert.AreNotEqual(-1, (decimal)res.Result);
        }
        [Test]
        public async Task GetCodeNotEmpty()
        {
            var res = await _client.GetContractCode(_address);
            Assert.AreNotEqual("", res.Result);
        }
    }
}
