using MusZil_Core.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MusZil_Core.Accounts;

namespace MusZil_Core
{
    public class Zilliqa
    {
        private MusZil_APIClient _client;

        public Zilliqa() : this(new MusZil_APIClient()){}
        public Zilliqa(MusZil_APIClient client)
        {
            _client = client;
        }
        
        private async Task<MusResult> GetBalanceForAddressAsync(string address)
        {
            var acc = new Account(address);
            return await _client.GetBalance(address);
        }

        #region private methods
        public void AddAccount()
        {
            throw new NotImplementedException();
        }
        private MusResult GetBalanceForAddress(string address)
        {
            return GetBalanceForAddressAsync(address).Result;
        }
        public decimal GetAccountBalance(string address)
        {
            throw new NotImplementedException();
        }

        public decimal GetAccountBalance(Account acc)
        {
            throw new NotImplementedException();
        }
        public void RemoveAccount()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
