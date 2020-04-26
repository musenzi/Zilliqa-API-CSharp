using MusZil_Core.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusZil_Core
{
    public class Zilliqa
    {
        private MusZil_APIClient _client;
        public Zilliqa()
        {
            _client = new MusZil_APIClient();
        }
        public Zilliqa(MusZil_APIClient client)
        {
            _client = client;
        }

        public async Task<decimal> GetBalanceForAddress(string address)
        {
            return await _client.GetBalance(address);
        }
    }
}
