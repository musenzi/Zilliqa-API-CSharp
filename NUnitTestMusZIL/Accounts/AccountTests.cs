using NUnit.Framework;
using MusZil_Core.Accounts;
using System;

namespace NUnitTestMusZIL.Accounts
{
    public class AccountTests
    {
        [Test]
        public void TestAccountNoAddressThrowsException()
        {
            Assert.Throws<ArgumentException>(() => {
                var acc  = new Account("");
            });
        }
    }
}