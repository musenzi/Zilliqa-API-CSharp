using MusZil_Core.Accounts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestMusZIL.IntegrationTests
{
    public class TransactionTests : MusTest
    {
        [SetUp]
        public void Initialize()
        {
            
        }

        [Test]
        public async Task MinGasPriceNotZero()
        {
            var gas = await _zil.GetMinimumGasPrice();
            Console.WriteLine($"Minimum gas price: {gas}");
            Assert.AreNotEqual(0,gas);
        }
        [Test]
        public async Task GetNumTxnsDSEpochNotNegative()
        {
            var num = await _zil.GetNumTxnsDSEpoch();
            Console.WriteLine($"Number of transactions in DS Epoch: {num}");
            Assert.AreNotEqual(0, num);
        }
        [Test]
        public async Task GetNumTxnsTxEpochNotNegative()
        {
            var num = await _zil.GetNumTxnsDSEpoch();
            Console.WriteLine($"Number of transactions in Tx Epoch: {num}");
            Assert.AreNotEqual(0, num);
        }
        [Test]
        public async Task GetTxnBodiesForTxBlockNotEmpty()
        {
            var txns = await _zil.GetTxnBodiesForTxBlock(2);
            Assert.IsTrue(txns.Any());
        }
        [Test]
        public async Task GetTransactionsForTxBlockNotEmpty()
        {
            var txns = await _zil.GetTransactionsForTxBlock(2); 
            Assert.IsTrue(txns.Any());
        }
        public async Task GetRecentTransactionsNotEmpty()
        {
            var txns = await _zil.GetRecentTransactions();
            Assert.IsTrue(txns.Any());
        }
        public async Task GetPendingTxnsNotEmpty()
        {
            var txns = await _zil.GetPendingTxns();
            Assert.IsTrue(txns.Any());
        }
        public async Task GetPendingTxnHashNotEmpty()
        {
            var txns = await _zil.GetPendingTxns();
            var txn = txns[0];
            var ftxns = await _zil.GetPendingTxn(txn.Hash);
            Assert.IsTrue(txns.Any());
        }
        public async Task GetTransactionNotNull()
        {
            var hash = "655107c300e86ee6e819af1cbfce097db1510e8cd971d99f32ce2772dcad42f2";
            var txn = await _zil.GetTransaction(hash);
            Assert.IsTrue(txn != null);
        }
        public async Task CreateTransactionHasId()
        {
            //TODO figure out how to create transaction tests with signatures
        }
    }
}
