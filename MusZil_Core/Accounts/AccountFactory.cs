using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Accounts
{
    public static class AccountFactory
    {
        public static Account New(string address = "")
        {
            return new Account(address);
        }
        public static Account New(Address address = null)
        {
            return new Account(address);
        }
    }
}
