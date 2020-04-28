using NUnit.Framework;
using MusZil_Core.Accounts;

namespace NUnitTestMusZIL.Accounts
{
    public class Tests
    {
        public Account TestAccount { get; set; }

        [SetUp]
        public void Setup()
        {

            //make an account ?? use a factory maybe
            TestAccount = new Account("");
        }

        [Test]
        public void TestAccountNoAddress()
        {
            Assert.AreEqual("",TestAccount.Address.Raw);
        }
    }
}