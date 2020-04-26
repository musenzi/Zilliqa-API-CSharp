﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Accounts
{
    public class Account
    {
        private string _accountAdress;

        /// <summary>
        /// Constructor for readonly account
        /// </summary>
        /// <param name="address"></param>
        public Account(string address)
        {
            Address = Address;
            // TODO Lookup public key from address
        }
        private string PrivateKey { get;  set; }
        public string PublicKey { get; set; }
        public string Address { get; set; }

    }
}