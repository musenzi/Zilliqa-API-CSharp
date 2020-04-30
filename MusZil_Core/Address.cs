using MusZil_Core.Enums;
using MusZil_Core.Utils;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MusZil_Core
{
    
    public partial class Address
    {
        protected enum Encoding
        {
            BECH32,
            BASE16
        }
        //TODO implement converter

        private string _address;
        private Balance _balance;
        private const string _b32encoding = "bech32";
        private const string _b16encoding = "base16";
        public Bech32 Bech32 { get; set; } 
        public string Base16 { get; set; }
        public string Raw
        {
            get => _address.ToLower();
            set
            {
                _address = value;
                if (_address.StartsWith("0x"))
                {
                    Base16 = _address;
                    _curr = Encoding.BASE16;
                    _address = value.TrimStart('0').TrimStart('x');
                }
                else if (_address.StartsWith("zil"))
                {

                    _curr = Encoding.BECH32;
                    Bech32 = new Bech32(value,null,"zil");
                    _address = value;
                }
            }
        }
        private Encoding _curr;
        public string Current_Encoding { get => _curr.ToString(); }


        public Address(string address = "")
        {
            Raw = address;
            _balance = new Balance();
        }
        public Address()
        {
            
            _balance = new Balance();
        }
        public override string ToString()
        {
            return Raw;
        }
        public void ToBech32Address()
        {
            Raw = MusBech32.Base16ToBech32Address(Raw);
        }
        
    }
}
