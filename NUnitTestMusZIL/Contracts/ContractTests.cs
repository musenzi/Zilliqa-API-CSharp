using System;
using System.Collections.Generic;
using System.Text;
using MusZil_Core.Contracts;
using NUnit.Framework;

namespace NUnitTestMusZIL.Contracts
{
    public class ContractTests
    {
        public SmartContract TestContract { get; set; }
        [SetUp]
        public void Setup()
        {
            //make an account ?? use a factory maybe
            TestContract = new SmartContract();
        }

        [Test]
        public void TestContractNoAddress()
        {

            Assert.AreEqual("", TestContract.Address.Raw);
        }
        [Test]
        public void NewContractCodeEmpty()
        {
            Assert.AreEqual("", TestContract.Code);
        }
    }
}
