using MusZil_Core.Enums;

namespace MusZil_Core
{
    public class Balance
    {
        private decimal _balance;
        private Unit _u;
        public Balance(decimal bal = 0)
        {
            _balance = bal;
            _u = Unit.QA;
        }
        public decimal GetBalance()
        {
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

