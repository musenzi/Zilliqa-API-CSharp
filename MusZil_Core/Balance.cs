using MusZil_Core.Enums;
using Newtonsoft.Json;

namespace MusZil_Core
{
    public class Balance
    {
        [JsonProperty("balance")]
        private decimal _balance;
        [JsonProperty("nonce")]
        public long Nonce;
        private Unit _u;

        
        public Balance(decimal bal = 0)
        {
            _balance = bal;
            _u = Unit.QA;
        }

        public decimal GetBalance(Unit u = Unit.ZIL)
        {
            if(_u != u)
            SwitchUnit(u);
            return _balance;
        }
        public void SwitchUnit(Unit u)
        {
            if (_u == Unit.QA)
            {
                switch (u)
                {
                    case Unit.ZIL:
                        _balance /= 1000000000000;
                        break;
                    case Unit.LI:
                        _balance /= 1000000;
                        break;
                }
            }
            if (_u == Unit.LI)
            {
                switch (u)
                {
                    case Unit.ZIL:
                        _balance /= 1000000;
                        break;
                    case Unit.QA:
                        _balance *= 1000000;
                        break;
                }
            }
            if (_u == Unit.ZIL)
            {
                switch (u)
                {
                    case Unit.LI:
                        _balance *= 1000000;
                        break;
                    case Unit.QA:
                        _balance *= 1000000000000;
                        break;
                }
            }

        }
    }
}

