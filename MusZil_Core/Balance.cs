using MusZil_Core.Enums;

namespace MusZil_Core
{
    public partial class Address
    {
        internal class Balance
        {
            private float _balance;
            private Unit _u;
            public Balance(float bal = 0)
            {
                _balance = bal;
                _u = Unit.QA;
            }
            public float GetBalance()
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
                            _balance *= 0.000001F;
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
}
