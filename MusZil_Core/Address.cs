using MusZil_Core.Enums;
using MusZil_Core.Utils;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MusZil_Core
{
    
    public class Address
    {
        public enum AddressEncoding
        {
            BECH32,
            BASE16
        }

        private string _address;
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
                    _curr = AddressEncoding.BASE16;
                    _address = value.TrimStart('0').TrimStart('x');
                }
                else if (_address.StartsWith("zil"))
                {

                    _curr = AddressEncoding.BECH32;
                    Bech32 = new Bech32(value,null,"zil");
                    _address = value;
                }
            }
        }
        private AddressEncoding _curr;
        public string Current_Encoding { get => _curr.ToString(); }


        public Address(string address = "")
        {
            Raw = address;
        }
        public override string ToString()
        {
            return Raw;
        }
        public void Base16ToBech32Address()
        {
            Raw = MusBech32.Base16ToBech32Address(Raw);
        }
        public void Bech32ToBase16Address()
        {
            Raw = MusBech32.Bech32ToBase16Address(Raw);
        }
        /// <summary>
        /// Changes Current encoding of address (default base16)
        /// </summary>
        /// <param name="enc"></param>
        public void SwitchEncoding(AddressEncoding enc = AddressEncoding.BASE16)
        {
            if (_curr == enc) return;

            switch (_curr)
            {
                case AddressEncoding.BASE16:
                    Base16ToBech32Address();
                    break;
                case AddressEncoding.BECH32:
                    Bech32ToBase16Address();
                    break;
            }
        }
        
    }
}
