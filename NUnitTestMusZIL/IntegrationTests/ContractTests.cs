using MusZil_Core;
using MusZil_Core.API;
using MusZil_Core.Contracts;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestMusZIL.IntegrationTests
{
    public class ContractTests : MusTest
    {
        private SmartContract _contract;
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _address.Raw = "0x96b324cbdacbf7087f1fb1cdbbe6601a6e8c04c5";
            _contract = new SmartContract(_address);
        }
        [Test]
        public async Task GetBalance()
        {
            var res = await _zil.GetContractBalance(_address);
            Assert.AreNotEqual(-1, res.GetBalance());
        }
        [Test]
        public async Task GetCodeNotEmpty()
        {
            var res = await _zil.GetSmartContractCode(_address);
            Assert.AreNotEqual("", res);
        }
        [Test]
        public async Task GetInitNotEmpty()
        {
            var res = await _zil.GetSmartContractInit(_address.Raw);
            Assert.AreNotEqual(null, res);
        }
        [Test]
        public async Task GetStateNotEmpty()
        {
            var res = await _zil.GetSmartContractState(_address.Raw);
            Assert.AreNotEqual(null, res);
        }
        [Test]
        public async Task GetStateGivesAllValues()
        {
            var res = await _zil.GetSmartContractState(_address.Raw);
            var valuesJson = ((JToken)res.AllValues).ToString();
            Assert.AreNotEqual("", valuesJson);
        }
        [Test]
        public async Task GetSubSateNotEmpty()
        {
            object[] parameters = new object[] { _address.Raw,"admins",new object[0] };
            var res = await _zil.GetSmartContractSubState(parameters);
            Assert.AreNotEqual("", res);
        }
        [Test]
        public async Task GetContractsNotEmpty()
        {
            var res = await _zil.GetSmartContracts(_account);
            Assert.IsTrue(res.Any());
        }
        [Test]
        public async Task GetContractAddressFromTransactionIDNotEmpty()
        {
            
            var res = await _client.GetContractAddressFromTransactionID(_address.Raw);
            Assert.AreNotEqual("", res.Result);
        }
        [Test]
        public async Task GetStateFromContract()
        {
            var res = await _zil.GetSmartContracts(_account);
            var res0 = res[0];

            Assert.AreNotEqual(null, res0.State);
        }
    }
}
