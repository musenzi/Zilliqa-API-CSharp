using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.Accounts
{
    public static class AccountFactory
    {
        public static Account New(string pk)
        {
            return new Account(pk);
        }
        public static Account FromJsonFile(string file, string passPhrase)
        {
            return new Account(file, passPhrase);
        }
    }
}
