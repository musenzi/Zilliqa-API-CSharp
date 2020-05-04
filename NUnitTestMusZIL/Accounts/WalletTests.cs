using MusZil_Core;
using MusZil_Core.Accounts;
using NUnit.Framework;
using NUnitTestMusZIL.IntegrationTests;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestMusZIL.Accounts
{
    public class WalletTests : MusTest
    {

        [Test]
        public void CreateWalletHasAccount()
        {
            Assert.AreNotEqual(null, _wallet);
        }
        [Test]
        public void GetBalanceFromWalletNotNegative()
        {
            var balance = _wallet.GetBalance(_account);
            var amount = balance.GetBalance();
            Console.WriteLine($"Balance is {amount}");
            Assert.AreNotEqual(-1, amount);
        }
    }
}
