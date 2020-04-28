using MusZil_Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core
{
    public partial class Address
    {
        //TODO implement converter

        private string _address;
        private Balance _balance;
        private Unit _curr;
        public string Bech32 { get; set; } 
        public string Base12 { get; set; }
        public string Raw
        {
            get => _address;
            set
            {
                _address = value;
                if (_address.StartsWith("0x"))
                {
                    Base12 = _address;
                    _address = value.TrimStart('0').TrimStart('x');
                }
                else if (_address.StartsWith("zil"))
                {
                    Bech32 = _address;
                }
            }
        }
       

        public Address(string address = "")
        {
            Raw = address;
            _balance = new Balance();
        }
        public override string ToString()
        {
            return Base12;
        }
    }
}
