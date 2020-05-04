using MusZil_Core;
using MusZil_Core.Accounts;
using MusZil_Core.API;
using NUnit.Framework;

namespace NUnitTestMusZIL
{
    /// <summary>
    /// Uses a wallet generated at https://dev-wallet.zilliqa.com/generate 
    /// </summary>
    public class MusTest
    {
        public AccountsRepository _repo { get; set; }

        protected MusZil_APIClient _client;
        protected Address _address;
        protected Account _account;
        protected Zilliqa _zil;
        protected Wallet _wallet;
        private string _pk = "a8f8f4c1e76e09c61dfeac0e1f73cf48c58bff0de81243a20a1ff087dc5fa08a";

        
        
        const string JSONRPC = "2.0";
        const string DEV_URL = "https://dev-api.zilliqa.com/";
        
        [SetUp]
        public virtual void SetUp()
        {
            _client = new MusZil_APIClient(DEV_URL);
            _repo = new AccountsRepository();
            _address = new Address("zil1fs6jhg4axvj9ekscq6v7ddwxxd9tthpxl7820q");
            _account = AccountFactory.New(_pk);
            _wallet = new Wallet(_account);
            _zil = new Zilliqa();
        }
    }
}
