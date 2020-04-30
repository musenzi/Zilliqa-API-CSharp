using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Accounts
{
    public class Account
    {
        // TODO Lookup public key from address

        private string _address;
        /// <summary>
        /// Constructor for readonly account
        /// </summary>
        /// <param name="address"></param>
        public Account(string address)
        {
            Address = new Address(address);
        }
        public Account(Address address)
        {
            Address = address;
        }
        protected string PrivateKey { get;  set; }
        public string PublicKey { get; set; }
        
        public Address Address { get; set; }
        public void SetPrivateKey(string pk)
        {
            PrivateKey = pk;
        }

    }
}
