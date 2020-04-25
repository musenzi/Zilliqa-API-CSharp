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
            
        }
        public Zilliqa(MusZil_APIClient client)
        {
            _client = client;
        }

        public async Task<string> GetBalanceForAddress(string address)
        {
            var req = new MusRequest("GetBalance",address);
            return await _client.CallMethod(req);
        }
    }
}
