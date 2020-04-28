using MusZil_Core;
using MusZil_Core.Accounts;
using MusZil_Core.API;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestMusZIL.IntegrationTests
{
    public class MusTest
    {
        public AccountsRepository _repo { get; set; }

        protected MusZil_APIClient _client;
        protected Address _address;
        protected Account _account;

        const string JSONRPC = "2.0";
        const string DEV_URL = "https://dev-api.zilliqa.com/";

        [SetUp]
        public virtual void SetUp()
        {
            _client = new MusZil_APIClient(DEV_URL);
            _repo = new AccountsRepository();
            _address = new Address("0x4C352ba2Bd33245CDA180699e6B5c6334AB5dC26");
            _account = AccountFactory.New(_address);
            _repo.Add(_account);
            _repo.Add(AccountFactory.New("zil1fs6jhg4axvj9ekscq6v7ddwxxd9tthpxl7820q"));
        }
    }
}
