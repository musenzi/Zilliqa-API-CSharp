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
        public Account(string address , decimal balance = 0)
        {
            Address = new Address(address);
            Balance = new Balance(0);
        }
        public Account(Address address, decimal balance = 0)
        {
            Address = address;
            Balance = new Balance(balance);
        }
        public Balance Balance { get; set; }
        protected string PrivateKey { get;  set; }
        public string PublicKey { get; set; }
        
        public Address Address { get; set; }
        public void SetPrivateKey(string pk)
        {
            PrivateKey = pk;
        }

    }
}
